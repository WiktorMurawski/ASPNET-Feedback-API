using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pre_Trainee_Task.DTOs;
using Pre_Trainee_Task.Services;

namespace Pre_Trainee_Task.Controllers;

/*
   POST /api/feedback — Create new feedback
   GET /api/feedback — List all feedback
   GET /api/feedback/{id} — Get feedback by ID
   PUT /api/feedback/{id} — Update feedback
   DELETE /api/feedback/{id} — Delete feedback
*/

/// <summary>
///     Handles CRUD operations for feedback
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FeedbackController : ControllerBase
{
    private readonly IEmailService _emailService;
    private readonly IFeedbackService _feedbackService;

    /// <summary>
    ///     Initializes a new instance of the controller
    /// </summary>
    /// <param name="feedbackService">
    ///     Service for handling feedback related CRUD
    ///     operations
    /// </param>
    /// <param name="emailService">
    ///     Service for sending dummy emails when feedbacks get
    ///     added/modified/deleted
    /// </param>
    public FeedbackController(IFeedbackService feedbackService,
        IEmailService emailService)
    {
        _feedbackService = feedbackService;
        _emailService = emailService;
    }

    /// <summary>
    ///     GET method that retrieves paginated feedback results
    /// </summary>
    /// <param name="pageNumber">Page number (default 1)</param>
    /// <param name="pageSize">Number of items per page (default 5)</param>
    /// <returns>Paginated list of feedback entries</returns>
    /// <response code="200">Feedback list retrieved successfully</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FeedbackReadDto>>> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 5)
    {
        (int totalItems, var feedbacks) = await _feedbackService.GetPagedAsync(pageNumber, pageSize);
        
        var result = new
        {
            TotalItems = totalItems,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
            Items = feedbacks
        };

        return Ok(result);
    }

    /// <summary>
    ///     GET method that retrieves feedback by its ID
    /// </summary>
    /// <param name="id">ID of feedback to retrieve</param>
    /// <returns>Retrieved feedback</returns>
    /// <response code="200">Feedback retrieved successfully</response>
    /// <response code="404">Feedback with the given ID was not found</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<FeedbackReadDto>> GetByIdAsync(Guid id)
    {
        var feedback = await _feedbackService.GetFeedbackByIdAsync(id);
        if (feedback == null) return NotFound();

        return Ok(feedback);
    }
    
    /// <summary>
    ///     POST method for creating new feedback
    /// </summary>
    /// <param name="dto">Feedback creation DTO</param>
    /// <returns>Created feedback data</returns>
    /// <response code="201">Feedback created successfully</response>
    /// <response code="400">Invalid request data</response>
    [HttpPost]
    public async Task<ActionResult<FeedbackReadDto>> PostFeedbackAsync([FromBody] FeedbackCreateDto? dto)
    {
        if (dto == null) return BadRequest();
        
        var feedback = await _feedbackService.CreateAsync(dto);
        
        SendEmailAsync("admin@admin.com", "New Feedback Created", $"Feedback content:\n{feedback}");
        
        return Created($"/api/feedback/{feedback.Id}", feedback); // Fix
        
        return CreatedAtAction(nameof(GetByIdAsync), new { id = feedback.Id }, feedback); // For some reason the nameof can't be resolved?
    }
    
    /// <summary>
    ///     PUT method for updating feedback with the specified id
    /// </summary>
    /// <param name="id">Id of the feedback to update</param>
    /// <param name="dto">New feedback data</param>
    /// <returns>Updated feedback</returns>
    /// <response code="200">Feedback updated successfully</response>
    /// <response code="400">Invalid request data</response>
    /// <response code="404">Feedback with the given ID was not found</response>
    [HttpPut("{id}")]
    public async Task<ActionResult<FeedbackReadDto>> UpdateFeedbackAsync(Guid id, [FromBody] FeedbackCreateDto? dto)
    {
        if (dto == null) return BadRequest();
        
        var feedback = await _feedbackService.UpdateAsync(id, dto);
        if (feedback == null) return NotFound();

        SendEmailAsync("admin@admin.com", $"Feedback {id} modified", $"New feedback content: {dto}");
        return Ok(feedback);
    }

    /// <summary>
    ///     DELETE method for deleting a feedback by ID
    /// </summary>
    /// <param name="id">ID of feedback to delete</param>
    /// <response code="204">Feedback deleted successfully</response>
    /// <response code="404">Feedback with the given ID was not found</response>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteFeedbackAsync(Guid id)
    {
        var feedback = await _feedbackService.GetFeedbackByIdAsync(id);
        var deleted = await _feedbackService.DeleteAsync(id);
        if (!deleted) return NotFound();

        SendEmailAsync("admin@admin.com", $"Feedback {id} deleted", $"Deleted feedback content: {feedback}");
        return NoContent();
    }
    
    private void SendEmailAsync(string email, string subject, string body)
    {
        _ = Task.Run(async () => await _emailService.SendAsync(email, subject, body));
    }
}
