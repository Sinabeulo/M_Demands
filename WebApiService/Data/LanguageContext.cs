using BizCommon_Std.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiService.Data
{
    public class LanguageContext : DbContext
    {
        public LanguageContext(DbContextOptions<LanguageContext> optins)
            : base(optins)
        {
        }

        public DbSet<LanguageControlModel> Languages { get; set; }

    }
}
