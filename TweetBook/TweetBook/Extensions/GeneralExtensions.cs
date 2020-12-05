namespace TweetBook.Extensions
{
    using System.Linq;
    using Microsoft.AspNetCore.Http;

    public static class GeneralExtensions
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            return httpContext.User?.Claims.Single(c => c.Type == "id").Value 
                   ?? string.Empty;
        }
    }
}
