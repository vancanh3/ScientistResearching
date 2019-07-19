using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Data;
using TweetBook.Domain;

namespace TweetBook.Services
{
    public class Posts : IPost
    {
        public DataContext _context;

        public Posts(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Post post)
        {
            _context.Posts.Add(post);
            var s = await _context.SaveChangesAsync();
            return s > 0;
        }

        public async Task<Post> GetOne(Guid id)
        {
            return await _context.Posts.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            return await _context.Posts.ToListAsync();
        }
    }
}
