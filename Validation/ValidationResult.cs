namespace IntelligencePipeline.Validation
{
    public class ValidationResult
    {   
        //This method is taking the validatons result 
        //if valid = true , Errmsg = null
        //if invalid = false , errmsg = the error reason
        public bool IsValid { get; }
        public string ErrorMessage { get; }

        private ValidationResult(bool isValid, string errorMessage)
        {
            IsValid = isValid;
            ErrorMessage = errorMessage;
        }

        public static ValidationResult Success()
            => new ValidationResult(true, string.Empty);

        public static ValidationResult Failure(string errorMessage)
            => new ValidationResult(false, errorMessage);
    }
}