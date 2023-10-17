using AutoMapper;
using EmployeeManagement.Common.Dtos.Teams;
using EmployeeManagement.Common.Interfaces;
using EmployeeManagement.Common.Model;
using System.Linq.Expressions;

namespace EmployeeManagement.Business.Services
{
    public class TeamService : ITeamService
    {
        private IGenericRepository<Team> _teamRepository { get; }
        private IGenericRepository<Employee> _employeeRepository { get; }
        private IMapper _mapper { get; }

        public TeamService(IGenericRepository<Team> teamRepository, IGenericRepository<Employee> employeeRepository, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<int> CreateTeamAsync(TeamCreate teamCreate)
        {
            // filter
            Expression<Func<Employee, bool>> employeeFilter = (employee) => teamCreate.Employees.Contains(employee.Id);
            
            var employees = await _employeeRepository.GetFilteredAsync(new[] {employeeFilter}, null, null);
            var entity = _mapper.Map<Team>(teamCreate);
            entity.Employees = employees;
            await _teamRepository.InsertAsync(entity);
            await _teamRepository.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteTeamAsync(TeamDelete teamDelete)
        {
            var entity = await _teamRepository.GetByIdAsync(teamDelete.Id);
            _teamRepository.Delete(entity);
            await _teamRepository.SaveChangesAsync();
        }

        public async Task<TeamGet> GetTeamAsync(int id)
        {
            var entity = await _teamRepository.GetByIdAsync(id, (team) => team.Employees);
            return _mapper.Map<TeamGet>(entity);
        }

        public async Task<List<TeamGet>> GetTeamsAsync()
        {
            var entities = await _teamRepository.GetAsync(null,null, (team) => team.Employees);
            return _mapper.Map<List<TeamGet>>(entities);
        }

        public async Task UpdateTeamAsync(TeamUpdate teamUpdate)
        {
            // filter
            Expression<Func<Employee, bool>> employeeFilter = (employee) => teamUpdate.Employees.Contains(employee.Id);

            var employees = await _employeeRepository.GetFilteredAsync(new[] { employeeFilter }, null, null);
            var existingEntity = await _teamRepository.GetByIdAsync(teamUpdate.Id, (team) => team.Employees);
            _mapper.Map(teamUpdate, existingEntity);
            existingEntity.Employees = employees;
            _teamRepository.Update(existingEntity);
            await _teamRepository.SaveChangesAsync();
        }
    }
}
