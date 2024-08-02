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

            //     var x = _configuration.GetConnectionString("TransportConnection");
            var x = "Server=localhost;Database=mysqlgamesdb;User=root;Password=123456;";

            optionsBuilder.UseMySql(x, ServerVersion.AutoDetect(x));
        }
    }
}
