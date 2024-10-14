namespace Domain.Models.Systems;

public class Result<T>
{
    public bool IsSuccess { get; set; }
    public int? Code { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }

    public static Result<T> Success(T data, int? code = null, string? message = null)
    {
        return new Result<T> { IsSuccess = true, Data = data, Code = code, Message = message };
    }

    public static Result<T> Failure(int code, string? message = null)
    {
        return new Result<T> { IsSuccess = false, Code = code, Message = message };
    }
}