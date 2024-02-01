namespace Optix.Domain.Services.Communication
{
    public class ServiceResponse<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public T? Data { get; set; }

        protected ServiceResponse(bool isSuccess, string message, T? data)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }

        public ServiceResponse(T data) : this(true, string.Empty, data) { }
        public ServiceResponse(string message) : this(false, message, null) { }
        public ServiceResponse(Exception ex) : this(ex.Message) { }
    }
}
