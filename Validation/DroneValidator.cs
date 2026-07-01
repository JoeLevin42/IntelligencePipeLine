using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Models.Validation;

namespace IntelligencePipeline.Validation
{
    class DroneValidator : BaseValidator
    {
        protected override ValidationResult ValidateSpecificFields(Report report)
        {
            DroneReport droneReport = (DroneReport)report;
            if (!(droneReport.Altitude >= 100 && droneReport.Altitude <= 10000) )
            {
                return ValidationResult.Failure("The Alitutde is not good to high or too low");
            }

            if (!(droneReport.ImageQuality >= 1 && droneReport.ImageQuality <= 100))
            {
                return ValidationResult.Failure("The Image quality is not valid its have to be between 1- 100");  

            }

            return ValidationResult.Success();
        }
    }
}