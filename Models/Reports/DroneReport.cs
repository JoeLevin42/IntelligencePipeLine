using IntelligencePipeline.Models.Reports;

namespace IntelligencePipeline.Models.Reports {
class DroneReport : Report
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

        DroneReport(DateTime timeStamp, double latitude, double longitude, string description, int altitude, int imageQuality)
            : base(timeStamp, latitude, longitude, description)
        {
            Altitude = altitude;
            ImageQuality = imageQuality;
        }


        public override string GetSourceType()
            => "Drone";

        public override int CalculateReliabilityScore()
        {
            const int BASE = 5;
            int realScore = 0;

            if (ImageQuality >= 80) realScore += 3;
            else if (ImageQuality >= 50) realScore += 2;

            if (Altitude >= 500 && Altitude <= 3000) realScore += 2;
            else if (Altitude > 7000) realScore -= 2;

            realScore += BASE;
            realScore = realScore > 10 ? 10 : realScore; // Clampts the score to 1 - 10 ; 10 is the highest
            realScore = realScore < 1 ? 1 : realScore; // Clampts the score to 1 - 10 ; 11 is the lower

            return realScore;
        }
        public override string GetSummary()
            => $"Report: {ReportId}, Timestamp: {Timestamp}, Latitude: {Latitude}, Longitude: {Longitude}, Description: {Description}, Status: {Status}";

    }
}