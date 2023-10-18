using EmployeeManagement.Common.Dtos.Teams;
using EmployeeManagement.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamController : ControllerBase
    {
        private ITeamService _teamService { get; }
        
        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateTeam(TeamCreate teamCreate)
        {
            var id = await _teamService.CreateTeamAsync(teamCreate);
            return Ok(id);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateTeam(TeamUpdate teamUpdate)
        {
            await _teamService.UpdateTeamAsync(teamUpdate);
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteTeam(TeamDelete teamDelete)
        {
            await _teamService.DeleteTeamAsync(teamDelete);
            return Ok();
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetTeam(int id)
        {
            var team = await _teamService.GetTeamAsync(id);   
            return Ok(team);
        }
        
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetTeams()
        {
            var teams = await _teamService.GetTeamsAsync(); 
            return Ok(teams);
        }

    }
}
