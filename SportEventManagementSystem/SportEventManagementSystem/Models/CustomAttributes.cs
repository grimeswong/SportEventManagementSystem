using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SportEventManagementSystem.Models
{
    public class CustomAttributes
    {
        [AttributeUsage(AttributeTargets.Property)]
        public sealed class IsGender : ValidationAttribute
        {
            private const string defaultError = "'{0}' is mandatory.";
            public IsGender() : base(defaultError)
            {
            }

            public override bool IsValid(object value)
            {
                int v = (int)value;
                return (v > 0 && v < 2) || false;
            }
        }

        [AttributeUsage(AttributeTargets.Property)]
        public sealed class IsDateBetween : ValidationAttribute
        {
            public string EventStart { get; private set; }
            public string EventEnd { get; private set; }
            public string ErrorFieldName { get; private set; }
            public IsDateBetween(string defaultError,string eventStartName, string eventEndName,string errorFieldName) : base(defaultError)
            {
                if (string.IsNullOrEmpty(eventStartName))
                {
                    throw new ArgumentNullException("eventStartName");
                }
                if (string.IsNullOrEmpty(eventEndName))
                {
                    throw new ArgumentNullException("eventEndName");
                }
                if(string.IsNullOrEmpty(errorFieldName))
                {
                    throw new ArgumentNullException("errorFieldName");
                }
                EventStart = eventStartName;
                EventEnd = eventEndName;
                ErrorFieldName = errorFieldName;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                DateTime time;
                //If value is of type DateTime
                if (value is DateTime)
                {
                   time = (DateTime)value;

                    if (value != null)
                    {
                        try
                        { // Try to reflect event start and event end dates and do validation
                            var eventStart = validationContext.ObjectInstance.GetType()
                            .GetProperty(EventStart);

                            DateTime eventStartValue = (DateTime)eventStart.GetValue(validationContext.ObjectInstance, null);

                            var eventEnd = validationContext.ObjectInstance.GetType()
                                .GetProperty(EventEnd);

                            DateTime eventEndValue = (DateTime)eventStart.GetValue(validationContext.ObjectInstance, null);

                            if (time.CompareTo(eventStartValue) >= 0)
                            {
                                if (time.CompareTo(eventEndValue) <= 0)
                                {
                                    //Validation successful
                                    return ValidationResult.Success;
                                }
                            }
                            else
                            {
                                return new ValidationResult("Please enter a value for {0} that is after or equal to the Event Start.",new[] { ErrorFieldName });
                            }

                        }
                        catch (InvalidCastException e)
                        { // If the event start / event end strings given to the validation class is wrong this will happen
                            throw e;
                        }
                    }
                    else
                    {
                        return new ValidationResult("Please enter a value for {0}.",new[] { ErrorFieldName });
                    }
                }
                else
                {
                    return new ValidationResult("Invalid date time format for {0}.",new[] { ErrorFieldName });
                }
                //If it gets to here something went very wrong
                return null;
            }
        }

        [AttributeUsage(AttributeTargets.Property)]
        public sealed class IsEndDateAfter : ValidationAttribute
        {
            public string FirstDateField { get; private set; }
            public string ErrorFieldName { get; private set; }
            public IsEndDateAfter(string defaultError,string firstDateField, string errorFieldName) : base(defaultError)
            {
                if (string.IsNullOrEmpty(firstDateField))
                {
                    throw new ArgumentNullException("firstDateField");
                }
                if (string.IsNullOrEmpty(errorFieldName))
                {
                    throw new ArgumentNullException("errorFieldName");
                }
                FirstDateField = firstDateField;
                ErrorFieldName = errorFieldName;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                DateTime time;
                //If value is of type DateTime
                if (value is DateTime)
                {
                    time = (DateTime)value;

                    if (value != null)
                    {
                        try
                        { // Try to reflect event start and event end dates and do validation
                            var firstDate = validationContext.ObjectInstance.GetType()
                            .GetProperty(FirstDateField);

                            DateTime firstDateValue = (DateTime)firstDate.GetValue(validationContext.ObjectInstance, null);

                            if (time.CompareTo(firstDateValue) > 0)
                            {
                                return ValidationResult.Success;
                            }
                            else
                            {
                                return new ValidationResult("Please enter a value for {0} that is after End Date / Time.", new[] { ErrorFieldName });
                            }

                        }
                        catch (InvalidCastException e)
                        { // If the event start / event end strings given to the validation class is wrong this will happen
                            throw e;
                        }
                    }
                    else
                    {
                        return new ValidationResult("Please enter a value for {0}.", new[] { ErrorFieldName });
                    }
                }
                else
                {
                    return new ValidationResult("Invalid date time format for {0}.", new[] { ErrorFieldName });
                }
            }
        }
    }
}
