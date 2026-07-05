using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Pipeline;
using IntelligencePipeline.Statistics;
using IntelligencePipeline.ConsoleUtils;
using IntelligencePipeline.Validation;
using IntelligencePipeline.ConsoleUtils;
using System;

namespace IntelligencePipeline.ConsoleUtils {
 
    
        class MenuProgram
        {
            public static void Menu()
            {
                ReportPipeline reportPipeline = new ReportPipeline();
                bool isRunning = true;

                while (isRunning)
                {
                    ConsoleUI.ShowMainMenu();

                    Console.Write("Select option: ");
                    string option = Console.ReadLine();

                    if (option == "1")
                    {
                        ReportCreator.AddReport(reportPipeline);
                    }
                    else if (option == "2")
                    {
                        ConsoleUI.PrintReports(reportPipeline.GetValidatedReports());
                    }
                    else if (option == "3")
                    {
                        Console.Write("Enter keyword: ");
                        string keyword = Console.ReadLine();

                        ConsoleUI.PrintReports(reportPipeline.Search(keyword));
                    }
                    else if (option == "4")
                    {
                        Priority priority =
                            InputValidator.GetPriority("Enter priority (Low / Medium / High / Critical): ");

                        ConsoleUI.PrintReports(reportPipeline.FilterByPriority(priority));
                    }
                    else if (option == "5")
                    {
                        ReportStatus status =
                            InputValidator.GetStatus("Enter status: ");

                        ConsoleUI.PrintReports(reportPipeline.FilterByStatus(status));
                    }
                    else if (option == "6")
                    {
                        DateTime from =
                            InputValidator.GetDateTime("From (yyyy-mm-dd HH:mm): ");

                        DateTime to =
                            InputValidator.GetDateTime("To (yyyy-mm-dd HH:mm): ");

                        ConsoleUI.PrintReports(reportPipeline.FilterByDateRange(from, to));
                    }
                    else if (option == "7")
                    {
                        ConsoleUI.PrintReports(reportPipeline.SortByTimestamp());
                    }
                    else if (option == "8")
                    {
                        ConsoleUI.PrintReports(reportPipeline.SortByPriority());
                    }
                    else if (option == "9")
                    {
                        ConsoleUI.PrintReports(reportPipeline.SortByReliability());
                    }
                    else if (option == "10")
                    {
                        int id = InputValidator.GetInt("Enter Report ID: ");
                        ReportStatus status = InputValidator.GetStatus("Enter new status: ");

                        bool updated = reportPipeline.UpdateReportStatus(id, status);

                        Console.WriteLine(updated ? "Updated successfully" : "Report not found");
                    }
                    else if (option == "11")
                    {
                        ConsoleUI.PrintReports(reportPipeline.GetRejectedReports());
                    }
                    else if (option == "12")
                    {
                        PipelineStatistics stats = reportPipeline.GetStatistics();
                        ConsoleUI.ShowStatistics(stats);
                    }
                    else if (option == "13")
                    {
                        int id = InputValidator.GetInt("Enter Report ID: ");

                        Report report = reportPipeline.GetReportById(id);

                        ConsoleUI.PrintReport(report);
                    }
                    else if (option == "0")
                    {
                        isRunning = false;
                    }

                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }

        }
  }
