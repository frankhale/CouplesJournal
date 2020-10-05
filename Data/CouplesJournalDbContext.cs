using CouplesJournal.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CouplesJournal.Data
{
    public class CouplesJournalDbContext : DbContext
    {
        public DbSet<JournalEntry> JournalEntries { get; set; }
        public DbSet<JournalReply> JournalReplies { get; set; }

        public CouplesJournalDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
