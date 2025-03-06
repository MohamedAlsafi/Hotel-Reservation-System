﻿using Hotel.Core.Data.Context;
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
            await _context.SaveChangesAsync();
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

        public void SoftDelete(T item)
        {
            item.Deleted = true;
            UpdateInclude(item, nameof(BaseEntity.Deleted));
        }

        public void UpdateInclude(T entity, params string[] modifiedProperties)
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
                if (modifiedProperties.Contains(prop.Metadata.Name))
                {
                    prop.CurrentValue = entity.GetType().GetProperty(prop.Metadata.Name).GetValue(entity);
                    prop.IsModified = true;
                }
            }
            _context.SaveChanges();

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
