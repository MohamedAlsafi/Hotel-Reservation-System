using Hotel.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Repository.IGenericRepository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        public IQueryable<T> GetAll();
        Task<List<T>> GetAllAsync();


        public Task<T> GetByIdAsync(int id);
        public IQueryable<T> GetAllByCriteria(Expression<Func<T, bool>> criteria);
        public Task<T> GetByCriteriaAsync(Expression<Func<T, bool>> criteria);
        public Task AddAsync(T entity);
        public void SoftDelete(T item);
        public void HardDelete(T item);
        public void UpdateInclude(T entity, params string[] modifiedProperties);
        public void UpdateExclude(T entity, params string[] unmodifiedProperties);
        Task AddRangeAsync(IEnumerable<T> entities);
     
    }
}
