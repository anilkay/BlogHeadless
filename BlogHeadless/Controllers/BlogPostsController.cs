using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogHeadless.Api.Models.Context;
using BlogHeadless.Api.Models.Ids;
using BlogHeadless.Data.Models.BlogPost;
using AutoMapper;
using BlogHeadless.Data.Dtos;

namespace BlogHeadless.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly BlogDbContext _context;
        private readonly IMapper _mapper;

        public BlogPostsController(BlogDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/BlogPosts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogPostDto>>> GetBlogPosts()
        {
            var blogPosts=await _context.BlogPosts.Include(blog=>blog.Author).ToListAsync();
            return _mapper.Map<List<BlogPostDto>>(blogPosts);
        }

        // GET: api/BlogPosts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPostDto>> GetBlogPost(Guid id)
        {
            var blogPost = await _context.BlogPosts.Include(blog => blog.Author).Where(blog => blog.Id == new BlogPostId(id)).FirstOrDefaultAsync();


            if (blogPost == null)
            {
                return NotFound();
            }

           var blogPostDto= _mapper.Map<BlogPostDto>(blogPost);

            return blogPostDto;
        }

       
        // POST: api/BlogPosts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BlogPostDto>> PostBlogPost(PostBlogPostRequest postBlogPost)
        {

            BlogPostId blogPostId = new BlogPostId();
            BlogPostBody blogBody = new BlogPostBody(postBlogPost.BlogBody);
            BlogPostHeader blogHeader = new BlogPostHeader(postBlogPost.BLogHeader);
            AuthorId authorId = new AuthorId(postBlogPost.AuthorId);

            var author = await _context.Authors.FindAsync(authorId);


            BlogPost blogPost = new BlogPost { Id=blogPostId,BlogHeader = blogHeader, BlogBody = blogBody, Author = author };

            _context.BlogPosts.Add(blogPost);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BlogPostExists(blogPost.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return _mapper.Map<BlogPostDto>(blogPost);
        }

        // DELETE: api/BlogPosts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPost(Guid id)
        {
            var blogPost = await _context.BlogPosts.FindAsync(new BlogPostId(id));
            if (blogPost == null)
            {
                return NotFound();
            }

            _context.BlogPosts.Remove(blogPost);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogPostExists(BlogPostId id)
        {
            return _context.BlogPosts.Any(e => e.Id == id);
        }
    }
    public record PostBlogPostRequest(string BlogBody, string BLogHeader, Guid AuthorId);
}
