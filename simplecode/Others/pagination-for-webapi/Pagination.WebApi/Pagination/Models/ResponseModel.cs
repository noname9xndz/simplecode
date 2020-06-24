namespace Pagination.WebApi.Pagination.Models
{
    public class ResponseModel<T>
    {
        public ResponseModel()
        {
        }
        public ResponseModel(T data)
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
        }
        public T Data { get; set; }
        public bool Succeeded { get; set; }

        public string[] Errors { get; set; }
        public string Message { get; set; }
    }
}
