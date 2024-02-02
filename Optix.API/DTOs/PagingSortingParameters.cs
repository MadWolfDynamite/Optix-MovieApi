namespace Optix.API.DTOs
{
    public class PagingSortingParameters
    {
        public int Page { get; set; } = 1;
        public int ItemsPerPage { get; set; } = 25;

        public string Sort {  get; set; } = string.Empty;
        public string Order { get; set; } = "ASC";
    }
}
