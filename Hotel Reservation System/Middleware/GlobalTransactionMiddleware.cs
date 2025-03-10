using Hotel.Core.Data.Context;
using Microsoft.EntityFrameworkCore.Storage;
using System.Text;

namespace Hotel_Reservation_System.Middleware
{
    public class GlobalTransactionMiddleware : IMiddleware
    {
        private readonly HotelDbContext  _dbContext;
        private readonly ILogger<GlobalTransactionMiddleware> _logger;
        public GlobalTransactionMiddleware(HotelDbContext dbContext, ILogger<GlobalTransactionMiddleware> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            IDbContextTransaction transaction = null!;
            try
            {
                transaction = _dbContext.Database.BeginTransaction();
                await next(context);
                transaction.Commit();

            }
            catch (Exception)
            {
                transaction.Rollback();
                _logger.LogError("Transaction rolled back");
            }



        }
    }
}
