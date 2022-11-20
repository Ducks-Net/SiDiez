namespace DucksNet.SharedKernel.Utils;

public class Result<T> : Result
{
    public T? Value { get; }

    protected internal Result(T? value, bool isSuccess, string? error) : base(isSuccess, error)
    {
        if (isSuccess && value == null)
        {
            throw new InvalidOperationException();
        }
        
        if (!isSuccess && error == null)
        {
            throw new InvalidOperationException();
        }
        
        if(IsSuccess && value != null) {
            Value = value;
        }
    }

    public static Result<T> Ok(T value) => new(value, true, null);

    public new static Result<T> Error(string error) => new(default(T), false, error);
}
