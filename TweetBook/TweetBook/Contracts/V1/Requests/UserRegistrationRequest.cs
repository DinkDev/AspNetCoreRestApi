namespace TweetBook.Contracts.V1.Requests
{
    using System;

    public class UserRegistrationRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
