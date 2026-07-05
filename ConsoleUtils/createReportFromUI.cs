using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Pipeline;
using IntelligencePipeline.ConsoleUtils;
using IntelligencePipeline.Validation;
using IntelligencePipeline;
using System;

namespace IntelligencePipeline.ConsoleUtils
{
    static class ReportCreator
    {
        //Sending the report to the pipeline after the creation
        public static void AddReport(ReportPipeline reportPipeline)
        {
            ConsoleUI.ShowAddReportMenu();

            string type = Console.ReadLine();

            if (type == "0")
                return;

            Report report = CreateReport(type);

            if (report == null)
            {
                Console.WriteLine("Invalid report type");
                return;
            }

            reportPipeline.ProcessReport(report);
            ConsoleUI.ShowProcessedReport(report);
        }

            static Report CreateReport(string type)
            {   //Getting first the common fields

                DateTime timestamp = InputValidator.GetDateTime("Timestamp: ");
                double latitude = InputValidator.GetDouble("Latitude: ");
                double longitude = InputValidator.GetDouble("Longitude: ");

                Console.Write("Description: ");
                string description = Console.ReadLine();
                //Small menu to choose the specific type of the reports with the specific fields

                if (type == "1")
                    return new DroneReport(timestamp, latitude, longitude, description,
                        InputValidator.GetInt("Altitude: "),
                        InputValidator.GetInt("Image Quality: "));

                if (type == "2")
                    return new RadarReport(timestamp, latitude, longitude, description,
                        InputValidator.GetInt("Speed: "),
                        InputValidator.GetInt("Direction: "),
                        InputValidator.GetInt("Distance: "));

                if (type == "3")
                {
                    Console.Write("Content: ");
                    return new SignalReport(timestamp, latitude, longitude, description,
                        InputValidator.GetDouble("Frequency: "),
                        Console.ReadLine(),
                        InputValidator.GetLanguage("Language: "),
                        InputValidator.GetInt("Signal Strength: "));
                }
                if (type == "4")
                {
                    Console.Write("Soldier Name: ");
                    string soldierName = Console.ReadLine();

                    Console.Write("Soldier ID: ");
                    string soldierId = Console.ReadLine();

                    Console.Write("Unit: ");
                    string unit = Console.ReadLine();

                    return new SoldierReport(timestamp, latitude, longitude, description,
                        soldierName,
                        soldierId,
                        unit,
                        InputValidator.GetInt("Confidence: "));
                }

                return null;
            }
    }
}