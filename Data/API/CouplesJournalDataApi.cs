using CouplesJournal.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouplesJournal.Data.API
{
    public class CouplesJournalDataApi : ICouplesJournalDataApi, IDisposable
    {
        private readonly CouplesJournalDbContext _db;
        private readonly IHttpContextAccessor _httpContext;
        private bool disposedValue;

        public CouplesJournalDataApi(CouplesJournalDbContext db, IHttpContextAccessor httpContext)
        {
            _db = db;
            _httpContext = httpContext;
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

        private void SetCreatedUpdated<T>(T entry) where T : Entity
        {
            if (entry.CreatedOn == DateTime.MinValue)
            {
                entry.CreatedOn = DateTime.Now;
            }

            entry.UpdatedOn = DateTime.Now;
            entry.UserName = _httpContext.HttpContext.User.Identity.Name;
            entry.UpdatedBy = _httpContext.HttpContext.User.Identity.Name;
        }
        #endregion

        #region Journal
        public async Task AddJournalEntryAsync(JournalEntry entry)
        {
            SetCreatedUpdated(entry);

            _db.JournalEntries.Add(entry);
            await _db.SaveChangesAsync();
        }

        public async Task EditJournalEntryAsync(Guid entryId, JournalEntry entry)
        {
            var journalEntry = _db.JournalEntries.FirstOrDefault(x => x.Id == entryId);

            if (journalEntry != null)
            {
                SetCreatedUpdated(entry);

                journalEntry.Title = entry.Title;
                journalEntry.Body = entry.Body;

                await SaveChangesAsync();
            }
        }

        public async Task<JournalEntry> GetJournalEntryAsync(Guid entryId)
        {
            return await _db.JournalEntries.FirstOrDefaultAsync(x => x.Id == entryId);
        }

        public async Task<IEnumerable<JournalEntry>> GetJournalEntriesAsync()
        {
            return await _db.JournalEntries
                            .Include(x => x.Status)
                            .Where(x => x.Status.Value.ToLower() != "draft")
                            .OrderByDescending(x => x.UpdatedOn)
                            .ToListAsync();
        }

        public async Task<IEnumerable<JournalEntry>> GetMyJournalEntriesAsync()
        {
            return await _db.JournalEntries
                            .Include(x => x.Status)
                            .Where(x => x.UserName == _httpContext.HttpContext.User.Identity.Name)
                            .OrderByDescending(x => x.UpdatedOn)
                            .ToListAsync();
        }
        #endregion

        #region Journal Reply
        public async Task AddJournalEntryReplyAsync(Guid entryId, JournalReply reply)
        {
            var entry = await _db.JournalEntries.FirstOrDefaultAsync(x => x.Id == entryId);

            if (entry != null)
            {
                SetCreatedUpdated(entry);

                entry.Replies.Add(reply);

                await SaveChangesAsync();
            }
        }

        public async Task EditJournalEntryReplyAsync(Guid entryId, JournalReply reply)
        {
            var replyEntry = _db.JournalReplies.FirstOrDefault(x => x.Id == entryId);

            if (replyEntry != null)
            {
                SetCreatedUpdated(reply);

                replyEntry.Body = reply.Body;

                await SaveChangesAsync();
            }
        }

        public async Task<JournalReply> GetJournalEntryReplyAsync(Guid entryId)
        {
            return await _db.JournalReplies.FirstOrDefaultAsync(x => x.Id == entryId);
        }

        public async Task<IEnumerable<JournalReply>> GetJournalEntryRepliesAsync()
        {
            return await _db.JournalReplies.OrderByDescending(x => x.UpdatedOn).ToListAsync();
        }
        #endregion

        #region Journal Status
        public async Task<IEnumerable<JournalStatus>> GetJournalStatusesAsync()
        {
            return await _db.JournalStatuses.ToListAsync();
        }
        #endregion
    }
}
