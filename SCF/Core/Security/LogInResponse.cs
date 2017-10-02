namespace Peyton.Core.Security
{
    public class LogInResponse
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public LogInResponse()
        {
            Result = false;
            Message = string.Empty;
        }

        public LogInResponse(string message) : this()
        {
            Message = message;
        }
    }
}