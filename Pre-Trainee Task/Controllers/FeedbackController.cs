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

    private void SendEmail(string email, string subject, string body)
    {
        _emailService.Send(email, subject, body);
    }

    // POST /api/feedback — Create new feedback
    /// <summary>
    ///     POST method for creating new feedback
    /// </summary>
    /// <param name="dto">Feedback creation DTO</param>
    /// <returns>Created feedback data</returns>
    /// <response code="201">Feedback created successfully</response>
    /// <response code="400">Invalid request data</response>
    [HttpPost]
    public ActionResult<FeedbackReadDto> PostFeedback(
        [FromBody] FeedbackCreateDto? dto)
    {
        if (dto == null) return BadRequest();

        var feedback = _feedbackService.Create(dto);

        SendEmail("admin@admin.com", "New Feedback Created",
            $"Feedback content:\n{feedback}");
        return CreatedAtAction(nameof(GetById), new { id = feedback.Id },
            feedback);
    }

    // GET /api/feedback — List all feedback
    /// <summary>
    ///     GET method that retrieves paginated feedback results
    /// </summary>
    /// <param name="pageNumber">Page number (default 1)</param>
    /// <param name="pageSize">Number of items per page (default 5)</param>
    /// <returns>Paginated list of feedback entries</returns>
    /// <response code="200">Feedback list retrieved successfully</response>
    [HttpGet]
    public ActionResult<IEnumerable<FeedbackReadDto>> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 5)
    {
        IQueryable<FeedbackReadDto> feedbacks = _feedbackService.GetAll();
        
        int totalItems = feedbacks.Count();
        List<FeedbackReadDto> pagedFeedbacks = feedbacks
            .OrderBy(f => f.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = new
        {
            TotalItems = totalItems,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
            Items = pagedFeedbacks
        };

        return Ok(result);
    }

    // GET /api/feedback/{id} — Get feedback by ID
    /// <summary>
    ///     GET method that retrieves feedback by its ID
    /// </summary>
    /// <param name="id">ID of feedback to retrieve</param>
    /// <returns>Retrieved feedback</returns>
    /// <response code="200">Feedback retrieved successfully</response>
    /// <response code="404">Feedback with the given ID was not found</response>
    [HttpGet("{id}")]
    public ActionResult<FeedbackReadDto> GetById(Guid id)
    {
        var feedback = _feedbackService.GetById(id);
        if (feedback == null) return NotFound();

        return Ok(feedback);
    }

    // PUT /api/feedback/{id} — Update feedback
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
    public ActionResult<FeedbackReadDto> UpdateFeedback(Guid id,
        [FromBody] FeedbackCreateDto? dto)
    {
        if (dto == null) return BadRequest();

        var feedback = _feedbackService.Update(id, dto);
        if (feedback == null) return NotFound();

        SendEmail("admin@admin.com", $"Feedback {id} modified",
            $"New feedback content: {dto}");
        return Ok(feedback);
    }

    // DELETE /api/feedback/{id} — Delete feedback
    /// <summary>
    ///     DELETE method for deleting a feedback by ID
    /// </summary>
    /// <param name="id">ID of feedback to delete</param>
    /// <response code="204">Feedback deleted successfully</response>
    /// <response code="404">Feedback with the given ID was not found</response>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public ActionResult DeleteFeedback(Guid id)
    {
        var feedback = _feedbackService.GetById(id);
        if (_feedbackService.Delete(id)) return NoContent();

        SendEmail("admin@admin.com", $"Feedback {id} deleted",
            $"Deleted feedback content: {feedback}");
        return NotFound();
    }
}
