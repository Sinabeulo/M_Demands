using BizCommon_Core.Model;
using Microsoft.EntityFrameworkCore;

namespace WebApiService.Data
{
    public class LoginContext : DbContext
    {
        public LoginContext(DbContextOptions<LoginContext> optins)
            : base(optins)
        {
        }

        public DbSet<ConnectionModel> Connections { get; set; }
    }
}
