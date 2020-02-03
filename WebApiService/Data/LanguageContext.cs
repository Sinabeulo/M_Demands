using BizCommon_Core.Models;
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
