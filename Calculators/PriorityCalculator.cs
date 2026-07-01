using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;

namespace IntelligencePipeline.Calculators
{
    class PriorityCalculator
    {
        public Priority Calculate(Report report)
        {
            string text = report.Description.ToLower();


            // Critical: keywords
            if (ContainsKeyword(text, "missile", "explosion", "attack", "fire"))
            {
                return Priority.Critical;
            }


            // Critical: Radar Speed >= 800
            if (report is RadarReport radar)
            {
                if (radar.Speed >= 800)
                {
                    return Priority.Critical;
                }
            }


            // Critical: Signal with target AND attack
            if (report is SignalReport signal)
            {
                if (signal.Content != null &&
                    ContainsKeyword(text, "attack"))
                {
                    return Priority.Critical;
                }
            }


            // High: keywords
            if (ContainsKeyword(text, "weapon", "suspicious", "border"))
            {
                return Priority.High;
            }


            // High: Drone Altitude < 500
            if (report is DroneReport drone)
            {
                if (drone.Altitude < 500)
                {
                    return Priority.High;
                }
            }


            // High: Radar Speed >= 400
            if (report is RadarReport radar2)
            {
                if (radar2.Speed >= 400)
                {
                    return Priority.High;
                }
            }


            // High: Soldier ConfidenceLevel >= 4 with movement
            if (report is SoldierReport soldier)
            {
                if (soldier.ConfidenceLevel >= 4 &&
                    ContainsKeyword(text, "movement"))
                {
                    return Priority.High;
                }
            }


            // Medium: keywords
            if (ContainsKeyword(text, "movement", "vehicle", "activity"))
            {
                return Priority.Medium;
            }


            // Medium: Radar Speed >= 120
            if (report is RadarReport radar3)
            {
                if (radar3.Speed >= 120)
                {
                    return Priority.Medium;
                }
            }


            // Medium: ReliabilityScore >= 7
            if (report.ReliabilityScore >= 7)
            {
                return Priority.Medium;
            }


            // Default
            return Priority.Low;
        }


        private bool ContainsKeyword(string text, params string[] keywords)
        {
            foreach (string keyword in keywords)
            {
                if (text.Contains(keyword.ToLower()))
                {
                    return true;
                }
            }

            return false;
        }
    }
}