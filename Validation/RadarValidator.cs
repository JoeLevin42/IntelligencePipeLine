using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Validation;

namespace IntelligencePipeline.Validation
{
    class RadarValidator : BaseValidator
       {   
        protected override ValidationResult ValidateSpecificFields(Report report)
        {   

            //Convert this to the the specific report for access the attributes
            // No magic numbers!!!!
            RadarReport radarReport = (RadarReport)report;

            const int minSpeed = 0;
            const int maxSpeed = 2000;
            if (!(radarReport.Speed >=minSpeed && radarReport.Speed <= maxSpeed))
            {
                return ValidationResult.Failure($"Invalid Speed: must be between " +
                    $"{minSpeed} and - value out of range - {maxSpeed}");
            }
            const int minDirection = 0;
            const int maxDirection = 360;
            if (!(radarReport.Direction >= minDirection && radarReport.Direction <= maxDirection))
            {
                return ValidationResult.Failure($"Invalid Speed: must be between " +
                    $"{minDirection} and - value out of range - {maxDirection}");
            }
            const int minDistance = 100;
            const int maxDistance = 100000;
            if (!(radarReport.Distance >= minDistance && radarReport.Distance <= maxDistance))
            {
                return ValidationResult.Failure($"Invalid Speed: must be between " +
                    $"{minDistance} and - value out of range - {maxDistance}");
            }

            return ValidationResult.Success();

        }
    }
}
