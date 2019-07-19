using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TweetBook.Contracts;
using TweetBook.Contracts.V1.Requests;
using TweetBook.Contracts.V1.Responses;
using TweetBook.Data;
using TweetBook.Domain;
using TweetBook.Extensions;
using TweetBook.Services;

namespace TweetBook.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostController : Controller
    {
        private List<Post> _post;

        private IPost await_iPost;
       
        public PostController(IPost iPost)
        {
            await_iPost = iPost;
            _post = new List<Post>();
            for (int i = 0; i < 5; i++)
            {
                _post.Add(new Post { Id = Guid.NewGuid(), Name = $"Post Name: {i}" });
            }
        }
        [HttpGet(ApiRoutes.Posts.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await await_iPost.GetPostsAsync());
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid postId)
        {
            var post = await await_iPost.GetOne(postId);
            return Ok(post);
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
        {
            var post = new Post
            {
                Id = Guid.NewGuid(),
                Name = postRequest.Name,
                UserId = HttpContext.GetUserId()

     
            };

            if (string.IsNullOrEmpty(post.Id.ToString()))
            {
                post.Id = Guid.NewGuid();
            }
            var s = await await_iPost.CreateAsync(post);

            var response = new PostResponse { Id = post.Id };
            Task<List<Post>> sd = await_iPost.GetPostsAsync();
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}", post.Id.ToString());
            return Created(locationUri, response);
            
        }
        
    }
}