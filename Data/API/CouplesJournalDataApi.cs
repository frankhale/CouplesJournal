using CouplesJournal.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CouplesJournal.Data.API
{
    public class CouplesJournalDataApi : ICouplesJournalDataApi, IDisposable
    {
        private readonly CouplesJournalDbContext _db;
        private bool disposedValue;

        public CouplesJournalDataApi(CouplesJournalDbContext db)
        {
            _db = db;
        }

        #region IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects)
                }

                // free unmanaged resources (unmanaged objects) and override finalizer
                // set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Misc
        public async Task<bool> SaveChangesAsync()
        {
            return (await _db.SaveChangesAsync()) > 0;
        }

        public void Add<T>(T entity) where T : class
        {
            _db.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _db.Remove(entity);
        }
        #endregion

        #region Journal
        public async Task AddJournalEntryAsync(JournalEntry entry)
        {
            _db.JournalEntries.Add(entry);
            await _db.SaveChangesAsync();
        }
        
        public async Task EditJounralEntryAsync(Guid entryId, JournalEntry entry)
        {
            var journalEntry = _db.JournalEntries.FirstOrDefault(x => x.Id == entryId);

            if (journalEntry != null)
            {                
                journalEntry.UpdatedOn = DateTime.Now;
                journalEntry.Title = entry.Title;
                journalEntry.Body = entry.Body;                

                await SaveChangesAsync();
            }
        }
        #endregion

        #region Journal Reply
        public async Task AddJournalReplyAsync(Guid entryId, JournalReply reply)
        {            
            var entry = await _db.JournalEntries.FirstOrDefaultAsync(x => x.Id == entryId);

            if(entry != null)
            {
                entry.Replies.Add(reply);

                await SaveChangesAsync();
            }
        }

        public async Task EditJounralEntryReplyAsync(Guid entryId, JournalReply reply)
        {
            var replyEntry = _db.JournalReplies.FirstOrDefault(x => x.Id == entryId);

            if(replyEntry != null)
            {
                replyEntry.UpdatedOn = DateTime.Now;
                replyEntry.Body = reply.Body;

                await SaveChangesAsync();
            }
        }
        #endregion
    }
}
