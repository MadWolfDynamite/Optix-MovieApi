namespace Optix.API.DTOs
{
    public class MovieSearchResult<T>
    {
        public int Count { get; set; }
        public T? Data { get; set; }

        public MovieSearchResult(int count, T? data)
        {
            Count = count;
            Data = data;
        }
    }
}
