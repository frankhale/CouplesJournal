using System;
using System.ComponentModel.DataAnnotations;

namespace CouplesJournal.Data.Entities
{
    public class JournalReply
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [MaxLength(450)]
        public string UserName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string Body { get; set; }        
    }
}
