

using IntelligencePipeline.Models.Validations;
using IntelligencePipeline.Models.Reports;


namespace IntelligencePipeline.Models.Validation
{
    public interface IValidator
    {
        ValidationResult Validate(Report report);
    }
}