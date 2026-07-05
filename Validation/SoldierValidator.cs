using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Validation;

namespace IntelligencePipeline.Validation
{
    class SoldierValidator : BaseValidator
    {
        protected override ValidationResult ValidateSpecificFields(Report report)
        {   
            //No magic numebers!!
            //This is after validation that this is the right type
            //Convert the type of the generic Report to specic report for access the attributes
            SoldierReport soldierReport = (SoldierReport)report;

            const int minNameLen = 2;
            const int maxNameLen = 50;
            if (!(soldierReport.SoldierName.Length >= minNameLen
                && soldierReport.SoldierName.Length <= maxNameLen))
            {
                return ValidationResult.Failure($"Invalid Speed: must be between " +
                    $"{minNameLen} and - value out of range - {maxNameLen}");
            }
            const int idLen = 7;
            if (soldierReport.SoldierId.ToString().Length!= idLen)
            {
                return ValidationResult.Failure($"The ID is not valid its not have exacly {idLen} digits!");
            }
            const int minConfidenceLevel = 1;
            const int maxConfidenceLevel = 5;
            if (!(soldierReport.ConfidenceLevel >= 1 && soldierReport.ConfidenceLevel <=5))
            {
                return ValidationResult.Failure($"Invalid Speed: must be between " +
                    $"{minConfidenceLevel} and - value out of range - {maxConfidenceLevel}");
            }
            //if all the validation went good this is will success
            return ValidationResult.Success();
        }
    }
}