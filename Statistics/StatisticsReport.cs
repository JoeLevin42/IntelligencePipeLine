using System;
using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
using System.Collections.Generic;

namespace IntelligencePipeline.Statistics
{
    public class PipelineStatistics
    {
        public int ValidatedCount;
        public int RejectedCount;

        public int DroneCount;
        public int RadarCount;
        public int SignalCount;
        public int SoldierCount;

        public int LowPriorityCount;
        public int MediumPriorityCount;
        public int HighPriorityCount;
        public int CriticalPriorityCount;

        public void Reset()
        {
            ValidatedCount = 0;
            RejectedCount = 0;

            DroneCount = 0;
            RadarCount = 0;
            SignalCount = 0;
            SoldierCount = 0;

            LowPriorityCount = 0;
            MediumPriorityCount = 0;
            HighPriorityCount = 0;
            CriticalPriorityCount = 0;
        }

        public void Calculate(List<Report> validatedReports, List<Report> rejectedReports)
        {
            Reset();

            // VALIDATED
            for (int index = 0; index < validatedReports.Count; index++)
            {
                Report report = validatedReports[index];

                ValidatedCount++;

                if (report is DroneReport) DroneCount++;
                if (report is RadarReport) RadarCount++;
                if (report is SignalReport) SignalCount++;
                if (report is SoldierReport) SoldierCount++;

                if (report.Priority == Priority.Low) LowPriorityCount++;
                if (report.Priority == Priority.Medium) MediumPriorityCount++;
                if (report.Priority == Priority.High) HighPriorityCount++;
                if (report.Priority == Priority.Critical) CriticalPriorityCount++;
            }

            // REJECTED
            for (int index = 0; index < rejectedReports.Count; index++)
            {
                RejectedCount++;
            }
        }

        public double GetSuccessRate()
        {
            int total = ValidatedCount + RejectedCount;

            if (total == 0)
                return 0;

            return (double)ValidatedCount * 100 / total;
        }
    }
}