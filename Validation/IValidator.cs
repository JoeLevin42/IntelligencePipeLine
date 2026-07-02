

using IntelligencePipeline.Validation;
using IntelligencePipeline.Models.Reports;


namespace IntelligencePipeline.Validation
{
    public interface IValidator
    {
        ValidationResult Validate(Report report);
    }
}