using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Domain;

namespace TweetBook.Services
{
    public interface IPost
    {
         Task<List<Post>> GetPostsAsync();
         Task<bool> CreateAsync(Post post);

        Task<Post> GetOne(Guid id);

    }
}
