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
                return ValidationResult.Failure("Timestamp is too old cant be before (2020, 1, 1");
            }
            //Check if not in the range 
            const double minLatitude = 29.5000;
            const double maxLatitude = 33.5000;
            if (report.Latitude < minLatitude
                || report.Latitude > maxLatitude)
            {
                return ValidationResult.Failure($"Invalid Speed: must be between " +
                    $"{minLatitude} and - value out of range - {maxLatitude}");
            }

            const double minLongitude = 34.0000;
            const double maxLongitude = 36.0000;
            if (report.Longitude < minLongitude
                || report.Longitude > maxLongitude)
            {
                return ValidationResult.Failure($"Invalid Speed: must be between " +
                    $"{minLongitude} and - value out of range - {maxLongitude}");

            }

            const int minDescLen = 10;
            const int maxDescLen = 500;
            if (string.IsNullOrWhiteSpace(report.Description) ||
            report.Description.Length < minDescLen ||
            report.Description.Length > maxDescLen)
            {
                return ValidationResult.Failure($"Invalid Speed: must be between " +
                    $"{minDescLen} and - value out of range - {maxDescLen}");
            }


            return ValidationResult.Success();
        }


        protected abstract ValidationResult ValidateSpecificFields(Report report);
    }
}