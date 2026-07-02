using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Validation;

namespace IntelligencePipeline.Validation
{
    class RadarValidator : BaseValidator
       {   
        protected override ValidationResult ValidateSpecificFields(Report report)
        {   

            //Convert this to the the specific report for access the attributes
            RadarReport radarReport = (RadarReport)report;

            if (!(radarReport.Speed >=0 && radarReport.Speed <= 2000))
            {
                return ValidationResult.Failure("The speed is invalid have to be between 0-2000!");
            }
            if (!(radarReport.Direction >= 0 && radarReport.Direction <= 360))
            {
                return ValidationResult.Failure("The Direction is invalid have to be between 0-360");
            }
            if (!(radarReport.Distance >= 100 && radarReport.Distance <= 100000))
            {
                return ValidationResult.Failure("The Distance is invalid have to be between 100 - 100000");
            }

            return ValidationResult.Success();

        }
    }
}
