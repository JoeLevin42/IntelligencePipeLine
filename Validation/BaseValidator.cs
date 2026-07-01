using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Models.Validation;

namespace IntelligencePipelineValidation
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
                return ValidationResult.Failure("Invalid latitude");
            }


            if (report.Longitude < 34.0000 || report.Longitude > 36.0000)
            {
                return ValidationResult.Failure("Invalid longitude");
            }


            if (string.IsNullOrEmpty(report.Description) ||
                report.Description.Length < 10 ||
                report.Description.Length > 500)
            {
                return ValidationResult.Failure("Invalid description");
            }


            return ValidationResult.Success();
        }


        protected abstract ValidationResult ValidateSpecificFields(Report report);
    }
}