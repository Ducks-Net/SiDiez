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

    public new static Result<T> ErrorList(List<string> errors) => new(default(T), false, null) { Errors = errors };

    public static Result<T> FromError<U>(Result<U> result, string? extraError = null) {
        if(result.IsSuccess || result.Errors == null) {
            throw new InvalidOperationException();
        }

        Result<T> ret = Result<T>.ErrorList(result.Errors);
        if(extraError != null) {
            ret.Errors.Add(extraError);
        }

        return ret;
    }
}
