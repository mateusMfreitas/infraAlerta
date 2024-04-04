namespace infraAlerta.Helper
{
    public interface IEmail
    {
        bool sendEmail(string email, string subject, string message);
    }
}
