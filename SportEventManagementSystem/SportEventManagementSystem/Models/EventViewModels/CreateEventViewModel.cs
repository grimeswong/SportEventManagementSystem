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
        public string EventName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Your {0} must be at max {1} character long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Event Venue Name")]
        public string EventVenueName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Your {0} must be at least {2} and at max {1} character long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Street")]
        public string EventStreetName { get; set; }

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
        [DataType(DataType.Date)]
        [Display(Name = "Event Start Date")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yy}")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Event End Date")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yy}")]
        public DateTime EndDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Registration Start Date & Time")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yy H:mm:ss zzz}")] // need to set the TimeSpan.Ten (option for the eastern and western time zone)
        public DateTime RegStartDateTime { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Registration End Date & Time")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yy H:mm:ss zzz}")] // need to set the TimeSpan.Ten (option for the eastern and western time zone)
        public DateTime RegEndDateTime { get; set; }

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

    }

}
