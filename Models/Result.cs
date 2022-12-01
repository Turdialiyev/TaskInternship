namespace Task.Models;

public class Result
{


    public bool IsSuccess { get; set; }
    public string? ErorrMessage { get; set; }

    public Result(bool isSuccess) => IsSuccess = isSuccess;
    public Result(string errorMessage) => ErorrMessage = errorMessage;
    public Result(bool isSuccess, string errorMessage)
    {
        IsSuccess = isSuccess;
        ErorrMessage = errorMessage; 
    }
}

public class Result<T> : Result
{

    public T? Data { get; set; }
    public Result(bool isSuccess) : base(isSuccess) { }

    public Result(string errorMessage) : base(errorMessage) { }

    public Result(bool isSuccess, string errorMessage) : base(isSuccess, errorMessage) { }

}