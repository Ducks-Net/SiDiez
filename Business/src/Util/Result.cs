namespace VetAppointment.Util;

public class Result
{
    public string? Error { get; private set; }
    public bool IsSuccess { get; private set; }
    public bool IsFailure { get; private set; }

    private Result(string errorMessage) {
        this.Error = errorMessage;
        this.IsSuccess = false;
        this.IsFailure = true;
    }

    private Result() {
        this.Error = null;
        this.IsSuccess = true;
        this.IsFailure = false;
    }

    public static Result Success()
    {
        return new Result();
    }

    public static Result Failure(string errorMessage)
    {
        return new Result(errorMessage);
    }
}

public class Result<T>
{
    public T? Entity { get; private set; }
    public string? Error { get; private set; }
    public bool IsSuccess { get; private set; }
    public bool IsFailure { get; private set; }

    private Result(T entity)
    {
        this.Entity = entity;
        this.Error = null;
        this.IsSuccess = true;
        this.IsFailure = false;
    }

    //NOTE(AL): the `bool failed` here is basically just a tag to diferentiate between the overloads in case T =
    private Result(string error, bool failed)
    {
        this.Entity = default(T); //FIXME(AL): this is not nice. but I don't want to restrict T to only reference types.
        this.Error = error;
        this.IsSuccess = false;
        this.IsFailure = true;
    }

    public static Result<T> Success(T ok)
    {
        return new Result<T>(ok);
    }

    public static Result<T> Failure(string errorMessage)
    {
        return new Result<T>(errorMessage, true);
    }
}