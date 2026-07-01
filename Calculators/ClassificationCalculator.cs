using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;

namespace IntelligencePipeline.Calculators
{
    class ClassificationCalculator
    {
        public Classification Calculate(Report report)
        {
            string description = report.Description.ToLower();


            // TOP SECRET

            if (report.Priority == Priority.Critical)
            {
                return Classification.TopSecret;
            }


            if (ContainsKeyword(description, "target", "attack", "missile"))
            {
                return Classification.TopSecret;
            }

            // SECRET
         
            if (report.Priority == Priority.High)
            {
                return Classification.Secret;
            }


            if (report.GetSourceType() == "Signal")
            {
                return Classification.Secret;
            }


            if (ContainsKeyword(description, "weapon", "border"))
            {
                return Classification.Secret;
            }

            // RESTRICTED
         

            if (report.Priority == Priority.Medium)
            {
                return Classification.Restricted;
            }


            if (report.GetSourceType() == "Soldier")
            {
                return Classification.Restricted;
            }

            // DEFAULT
            

            return Classification.Unclassified;
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