namespace TweetBook.Controllers.V1
{
    using System;
    using System.Collections.Generic;
    using Contracts.V1;
    using Contracts.V1.Requests;
    using Contracts.V1.Responses;
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

        // TODO: should have a contract
        [HttpPost(ApiRoutes.Posts.Create)]
        public IActionResult Create([FromBody] CreatePostRequest postRequest)
        {
            var post = new Post {Id = postRequest.Id};

            if (string.IsNullOrEmpty(post.Id))
            {
                post.Id = Guid.NewGuid().ToString();
            }

            _posts.Add(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = $"{baseUrl}/{ApiRoutes.Posts.Get.Replace("{postId}", post.Id)}";

            var postResponse = new PostResponse {Id = post.Id};

            return Created(locationUri, postResponse);
        }
    }
}
