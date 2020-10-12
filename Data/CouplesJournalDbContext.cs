using CouplesJournal.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CouplesJournal.Data
{
    public class CouplesJournalDbContext : DbContext
    {
        public DbSet<JournalEntry> JournalEntries { get; set; }
        public DbSet<JournalReply> JournalReplies { get; set; }
        public DbSet<JournalStatus> JournalStatuses { get; set; }

        public CouplesJournalDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JournalStatus>().HasData(new JournalStatus[]
            {
                new JournalStatus { Id = 1, Value = "Draft" },
                new JournalStatus { Id = 2, Value = "Final" }
            });
        }
    }
}
