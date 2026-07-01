
using IntelligencePipeline.Models.Reports;


namespace IntelligencePipeline.Calculators
{
    class ReliabilityCalculator
    {
        public int Calculate(Report report)
        {
            int score = report.CalculateReliabilityScore();

            return score;
        }
    }
}