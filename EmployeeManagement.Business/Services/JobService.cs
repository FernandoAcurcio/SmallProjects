using AutoMapper;
using EmployeeManagement.Common.Dtos.Job;
using EmployeeManagement.Common.Interfaces;
using EmployeeManagement.Common.Model;

namespace EmployeeManagement.Business.Services
{
    public class JobService : IJobService
    {
        private IMapper _mapper { get; }
        private IGenericRepository<Job> _jobRepository { get; }

        public JobService(IMapper mapper, IGenericRepository<Job> jobRepository)
        {
            _mapper = mapper;
            _jobRepository = jobRepository;
        }

        public async Task<int> CreateJobAsync(JobCreate jobCreate)
        {
            var entity = _mapper.Map<Job>(jobCreate);
            await _jobRepository.InsertAsync(entity);
            await _jobRepository.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteJobAsync(JobDelete jobDelete)
        {
            var entity = await _jobRepository.GetByIdAsync(jobDelete.Id);
            _jobRepository.Delete(entity);
            await _jobRepository.SaveChangesAsync();    
        }

        public async Task<JobGet> GetJobAsync(int id)
        {
            var entity = await _jobRepository.GetByIdAsync(id);
            return _mapper.Map<JobGet>(entity);
        }

        public async Task<List<JobGet>> GetJobsAsync()
        {
            var entity = await _jobRepository.GetAsync(null, null);
            return _mapper.Map<List<JobGet>>(entity);
        }

        public async Task UpdateJobAsync(JobUpdate jobUpdate)
        {
            var entity = _mapper.Map<Job>(jobUpdate);
            _jobRepository.Update(entity);
            await _jobRepository.SaveChangesAsync();
        }
    }
}
