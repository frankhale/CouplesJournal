﻿using CouplesJournal.Data.Entities;
using CouplesJournal.Data.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CouplesJournal.Data.API
{
    interface ICouplesJournalDataApi
    {
        Task<bool> SaveChangesAsync();
        void Add<T>(T entity) where T : Entity;
        void Delete<T>(T entity) where T : Entity;

        UserStats GetUserStats(string userName);

        #region Journal
        Task<IEnumerable<JournalEntry>> GetPagedJournalEntriesAsync(int pageNumber, int pageSize, string filter);
        bool HasJournalsToView();
        int GetTotalJournals(string filter);
        Task AddJournalEntryAsync(JournalEntry entry);
        Task EditJournalEntryAsync(Guid entryId, JournalEntry entry);
        Task<JournalEntry> GetJournalEntryAsync(Guid entryId);
        Task<IEnumerable<JournalEntry>> GetJournalEntriesAsync();
        Task<JournalEntry> GetJournalEntryWithRepliesAsync(Guid entryId);
        Task<IEnumerable<JournalEntry>> GetMyJournalEntriesAsync(string userName);
        Task DeleteJournalEntryAsync(Guid entryId);
        bool HasUnreadReplies(Guid entryId);
        bool GetViewTracking(JournalViewTracker viewTracker, string userName);
        Task AddViewTrackingAsync(JournalViewTracker viewTracker, string userName);
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
        Task SetEmailNotificationProcessed(EmailNotification emailNotification);
        #endregion
    }
}
