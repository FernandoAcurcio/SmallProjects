using EmployeeManagement.Common.Dtos.Job;
using EmployeeManagement.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        public IJobService _jobService { get; }

        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateJob(JobCreate jobCreate)
        {
            var id = await _jobService.CreateJobAsync(jobCreate);
            return Ok(id);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateJob(JobUpdate jobUpdate)
        {
            await _jobService.UpdateJobAsync(jobUpdate);
            return Ok();    
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteJob(JobDelete jobDelete)
        {
            await _jobService.DeleteJobAsync(jobDelete);    
            return Ok();
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetJob(int id)
        {
            var job = await _jobService.GetJobAsync(id);
            return Ok(job);
        }
        
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetJobs()
        {
            var jobs = await _jobService.GetJobsAsync();
            return Ok(jobs);
        }
            
    }
}
