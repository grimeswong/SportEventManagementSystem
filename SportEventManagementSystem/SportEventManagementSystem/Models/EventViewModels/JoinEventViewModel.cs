using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SportEventManagementSystem.Models.EventViewModels
{
    public class JoinEventViewModel
    { 
        public string TeamName { get; set; }
        public List<TeamMemberValidationModel> members { get; set; }
    }

    public class TeamMemberValidationModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Team Member Name")]
        [StringLength(70, ErrorMessage = "Your {0} must be at max {1} character long", MinimumLength = 1)]
        public string MemberName { get; set; }
    }
}
