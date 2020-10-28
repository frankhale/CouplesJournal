using CouplesJournal.Blazor.Data.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CouplesJournal.Blazor.Data.API
{
    interface ICouplesJournalDataApi
    {
        Task<bool> SaveChangesAsync();
        void Add<T>(T entity) where T : Entity;
        void Delete<T>(T entity) where T : Entity;

        #region Journal
        Task<IEnumerable<JournalEntry>> GetPagedJournalEntriesAsync(int pageNumber, int pageSize);
        bool HasJournalsToView();
        int GetTotalJournals();
        Task AddJournalEntryAsync(JournalEntry entry);
        Task EditJournalEntryAsync(Guid entryId, JournalEntry entry);
        Task<JournalEntry> GetJournalEntryAsync(Guid entryId);
        Task<IEnumerable<JournalEntry>> GetJournalEntriesAsync();
        Task<JournalEntry> GetJournalEntryWithRepliesAsync(Guid entryId);
        Task<IEnumerable<JournalEntry>> GetMyJournalEntriesAsync(string userName);
        Task DeleteJournalEntryAsync(Guid entryId);
        #endregion

        #region Journal Reply
        Task AddJournalEntryReplyAsync(Guid entryId, JournalReply reply);
        Task EditJournalEntryReplyAsync(Guid entryId, JournalReply reply);
        Task<JournalReply> GetJournalEntryReplyAsync(Guid entryId);
        Task<IEnumerable<JournalReply>> GetJournalEntryRepliesAsync();
        Task DeleteJournalReplyAsync(Guid entryId);
        #endregion

        #region Journal Status
        Task<IEnumerable<JournalStatus>> GetJournalStatusesAsync();
        #endregion

        #region Email Notification
        Task AddEmailNotificationAsync(EmailNotification emailNotification);
        Task<IEnumerable<EmailNotification>> GetEmailNotificationsToProcess();
        #endregion
    }
}
