using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CouplesJournal.Data.Entities
{
    public class JournalReply : Entity
    {
        [Required]
        public string Body { get; set; }        
        public Guid JournalEntryId { get; set; }
    }
}
