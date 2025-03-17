using Hotel.Core.Data.Context;
using Microsoft.EntityFrameworkCore.Storage;
using System.Text;

namespace Hotel_Reservation_System.Middleware
{
    public class GlobalTransactionMiddleware : IMiddleware
    {
        private readonly CustomerIdentityDbContext  _dbContext;
        public GlobalTransactionMiddleware(CustomerIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            IDbContextTransaction Transaction = null;
            try
            {
                Transaction = _dbContext.Database.BeginTransaction();
                await next(context);
                Transaction.Commit();

            }
            catch (Exception)
            {
                
                Transaction?.Rollback();
                throw;
            }


        }
    }
}
