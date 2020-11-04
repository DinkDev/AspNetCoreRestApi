namespace TweetBook.Services
{
    using System;
    using System.Collections.Generic;
    using Domain;

    public interface IPostService
    {
        List<Post> GetPosts();

        Post GetPostById(Guid postId);

        bool UpdatePost(Post postToUpdate);
    }
}
