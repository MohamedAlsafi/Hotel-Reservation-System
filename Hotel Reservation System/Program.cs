using FluentValidation.AspNetCore;
using FluentValidation;
using Hotel.Core.Data.Context;
using Hotel.Core.Profiles;
using Hotel.Core.Validators;
using Hotel.Repository.GenericRepository;
using Hotel.Repository.IGenericRepository;
using Hotel.Repository.Services.OfferService;
using Hotel.Repository.Services.OfferService.JWT_Token;
using Hotel.Repository.Services.Payment;
using Hotel.Repository.Services.ReservationService;
using Hotel.Repository.Services.RoomService;
using Hotel.Repository.UnitOfWork;
using Hotel_Reservation_System.Error;
using Hotel_Reservation_System.Middleware;
using Hotel_Reservation_System.ProfilesVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hotel.Core.Dtos.Room.Create;
using Microsoft.AspNetCore.Identity;
using Hotel.Core.Entities.customer;
using Hotel_Reservation_System.Helpers;
using Hotel.Repository.Services.FeedbackServices;
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
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IRoomServices, RoomService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IOfferService, OfferService>();
            builder.Services.AddScoped<IReservationService, ReservationService>();
            builder.Services.AddScoped<IFeedbackService, FeedbackService>();
           
            builder.Services.AddScoped<GlobalTransactionMiddleware>();
            builder.Services.AddAutoMapper(typeof(DomainMappingProfile), typeof(ViewModelMappingProfile));
            builder.Services.AddDbContext<CustomerIdentityDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
           // builder.Services.AddDbContext<CustomerIdentityDbContext>(options =>
           //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

             //builder.Services.AddControllers()
            // .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateRoomValidator>());

            builder.Services.AddScoped<IValidator<CreateRoomDTO>, CreateRoomValidator>();
            builder.Services.AddIdentityService(builder.Configuration);
            #region ApiValidationErrorr
            builder.Services.Configure<ApiBehaviorOptions>(opthion =>
               {
                   opthion.InvalidModelStateResponseFactory = (actionContext) =>
                   {
                       var errors = actionContext.ModelState.Where(o => o.Value?.Errors.Count() > 0)
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
            app.UseMiddleware<GlobalTransactionMiddleware>();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
