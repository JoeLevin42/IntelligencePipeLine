using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;

namespace IntelligencePipeline.Calculators
{
    class PriorityCalculator
    {
        public Priority Calculate(Report report)
        {
            string description = report.Description?.ToLower() ?? "";

            // Critical:

            // Critical: global keywords
            if (ContainsKeyword(description, "missile", "explosion", "attack", "fire"))
            {
                return Priority.Critical;
            }

            // Critical: Radar Speed >= 800
            const int criticalSpeed = 800;
            if (report is RadarReport radar)
            {
                if (radar.Speed >= criticalSpeed)
                {
                    return Priority.Critical;
                }
            }

            // Critical: Signal target + attack content
            if (report is SignalReport signal)
            {
                string content = signal.Content?.ToLower() ?? "";

                bool hasTarget = ContainsKeyword(description, "target");
                bool hasAttack = ContainsKeyword(content, "attack");

                if (hasTarget && hasAttack)
                {
                    return Priority.Critical;
                }
            }

            //High:            

            if (ContainsKeyword(description, "weapon", "suspicious", "border"))
            {
                return Priority.High;
            }

            const int hightAltitude = 500;
            if (report is DroneReport drone)
            {
                if (drone.Altitude < hightAltitude)
                {
                    return Priority.High;
                }
            }

            const int highSpeed = 400;
            if (report is RadarReport radar2)
            {
                if (radar2.Speed >= highSpeed)
                {
                    return Priority.High;
                }
            }

            const int highConfidenceLevel = 4;
            if (report is SoldierReport soldier)
            {
                if (soldier.ConfidenceLevel >= highConfidenceLevel &&
                    ContainsKeyword(description, "movement"))
                {
                    return Priority.High;
                }
            }

            // Medium:

            if (ContainsKeyword(description, "movement", "vehicle", "activity"))
            {
                return Priority.Medium;
            }

            const int mediumSpeed = 120;
            if (report is RadarReport radar3)
            {
                if (radar3.Speed >= mediumSpeed)
                {
                    return Priority.Medium;
                }
            }

            // Reliability rule should NOT override higher priority logic
            const int mediumReliabilityScore = 7;
            if (report.ReliabilityScore >= mediumReliabilityScore)
            {
                return Priority.Medium;
            }

            // Low:

            return Priority.Low;
        }

        private bool ContainsKeyword(string text, params string[] keywords)
        {
            foreach (string keyword in keywords)
            {
                if (text.Contains(keyword))
                {
                    return true;
                }
            }

            return false;
        }
    }
}