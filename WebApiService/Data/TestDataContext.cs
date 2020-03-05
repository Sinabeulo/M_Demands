using BizCommon_Std.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiService.Data
{
    public class TestDataContext : DbContext
    {
        public TestDataContext(DbContextOptions<TestDataContext> optins)
               : base(optins)
        {
        }

        public DbSet<TestDataMakerModel> Makers { get; set; }
    }
}
