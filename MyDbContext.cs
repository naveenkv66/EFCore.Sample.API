using EFCore.Sample.API.DataModels;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Sample.API
{
    public class MyDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public MyDbContext(IConfiguration configuration, DbContextOptions<MyDbContext> options) : base(options)
        {
            Configuration = configuration;
        }



        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("OnesanofiConnection"));
        }

        public DbSet<User> Users { get; set; }

    }
}
