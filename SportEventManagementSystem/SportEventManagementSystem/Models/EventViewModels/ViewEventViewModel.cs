using System.ComponentModel.DataAnnotations;
namespace SportEventManagementSystem.Models.EventViewModels
{
    public class ViewEventViewModel
    {
        public Event CurrentEvent { get; set; }

        [Required]
        public InviteViewModel Invite { get; set; }

    }
}
