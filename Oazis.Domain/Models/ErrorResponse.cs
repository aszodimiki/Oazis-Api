namespace Oazis.Domain.Models
{
    public class ErrorResponse
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}
