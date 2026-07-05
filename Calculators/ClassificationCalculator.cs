using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;

namespace IntelligencePipeline.Calculators
{
    class ClassificationCalculator
    {
        public Classification Calculate(Report report)
        {
            string description = report.Description?.ToLower() ?? "";

            // ================= TOP SECRET =================

            if (report.Priority == Priority.Critical)
            {
                return Classification.TopSecret;
            }

            // Top Secret: Signal Content contains target / attack / missile
            if (report is SignalReport signal)
            {
                string content = signal.Content?.ToLower() ?? "";

                if (ContainsKeyword(content, "target", "attack", "missile"))
                {
                    return Classification.TopSecret;
                }
            }

            // ================= SECRET =================

            if (report.Priority == Priority.High)
            {
                return Classification.Secret;
            }

            if (report is SignalReport)
            {
                return Classification.Secret;
            }

            if (ContainsKeyword(description, "weapon", "border"))
            {
                return Classification.Secret;
            }

            // ================= RESTRICTED =================

            if (report.Priority == Priority.Medium)
            {
                return Classification.Restricted;
            }

            if (report is SoldierReport)
            {
                return Classification.Restricted;
            }

            // ================= UNCLASSIFIED =================

            return Classification.Unclassified;
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