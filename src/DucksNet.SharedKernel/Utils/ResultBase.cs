namespace DucksNet.SharedKernel.Utils;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public List<string> Errors { get; }

    protected Result(bool isSuccess, string? error)
    {
        if (isSuccess && error != null)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == null)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Errors = new List<string>();
        if (error != null)
        {
            Errors.Add(error);
        }
    }

    public static Result Ok() => new(true, null);
    public static Result Error(string error) => new(false, error);
    public void AddError(string error)
    {
        if( IsSuccess || Errors == null)
        {
            throw new InvalidOperationException();
        }
        
        Errors.Add(error);
    }
}
