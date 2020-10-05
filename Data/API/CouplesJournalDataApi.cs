﻿using CouplesJournal.Data.Entities;
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

        public async Task AddJournalEntryAsync(JournalEntry entry)
        {
            _db.JournalEntries.Add(entry);
            await _db.SaveChangesAsync();
        }

        public async Task AddJournalReplyAsync(Guid entryId, JournalReply reply)
        {            
            var entry = await _db.JournalEntries.FirstOrDefaultAsync(x => x.Id == entryId);

            if(entry != null)
            {
                entry.Replies.Add(reply);
            }
        }
    }
}
