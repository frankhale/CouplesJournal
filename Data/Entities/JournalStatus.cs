using System.ComponentModel.DataAnnotations;

namespace CouplesJournal.Data.Entities
{
    public class JournalStatus
    {
        [Key]
        public int Id { get; set; }
        public string Value { get; set; }
    }
}