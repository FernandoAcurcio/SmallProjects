using Academy.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Academy
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        // Private fields to hold a reference to the database context and the corresponding DbSet
        private ApplicationDbContext _dbContext { get; }
        private DbSet<T> _dbSet { get; }

        // Constructor that initializes the database context and the DbSet for the specified entity type (T)
        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        // Method to delete an entity of type T
        public void Delete(T entity)
        {
            // If the entity is detached from the context, attach it
            if (_dbContext.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);
            _dbSet.Remove(entity); // Remove the entity from the DbSet
        }

        // Method to retrieve a list of entities of type T, with optional skip, take, and include operations
        public async Task<List<T>> GetAsync(int? skip, int? take, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            // Include related entities as specified in the 'includes' parameter
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (skip != null)
                query = query.Skip(skip.Value);
            if (take != null)
                query = query.Skip(take.Value);

            return await query.ToListAsync(); // Execute the query and return the result as a list
        }

        // Method to retrieve an entity of type T by its unique ID, with optional include operations
        public async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            // Filter the entities by their unique ID
            query = query.Where(x => x.Id == id);

            // Include related entities as specified in the 'includes' parameter
            foreach (var include in includes)
                query = query.Include(include);

            return await query.SingleOrDefaultAsync(); // Execute the query and return the single result, if found
        }

        // Method to retrieve a filtered list of entities of type T based on provided filters, skip, take, and includes
        public async Task<List<T>> GetFilteredAsync(Expression<Func<T, bool>>[] filters, int? skip, int? take, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            // Apply filters as specified in the 'filters' parameter
            foreach (var filter in filters)
                query = query.Where(filter);

            if (skip != null)
                query = query.Skip(skip.Value);
            if (take != null)
                query = query.Skip(take.Value);

            return await query.ToListAsync(); // Execute the query and return the filtered results as a list
        }

        // Method to insert a new entity of type T asynchronously
        public async Task<int> InsertAsync(T entity)
        {
            await _dbSet.AddAsync(entity); // Add the entity to the DbSet
            return entity.Id; // Return the ID of the newly inserted entity
        }

        // Method to save changes made to the context to the underlying database asynchronously
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        // Method to update an entity of type T
        public void Update(T entity)
        {
            _dbSet.Attach(entity); // Attach the entity to the context
            _dbSet.Entry(entity).State = EntityState.Modified; // Mark the entity as modified
        }
    }
}
