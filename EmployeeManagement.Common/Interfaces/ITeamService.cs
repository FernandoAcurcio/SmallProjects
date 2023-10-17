using EmployeeManagement.Common.Dtos.Teams;

namespace EmployeeManagement.Common.Interfaces
{
    public interface ITeamService
    {
        Task<int> CreateTeamAsync(TeamCreate teamCreate);
        Task UpdateTeamAsync(TeamUpdate teamUpdate);
        Task<List<TeamGet>> GetTeamsAsync();
        Task<TeamGet> GetTeamAsync(int id);
        Task DeleteTeamAsync(TeamDelete teamDelete);
    }
}
