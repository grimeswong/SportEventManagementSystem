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
        [CustomAttributes.IsDateBetween("'{0}' must be a later date / time then event start and an earlier date / time than event end.","StartTime","EndTime", "Registration State Date & Time")]
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
        [StringLength(4, ErrorMessage = "Please enter a valid competition name. {1} characters long.", MinimumLength = 4)]
        [Display(Name="Competition Name")]
        public string CompName { get; set; }

        public int RestrictionCount { get; set; }
    }
}
