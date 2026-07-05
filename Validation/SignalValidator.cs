using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Validation;
using IntelligencePipeline.Models.Enums;

namespace IntelligencePipeline.Validation
{
    class SignalValidator : BaseValidator
    {   
        //No magic Numbers!!
        protected override ValidationResult ValidateSpecificFields(Report report)
        {
            //This is after validation that this is the right type
            //Convert the type of the generic Report to specic report for access the attributes
            SignalReport signalReport = (SignalReport)report;

            const double minFrequency = 1.0;
            const double maxFrequency = 3000.0;
            if (!(signalReport.Frequency >= minFrequency && signalReport.Frequency <= maxFrequency))
            {
                return ValidationResult.Failure($"Invalid Frequency: must be between " +
                    $"{minFrequency} and  {maxFrequency}");
            }
            const int minContentLength = 5;
            const int maxContentLength = 1000;
            if (!(signalReport.Content.Length >= minContentLength 
                && signalReport.Content.Length <= maxContentLength))
            {
                return ValidationResult.Failure($"Invalid Content: must be between " +
                    $"{minContentLength} and - {maxContentLength}");
            }
            if (!Enum.IsDefined(typeof(Language) , signalReport.Language))
            {
                return ValidationResult.Failure("Invalid language this is have to be one of the legal langauge");
            }

            const int minSingalStrength = -120;
            const int maxSignalStrength = 0;
            if (!(signalReport.SignalStrength >= minSingalStrength
                && signalReport.SignalStrength <= maxSignalStrength))
            {
                return ValidationResult.Failure($"The signal strength is " +
                    $"invalid have to be between {minSingalStrength} - {maxSignalStrength}");
            }
            return ValidationResult.Success();
          
        }
    }
}