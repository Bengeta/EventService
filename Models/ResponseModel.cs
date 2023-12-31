using Analysis.Enums;

namespace Analysis.Models;

public class ResponseModel<T>
{
    public ResultCode ResultCode { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
}