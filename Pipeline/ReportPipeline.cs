using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Storage;
using IntelligencePipeline.Validation;
using IntelligencePipeline.Calculators;

namespace IntelligencePipeline.Pipeline
{
    class ReportPipeline
    {
        private ReportRepository _reportRepository;
        private RejectedReportRepository _rejectedRepository;

        private ReliabilityCalculator _reliabilityCalculator;
        private PriorityCalculator _priorityCalculator;
        private ClassificationCalculator _classificationCalculator;

        public ReportPipeline()
        {
            _reportRepository = new ReportRepository();
            _rejectedRepository = new RejectedReportRepository();

            _reliabilityCalculator = new ReliabilityCalculator();
            _priorityCalculator = new PriorityCalculator();
            _classificationCalculator = new ClassificationCalculator();
        }

        public void ProcessReport(Report report)
        {
            report.Status = ReportStatus.Validating;

            IValidator validator = GetValidator(report);

            // if no validator found reject
            if (validator == null)
            {
                report.Status = ReportStatus.Rejected;
                report.RejectionReason = "No validator found for report type";

                _rejectedRepository.Add(report);
                return;
            }

            ValidationResult result = validator.Validate(report);

            if (!result.IsValid)
            {
                report.Status = ReportStatus.Rejected;
                report.RejectionReason = result.ErrorMessage;

                _rejectedRepository.Add(report);
                return;
            }

            report.Status = ReportStatus.Validated;

            CalculateMetrics(report);

            _reportRepository.Add(report);
        }

        private IValidator GetValidator(Report report)
        {
            if (report is DroneReport)
                return new DroneValidator();

            if (report is RadarReport)
                return new RadarValidator();

            if (report is SignalReport)
                return new SignalValidator();

            if (report is SoldierReport)
                return new SoldierValidator();

            return null;
        }

        private void CalculateMetrics(Report report)
        {
            report.ReliabilityScore = _reliabilityCalculator.Calculate(report);
            report.Priority = _priorityCalculator.Calculate(report);
            report.Classification = _classificationCalculator.Calculate(report);
        }

        public ReportRepository GetValidatedReports()
        {
            return _reportRepository;
        }

        public RejectedReportRepository GetRejectedReports()
        {
            return _rejectedRepository;
        }

        public void DisplayStatistics()
        {
            Console.WriteLine("=== Pipeline Statistics ===");
            Console.WriteLine($"Validated: {_reportRepository.GetTotalCount()}");
            Console.WriteLine($"Rejected: {_rejectedRepository.GetTotalCount()}");

        }
    }
}