using CouplesJournal.Data.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CouplesJournal.Data.API
{
    interface ICouplesJournalDataApi
    {
        Task<bool> SaveChangesAsync();
        void Add<T>(T entity) where T : Entity;
        void Delete<T>(T entity) where T : Entity;
        Task<IEnumerable<T>> GetPagedResultsAsync<T>(int pageNumber, int pageSize) where T : Entity;

        #region Journal
        int GetTotalJournals();
        Task AddJournalEntryAsync(JournalEntry entry);
        Task EditJournalEntryAsync(Guid entryId, JournalEntry entry);
        Task<JournalEntry> GetJournalEntryAsync(Guid entryId);
        Task<IEnumerable<JournalEntry>> GetJournalEntriesAsync();
        Task<JournalEntry> GetJournalEntryWithRepliesAsync(Guid entryId);
        Task<IEnumerable<JournalEntry>> GetMyJournalEntriesAsync();
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
    }
}
