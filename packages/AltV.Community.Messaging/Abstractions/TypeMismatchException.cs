namespace AltV.Community.Messaging.Abstractions;

public class TypeMismatchException : Exception
{
    public TypeMismatchException()
        : base() { }

    public TypeMismatchException(string message)
        : base(message) { }

    public TypeMismatchException(string message, Exception inner)
        : base(message, inner) { }
}
