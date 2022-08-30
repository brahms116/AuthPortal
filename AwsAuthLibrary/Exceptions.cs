using System.Runtime.Serialization;

namespace AwsAuthLibrary;


public class UnknownException : Exception
{
    public UnknownException()
    {
    }

    public UnknownException(string? message) : base(message)
    {
    }

    public UnknownException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected UnknownException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}

public class NewPasswordNeededException : Exception
{
    public NewPasswordNeededException()
    {
    }

    public NewPasswordNeededException(string? message) : base(message)
    {
    }

    public NewPasswordNeededException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected NewPasswordNeededException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
public class UnknownAuthChallengeException : Exception
{
    public UnknownAuthChallengeException()
    {
    }

    public UnknownAuthChallengeException(string? message) : base(message)
    {
    }

    public UnknownAuthChallengeException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected UnknownAuthChallengeException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
