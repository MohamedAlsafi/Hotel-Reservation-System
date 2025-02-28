using Hotel.Core.Data.Context;
using Hotel_Reservation_System.Error;
using Hotel_Reservation_System.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Reservation_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<HotelDbContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
           
            #region ApiValidathionErrorr
            builder.Services.Configure<ApiBehaviorOptions>(opthion =>
               {
                   opthion.InvalidModelStateResponseFactory = (actionContext) =>
                   {
                       var errors = actionContext.ModelState.Where(o => o.Value.Errors.Count() > 0)
                                                            .SelectMany(o => o.Value.Errors)
                                                            .Select(e => e.ErrorMessage)
                                                            .ToList();


                       var response = new ApiValidationError()
                       {
                           Errors = errors
                       };
                       return new BadRequestObjectResult(response);
                   };
               }); 
            #endregion



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
