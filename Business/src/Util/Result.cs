namespace VetAppointment.Util;


// TODO(AL): make this less bad
public class Result {
    public string Error{ get; private set;}
    public bool IsSuccess { get; private set;}
    public bool IsFailure { get; private set;}



    public static Result Success() {
        return new Result() { IsSuccess = true, IsFailure = false };
    }

    public static Result Failure(string Error) {
        return new Result() { Error = Error, IsFailure = true, IsSuccess = false };
    }
}

public class Result<EntityT> {
    public EntityT Entity { get; private set; }
    public string Error{ get; private set;}
    public bool IsSuccess { get; private set;}
    public bool IsFailure { get; private set;}

    public static Result<EntityT> Success(EntityT ok) {
        return new Result<EntityT>() { Entity = ok, IsSuccess = true, IsFailure = false };
    }

    public static Result<EntityT> Failure(string Error) {
        return new Result<EntityT>() { Error = Error, IsFailure = true, IsSuccess = false };
    }
}