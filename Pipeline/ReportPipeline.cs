using IntelligencePipeline.Calculators;
using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Statistics;
using IntelligencePipeline.Storage;
using IntelligencePipeline.Validation;
using IntelligencePipeline.reportCreation;
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

        // Valid reports - going to the repository
        //Invalid reports going to the rjected repository

        // ---ProcessReport (For creation of new report and process data)
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
        //Declare on report that he rejected and adding him to the rejected repository
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
        //Calculate all the calculations in the program
        private void CalculateMetrics(Report report)
        {
            report.ReliabilityScore = _reliabilityCalculator.Calculate(report);
            report.Priority = _priorityCalculator.Calculate(report);
            report.Classification = _classificationCalculator.Calculate(report);
        }

        // Get Methods:

        //Takes all the valid repots from the Valid-Repository
        public List<Report> GetValidatedReports()
        {
            return _reportRepository.GetAll();
        }

        //Takes all the invalid (rejected) report form the rejected-repository
        public List<Report> GetRejectedReports()
        {
            return _rejectedRepository.GetAll();
        }
        //Return the report by id (null if not found)
        public Report GetReportById(int reportId)
        {
            return _reportRepository.GetById(reportId);
        }

        // Search method : (searching only valid reports from desciprtion)

        // Searches validated reports by description keyword
        public List<Report> Search(string keyword)
        {
            return _reportRepository.Search(keyword);
        }

        // Filters Methods:

        // Returns all reports with the selected priority
        public List<Report> FilterByPriority(Priority priority)
        {
            return _reportRepository.GetByPriority(priority);
        }

        // Returns all reports with the selected status.
        public List<Report> FilterByStatus(ReportStatus status)
        {
            return _reportRepository.GetByStatus(status);
        }

        // Returns all reports by order of the date time, giving as param (from date - to date)
        public List<Report> FilterByDateRange(DateTime fromDate, DateTime toDate)
        {
            List<Report> result = new List<Report>();
            List<Report> allReports = _reportRepository.GetAll();

            for (int index = 0; index < allReports.Count; index++)
            {
                if (allReports[index].Timestamp >= fromDate &&
                    allReports[index].Timestamp <= toDate)
                {
                    result.Add(allReports[index]);
                }
            }

            return result; // List of the dates after filtering
        }

        // Returns all reports with the selected classification
        public List<Report> FilterByClassification(Classification classification)
        {
            List<Report> result = new List<Report>();

            List<Report> reports = _reportRepository.GetAll();

            for (int index = 0; index < reports.Count; index++)
            {
                if (reports[index].Classification == classification)
                {
                    result.Add(reports[index]);
                }
            }

            return result; // list of the reports after filtering
        }

        // Returns all reports of the selected source type
        public List<Report> FilterBySourceType(Type sourceType)
        {
            List<Report> result = new List<Report>();

            List<Report> reports = _reportRepository.GetAll();

            for (int index = 0; index < reports.Count; index++)
            {
                if (reports[index].GetType() == sourceType)
                {
                    result.Add(reports[index]);
                }
            }

            return result;
        }

        // Sort Methods:

        // Sorting the repots by time stamp from older to new
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

            return list; // The sorted by time reports list
        }
        //This method is giving weight to the priority , For sorting the priority
        //form the Higher to lower (Helper fucniton)
        
        private int GetPriorityWeight(Priority priority)
        {
            return (int)priority;
        }

        // Sorting the reports by priority (numeric values up)
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

            return list; //The sorted priority list from (Critical -> Low) (by numeric values)
        }

        //Sorting the reports by realidabilty number
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

            return list; // The sorted reports list after sorting by realiabilty
        }



        // Update status Method:

        //This method can update the status of the report (Bool)
        public bool UpdateReportStatus(int reportId, ReportStatus newStatus)
        {
            Report report = _reportRepository.GetById(reportId);

            if (report == null)
            {
                return false; //Flag that the update status failed
            }

            _reportRepository.UpdateStatus(reportId, newStatus);

            return true; // Flag that the update status happend
        }

        // Get Statistics method

        public PipelineStatistics GetStatistics()
        {
            _statistics.Calculate(
                _reportRepository.GetAll(),
                _rejectedRepository.GetAll()
            );

            return _statistics; // returns the calculations
        }
    }
}