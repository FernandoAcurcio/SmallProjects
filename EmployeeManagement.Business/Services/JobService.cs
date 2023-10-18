using AutoMapper;
using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.Business.Validation;
using EmployeeManagement.Common.Dtos.Job;
using EmployeeManagement.Common.Interfaces;
using EmployeeManagement.Common.Model;
using FluentValidation;

namespace EmployeeManagement.Business.Services
{
    public class JobService : IJobService
    {
        private IMapper _mapper { get; }
        private IGenericRepository<Job> _jobRepository { get; }
        private JobCreateValidator _jobCreateValidator { get; }
        private JobUpdateValidator _jobUpdateValidator { get; }

        public JobService(IMapper mapper, IGenericRepository<Job> jobRepository,
            JobCreateValidator jobCreateValidator, JobUpdateValidator jobUpdateValidator)
        {
            _mapper = mapper;
            _jobRepository = jobRepository;
            _jobCreateValidator = jobCreateValidator;
            _jobUpdateValidator = jobUpdateValidator;
        }

        public async Task<int> CreateJobAsync(JobCreate jobCreate)
        {
            await _jobCreateValidator.ValidateAndThrowAsync(jobCreate);
            var entity = _mapper.Map<Job>(jobCreate);
            await _jobRepository.InsertAsync(entity);
            await _jobRepository.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteJobAsync(JobDelete jobDelete)
        {
            var entity = await _jobRepository.GetByIdAsync(jobDelete.Id, job => job.Employees);

            if (entity == null) { throw new JobNotFoundException(jobDelete.Id); }

            if (entity.Employees.Count > 0)
                throw new DependentEmployeesExistException(entity.Employees);

            _jobRepository.Delete(entity);
            await _jobRepository.SaveChangesAsync();
        }

        public async Task<JobGet> GetJobAsync(int id)
        {
            var entity = await _jobRepository.GetByIdAsync(id);

            if (entity == null) { throw new JobNotFoundException(id); }
            return _mapper.Map<JobGet>(entity);
        }

        public async Task<List<JobGet>> GetJobsAsync()
        {
            var entity = await _jobRepository.GetAsync(null, null);
            return _mapper.Map<List<JobGet>>(entity);
        }

        public async Task UpdateJobAsync(JobUpdate jobUpdate)
        {
            await _jobUpdateValidator.ValidateAndThrowAsync(jobUpdate);

            var existingJob = _jobRepository.GetByIdAsync(jobUpdate.Id);

            if (existingJob == null)
                throw new JobNotFoundException(jobUpdate.Id);

            var entity = _mapper.Map<Job>(jobUpdate);
            _jobRepository.Update(entity);
            await _jobRepository.SaveChangesAsync();
        }
    }
}
