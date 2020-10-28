using System;
using System.ComponentModel.DataAnnotations;

namespace CouplesJournal.Blazor.Data.Entities
{
    public class JournalReply : Entity
    {
        [Required]
        public string Body { get; set; }        
        public Guid JournalEntryId { get; set; }
    }
}
