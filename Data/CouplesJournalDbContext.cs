using CouplesJournal.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace CouplesJournal.Data
{
    public class CouplesJournalDbContext : DbContext
    {
        public DbSet<JournalEntry> JournalEntries { get; set; }
        public DbSet<JournalReply> JournalReplies { get; set; }
        public DbSet<JournalStatus> JournalStatuses { get; set; }
        public DbSet<EmailNotification> EmailNotifications { get; set; }

        public CouplesJournalDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JournalEntry>().HasOne<JournalStatus>();
            modelBuilder.Entity<JournalEntry>().HasMany<JournalReply>();
            modelBuilder.Entity<JournalEntry>().HasQueryFilter(x => !x.MarkedForDeletion);
            modelBuilder.Entity<JournalReply>().HasQueryFilter(x => !x.MarkedForDeletion);

            modelBuilder.Entity<JournalStatus>().HasData(new JournalStatus[]
            {
                new JournalStatus { Id = 1, Value = "Draft" },
                new JournalStatus { Id = 2, Value = "Final" }
            });

            //modelBuilder.Entity<JournalEntry>().HasData(new JournalEntry[]
            //{
            //    new JournalEntry { Id = Guid.NewGuid(), UserName = "user1", Title = "Test 1", Body = "Test body 1", CreatedOn = DateTime.Now, JournalStatusId = 2 },
            //    new JournalEntry { Id = Guid.NewGuid(), UserName = "user2", Title = "Test 2", Body = "Test body 2", CreatedOn = DateTime.Now, JournalStatusId = 2 },
            //    new JournalEntry { Id = Guid.NewGuid(), UserName = "user1", Title = "Test 3", Body = "Test body 3", CreatedOn = DateTime.Now, JournalStatusId = 2 },
            //    new JournalEntry { Id = Guid.NewGuid(), UserName = "user2", Title = "Test 4", Body = "Test body 4", CreatedOn = DateTime.Now, JournalStatusId = 2 },
            //    new JournalEntry { Id = Guid.NewGuid(), UserName = "user1", Title = "Test 5", Body = "Test body 5", CreatedOn = DateTime.Now, JournalStatusId = 2 },
            //    new JournalEntry { Id = Guid.NewGuid(), UserName = "user2", Title = "Test 6", Body = "Test body 6", CreatedOn = DateTime.Now, JournalStatusId = 2 },
            //    new JournalEntry { Id = Guid.NewGuid(), UserName = "user1", Title = "Test 7", Body = "Test body 7", CreatedOn = DateTime.Now, JournalStatusId = 2 },
            //    new JournalEntry { Id = Guid.NewGuid(), UserName = "user2", Title = "Test 8", Body = "Test body 8", CreatedOn = DateTime.Now, JournalStatusId = 2 },
            //    new JournalEntry { Id = Guid.NewGuid(), UserName = "user1", Title = "Test 9", Body = "Test body 9", CreatedOn = DateTime.Now, JournalStatusId = 2 },
            //    new JournalEntry { Id = Guid.NewGuid(), UserName = "user2", Title = "Test 10", Body = "Test body 10", CreatedOn = DateTime.Now, JournalStatusId = 2 },
            //    new JournalEntry { Id = Guid.NewGuid(), UserName = "user1", Title = "Test 11", Body = "Test body 11", CreatedOn = DateTime.Now, JournalStatusId = 2 },
            //    new JournalEntry { Id = Guid.NewGuid(), UserName = "user2", Title = "Test 12", Body = "Test body 12", CreatedOn = DateTime.Now, JournalStatusId = 2 },
            //    new JournalEntry { Id = Guid.NewGuid(), UserName = "user1", Title = "Test 13", Body = "Test body 13", CreatedOn = DateTime.Now, JournalStatusId = 2 },
            //    new JournalEntry { Id = Guid.NewGuid(), UserName = "user2", Title = "Test 14", Body = "Test body 14", CreatedOn = DateTime.Now, JournalStatusId = 2 },
            //    new JournalEntry { Id = Guid.NewGuid(), UserName = "user1", Title = "Test 15", Body = "Test body 15", CreatedOn = DateTime.Now, JournalStatusId = 2 },
            //    new JournalEntry { Id = Guid.NewGuid(), UserName = "user2", Title = "Test 16", Body = "Test body 16", CreatedOn = DateTime.Now, JournalStatusId = 2 },
            //    new JournalEntry { Id = Guid.NewGuid(), UserName = "user1", Title = "Test 17", Body = "Test body 17", CreatedOn = DateTime.Now, JournalStatusId = 2 },
            //    new JournalEntry { Id = Guid.NewGuid(), UserName = "user2", Title = "Test 18", Body = "Test body 18", CreatedOn = DateTime.Now, JournalStatusId = 2 },
            //    new JournalEntry { Id = Guid.NewGuid(), UserName = "user1", Title = "Test 19", Body = "Test body 19", CreatedOn = DateTime.Now, JournalStatusId = 2 },
            //    new JournalEntry { Id = Guid.NewGuid(), UserName = "user2", Title = "Test 20", Body = "Test body 20", CreatedOn = DateTime.Now, JournalStatusId = 2 }
            //});
        }
    }
}
