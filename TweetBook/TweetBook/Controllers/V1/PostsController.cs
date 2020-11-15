namespace TweetBook.Controllers.V1
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Mvc;

    using Contracts.V1;
    using Contracts.V1.Requests;
    using Contracts.V1.Responses;
    using Domain;
    using Services;

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostsController : Controller
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
        {
            var post = new Post { Name = postRequest.Name };

            await _postService.CreatePostAsync(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = $"{baseUrl}/{ApiRoutes.Posts.Get.Replace("{postId}", post.Id.ToString())}";

            var postResponse = new PostResponse { Id = post.Id };

            return Created(locationUri, postResponse);
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _postService.GetPostsAsync());
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public async Task<IActionResult> Get([FromRoute]Guid postId)
        {
            var post = await _postService.GetPostByIdAsync(postId);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpPut(ApiRoutes.Posts.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid postId, [FromBody] UpdatePostRequest request)
        {
            var post = new Post
            {
                Id = postId,
                Name = request.Name
            };

            var updated = await _postService.UpdatePostAsync(post);

            if (updated)
            {
                return Ok(post);
            }

            return NotFound();
        }

        [HttpDelete(ApiRoutes.Posts.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid postId)
        {
            var deleted = await _postService.DeletePostAsync(postId);

            if (deleted)
            {
                // Can return Ok()/200, or NoContent()/204 (which makes sense)
                return NoContent();
            }

            return NotFound();
        }
    }
}
