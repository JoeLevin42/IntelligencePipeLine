using IntelligencePipeline.Models.Enums;

namespace IntelligencePipeline.Models.Reports
{

    public abstract class Report
    {
        private static int _nextReportId = 1;
        
      
        private int _reportId;

        private DateTime _timestamp;
        private double _latitude;
        private double _longitude;
        private string _description;
        private ReportStatus _status;
        private Priority _priority;
        private Classification _classification;
        private int _reliabilityScore;
        private string _rejectionReason;
        

        public int ReportId { get => _reportId; } 
        public DateTime Timestamp { get => _timestamp; 
           set { _timestamp = value; }
        }
            
            
        

        
        public double Latitude { get => _latitude; 
            
                 set { _latitude = value; }
        }

        public double Longitude { get => _longitude;
             set { _longitude = value; }
        }
        public string Description { get => _description;
             set { _description = value; }
        }
            
              
        public ReportStatus Status { get => _status;
                     set { _status = value; }
        }
            
        public Priority Priority { get => _priority; 
                  set { _priority = value; }
        }
            
        public Classification Classification
        {
            get => _classification; 
              set { _classification = value; }
        }
        

        public int ReliabilityScore { get => _reliabilityScore;
             set { _reliabilityScore = value; }
        }
        public string? RejectionReason { get => _rejectionReason; set{
                _rejectionReason = value;

            } }

      
        
        protected Report(DateTime timeStamp, double latitude, double longitude, string description)
        {
            _reportId = _nextReportId++;
            Timestamp = timeStamp;
            Latitude = latitude;
            Longitude = longitude;
            Description = description;
            Status = ReportStatus.New;
           
        }

        public abstract string GetSourceType();
        public abstract int CalculateReliabilityScore();

        public virtual string GetSummary()
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
                $"Reliability Score: {ReliabilityScore}";
        }


    }   
}