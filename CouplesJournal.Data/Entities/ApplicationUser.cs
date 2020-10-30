using Microsoft.AspNetCore.Identity;

namespace CouplesJournal.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string DisplayName { get; set; }
    }
}
