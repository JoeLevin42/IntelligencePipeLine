using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Validation;
using IntelligencePipeline.Models.Enums;

namespace IntelligencePipeline.Validation
{
    class SignalValidator : BaseValidator
    {
        protected override ValidationResult ValidateSpecificFields(Report report)
        {
            //This is after validation that this is the right type
            //Convert the type of the generic Report to specic report for access the attributes
            SignalReport signalReport = (SignalReport)report;

            if (!(signalReport.Frequency >= 1.0 && signalReport.Frequency <= 3000.0))
            {
                return ValidationResult.Failure("The Freqeuncy is invalid have to be between 1.0 - 3000.0");
            }
            if (!(signalReport.Content.Length >= 5 && signalReport.Content.Length <= 1000))
            {
                return ValidationResult.Failure("The content is invalid have to be between 5 - 1000 chars");
            }
            if (!Enum.IsDefined(typeof(Language) , signalReport.Language))
            {
                return ValidationResult.Failure("Invalid language this is have to be one of the legal langauge");
            }

            if (!(signalReport.SignalStrength >= -120 && signalReport.SignalStrength <= 0))
            {
                return ValidationResult.Failure("The signal strength is invalid have to be between -120 - 0");
            }
            return ValidationResult.Success();
          
        }
    }
}