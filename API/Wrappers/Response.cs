using XProject.Core.CustomEntities;

namespace XProject.API.Wrappers
{
    public class Response<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
        public Metadata Meta { get; set; }

        public Response() { }

        public Response(T data, string message = null)
        {
            Succeeded = true;
            Data = data;
            Message = message;
        }
        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }
        public Response(List<string> errors)
        {
            Succeeded = false;
            Message = "ERROR";
            Errors = errors;
        }
    }
}