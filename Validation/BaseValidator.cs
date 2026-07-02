using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Validation;

namespace IntelligencePipeline.Validation
{
    public abstract class BaseValidator : IValidator
    {
        public ValidationResult Validate(Report report)
        {
            ValidationResult commonResult = ValidateCommonFields(report);

            if (!commonResult.IsValid)
            {
                return commonResult;
            }
            
            return ValidateSpecificFields(report);
        }


        protected ValidationResult ValidateCommonFields(Report report)
        {
            if (report.Timestamp > DateTime.Now)
            {
                return ValidationResult.Failure("Timestamp cannot be in the future");
            }

            if (report.Timestamp < new DateTime(2020, 1, 1))
            {
                return ValidationResult.Failure("Timestamp is too old");
            }


            if (report.Latitude < 29.5000 || report.Latitude > 33.5000)
            {
                return ValidationResult.Failure("Invalid Latitude: must be between 29.500 and 33.500");
            }


            if (report.Longitude < 34.0000 || report.Longitude > 36.0000)
            {
                return ValidationResult.Failure("Invalid longitude: must be between 34.00 and 36.00");
            }


            if (string.IsNullOrEmpty(report.Description) ||
                report.Description.Length < 10 ||
                report.Description.Length > 500)
            {
                return ValidationResult.Failure("Invalid description have to conatin at least 10 - 500 chars");
            }


            return ValidationResult.Success();
        }


        protected abstract ValidationResult ValidateSpecificFields(Report report);
    }
}