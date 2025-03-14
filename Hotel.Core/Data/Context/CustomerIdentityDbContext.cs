using Hotel.Core.Entities.customer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Hotel.Core.Data.Context
{
    public class CustomerIdentityDbContext : IdentityDbContext<Customer>
    {

        public CustomerIdentityDbContext(DbContextOptions<CustomerIdentityDbContext> options) : base(options)
        {
        }
    }
}
