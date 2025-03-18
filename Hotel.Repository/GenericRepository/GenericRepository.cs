using Hotel.Core.Data.Context;
using Hotel.Core.Entities;
using Hotel.Repository.IGenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Hotel.Repository.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly HotelDbContext _context;
        DbSet<T> _dbSet;

        public GenericRepository(HotelDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();  
        }
        public IQueryable<T> GetAll()
        => _dbSet.Where(c => !c.Deleted);

        public IQueryable<T> GetAllByCriteria(Expression<Func<T, bool>> criteria)
        => _dbSet.Where(criteria);

        public async Task<T> GetByCriteriaAsync(Expression<Func<T, bool>> criteria)
              => await _dbSet.Where(criteria).FirstOrDefaultAsync();


        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet
                .Where(c => !c.Deleted && c.Id == id)
                .FirstOrDefaultAsync();
        }

        public void HardDelete(T item)
        {
            _dbSet.Remove(item);
        }

        public async Task SoftDelete(T item)
        {
            item.Deleted = true;
           await UpdateInclude(item, nameof(BaseEntity.Deleted));
        }

        public async Task UpdateInclude(T entity, params string[] Includes)
        {
            var local = _context.Set<T>().Local.FirstOrDefault(x => x.Id == entity.Id);
            EntityEntry entityEntry;
            if (local is null)
                entityEntry = _context.Entry(entity);

            else
                entityEntry = _context.ChangeTracker.Entries<T>().FirstOrDefault(X => X.Entity.Id == entity.Id);

            foreach (var property in entityEntry.Properties)
            {
                if (Includes.Contains(property.Metadata.Name))
                {
                    property.CurrentValue = entity.GetType().GetProperty(property.Metadata.Name).GetValue(entity);
                    property.IsModified = true;

                }
            }
            await _context.SaveChangesAsync();
        }
        public void UpdateExclude(T entity, params string[] unmodifiedProperties)
        {
            if (!_dbSet.Any(x => x.Id == entity.Id && !x.Deleted))
                return;

            var local = _dbSet.Local.FirstOrDefault(x => x.Id == entity.Id);
            EntityEntry entityEntry;
            if (local is null)
                entityEntry = _context.Entry(entity);
            else
                entityEntry = _context.ChangeTracker.Entries<T>().FirstOrDefault(x => x.Entity.Id == entity.Id);

            foreach (var prop in entityEntry.Properties)
            {
                if (!unmodifiedProperties.Contains(prop.Metadata.Name))
                {
                    prop.CurrentValue = entity.GetType().GetProperty(prop.Metadata.Name).GetValue(entity);
                    prop.IsModified = true;
                }
            }

            _context.SaveChanges();

        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }
    }
}
