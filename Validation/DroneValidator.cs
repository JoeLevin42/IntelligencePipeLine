using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Validation;

namespace IntelligencePipeline.Validation
{
    class DroneValidator : BaseValidator
    {
        protected override ValidationResult ValidateSpecificFields(Report report)
        {
            DroneReport droneReport = (DroneReport)report;

            const int minAltitude = 100;
            const int maxAltitude = 10000;
            if (!(droneReport.Altitude >= minAltitude
                && droneReport.Altitude <= maxAltitude) )
            {
                return ValidationResult.Failure($"Invalid Speed: must be between " +
                    $"{minAltitude} and - {maxAltitude}");
            }

            const int minQuality = 1;
            const int maxQuality = 100;
            if (!(droneReport.ImageQuality >= 1 && droneReport.ImageQuality <= 100))
            {
                return ValidationResult.Failure($"Invalid Speed: must be between " +
                    $"{minQuality} and - {maxQuality}");

            }

            return ValidationResult.Success();
        }
    }
}