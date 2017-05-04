using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportEventManagementSystem.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        private const string PHONENUMBERREGEX = @"(^1(3|8)[0-9]{2}(\ |-){0,1}[0-9]{3}(\ |-){0,1}[0-9]{3})|(^\({0,1}(0|\+61){0,1}(\ |-){0,1}0{0,1}(2|4|3|7|8){0,1}\){0,1}(\ |-){0,1}[0-9]{2}(\ |-){0,1}[0-9]{2}(\ |-){0,1}[0-9]{1}(\ |-){0,1}[0-9]{3})|(^13((\ |-){0,1}[0-9]{2}(\ |-){0,1}[0-9]{2}))|(^13[0-9](\ |-){0,1}[0-9]{3})";

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(35, ErrorMessage = "Your {0} must be at max {1} character long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(35, ErrorMessage = "Your {0} must be at max {1} character long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(35, ErrorMessage = "Your {0} must be at max {1} character long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "Your {0} must be at least {2} and at max {1} character long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Street")]
        public string Street { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Your {0} must be at least {2} and at max {1} character long.", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Display(Name = "Suburb")]
        public string Suburb { get; set; }

        [Required]
        [StringLength(4, ErrorMessage = "Please enter a valid postcode. {1} characters long.", MinimumLength = 4)]
        [DataType(DataType.Text)]
        [Display(Name = "Postcode")]
        public string PostCode { get; set; }

        [Required]
        [DataType("Gender")]
        [Display(Name = "Gender")]
        [IsGender(ErrorMessage = "Please select a gender.")]
        public int Gender { get; set; }

        [Required]
               [DataType(DataType.Text)]
        [RegularExpression(PHONENUMBERREGEX, ErrorMessage = "Not a valid phone number.")]
        [Display(Name = "Home Phone")]
        public string HomePhone { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [RegularExpression(PHONENUMBERREGEX, ErrorMessage = "Not a valid phone number.")]
        [Display(Name = "Mobile Phone")]
        public string MobilePhone { get; set; }

        [Required]
        [DataType(DataType.Text)]
       // [StringLength(70, ErrorMessage = "{0} must be at max {1} character long.", MinimumLength = 1)]
        [Display(Name = "Emergency Contact Name")]
        public string EmergencyContact { get; set; }

        [Required]
        [RegularExpression(PHONENUMBERREGEX, ErrorMessage = "Not a valid phone number.")]
        [Display(Name = "Emergency Contact Number")]
        public string EmergencyContactNo { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class IsGender : ValidationAttribute
    {
        private const string defaultError = "'{0}' must have at least one element.";
        public IsGender() : base(defaultError) //
        {
        }

        public override bool IsValid(object value)
        {
            int v = (int)value;
            return (v > 0 && v < 2) || false;
        }
    }
}
