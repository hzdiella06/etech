using backend.DTOs;

namespace backend.Interfaces;

public interface IJobApplicationService{
    Task<List<JobApplicationResponseDto>> GetAllAsync(string userId);
    Task<JobApplicationResponseDto?> GetByIdAsync(int id, string userId);
    Task<JobApplicationResponseDto> CreateAsync(CreateJobApplicationDto createDto, string userId);
    Task<bool> UpdateAsync(int id, UpdateJobApplicationDto updateDto, string userId);
    Task<bool> DeleteAsync(int id, string userId);
}
