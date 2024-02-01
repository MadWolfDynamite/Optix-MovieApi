namespace Optix.Domain.Services.Communication
{
    public class SearchResponse<T> : ServiceResponse<T> where T : class
    {
        public int Count { get; set; }

        private SearchResponse(bool isSuccess, string message, int count, T? data) : base (isSuccess, message, data)
        {
            Count = count;
        }

        public SearchResponse(int count, T data) : this(true, string.Empty, count, data) { }
        public SearchResponse(string message) : this(false, message, 0, null) { }
        public SearchResponse(Exception ex) : this(ex.Message) { }
    }
}
