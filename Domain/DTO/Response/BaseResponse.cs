namespace Domain.DTO.Response
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }

    public class BaseResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }

        public T Value { get; set; }
    }
}
