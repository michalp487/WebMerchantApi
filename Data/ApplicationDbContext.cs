using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace WebMerchantApi.Data
{
    //public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    //{
    //    public ApplicationDbContext(
    //        DbContextOptions options,
    //        IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
    //    {
    //    }

    //    //public DbSet<Product> Products { get; set; }

    //    //protected override void OnModelCreating(ModelBuilder builder)
    //    //{
    //    //    base.OnModelCreating(builder);
    //    //}
    //}

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        //public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
