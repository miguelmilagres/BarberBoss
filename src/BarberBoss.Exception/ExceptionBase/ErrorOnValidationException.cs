namespace BarberBoss.Exception.ExceptionBase;
public class ErrorOnValidationException : BarberBossException
{
    public List<string> Errors { get; set; }

    public ErrorOnValidationException(List<string> errorMessages)
    {
        Errors = errorMessages;
    }
}
