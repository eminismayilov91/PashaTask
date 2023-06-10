using Entity.Helper;

namespace ApiUI.Model
{
    public class FilteredResponse<T>
    {
        public T Data { get; set; }
        public bool Status { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public static FilteredResponse<T> Succeed(Response<T> response)
        {
            return new FilteredResponse<T>
            {
                Status = response.Status,
                StatusCode = StatusCodes.Status200OK,
                Message = response.Message,
                Data = response.Data
            };
        }

        public static FilteredResponse<T> Failed(Response<T> response)
        {
            return new FilteredResponse<T>
            {
                Status = response.Status,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = response.Message
            };
        }
    }
}
