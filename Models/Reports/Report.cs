using IntelligencePipeline.Models.Enums;

namespace IntelligencePipeline.Models.Reports
{

    abstract class Report
    {
        private static int _nextReportId = 1;
        
      
        private int _reportId;

        private DateTime _timestamp;
        private double _latitude;
        private double _longitude;
        private string _description;
        private ReportStatus _status;
        private Priority? _priority;
        private Classification? _classification;
        private int _reliabilityScore;
        private string _rejectionReason;
        

        public int ReportId { get => _reportId; } 
        public DateTime Timestamp { get => _timestamp; 
          protected set { _timestamp = value; }
        }
            
            
        

        
        public double Latitude { get => _latitude; 
            
                 protected set { _latitude = value; }
        }

        public double Longitude { get => _longitude;
            protected set { _longitude = value; }
        }
        public string Description { get => _description;
            protected set { _description = value; }
        }
            
              
        public ReportStatus Status { get => _status;
                     protected set { _status = value; }
        }
            
        public Priority? Priority { get => _priority; 
                 protected set { _priority = value; }
        }
            
        public Classification? Classification
        {
            get => _classification; 
             protected set { _classification = value; }
        }
        

        public int ReliabilityScore { get => _reliabilityScore;
            protected set { _reliabilityScore = value; }
        }
        public string? RejectionReason { get => _rejectionReason; protected set{
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
            Priority = null; // starts with null and only in the pipeline its will be implemented
            Classification = null; // starts with null and only in the pipeline its will be implemented
            RejectionReason = null;
        }

        public abstract string GetSourceType();
        public abstract int CalculateReliabilityScore();

        public virtual string GetSummary()
        => $"Report: {ReportId}, Timestamp: {Timestamp}, Latitude: {Latitude}, Longitude: {Longitude}, Description: {Description}, Status: {Status}";
        public override string ToString()
         => $"Report: {ReportId}, Timestamp: {Timestamp}, Latitude: {Latitude}, Longitude: {Longitude}, Description: {Description}, Status: {Status}";



    }   
}