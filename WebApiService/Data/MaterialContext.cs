using Microsoft.EntityFrameworkCore;

namespace WebApiService.Data
{
    public class MaterialContext : DbContext
    {
        public MaterialContext(DbContextOptions<MaterialContext>options)
            : base(options)
        {
        }

        public DbSet<object> MaterialInfos { get; set; }
    }
}
