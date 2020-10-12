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
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;

        #region Journal
        Task AddJournalEntryAsync(JournalEntry entry);
        Task EditJournalEntryAsync(Guid entryId, JournalEntry entry);
        Task<JournalEntry> GetJournalEntryAsync(Guid entryId);
        Task<IEnumerable<JournalEntry>> GetJournalEntriesAsync();
        Task<JournalEntry> GetJournalEntryWithRepliesAsync(Guid entryId);
        Task<IEnumerable<JournalEntry>> GetMyJournalEntriesAsync();
        #endregion

        #region Journal Entry
        Task AddJournalEntryReplyAsync(Guid entryId, JournalReply reply);
        Task EditJournalEntryReplyAsync(Guid entryId, JournalReply reply);
        Task<JournalReply> GetJournalEntryReplyAsync(Guid entryId);
        Task<IEnumerable<JournalReply>> GetJournalEntryRepliesAsync();
        #endregion

        #region Journal Status
        Task<IEnumerable<JournalStatus>> GetJournalStatusesAsync();
        #endregion
    }
}
