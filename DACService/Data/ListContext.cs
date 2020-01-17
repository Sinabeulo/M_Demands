using DACService.Models;
using Microsoft.EntityFrameworkCore;

namespace DACService.Data
{
    public class ListContext : DbContext
    {
        public ListContext(DbContextOptions<ListContext> optins)
            : base(optins)
        {
        }

        public DbSet<ConnectionModel> ConnectionList { get; set; }
    }
}
