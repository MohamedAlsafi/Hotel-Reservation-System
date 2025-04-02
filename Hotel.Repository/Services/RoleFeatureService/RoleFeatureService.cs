using Hotel.Core.Entities;
using Hotel.Core.Entities.Enum;
using Hotel.Repository.UnitOfWork;
namespace Hotel.Repository.Services.RoleFeatureService
{
   public class RoleFeatureService
   {
        private readonly IUnitOfWork _unitOfWork;

        public RoleFeatureService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void AssignRoleToFeature(Roles roles , Features features)
        {
            var roleFeature = new RoleFeatures()
            {
                Roles = roles,
                Features = features
            };
            _unitOfWork.Repository<RoleFeatures>().AddAsync(roleFeature);
            _unitOfWork.SaveChangesAsync();

        }
       
        public async Task<bool> HasAccessAsync(Roles roles, Features features)
        {
            var roleFeature = await _unitOfWork.Repository<RoleFeatures>()
                .GetByCriteriaAsync(x => x.Roles == roles && x.Features == features);
            return roleFeature != null;
        }
    }

}
