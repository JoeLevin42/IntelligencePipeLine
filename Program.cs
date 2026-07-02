using System;
using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Pipeline;
using IntelligencePipeline.Validation;

namespace IntelligencePipeline
{
    class Program
    {
        static void Main(string[] args)
        {
            ReportPipeline pipeline = new ReportPipeline();
            bool exit = false;

            while (!exit)
            {
                Console.Clear();

                Console.WriteLine("===== Intelligence Report System =====");
                Console.WriteLine("1. Create Drone Report");
                Console.WriteLine("2. Create Radar Report");
                Console.WriteLine("3. Create Signal Report");
                Console.WriteLine("4. Create Soldier Report");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                string menuChoice = Console.ReadLine();

                if (menuChoice == "0")
                {
                    exit = true;
                    continue;
                }

                Report report = CreateReport(menuChoice);

                if (report == null)
                {
                    Console.WriteLine("Invalid option.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    continue;
                }

                pipeline.ProcessReport(report);

                Console.WriteLine($"Status: {report.Status}");

                if (report.Status == ReportStatus.Rejected)
                {
                    Console.WriteLine($"Reason: {report.RejectionReason}");
                }

                Console.WriteLine("\nReport processed successfully.\n");

                pipeline.DisplayStatistics();

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        private static Report CreateReport(string menuChoice)
        {
            DateTime reportTimestamp = InputValidator.GetDateTime("Timestamp (yyyy-MM-dd HH:mm): ");
            double latitude = InputValidator.GetDouble("Latitude: ");
            double longitude = InputValidator.GetDouble("Longitude: ");

            Console.Write("Description: ");
            string reportDescription = Console.ReadLine();

            switch (menuChoice)
            {
                case "1":
                    {
                        int altitude = InputValidator.GetInt("Altitude: ");
                        int imageQuality = InputValidator.GetInt("Image Quality: ");

                        return new DroneReport(
                            reportTimestamp,
                            latitude,
                            longitude,
                            reportDescription,
                            altitude,
                            imageQuality);
                    }

                case "2":
                    {
                        int speed = InputValidator.GetInt("Speed: ");
                        int direction = InputValidator.GetInt("Direction: ");
                        int distance = InputValidator.GetInt("Distance: ");

                        return new RadarReport(
                            reportTimestamp,
                            latitude,
                            longitude,
                            reportDescription,
                            speed,
                            direction,
                            distance);
                    }

                case "3":
                    {
                        double frequency = InputValidator.GetDouble("Frequency: ");

                        Console.Write("Content: ");
                        string signalContent = Console.ReadLine();

                        int languageValue = InputValidator.GetEnum("Language: ", typeof(Language));
                        Language signalLanguage = (Language)languageValue;

                        int signalStrength = InputValidator.GetInt("Signal Strength: ");

                        return new SignalReport(
                            reportTimestamp,
                            latitude,
                            longitude,
                            reportDescription,
                            frequency,
                            signalContent,
                            signalLanguage,
                            signalStrength);
                    }

                case "4":
                    {
                        Console.Write("Soldier Name: ");
                        string soldierName = Console.ReadLine();

                        Console.Write("Soldier ID: ");
                        string soldierId = Console.ReadLine();

                        Console.Write("Unit: ");
                        string militaryUnit = Console.ReadLine();

                        return new SoldierReport(
                            reportTimestamp,
                            latitude,
                            longitude,
                            reportDescription,
                            soldierName,
                            soldierId,
                            militaryUnit);
                    }

                default:
                    return null;
            }
        }
    }
}