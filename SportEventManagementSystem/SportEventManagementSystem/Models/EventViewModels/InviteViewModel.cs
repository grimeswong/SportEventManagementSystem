using System.ComponentModel.DataAnnotations;

namespace SportEventManagementSystem.Models.EventViewModels
{
    public class InviteViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string email { get; set; }
        
        [Required]
        public string eventID { get; set; }
    }
}
