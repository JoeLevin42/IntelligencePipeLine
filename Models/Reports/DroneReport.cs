using IntelligencePipeline.Models.Reports;

namespace IntelligencePipeline.Models.Reports {
class DroneReports : Report
    {
        private int _altitude;
        private int _imageQuality;

        public int Altitude
        {
            get => _altitude;
            protected set { _altitude = value; }
        }

        public int ImageQuality
        {
            get => _imageQuality;
            protected set { _imageQuality = value; }
        }

        DroneReports(DateTime timeStamp, double latitude, double longitude, string description, int altitude, int imageQuality)
            : base(timeStamp, latitude, longitude, description)
        {
            Altitude = altitude;
            ImageQuality = imageQuality;
        }


        public override string GetSourceType()
            => "Drone";

        public override int CalculateReliabilityScore()
        {
            return 0; //TODO calculate
        }
        public override string GetSummary()
            => $"Report: {ReportId}, Timestamp: {Timestamp}, Latitude: {Latitude}, Longitude: {Longitude}, Description: {Description}, Status: {Status}";

    }
}