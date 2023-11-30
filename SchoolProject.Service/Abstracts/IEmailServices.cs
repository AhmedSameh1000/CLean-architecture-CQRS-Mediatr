namespace SchoolProject.Service.Abstracts
{
    public interface IEmailServices
    {
        Task<bool> SendEmail(string email, string Message, string Subject);
    }
}