namespace DucksNet.SharedKernel.Utils;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public List<string> Errors { get; protected set; }

    protected Result(bool isSuccess, List<string>? errors)
    {
        if (isSuccess && errors != null)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && errors == null)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Errors = errors ?? new List<string>();
    }

    public static Result Ok() => new(true, null);
    public static Result Error(string error) => new(false, new List<string> { error });
    public static Result ErrorList(List<string> errors) => new(false, errors);
    public void AddError(string error)
    {
        if( IsSuccess || Errors == null)
        {
            throw new InvalidOperationException();
        }
        
        Errors.Add(error);
    }
}
