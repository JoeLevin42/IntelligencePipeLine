using IntelligencePipeline.Models.Reports;

class RadarReport : Report
{
    private int _speed;
    private int _direction;
    private int _distance;

    public int Speed
    {
        get => _speed;
        protected set { _speed = value; }
    }

    public int Direction
    {
        get => _direction;
        protected set { _direction = value; }

    }

    public int Distance { get => _distance;
    protected set { _distance = value; }
    }

    public RadarReport(DateTime timestamp, double latitude, double longitude, string description, int speed , int direction, int distance)
        : base(timestamp, latitude, longitude, description)
    {
        Speed = speed;
        Direction = direction;
        Distance = distance;
    }

    public override string GetSourceType()
            => "Radar";

    public override int CalculateReliabilityScore()
    {
        const int BASE = 6;
        int realScore = 0;

        if (Distance >= 500 && Distance <= 30000) realScore += 2;
        else if (Distance > 70000) realScore -= 1;

        if (Speed >= 10 && Speed <= 900) realScore += 1;
        else if (Speed > 1500) realScore -= 2;

        realScore += BASE;
        realScore = realScore > 10 ? 10 : realScore; // Clampts the score to 1 - 10 ; 10 is the highest
        realScore = realScore < 1 ? 1 : realScore; // Clampts the score to 1 - 10 ; 1 is the lower

        return realScore;

    }
    public override string GetSummary()
        => $"Report: {ReportId}, Timestamp: {Timestamp}, Latitude: {Latitude}, Longitude: {Longitude}, Description: {Description}, Status: {Status}";

}
