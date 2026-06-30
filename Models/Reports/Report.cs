using IntelligencePipeline.Models.Enums;

namespace IntelligencePipeline.Models.Reports
{
    class Report
    {
        private int _reportId;
        private DateTime _timestamp;
        private double _latitude;
        private double _longitude;
        private string _description;
        private ReportStatus _status;
        private Priority _priority;
        private Classification _classification;
        private int _realiabiltyScore;
        private string _rejectionReason;

        int ReportId { get; } //read only
        DateTime Timestamp { get => _timestamp; set
            {
                if (value> DateTime.Now)
                {
                    throw new ArgumentException("You cant put date that never happend!");
                        // throws error on future timedate | EXTRA VALIDATION!
                }
                else
                {
                    _timestamp = value;
                }
                } 
        }

        double Latitude { get => _latitude; set
            {
                if (!(value< 29.5000 || value > 33.5000))
                {
                    throw new ArgumentException("The latitude is invalid ");
                    // EXTRA VALIDATION NEED TO BE ALREADY VALIDATED BEFORE
                }
                else { _latitude = value; }
            } }
        string Description { get => _description; set
            {
                if (!(value.Length <10 || value.Length > 500))
                {
                    throw new ArgumentException("The description is too short or too long");
                    // EXTRA VALIDATION
                }
                else
                {
                    _description = value;
                }
            } }
        ReportStatus Status { get => _status; set
            {
                if (!(Enum.IsDefined(typeof(ReportStatus), value)))
                {
                    throw new ArgumentException("Not valid satatus!");
                    //EXTRA VALIDATION
                }
                else
                {
                    _status = value;
                }
            } }
        Priority Priority { get => _priority; set
            {
                if (!(Enum.IsDefined(typeof(Priority), value)))
                {
                    throw new ArgumentException("Not Valid Priority!");
                    //EXTRA VALIDATION
                }
                else
                {
                    _priority = value;
                }
            } }
    }
}