using WebApiService.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiService.Data
{
    public class ConnectionContext : DbContext
    {
        public ConnectionContext(DbContextOptions<ConnectionContext>optins)
            : base(optins)
        {
        }

        public DbSet<ConnectionModel> Connections { get; set; }
    }
}
