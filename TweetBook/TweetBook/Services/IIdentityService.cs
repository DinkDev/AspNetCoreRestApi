namespace TweetBook.Services
{
    using System.Threading.Tasks;
    using Domain;

    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string requestEmail, string requestPassword);
        Task<AuthenticationResult> LoginAsync(string email, string password);
    }
}
