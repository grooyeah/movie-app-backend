namespace movie_app_backend.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}