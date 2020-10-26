namespace TweetBook.Controllers.V1
{
    using System;
    using System.Collections.Generic;
    using Contracts.V1;
    using Microsoft.AspNetCore.Mvc;

    using Domain;

    public class PostsController : Controller
    {
        private readonly List<Post> _posts;

        public PostsController()
        {
            _posts = new List<Post>();
            for (var i = 0; i < 5; i++)
            {
                _posts.Add(new Post{ Id = Guid.NewGuid().ToString()});
            }
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_posts);
        }
    }
}
