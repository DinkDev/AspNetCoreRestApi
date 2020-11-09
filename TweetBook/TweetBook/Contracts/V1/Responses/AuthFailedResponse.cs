namespace TweetBook.Contracts.V1.Responses
{
    using System.Collections.Generic;

    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}