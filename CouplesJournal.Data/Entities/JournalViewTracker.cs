﻿using System;

namespace CouplesJournal.Data.Entities
{
    public class JournalViewTracker : Entity
    {
        public Guid? JournalEntryId { get; set; }
        public Guid? ReplyId { get; set; }
    }
}
