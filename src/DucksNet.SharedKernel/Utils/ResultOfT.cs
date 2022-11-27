namespace DucksNet.SharedKernel.Utils;

public class Result<T> : Result
{
    public T? Value { get; }

    protected internal Result(T? value, bool isSuccess, List<string>? errors) : base(isSuccess, errors)
    {       
        Value = value;
    }

    public static Result<T> Ok(T value) => new(value, true, null);

    public new static Result<T> Error(string error) => new(default(T), false, new List<string>{error});

    public new static Result<T> ErrorList(List<string> errors) => new(default(T), false, errors);

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
