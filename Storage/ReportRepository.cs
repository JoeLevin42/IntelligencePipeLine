using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Models.Enums;

namespace IntelligencePipeline.Repositories
{
    class ReportRepository
    {
            private List<Report> _reports;

            public List<Report> Reports {get => _reports; }

            public ReportRepository()
        {
            _reports = new List<Report>();
        }

        public void Add(Report report)
        {
            _reports.Add(report);
        }

        public List<Report> GetAll()
        {
            return _reports.ToList();
        }

        public List<Report> GetByStatus(ReportStatus status)
        {
            List<Report> result = new List<Report>();
            
            foreach (Report report in _reports)
            {
                if (report.Status == status)
                {
                    result.Add(report);
                }

            }
            return result;
        }

        public List<Report> GetByPriority(Priority priority)
        {
            List<Report> result = new List<Report>();

            foreach (Report report in _reports)
            {
                if (report.Priority == priority)
                {
                    result.Add(report);
                }
            }
            return result;
        }

        public List<Report> Search(string keyword)
        {
            List<Report> result = new List<Report>();

            foreach (Report report in _reports)
            {
                if (report.Description.Contains(keyword))
                {
                    result.Add(report);
                }
            }
            return result;
        }

        public Report GetById(int reportId)
        {
            foreach (Report report in _reports)
            {
                if (report.ReportId == reportId)
                {
                    return report;
                }
            }
            return null; //No id mybe can be handle better
        }

        public void UpdateStatus(int reportId, ReportStatus newStatus)
        {
            Report report = GetById(reportId);

            if (report != null)
            {
                report.Status = newStatus;
            }
        }
        public int GetCountByStatus(ReportStatus status)
        {
            int count = 0;

            foreach (Report report in _reports)
            {
                if (report.Status == status)
                {
                    count++;
                }
            }

            return count;
        }

        public int GetTotalCount()
        {
            return _reports.Count;
        }
    }
}