using Hotel.Core.Data.Context;
using System.Text;

namespace Hotel_Reservation_System.Middleware
{
    public class GlobalTransactionMiddleware : IMiddleware
    {
        private readonly HotelDbContext  _dbContext;
        public GlobalTransactionMiddleware(HotelDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _dbContext.Database.BeginTransaction();
          if(context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put || context.Request.Method == HttpMethods.Delete)
          {
                context.Request.EnableBuffering();
          }
            else if (context.Request.Method == HttpMethods.Get)
            {
                   return next(context);
            }
                var result = next(context);
            if (context.Response.StatusCode >= 400)
            {
                _dbContext.Database.RollbackTransaction();
            }
            else
            {
                _dbContext.Database.CommitTransaction();
            }
            return result;


        }
    }
}
