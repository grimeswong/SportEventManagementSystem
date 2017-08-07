using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace SportEventManagementSystem.Models.EventViewModels
{
    public class CreateEventViewModel
    {
        [Required]
        [StringLength(30, ErrorMessage = "Your {0} must be at max {1} character long", MinimumLength = 1)]
        [Display(Name = "Event Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Your {0} must be at max {1} character long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Event Venue Name")]
        public string VenueName { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "Your {0} must be at max {1} character long.", MinimumLength = 1)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Event Description")]
        public string Description { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Your {0} must be at least {2} and at max {1} character long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Street")]
        public string StreetAddress { get; set; }

        [Required]
        [StringLength(35, ErrorMessage = "Your {0} must be at least {2} and at max {1} character long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Suburb")]
        public string Suburb { get; set; }

        [Required]
        [StringLength(4, ErrorMessage = "Please enter a valid postcode. {1} characters long.", MinimumLength = 4)]
        [DataType(DataType.Text)]
        [Display(Name = "Postcode")]
        public string PostCode { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Event Start Date & Time")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yy H:mm:ss tt}")]
        public DateTime StartTime { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [CustomAttributes.IsEndDateAfter("{0} must be a later date / time then event start.","StartTime","Event End Date & Time")]
        [Display(Name = "Event End Date & Time")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yy H:mm:ss tt}")]
        public DateTime EndTime { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Registration Start Date & Time")]
        [CustomAttributes.IsDateBetween("'{0}' must be a later date / time then event start and an earlier date / time than event end.","StartTime","EndTime", "Registration Start Date & Time")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yy H:mm:ss tt}")] // need to set the TimeSpan.Ten (option for the eastern and western time zone)
        public DateTime RegStartTime { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [CustomAttributes.IsDateBetween("'{0}' must be a later date / time then event start and an earlier date / time than event end.", "StartTime", "EndTime", "Registration End Date & Time")]
        [Display(Name = "Registration End Date & Time")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yy H:mm:ss tt}")] // need to set the TimeSpan.Ten (option for the eastern and western time zone)
        public DateTime RegEndTime { get; set; }

        [Required]
        [Range(1, 100000)]    // Range from 1 to 100000 (need to see work or not) or need to use custom one
        [Display(Name = "Entry Capacity")]
        public int EntryCapacity { get; set; }


        [StringLength(70, ErrorMessage = "Your {0} must be at least {2} and at max {1} character long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Organiser Name")]
        public string OrganiserName { get; set; }

        [StringLength(70, ErrorMessage = "Your {0} must be at least {2} and at max {1} character long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Organiser Club")]
        public string OrganiserClub { get; set; }

        [Required]
        [Display(Name = "Private Event?")]
        public bool IsPrivate { get; set; }

        [Required]
        public List<CompetitionModel> Competitions { get; set; }

        public int CompetitionCount { get; set; }

    }

    public class CompetitionModel
    {
        [Required]
        [StringLength(30, ErrorMessage = "Please enter a valid competition name. {1} characters long.", MinimumLength = 4)]
        [Display(Name = "Competition Name")]
        public string CompName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Please enter a valid division name. {1} characters long.", MinimumLength = 1)]
        [Display(Name = "Division Name")]
        public Division DivisionName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Please enter a valid sport type. {1} characters long.", MinimumLength = 1)]
        [Display(Name = "Sport Type")]
        public SportType SportName { get; set; }

        [Required]
        [StringLength(4, ErrorMessage = "Please enter a valid location. {1} characters long.", MinimumLength = 1)]
        [Display(Name = "Compeition Location")]
        public string Location { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Competition Start Date & Time")]
        [CustomAttributes.IsDateBetween("'{0}' must be a later date / time then event start and an earlier date / time than event end.", "StartTime", "EndTime", "Competition Start Date & Time")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yy H:mm:ss tt}")] // need to set the TimeSpan.Ten (option for the eastern and western time zone)
        public DateTime StartTime { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [CustomAttributes.IsDateBetween("'{0}' must be a later date / time then event start and an earlier date / time than event end.", "StartTime", "EndTime", "Competition End Date & Time")]
        [Display(Name = "Competition End Date & Time")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yy H:mm:ss tt}")] // need to set the TimeSpan.Ten (option for the eastern and western time zone)
        public DateTime EndTime { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Entry Capacity must be between 1 to 1000")]    // Range from 1 to 1000 (need to see work or not) or need to use custom one
        [Display(Name = "Entry Capacity")]
        public int EntryCapacity { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Team Minimum Number must be between 1 to 1000")]    // Range from 1 to 1000 
        [Display(Name = "Team Minimum Number")]
        public int TeamSizeMin { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Team Maximum Number must be between 1 to 1000")]    // Range from 1 to 1000 
        [Display(Name = "Team Maximum Number")]
        public int TeamSizeMax { get; set; }

        [Range(0, 130, ErrorMessage = "Age must be between 0 to 130")]  // Range from 0 to 130 (Age minimum) 
        [Display(Name = "Minimum Age")]
        public int MinimumAge { get; set; }

        [Range(0, 130, ErrorMessage = "Age must be between 0 to 130")]  // Range from 0 to 130 (Age maximum) 
        [Display(Name = "Maximum Age")]
        public int MaximumAge { get; set; }
        
        [Display(Name = "Gender Restriction")]
        [Range(1,3, ErrorMessage = "Please select one of the options")]  // 0=no selection, 1=male, 2=female, 3=mixed  
        public int Gender { get; set; } 
    }
}
