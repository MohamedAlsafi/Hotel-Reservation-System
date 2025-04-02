using FluentValidation;
using Hotel.Core.Data.Context;
using Hotel.Core.Dtos.Room.Create;
using Hotel.Core.Profiles;
using Hotel.Core.Validators;
using Hotel.Repository.GenericRepository;
using Hotel.Repository.IGenericRepository;
using Hotel.Repository.Services.FeedbackServices;
using Hotel.Repository.Services.OfferService;
using Hotel.Repository.Services.PasswordHashing;
using Hotel.Repository.Services.Payment;
using Hotel.Repository.Services.ReservationService;
using Hotel.Repository.Services.RoleFeatureService;
using Hotel.Repository.Services.RoomService;
using Hotel.Repository.Services.Username_Hashing;
using Hotel.Repository.UnitOfWork;
using Hotel_Reservation_System.Error;
using Hotel_Reservation_System.Middleware;
using Hotel_Reservation_System.ProfilesVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Reservation_System.Helpers
{
    public static class ExtensionMethods
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection Services)
        {
           Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddScoped<IRoomServices, RoomService>();
            Services.AddScoped<IPaymentService, PaymentService>();
            Services.AddScoped<IOfferService, OfferService>();
            Services.AddScoped<IReservationService, ReservationService>();
            Services.AddScoped<IFeedbackService, FeedbackService>();
            Services.AddScoped<IPasswordHasher, PasswordHasher>();
            Services.AddScoped<IUsernameHasher, UserNameHaser>();
            Services.AddScoped<IReservationService, ReservationService>();
            Services.AddScoped<RoleFeatureService>();
            Services.AddScoped<GlobalTransactionMiddleware>();

            return Services;
        }

        public static IServiceCollection AddServices(this IServiceCollection Services , IConfiguration configuration)
        {
            Services.AddAutoMapper(typeof(DomainMappingProfile), typeof(ViewModelMappingProfile));
            Services.AddDbContext<HotelDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            Services.AddScoped<IValidator<CreateRoomDTO>, CreateRoomValidator>();
            Services.AddIdentityService(configuration);
            #region ApiValidationErrorr
            Services.Configure<ApiBehaviorOptions>(opthion =>
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
            return Services;
            #endregion
        }
    }


}
