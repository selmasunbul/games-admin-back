using data_access.entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace data_access.context
{
    public class MysqlDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        private readonly IConfiguration? _configuration;

        public MysqlDbContext()
        {

        }

        public MysqlDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }





        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var x = "Server=185.141.33.80;Database=mysqlgamesdb;User Id=root;Password=123456;";

            optionsBuilder.UseMySql(x, ServerVersion.AutoDetect(x));
        }
    }
}
