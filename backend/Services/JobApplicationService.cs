using backend.Data;
using backend.DTOs;
using backend.Interfaces;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class JobApplicationService : IJobApplicationService{
    private ApplicationDbContext context;
    public JobApplicationService(ApplicationDbContext context){
        this.context = context;
    }

    public async Task<List<JobApplicationResponseDto>> GetAllAsync(string userId){
        var applications = await context.JobApplications.Where(a => a.UserId == userId).ToListAsync();

        var result = applications.Select(a => new JobApplicationResponseDto
        {
            Id = a.Id,
            CompanyName = a.CompanyName,
            Position = a.Position,
            DateApplied = a.DateApplied,
            Status = a.Status,
            Notes = a.Notes
        }).ToList();

        return result;
    }

    public async Task<JobApplicationResponseDto?> GetByIdAsync(int id, string userId){
        var application = await context.JobApplications.FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
        
        if (application == null){
            return null;
        }

        return new JobApplicationResponseDto{
            Id = application.Id,
            CompanyName = application.CompanyName,
            Position = application.Position,
            DateApplied = application.DateApplied,
            Status = application.Status,
            Notes = application.Notes
        };
    }

    public async Task<JobApplicationResponseDto> CreateAsync(CreateJobApplicationDto createDto, string userId){
        var application = new JobApplication{
            CompanyName = createDto.CompanyName,
            Position = createDto.Position,
            DateApplied = createDto.DateApplied,
            Status = createDto.Status,
            Notes = createDto.Notes,
            UserId = userId
        };

        context.JobApplications.Add(application);
        await context.SaveChangesAsync();

        return new JobApplicationResponseDto{
            Id = application.Id,
            CompanyName = application.CompanyName,
            Position = application.Position,
            DateApplied = application.DateApplied,
            Status = application.Status,
            Notes = application.Notes
        };
    }

    public async Task<bool> UpdateAsync(int id, UpdateJobApplicationDto updateDto, string userId){
        var application = await context.JobApplications.FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);

        if (application == null){
            return false;
        }

        application.CompanyName = updateDto.CompanyName;
        application.Position = updateDto.Position;
        application.DateApplied = updateDto.DateApplied;
        application.Status = updateDto.Status;
        application.Notes = updateDto.Notes;

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id, string userId){
        var application = await context.JobApplications.FirstOrDefaultAsync(a=> a.Id == id && a.UserId == userId);

        if(application == null){
            return false;
        }

        context.JobApplications.Remove(application);
        await context.SaveChangesAsync();

        return true;
    }
}
