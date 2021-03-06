﻿namespace TweetBook.Contracts.V1.Requests
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UserRegistrationRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
