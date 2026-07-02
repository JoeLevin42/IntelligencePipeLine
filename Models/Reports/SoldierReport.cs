
using IntelligencePipeline.Models.Reports;

namespace IntelligencePipeline.Models.Reports { 
    class SoldierReport : Report
    {
        private string _soldierName;
        private string _soldierId;
        private string _unit;
        private int _confidenceLevel;

        public string SoldierName
        {
            get => _soldierName;
            protected set { _soldierName = value; }
        }

        public string SoldierId { get => _soldierId;
            protected set { _soldierId = value; }
        }
        public string Unit { get => _unit;
        protected set { _unit = value; }
        }
        public int ConfidenceLevel { get => _confidenceLevel;
        protected set { _confidenceLevel = value; }
        }

        public SoldierReport(DateTime timeStamp, double latitude, double longitude, string description, string soldierName, string soldierId, string unit,int confidenceLevel)
            : base(timeStamp, latitude, longitude, description)
        {
            SoldierName = soldierName;
            SoldierId = soldierId;
            Unit = unit;
            ConfidenceLevel = confidenceLevel;
        }

        public override string GetSourceType()
       => "Soldier";

        public override int CalculateReliabilityScore()
        {
            const int BASE = 4;
            int realScore = 0;

            string[] dangerousWords = { "weapon", "vechile", "movment", "explotion" };
            if (dangerousWords.Any(word => Description.Contains(word , StringComparison.OrdinalIgnoreCase)))
            {
                realScore += 1;
            }
            realScore += ConfidenceLevel + BASE;
            realScore = realScore > 10 ? 10 : realScore; // Clampts the score to 1 - 10 ; 10 is the highest
            realScore = realScore < 1 ? 1 : realScore; // Clampts the score to 1 - 10 ; 1 is the lower

            return realScore;
        }
        public override string GetSummary()
            => $"Report: {ReportId}, Timestamp: {Timestamp}, Latitude: {Latitude}, Longitude: {Longitude}, Description: {Description}, Status: {Status}";

        public override string ToString()
        {
            return
                $"ID: {ReportId}\n" +
                $"Type: {GetType().Name}\n" +
                $"Timestamp: {Timestamp}\n" +
                $"Latitude: {Latitude}\n" +
                $"Longitude: {Longitude}\n" +
                $"Description: {Description}\n" +
                $"Status: {Status}\n" +
                $"Priority: {Priority}\n" +
                $"Classification: {Classification}\n" +
                $"Reliability Score: {ReliabilityScore}\n" +
                $"SoldierName : {SoldierName}\n" +
                $"SoldierId : {SoldierId}\n" +
                $"Unit: {Unit}\n" +
                $"ConfidenceLevel: {ConfidenceLevel}";
        }

    }



}





