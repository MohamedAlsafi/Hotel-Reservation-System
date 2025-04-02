using Hotel.Core.Entities.Enum;
using Hotel.Repository.Services.RoleFeatureService;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Hotel_Reservation_System.Filter
{
    public class CustomAuthorizeFilter :ActionFilterAttribute
    {
        private readonly RoleFeatureService _roleFeatureService;
        private readonly Features _feature;

        public CustomAuthorizeFilter(RoleFeatureService roleFeatureService , Features feature )
        {
            _roleFeatureService = roleFeatureService;
            _feature = feature;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
           var RoleId  =context.HttpContext.User.FindFirst(ClaimTypes.Role);
            if (RoleId is null || string.IsNullOrEmpty(RoleId.Value))
            {
               throw new UnauthorizedAccessException();
            }
            var role = (Roles)Enum.Parse(typeof(Roles), RoleId.Value);
            base.OnActionExecuting(context);
        }
    }
}
