

namespace N5Permission.Application.Result
{
    public class Response<T>
    {
        
        public Response()
        {
            this.Succeeded = true;
        }
        public Response(T data, string? message = null)
        {
            Succeeded = true; 
            Message = message ?? string.Empty; 
            Data = data; 
        }
        public Response(string message)
        {
            Succeeded = false;
            Message = message; 
        }
        public bool Succeeded { get; set; }
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }
        public T Data { get; set; }
    }
}
