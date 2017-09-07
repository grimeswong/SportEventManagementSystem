using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SportEventManagementSystem.Models.EventViewModels
{
    public class JoinCompetitionViewModel
    { 
        public Competition Competition { get; set; }

        public string TeamName { get; set; }

        [CustomAttributes.IsListCountLargerThan(1, "JoinIndividual",false,"Please enter atleast one team member to register as a team.")]
        public String[] TeamMembers { get; set; }

        [Required]
        public bool JoinIndividual { get; set; }

        [Display(Name = "Are you participating in the team?")]
        public bool ManagerParticipation { get; set; }
    }
}
