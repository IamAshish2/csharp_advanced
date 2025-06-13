namespace interview_prep.Responses;

public class OperationalResult<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public List<string> Errors { get; set; } = new List<string>();

    public static OperationalResult<T> SuccessResult(T data) => 
        new() { Success = true, Data = data };

    public static OperationalResult<T> FailureResult(string[] errors) => 
        new() { Success = false, Errors = errors.ToList()};
}


public class OperationalResult
{
    public bool Success { get; set; }
    public List<string> Message { get; set; } = new List<string>();
    public List<string> Errors { get; set; } = new List<string>();
    public static OperationalResult SuccessResult(string [] messages) => new() { Success = true, Message = messages.ToList()};
    public static OperationalResult FailureResult(string[] errors) => new() { Success = false, Errors = errors.ToList() };
    public static OperationalResult FailureResult(string error) => new() { Success = false, Errors = new List<string> { error } }; // Overload for single error
}
