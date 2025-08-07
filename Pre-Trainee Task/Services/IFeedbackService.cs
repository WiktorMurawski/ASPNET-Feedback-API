using Pre_Trainee_Task.DTOs;

namespace Pre_Trainee_Task.Services;

public interface IFeedbackService
{
    IQueryable<FeedbackReadDto> GetAll();
    FeedbackReadDto? GetById(Guid id);
    FeedbackReadDto Create(FeedbackCreateDto dto);
    FeedbackReadDto? Update(Guid id, FeedbackCreateDto dto);
    bool Delete(Guid id);
}
