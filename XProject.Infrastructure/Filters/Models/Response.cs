namespace XProject.Infrastructure.Filters.Models
{
    public class Response
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }

        public Response() { }

        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }

    }
}
