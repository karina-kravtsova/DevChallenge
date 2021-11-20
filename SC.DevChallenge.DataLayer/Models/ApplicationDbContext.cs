using Microsoft.EntityFrameworkCore;
using SC.DevChallenge.DataLayer.Tables;

namespace SC.DevChallenge.DataLayer.Db
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<FinanceInstrument> FinanceInstruments { get; set; }
    }
}
