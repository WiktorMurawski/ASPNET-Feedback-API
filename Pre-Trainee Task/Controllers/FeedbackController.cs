using Microsoft.AspNetCore.Mvc;
using Pre_Trainee_Task.Data;
using Pre_Trainee_Task.DTOs;
using Pre_Trainee_Task.Models;
using Pre_Trainee_Task.Services;

namespace Pre_Trainee_Task.Controllers;

/*
   POST /api/feedback — Create new feedback
   
   GET /api/feedback — List all feedback
   
   GET /api/feedback/{id} — Get feedback by ID
   
   PUT /api/feedback/{id} — Update feedback
   
   DELETE /api/feedback/{id} — Delete feedback
*/

[ApiController]
[Route("api/[controller]")]
public class FeedbackController : ControllerBase
{
    // private readonly FeedbackDbContext _context;
    //
    // public FeedbackController(FeedbackDbContext context)
    // {
    //     _context = context;
    //     _feedbackService = new FeedbackService(context);
    // }
    
    private readonly IFeedbackService _feedbackService;
    
    public FeedbackController(IFeedbackService feedbackService)
    {
        _feedbackService = feedbackService;
    }
    
    // POST /api/feedback — Create new feedback
    [HttpPost]
    public ActionResult<FeedbackReadDto> PostFeedback([FromBody] FeedbackCreateDto dto)
    {
        if (dto == null)
        {
            return BadRequest();
        }

        var feedback = _feedbackService.Create(dto);

        return CreatedAtAction(nameof(GetById), new { id = feedback.Id }, feedback);
    }
   
   // GET /api/feedback — List all feedback
   [HttpGet]
   public ActionResult<IEnumerable<FeedbackReadDto>> GetAll()
   {
       var feedbacks = _feedbackService.GetAll();
       return Ok(feedbacks);
   }

   // GET /api/feedback/{id} — Get feedback by ID
   [HttpGet("{id}")]
   public ActionResult<FeedbackReadDto> GetById(Guid id)
   {
       var feedback = _feedbackService.GetById(id);
       if (feedback == null)
       {
           return NotFound();
       }
       
       return Ok(feedback);
   }
   
   // PUT /api/feedback/{id} — Update feedback
   [HttpPut("{id}")]
   public ActionResult<FeedbackReadDto> UpdateFeedback(Guid id, [FromBody] FeedbackCreateDto dto)
   {
       if (dto == null)
       {
           return BadRequest();
       }

       var feedback = _feedbackService.Update(id, dto);
       if (feedback == null)
       {
           return NotFound();
       }

       return Ok(feedback);
   }
   
   // DELETE /api/feedback/{id} — Delete feedback
   [HttpDelete("{id}")]
   public ActionResult DeleteFeedback(Guid id)
   {
       if (_feedbackService.Delete(id))
       {
           return NoContent();
       }

       return NotFound();
   }
   
   //  // GET /api/feedback — List all feedback
   // [HttpGet]
   // public ActionResult<IEnumerable<Feedback>> Get()
   // {
   //     var feedbacks = _context.Feedbacks.ToList();
   //     return Ok(feedbacks);
   // }
   //
   // // GET /api/feedback/{id} — Get feedback by ID
   // [HttpGet("{id}")]
   // public ActionResult<Feedback> Get(Guid id)
   // {
   //     var feedback = _context.Feedbacks.Find(id);
   //     return Ok(feedback);
   // }
   
}
