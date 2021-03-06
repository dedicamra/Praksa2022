namespace BandAPI.Helpers
{
    public class BandResourceParameters
    {
        public string MainGenre { get; set; }
        public string SearchQuery { get; set; }
        
        //pagination
        const int maxPageSize = 13;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 13;
        public int PageSize
        {
            get =>  _pageSize; 
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value; 
        }

        public string OrderBy { get; set; } = "Name";
        public string Fields { get; set; }
    }
}
