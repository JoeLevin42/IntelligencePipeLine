using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Pipeline;

using System;
using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Pipeline;

namespace IntelligencePipeline
{
    class Program
    {
        public static void Main(string[] args)
        {
            ReportPipeline pipeline = new ReportPipeline();

            while (true)
            {
                Console.WriteLine("\n=== CREATE REPORT ===");
                Console.WriteLine("1 - Drone");
                Console.WriteLine("2 - Radar");
                Console.WriteLine("3 - Signal");
                Console.WriteLine("4 - Soldier");
                Console.WriteLine("0 - Exit");
                Console.Write("Choose type: ");

                string choice = Console.ReadLine();

                if (choice == "0")
                    break;
            

                Report report = CreateReport(choice);

                if (report == null)
                {
                    Console.WriteLine("Invalid selection");
                    continue;
                }

                pipeline.ProcessReport(report);

                Console.WriteLine("Report processed!\n");

                pipeline.DisplayStatistics();
            }
        }

        private static Report CreateReport(string choice)
        {

            Console.Write("Pleae ");
        }
    }
}