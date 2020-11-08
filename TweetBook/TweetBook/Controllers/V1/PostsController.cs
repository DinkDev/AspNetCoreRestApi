﻿namespace TweetBook.Controllers.V1
{
    using System;
    using Microsoft.AspNetCore.Mvc;

    using Contracts.V1;
    using Contracts.V1.Requests;
    using Contracts.V1.Responses;

    using Domain;
    using Services;

    public class PostsController : Controller
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public IActionResult Create([FromBody] CreatePostRequest postRequest)
        {
            var post = new Post { Id = postRequest.Id };

            if (post.Id != Guid.Empty)
            {
                post.Id = Guid.NewGuid();
            }

            // TODO: fix this hack
            _postService.GetPosts().Add(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = $"{baseUrl}/{ApiRoutes.Posts.Get.Replace("{postId}", post.Id.ToString())}";

            var postResponse = new PostResponse { Id = post.Id };

            return Created(locationUri, postResponse);
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_postService.GetPosts());
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public IActionResult Get([FromRoute]Guid postId)
        {
            var post = _postService.GetPostById(postId);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpPut(ApiRoutes.Posts.Update)]
        public IActionResult Update([FromRoute] Guid postId, [FromBody] UpdatePostRequest request)
        {
            var post = new Post
            {
                Id = postId,
                Name = request.Name
            };

            var updated = _postService.UpdatePost(post);

            if (updated)
            {
                return Ok(post);
            }

            return NotFound();
        }

        // TODO: Delete will go here
        [HttpDelete(ApiRoutes.Posts.Delete)]
        public IActionResult Delete([FromRoute] Guid postId)
        {
            var deleted = _postService.DeletePost(postId);

            if (deleted)
            {
                // Can return Ok()/200, or NoContent()
                return NoContent();
            }

            return NotFound();
        }
    }
}
