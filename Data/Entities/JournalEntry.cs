using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CouplesJournal.Data.Entities
{
    public class JournalEntry : Entity
    {
        [MaxLength(512)]
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public JournalStatus Status { get; set; }
        public ICollection<JournalReply> Replies { get; set; } = new List<JournalReply>();
    }
}
