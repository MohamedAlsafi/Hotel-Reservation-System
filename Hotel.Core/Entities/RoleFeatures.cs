using Hotel.Core.Entities.Enum;

namespace Hotel.Core.Entities
{
    public class RoleFeatures :BaseEntity
    {
        public Roles Roles { get; set; }
        public Features Features { get; set; }

    }
}
