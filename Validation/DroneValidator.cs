using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Models.Validation;

namespace IntelligencePipeline.Validation
{
    class DroneValidator : BaseValidator
    {
        protected override ValidationResult ValidateSpecificFields(Report report)
        {
            const string SOURCE_TYPE = "Drone";
            string reportType = report.GetSourceType();
            if (reportType != SOURCE_TYPE)
            {
                return ValidationResult.Failure($"The type has to be {SOURCE_TYPE} and not: {reportType}");
            }
            DroneReport Dreport = (DroneReport)report;
            if (!(Dreport.Altitude >= 100 && Dreport.Altitude <= 10000))
            {
                return ValidationResult.Failure("The Alitutde is not good to high or too low");
            }




    }
}