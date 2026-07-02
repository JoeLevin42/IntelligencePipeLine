using IntelligencePipeline.Calculators;
using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Statistics;
using IntelligencePipeline.Storage;
using IntelligencePipeline.Validation;
using System;


namespace IntelligencePipeline.Pipeline
{
    class ReportPipeline
    {
        private ReportRepository _reportRepository;
        private RejectedReportRepository _rejectedRepository;

        private ReliabilityCalculator _reliabilityCalculator;
        private PriorityCalculator _priorityCalculator;
        private ClassificationCalculator _classificationCalculator;

        private PipelineStatistics _statistics;

        public ReportPipeline()
        {
            _reportRepository = new ReportRepository();
            _rejectedRepository = new RejectedReportRepository();

            _reliabilityCalculator = new ReliabilityCalculator();
            _priorityCalculator = new PriorityCalculator();
            _classificationCalculator = new ClassificationCalculator();

            _statistics = new PipelineStatistics();
        }

        // ================= CORE =================

        public void ProcessReport(Report report)
        {
            report.Status = ReportStatus.Validating;

            IValidator validator = GetValidator(report);

            if (validator == null)
            {
                Reject(report, "No validator found");
                return;
            }

            ValidationResult result = validator.Validate(report);

            if (!result.IsValid)
            {
                Reject(report, result.ErrorMessage);
                return;
            }

            report.Status = ReportStatus.Validated;

            CalculateMetrics(report);

            _reportRepository.Add(report);
        }

        private void Reject(Report report, string reason)
        {
            report.Status = ReportStatus.Rejected;
            report.RejectionReason = reason;

            _rejectedRepository.Add(report);
        }

        private IValidator GetValidator(Report report)
        {
            if (report is DroneReport) return new DroneValidator();
            if (report is RadarReport) return new RadarValidator();
            if (report is SignalReport) return new SignalValidator();
            if (report is SoldierReport) return new SoldierValidator();

            return null;
        }

        private void CalculateMetrics(Report report)
        {
            report.ReliabilityScore = _reliabilityCalculator.Calculate(report);
            report.Priority = _priorityCalculator.Calculate(report);
            report.Classification = _classificationCalculator.Calculate(report);
        }

        // ================= DATA =================

        public List<Report> GetValidatedReports()
        {
            return _reportRepository.GetAll();
        }

        public List<Report> GetRejectedReports()
        {
            return _rejectedRepository.GetAll();
        }
        public Report GetReportById(int reportId)
        {
            return _reportRepository.GetById(reportId);
        }

        // ================= SEARCH =================

        public List<Report> Search(string keyword)
        {
            List<Report> result = new List<Report>();
            List<Report> allReports = _reportRepository.GetAll();

            for (int index = 0; index < allReports.Count; index++)
            {
                if (allReports[index].Description != null &&
                    allReports[index].Description.ToLower().Contains(keyword.ToLower()))
                {
                    result.Add(allReports[index]);
                }
            }

            return result;
        }

        // ================= FILTERS =================

        public List<Report> FilterByPriority(Priority priority)
        {
            List<Report> result = new List<Report>();
            List<Report> allReports = _reportRepository.GetAll();

            for (int index = 0; index < allReports.Count; index++)
            {
                if (allReports[index].Priority == priority)
                    result.Add(allReports[index]);
            }

            return result;
        }

        public List<Report> FilterByStatus(ReportStatus status)
        {
            List<Report> result = new List<Report>();
            List<Report> allReports = _reportRepository.GetAll();

            for (int index = 0; index < allReports.Count; index++)
            {
                if (allReports[index].Status == status)
                    result.Add(allReports[index]);
            }

            return result;
        }

        public List<Report> FilterByDateRange(DateTime from, DateTime to)
        {
            List<Report> result = new List<Report>();
            List<Report> allReports = _reportRepository.GetAll();

            for (int index = 0; index < allReports.Count; index++)
            {
                if (allReports[index].Timestamp >= from &&
                    allReports[index].Timestamp <= to)
                {
                    result.Add(allReports[index]);
                }
            }

            return result;
        }

        // ================= SORT =================

        public List<Report> SortByTimestamp()
        {
            List<Report> list = _reportRepository.GetAll();

            for (int outer = 0; outer < list.Count - 1; outer++)
            {
                for (int inner = outer + 1; inner < list.Count; inner++)
                {
                    if (list[outer].Timestamp > list[inner].Timestamp)
                    {
                        Report temp = list[outer];
                        list[outer] = list[inner];
                        list[inner] = temp;
                    }
                }
            }

            return list;
        }

        private int GetPriorityWeight(Priority priority)
        {
            if (priority == Priority.Critical) return 4;
            if (priority == Priority.High) return 3;
            if (priority == Priority.Medium) return 2;
            return 1;
        }

        public List<Report> SortByPriority()
        {
            List<Report> list = _reportRepository.GetAll();

            for (int outer = 0; outer < list.Count - 1; outer++)
            {
                for (int inner = outer + 1; inner < list.Count; inner++)
                {
                    if (GetPriorityWeight(list[outer].Priority) <
                        GetPriorityWeight(list[inner].Priority))
                    {
                        Report temp = list[outer];
                        list[outer] = list[inner];
                        list[inner] = temp;
                    }
                }
            }

            return list;
        }

        public List<Report> SortByReliability()
        {
            List<Report> list = _reportRepository.GetAll();

            for (int outerIndex = 0; outerIndex < list.Count - 1; outerIndex++)
            {
                for (int innerIndex = outerIndex + 1; innerIndex < list.Count; innerIndex++)
                {
                    if (list[outerIndex].ReliabilityScore < list[innerIndex].ReliabilityScore)
                    {
                        Report temp = list[outerIndex];
                        list[outerIndex] = list[innerIndex];
                        list[innerIndex] = temp;
                    }
                }
            }

            return list;
        }



        // ================= UPDATE STATUS =================

        public bool UpdateReportStatus(int reportId, ReportStatus newStatus)
        {
            Report report = _reportRepository.GetById(reportId);

            if (report == null)
            {
                return false;
            }

            _reportRepository.UpdateStatus(reportId, newStatus);

            return true;
        }

        // ================= STATISTICS =================

        public PipelineStatistics GetStatistics()
        {
            _statistics.Calculate(
                _reportRepository.GetAll(),
                _rejectedRepository.GetAll()
            );

            return _statistics;
        }
    }
}