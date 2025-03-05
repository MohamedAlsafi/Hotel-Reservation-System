using Hotel.Core.Entities;
using Hotel.Repository.IGenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Repository.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
       public Task<int> CompleteAsync();
       public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
       public Task<int> SaveChangesAsync();

    }

}
