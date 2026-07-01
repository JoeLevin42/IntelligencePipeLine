
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

        public SoldierReport(DateTime timeStamp, double latitude, double longitude, string description, string soldierName, string soldierId, string unit)
            : base(timeStamp, latitude, longitude, description)
        {
            SoldierName = soldierName;
            SoldierId = soldierId;
            Unit = unit;
        }

        public override string GetSourceType()
       => "Soldier";

        public override int CalculateReliabilityScore()
        {
            return 0; //TODO calculate
        }
        public override string GetSummary()
            => $"Report: {ReportId}, Timestamp: {Timestamp}, Latitude: {Latitude}, Longitude: {Longitude}, Description: {Description}, Status: {Status}";
    }



}





