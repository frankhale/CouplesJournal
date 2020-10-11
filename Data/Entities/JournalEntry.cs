using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CouplesJournal.Data.Entities
{
    public class JournalEntry
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [MaxLength(450)]
        public string UserName { get; set; }        
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        [MaxLength(512)]
        public string Title { get; set; }
        public string Body { get; set; }
        public ICollection<JournalReply> Replies { get; set; } = new List<JournalReply>();
    }
}
