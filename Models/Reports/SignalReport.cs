
using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
using System.Collections;
using System.Reflection;

namespace IntelligencePipeline.Models.Reports
{
    class SignalReport: Report
    {
        private double _frequency;
        private string _content;
        private Language _language;
        private int _signalStrength;

        public double Frequency { get => _frequency;
            protected set { _frequency = value; }
        }

        public string Content { get => _content;
            protected set { _content = value; }
        }

        public Language Language { get => _language;
            protected set { _language = value; }
        }

        public int SignalStrength { get => _signalStrength;
            protected set { _signalStrength = value; }
        }

        public SignalReport(DateTime timeStamp, double latitude, double longitude, string description,
            double frequency, string content, Language language, int signalStrength)
            :base(timeStamp, latitude, longitude, description)
        {
            Frequency = frequency;
            Content = content;
            Language = language;
            SignalStrength = signalStrength;
        }

        public override string GetSourceType()
            => "Signal";
        public override int CalculateReliabilityScore()
        {
            string[] dangerousWords = { "attack", "target", "border", "vehicle" };
            const int BASE = 5;
            int realScore = 0 ;


            if (SignalStrength >= -40) realScore += 3;
            else if (SignalStrength >= -70) realScore += 2; 
            else if (SignalStrength < -100) realScore -= 2;

            if (dangerousWords.Any(word => Content.Contains(word, StringComparison.OrdinalIgnoreCase))){
                realScore += 1;
            }

            realScore += BASE;
            realScore = realScore > 10 ? 10 : realScore; // Clampts the score to 1 - 10 ; 10 is the highest
            realScore = realScore < 1 ? 1 : realScore; // Clampts the score to 1 - 10 ; 1 is the lower
            return realScore;
         }
        public override string GetSummary()
          => $"Report: {ReportId}, Timestamp: {Timestamp}, Latitude: {Latitude}, Longitude: {Longitude}, Description: {Description}, Status: {Status}";

    }
}