using System.ComponentModel.DataAnnotations;

namespace CouplesJournal.Data.Entities
{
    public class JournalReply : Entity
    {
        [Required]
        public string Body { get; set; }
    }
}
