using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CouplesJournal.Data.Entities
{
    public class JournalEntry : Entity
    {
        [MaxLength(512)]
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }          
        [ForeignKey("fk_journalstatus")]
        public JournalStatus Status { get; set; }
        [ForeignKey("fk_journalentry")]
        public ICollection<JournalReply> Replies { get; set; } = new List<JournalReply>();
    }
}
