using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Pre_Trainee_Task.Config;
using Pre_Trainee_Task.Data;
using Pre_Trainee_Task.Filters;
using Pre_Trainee_Task.Middleware;
using Pre_Trainee_Task.Services;

namespace Pre_Trainee_Task;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        try
        {
            AddServicesToBuilder(builder);
        }
        catch(Exception e)
        {
            Console.WriteLine($"Error during adding services to builder: {e}");
        }

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseMiddleware<RequestLoggingMiddleware>();
        app.UseMiddleware<ErrorHandlingMiddleware>();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static void AddServicesToBuilder(WebApplicationBuilder builder)
    {
        // Add services to the container.

        // builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        // builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<FeedbackDbContext>(options =>
            options.UseSqlite(
                builder.Configuration
                    .GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<IFeedbackService, FeedbackService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddSingleton<IEmailService, DummyEmailService>();
        
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<IAuditService, AuditService>();

        // Makes JWT work with swagger
        builder.Services.AddSwaggerGen(options =>
        {
            var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
            
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Enter JWT token in the form: Bearer {token}",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        builder.Services.Configure<JwtConfig>(
            builder.Configuration.GetSection("Jwt")
        );
        
        // Bind JwtConfig
        JwtConfig? jwtConfig = builder.Configuration
            .GetSection("Jwt")
            .Get<JwtConfig>();
        if (jwtConfig == null)
        {
            throw new Exception("JWT config couldn't be found in appsettings.json");
        }

        builder.Services.Configure<JwtConfig>(
            builder.Configuration.GetSection("Jwt")
        );

        // JWT auth
        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig.Issuer,
                    ValidAudience = jwtConfig.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtConfig.Key))
                };
            });
        
        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<ExecutionTimeFilter>();
        });

        builder.Services.AddScoped<ExecutionTimeFilter>();
        
    }
}
