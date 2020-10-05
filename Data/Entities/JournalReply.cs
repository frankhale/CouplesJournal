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
        public string Body { get; set; }        
    }
}
