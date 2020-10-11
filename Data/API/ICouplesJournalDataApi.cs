using CouplesJournal.Data.Entities;
using System;
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
        Task EditJounralEntryAsync(Guid entryId, JournalEntry entry);
        #endregion

        #region Journal Entry
        Task AddJournalReplyAsync(Guid entryId, JournalReply reply);
        Task EditJounralEntryReplyAsync(Guid entryId, JournalReply reply);
        #endregion

    }
}
