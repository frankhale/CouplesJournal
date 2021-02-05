using CouplesJournal.Data.Entities;
using CouplesJournal.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CouplesJournal.Data.API
{
    public class CouplesJournalDataApi : ICouplesJournalDataApi, IDisposable
    {
        private readonly CouplesJournalDbContext _db;
        private readonly ClaimsPrincipal _user;
        private bool disposedValue;

        public CouplesJournalDataApi(CouplesJournalDbContext db, ClaimsPrincipal claimsPrincipal = null)
        {
            _db = db;
            _user = claimsPrincipal;
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

        public void Add<T>(T entity) where T : Entity
        {
            _db.Add(entity);
        }

        public void Delete<T>(T entity) where T : Entity
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

            if (_user != null)
            {
                entry.UserName = _user.Identity.Name;
                entry.UpdatedBy = _user.Identity.Name;
            }
        }
        #endregion

        public UserStats GetUserStats(string userName)
        {
            var journals = _db.JournalEntries.Count(x => x.UserName == userName && x.Status.Value != "Draft");
            var replies = _db.JournalReplies.Count(x => x.UserName == userName);

            return new UserStats
            {
                NumberOfJournals = journals,
                NumberOfReplies = replies
            };
        }

        #region Journal
        public async Task<IEnumerable<JournalEntry>> GetPagedJournalEntriesAsync(int pageNumber, int pageSize, string filter)
        {
            var query = _db.JournalEntries.Include(x => x.Status)
                                          .Include(x => x.Replies)                                          
                                          .OrderByDescending(x => x.UpdatedOn)
                                          .AsQueryable();                                          

            if(!string.IsNullOrEmpty(filter) && filter.ToLower() == "me")
            {
                query = query.Where(x => x.UserName == _user.Identity.Name &&
                                         x.Status.Value == "Draft" ||
                                         x.Status.Value == "Final");
            }
            else
            {
                query = query.Where(x => x.Status.Value != "Draft");
            }

            query = query.Skip((pageNumber - 1) * pageSize)
                         .Take(pageSize);

            return await query.ToListAsync();
        }

        public bool HasJournalsToView()
        {
            return _db.JournalEntries.Any(x => x.Status.Value.ToLower() == "final");
        }

        public int GetTotalJournals(string filter)
        {
            var result = _db.JournalEntries.Include(x => x.Status);

            if(!string.IsNullOrEmpty(filter) && filter.ToLower() == "me")
            {
                return result.Where(x => x.UserName == _user.Identity.Name).Count(x => x.Status.Value != "Draft");
            }

            return result.Count(x => x.Status.Value != "Draft");
        }

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

        public async Task<IEnumerable<JournalEntry>> GetMyJournalEntriesAsync(string userName)
        {
            return await _db.JournalEntries
                            .Include(x => x.Status)
                            .Where(x => x.UserName == userName)
                            .OrderByDescending(x => x.UpdatedOn)
                            .ToListAsync();
        }

        public async Task<JournalEntry> GetJournalEntryWithRepliesAsync(Guid entryId)
        {
            return await _db.JournalEntries
                            .Include(x => x.Status)
                            .Include(x => x.Replies)
                            .FirstOrDefaultAsync(x => x.Id == entryId);
        }

        public async Task DeleteJournalEntryAsync(Guid entryId)
        {
            var journalEntry = await _db.JournalEntries.FirstOrDefaultAsync(x => x.Id == entryId);

            if (journalEntry != null)
            {
                SetCreatedUpdated(journalEntry);

                journalEntry.MarkedForDeletion = true;

                await _db.SaveChangesAsync();
            }
        }

        public bool GetViewTracking(JournalViewTracker viewTracker, string userName)
        {
            if (viewTracker.JournalEntryId != null)
            {
                return _db.JournalViewTracker.Any(x => x.JournalEntryId == viewTracker.JournalEntryId && x.UserName == userName);
            }
            else if (viewTracker.ReplyId != null)
            {
                return _db.JournalViewTracker.Any(x => x.ReplyId == viewTracker.ReplyId && x.UserName == userName);
            }

            return false;
        }

        public async Task AddViewTrackingAsync(JournalViewTracker viewTracker, string userName)
        {
            bool trackingExists = false;

            if (viewTracker.JournalEntryId != null)
            {
                trackingExists = _db.JournalViewTracker.Any(x => x.JournalEntryId == viewTracker.JournalEntryId && x.UserName == userName);
                viewTracker.ReplyId = null;
            }
            else
            {
                trackingExists = _db.JournalViewTracker.Any(x => x.ReplyId == viewTracker.ReplyId && x.UserName == userName);
                viewTracker.JournalEntryId = null;
            }

            if (!trackingExists)
            {
                var journalViewTracker = new JournalViewTracker()
                {
                    JournalEntryId = viewTracker.JournalEntryId,
                    ReplyId = viewTracker.ReplyId,
                    UserName = userName,
                    CreatedOn = DateTime.Now
                };

                _db.JournalViewTracker.Add(journalViewTracker);
                await _db.SaveChangesAsync();
            }
        }
        #endregion

        #region Journal Reply
        public async Task AddJournalEntryReplyAsync(Guid entryId, JournalReply reply)
        {
            var entry = await _db.JournalEntries.FirstOrDefaultAsync(x => x.Id == entryId);

            if (entry != null)
            {
                SetCreatedUpdated(reply);

                reply.JournalEntryId = entryId;

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

        public async Task DeleteJournalReplyAsync(Guid entryId)
        {
            var replyEntry = await _db.JournalReplies.FirstOrDefaultAsync(x => x.Id == entryId);

            if (replyEntry != null)
            {
                SetCreatedUpdated(replyEntry);

                replyEntry.MarkedForDeletion = true;

                await _db.SaveChangesAsync();
            }
        }
        #endregion

        #region Journal Status
        public async Task<IEnumerable<JournalStatus>> GetJournalStatusesAsync()
        {
            return await _db.JournalStatuses.ToListAsync();
        }
        #endregion

        #region Email Notification
        public async Task AddEmailNotificationAsync(EmailNotification emailNotification)
        {
            emailNotification.CreatedOn = DateTime.Now;

            _db.EmailNotifications.Add(emailNotification);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmailNotification>> GetEmailNotificationsToProcess()
        {
            return await _db.EmailNotifications.Where(x => !x.Processed).ToListAsync();
        }

        public async Task SetEmailNotificationProcessed(EmailNotification emailNotification)
        {
            emailNotification.Processed = true;
            await _db.SaveChangesAsync();
        }
        #endregion
    }
}
