﻿using AutoMapper;
using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.Business.Validation;
using EmployeeManagement.Common.Dtos.Teams;
using EmployeeManagement.Common.Interfaces;
using EmployeeManagement.Common.Model;
using FluentValidation;
using System.Linq.Expressions;

namespace EmployeeManagement.Business.Services
{
    public class TeamService : ITeamService
    {
        private IGenericRepository<Team> _teamRepository { get; }
        private IGenericRepository<Employee> _employeeRepository { get; }
        private IMapper _mapper { get; }
        private TeamCreateValidator _teamCreateValidator { get; }
        private TeamUpdateValidator _teamUpdateValidator { get; }

        public TeamService(IGenericRepository<Team> teamRepository, IGenericRepository<Employee> employeeRepository, IMapper mapper,
               TeamCreateValidator teamCreateValidator, TeamUpdateValidator teamUpdateValidator)
        {
            _teamRepository = teamRepository;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _teamCreateValidator = teamCreateValidator;
            _teamUpdateValidator = teamUpdateValidator;
        }

        public async Task<int> CreateTeamAsync(TeamCreate teamCreate)
        {
            await _teamCreateValidator.ValidateAndThrowAsync(teamCreate);

            // filter
            Expression<Func<Employee, bool>> employeeFilter = (employee) => teamCreate.Employees.Contains(employee.Id);
            var employees = await _employeeRepository.GetFilteredAsync(new[] {employeeFilter}, null, null);
            
            var missinEmployee = teamCreate.Employees.Where(id => !employees.Any(existing => existing.Id == id));
            if (missinEmployee.Any())
                throw new EmployeesNotFoundException(missinEmployee.ToArray());

            var entity = _mapper.Map<Team>(teamCreate);
            entity.Employees = employees;
            await _teamRepository.InsertAsync(entity);
            await _teamRepository.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteTeamAsync(TeamDelete teamDelete)
        {
            var entity = await _teamRepository.GetByIdAsync(teamDelete.Id);

            if (entity == null)
                throw new TeamNotFoundException(teamDelete.Id);

            _teamRepository.Delete(entity);
            await _teamRepository.SaveChangesAsync();
        }

        public async Task<TeamGet> GetTeamAsync(int id)
        {
            var entity = await _teamRepository.GetByIdAsync(id, (team) => team.Employees);

            if (entity == null)
                throw new TeamNotFoundException(id);

            return _mapper.Map<TeamGet>(entity);
        }

        public async Task<List<TeamGet>> GetTeamsAsync()
        {
            var entities = await _teamRepository.GetAsync(null,null, (team) => team.Employees);

            if (entities == null)
                throw new TeamNotFoundException();

            return _mapper.Map<List<TeamGet>>(entities);
        }

        public async Task UpdateTeamAsync(TeamUpdate teamUpdate)
        {
            await _teamUpdateValidator.ValidateAndThrowAsync(teamUpdate);
            // filter
            Expression<Func<Employee, bool>> employeeFilter = (employee) => teamUpdate.Employees.Contains(employee.Id);

            var employees = await _employeeRepository.GetFilteredAsync(new[] { employeeFilter }, null, null);

            var missinEmployee = teamUpdate.Employees.Where(id => !employees.Any(existing => existing.Id == id));
            if (missinEmployee.Any())
                throw new EmployeesNotFoundException(missinEmployee.ToArray());

            var existingEntity = await _teamRepository.GetByIdAsync(teamUpdate.Id, (team) => team.Employees);
            if(existingEntity == null)
                throw new TeamNotFoundException(teamUpdate.Id);

            _mapper.Map(teamUpdate, existingEntity);
            existingEntity.Employees = employees;
            _teamRepository.Update(existingEntity);
            await _teamRepository.SaveChangesAsync();
        }
    }
}
