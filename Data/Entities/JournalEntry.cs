using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CouplesJournal.Data.Entities
{
    public class JournalEntry : Entity
    {
        
        [MaxLength(512)]
        public string Title { get; set; }
        public string Body { get; set; }
        public ICollection<JournalReply> Replies { get; set; } = new List<JournalReply>();
    }
}
