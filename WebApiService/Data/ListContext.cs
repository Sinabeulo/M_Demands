using Microsoft.EntityFrameworkCore;
using BizCommon_Core.Models;

namespace WebApiService.Data
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
