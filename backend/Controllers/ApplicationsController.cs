using backend.DTOs;
using backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend.Controllers;

[ApiController]
[Route("api/applications")]
[Authorize]
public class ApplicationsController : ControllerBase{
    private IJobApplicationService jobService;

    public ApplicationsController(IJobApplicationService jobService){
        this.jobService = jobService;
    }

    private string GetUserId(){
        return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(){
        var userId = GetUserId();
        
        var result = await jobService.GetAllAsync(userId);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id){
        var userId = GetUserId();
        
        var result = await jobService.GetByIdAsync(id, userId);

        if(result == null){
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateJobApplicationDto createDto){
        var userId = GetUserId();
        
        var result = await jobService.CreateAsync(createDto, userId);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateJobApplicationDto updateDto){
        var userId = GetUserId();
        
        var updated = await jobService.UpdateAsync(id, updateDto, userId);

        if(!updated){
            return NotFound();
        }
        return Ok("updated successfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id){
        var userId = GetUserId();
        
        var deleted = await jobService.DeleteAsync(id, userId);

        if(!deleted){
            return NotFound();
        }
        return Ok("deleted successfully");
    }
}