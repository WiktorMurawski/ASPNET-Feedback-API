using Pre_Trainee_Task.DTOs;

namespace Pre_Trainee_Task.Services;

public interface IFeedbackService
{
    // Task<(int total, List<FeedbackReadDto> list)> GetAllAsync();
    Task<(int, List<FeedbackReadDto>)>  GetPagedAsync(int pageNumber, int pageSize);
    Task<FeedbackReadDto?> GetFeedbackByIdAsync(Guid id);
    Task<FeedbackReadDto> CreateAsync(FeedbackCreateDto dto);
    Task<FeedbackReadDto?> UpdateAsync(Guid id, FeedbackCreateDto dto);
    Task<bool> DeleteAsync(Guid id);
}
