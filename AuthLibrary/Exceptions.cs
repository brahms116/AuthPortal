using System.Runtime.Serialization;

namespace AuthLibrary;


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

public class LoginFailureException : Exception
{
    public LoginFailureException()
    {
    }

    public LoginFailureException(string? message) : base(message)
    {
    }

    public LoginFailureException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected LoginFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}

public class SignUpFailureException : Exception
{
    public SignUpFailureException()
    {
    }

    public SignUpFailureException(string? message) : base(message)
    {
    }

    public SignUpFailureException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected SignUpFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}

public class ChangePasswordFailureException : Exception
{
    public ChangePasswordFailureException()
    {
    }

    public ChangePasswordFailureException(string? message) : base(message)
    {
    }

    public ChangePasswordFailureException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected ChangePasswordFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}

public class ForgotPasswordFailureException : Exception
{
    public ForgotPasswordFailureException()
    {
    }

    public ForgotPasswordFailureException(string? message) : base(message)
    {
    }

    public ForgotPasswordFailureException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected ForgotPasswordFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}

public class ConfirmForgotPasswordFailureException : Exception
{
    public ConfirmForgotPasswordFailureException()
    {
    }

    public ConfirmForgotPasswordFailureException(string? message) : base(message)
    {
    }

    public ConfirmForgotPasswordFailureException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected ConfirmForgotPasswordFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}


public class PasswordValidationException : Exception
{
    public PasswordValidationException()
    {
    }

    public PasswordValidationException(string? message) : base(message)
    {
    }

    public PasswordValidationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected PasswordValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
