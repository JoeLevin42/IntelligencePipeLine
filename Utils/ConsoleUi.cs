using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Statistics;
using System;
using System.Collections.Generic;

namespace IntelligencePipeline.Utils
{
    public static class ConsoleUI
    {
        public static void ShowMainMenu()
        {
            Console.Clear();

            Console.WriteLine($"\n===== INTELLIGENCE PIPELINE SYSTEM =====");
            Console.WriteLine($"1. Add Report");
            Console.WriteLine($"2. Show Validated Reports");
            Console.WriteLine($"3. Search Reports");
            Console.WriteLine($"4. Filter by Priority");
            Console.WriteLine($"5. Filter by Status");
            Console.WriteLine($"6. Filter by Date Range");

            Console.WriteLine($"7. Sort by Timestamp");
            Console.WriteLine($"8. Sort by Priority ");
            Console.WriteLine($"9. Sort by Reliability Score");

            Console.WriteLine($"10. Update Report Status");

            Console.WriteLine($"11. Show Rejected Reports");
            Console.WriteLine($"12. Statistics");
            Console.WriteLine("13. GetReportById");

            Console.WriteLine($"0. Exit");
            Console.WriteLine($"=========================================");
        }

        public static void ShowAddReportMenu()
        {
            Console.WriteLine($"\n=== ADD REPORT ===");
            Console.WriteLine($"1. Drone Report");
            Console.WriteLine($"2. Radar Report");
            Console.WriteLine($"3. Signal Report");
            Console.WriteLine($"4. Soldier Report");
            Console.WriteLine($"0. Back");
        }

        public static void PrintReports(List<Report> reports)
        {
            Console.WriteLine("\n===== REPORT LIST =====");

            if (reports.Count == 0)
            {
                Console.WriteLine("No reports found.");
                return;
            }

            for (int reportIndex = 0; reportIndex < reports.Count; reportIndex++)
            {
                Console.WriteLine("----------------------------------");
                Console.WriteLine(reports[reportIndex]);
            }

            Console.WriteLine("----------------------------------");
        }

        public static void PrintReport(Report report)
        {
            if (report == null)
            {
                Console.WriteLine("Report not found.");
                return;
            }

            Console.WriteLine(report);
        }
        public static void ShowProcessedReport(Report report)
        {
            Console.WriteLine($"\n=== REPORT PROCESSED ===");
            Console.WriteLine($"Status: {report.Status}");

            if (report.Status == ReportStatus.Rejected)
            {
                Console.WriteLine($"Rejection Reason: {report.RejectionReason}");
            }
        }

        public static void ShowStatistics(PipelineStatistics stats)
        {
            Console.WriteLine($"\n===== PIPELINE STATISTICS =====");

            Console.WriteLine($"Validated Reports: {stats.ValidatedCount}");
            Console.WriteLine($"Rejected Reports: {stats.RejectedCount}");

            Console.WriteLine($"\n--- Source Types ---");
            Console.WriteLine($"Drone: {stats.DroneCount}");
            Console.WriteLine($"Radar: {stats.RadarCount}");
            Console.WriteLine($"Signal: {stats.SignalCount}");
            Console.WriteLine($"Soldier: {stats.SoldierCount}");

            Console.WriteLine($"\n--- Priority ---");
            Console.WriteLine($"Low: {stats.LowPriorityCount}");
            Console.WriteLine($"Medium: {stats.MediumPriorityCount}");
            Console.WriteLine($"High: {stats.HighPriorityCount}");
            Console.WriteLine($"Critical: {stats.CriticalPriorityCount}");

            Console.WriteLine($"\nSuccess Rate: {stats.GetSuccessRate()}%");
        }
    }
}