namespace Pagination.WebApi.Pagination.Models
{
    public class PaginationModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PaginationModel()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public PaginationModel(int pageNumber,int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}
