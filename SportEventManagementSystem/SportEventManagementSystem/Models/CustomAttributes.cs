using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Collections;

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

        public class IsDateBeforeNow : ValidationAttribute
        {
            public IsDateBeforeNow()
            {
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var dt = Convert.ToDateTime(value);
                if (dt < DateTime.Now)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("Please select a valid date.");
            }
        }

        [AttributeUsage(AttributeTargets.Property)]
        public sealed class IsListCountLargerThan : ValidationAttribute
        {
            private const string DefaultError = "'{0}' is mandatory.";
            private readonly int MinimumElements;
            private readonly string BoolField;
            private readonly bool BoolRequiredVal;

            public IsListCountLargerThan(int min, string boolField, bool boolRequiredVal, string defaultError = null) : base(defaultError != null ? defaultError : DefaultError)
            {
                MinimumElements = min;
                if (string.IsNullOrEmpty(boolField))
                {
                    throw new ArgumentNullException("boolField");
                }
                BoolRequiredVal = boolRequiredVal;
                BoolField = boolField;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                    try
                    { // Try to reflect event start and event end dates and do validation
                        var boolField = validationContext.ObjectInstance.GetType()
                        .GetProperty(BoolField);

                        bool boolFieldValue = (bool)boolField.GetValue(validationContext.ObjectInstance, null);

                        var list = value as IList;
                        if (list != null)
                        {
                            if (list.Count >= MinimumElements && boolFieldValue == BoolRequiredVal)
                            {
                                return ValidationResult.Success;
                            }
                            else
                            {
                                return new ValidationResult("Please enter atleast " + MinimumElements + " item/s");
                            }
                        }
                    }
                    catch (InvalidCastException e)
                    { 
                        throw e;
                    }
                return null;
            }
        }

        [AttributeUsage(AttributeTargets.Property)]
        public sealed class IsDateBetweenTwoFields : ValidationAttribute
        {
            public string StartDateField { get; private set; }
            public string EndDateField { get; private set; }
            public string ErrorFieldName { get; private set; }
            public IsDateBetweenTwoFields(string defaultError, string startDateField, string endDateField, string errorFieldName) : base(defaultError)
            {
                if (string.IsNullOrEmpty(startDateField))
                {
                    throw new ArgumentNullException("eventStartName");
                }
                if (string.IsNullOrEmpty(endDateField))
                {
                    throw new ArgumentNullException("eventEndName");
                }
                if (string.IsNullOrEmpty(errorFieldName))
                {
                    throw new ArgumentNullException("errorFieldName");
                }
                StartDateField = startDateField;
                EndDateField = endDateField;
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
                            var startDate = validationContext.ObjectInstance.GetType()
                            .GetProperty(StartDateField);

                            DateTime startDateValue = (DateTime)startDate.GetValue(validationContext.ObjectInstance, null);

                            var endDate = validationContext.ObjectInstance.GetType()
                                .GetProperty(EndDateField);

                            DateTime endDateValue = (DateTime)endDate.GetValue(validationContext.ObjectInstance, null);

                            if (time.CompareTo(startDateValue) >= 0)
                            {
                                if (time.CompareTo(endDateValue) <= 0)
                                {
                                    //Validation successful
                                    return ValidationResult.Success;
                                }
                            }
                            else
                            {
                                return new ValidationResult("Please enter a value for {0} that is after or equal to the Event Start.", new[] { ErrorFieldName });
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
                //If it gets to here something went very wrong
                return null;
            }
        }

        [AttributeUsage(AttributeTargets.Property)]
        public sealed class IsDateBetweenNowAndField : ValidationAttribute
        {
            public string EndTimeField { get; private set; }
            public string ErrorFieldName { get; private set; }
            public IsDateBetweenNowAndField(string defaultError, string endTimeField, string errorFieldName) : base(defaultError)
            {
                if (string.IsNullOrEmpty(endTimeField))
                {
                    throw new ArgumentNullException("endTimeField");
                }
                if (string.IsNullOrEmpty(errorFieldName))
                {
                    throw new ArgumentNullException("errorFieldName");
                }
                EndTimeField = endTimeField;
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
                        { // Try to reflect end time from field name and do validation

                            var endTime = validationContext.ObjectInstance.GetType()
                                .GetProperty(EndTimeField);

                            DateTime eventEndValue = (DateTime)endTime.GetValue(validationContext.ObjectInstance, null);

                            if (time.CompareTo(DateTime.Now) >= 0)
                            {
                                if (time.CompareTo(eventEndValue) <= 0)
                                {
                                    //Validation successful
                                    return ValidationResult.Success;
                                }
                            }
                            else
                            {
                                return new ValidationResult("Please enter a value for {0} that is after or equal to the Event Start.", new[] { ErrorFieldName });
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
                //If it gets to here something went very wrong
                return null;
            }
        }

        [AttributeUsage(AttributeTargets.Property)]
        public sealed class IsDateAfter : ValidationAttribute
        {
            public string FirstDateField { get; private set; }
            public string ErrorFieldName { get; private set; }
            public IsDateAfter(string defaultError, string firstDateField, string errorFieldName) : base(defaultError)
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

        [AttributeUsage(AttributeTargets.Property)]
        public sealed class IsDateBefore : ValidationAttribute
        {
            public string EndDateField { get; private set; }
            public string ErrorFieldName { get; private set; }
            public IsDateBefore(string defaultError, string endDateField, string errorFieldName) : base(defaultError)
            {
                if (string.IsNullOrEmpty(endDateField))
                {
                    throw new ArgumentNullException("endDateField");
                }
                if (string.IsNullOrEmpty(errorFieldName))
                {
                    throw new ArgumentNullException("errorFieldName");
                }
                EndDateField = endDateField;
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
                            var endDate = validationContext.ObjectInstance.GetType()
                            .GetProperty(EndDateField);

                            DateTime firstDateValue = (DateTime)endDate.GetValue(validationContext.ObjectInstance, null);

                            if (time.CompareTo(firstDateValue) < 0)
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
