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

        Task AddJournalEntryAsync(JournalEntry entry);
        Task AddJournalReplyAsync(Guid entryId, JournalReply reply);
    }
}
