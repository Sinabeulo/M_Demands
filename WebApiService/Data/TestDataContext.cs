using Microsoft.EntityFrameworkCore;

namespace WebApiService.Data
{
    public class TestDataContext : DbContext
    {
        public TestDataContext(DbContextOptions<MaterialContext> options)
          : base(options)
        {
        }

        public DbSet<object> TestDataInfos { get; set; }
    }
}
