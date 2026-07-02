using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Validation;

namespace IntelligencePipeline.Validation
{
    class SoldierValidator : BaseValidator
    {
        protected override ValidationResult ValidateSpecificFields(Report report)
        {   
            //This is after validation that this is the right type
            //Convert the type of the generic Report to specic report for access the attributes
            SoldierReport soldierReport = (SoldierReport)report;
            if (!(soldierReport.SoldierName.Length >=2 && soldierReport.SoldierName.Length <= 50))
            {
                return ValidationResult.Failure("The name of the soldier is too short or too long");
            }
            if (soldierReport.SoldierId.ToString().Length!= 7)
            {
                return ValidationResult.Failure("The ID is not valid its not have exacly 7 digits!");
            }
            if (!(soldierReport.ConfidenceLevel >= 1 && soldierReport.ConfidenceLevel <=5))
            {
                return ValidationResult.Failure("The ConfidenceLevel is Invalid");
            }
            //if all the validation went good this is will success
            return ValidationResult.Success();
        }
    }
}