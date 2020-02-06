using Microsoft.EntityFrameworkCore;
using BizCommon_Std.Models;

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
