using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogHeadless.Api.Models;
using BlogHeadless.Api.Models.Context;
using BlogHeadless.Api.Models.Ids;
using BlogHeadless.Data.Models.Author;

namespace BlogHeadless.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BlogDbContext _context;

        public AuthorsController(BlogDbContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            return await _context.Authors.ToListAsync();
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(Guid id)
        {
            var author = await _context.Authors.FindAsync(new AuthorId(id));

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

      
      
        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(PostAuthorRequest authorRequest)
        {
            AuthorId id = new AuthorId();
            AuthorName authorName = new AuthorName(authorRequest.Name);
            Email email = new Email(authorRequest.Email);

            Author author=new Author { Email = email, Name=authorName,Id=id};



            _context.Authors.Add(author);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AuthorExists(author.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            var author = await _context.Authors.FindAsync(new AuthorId(id));
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorExists(AuthorId id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
    public record PostAuthorRequest(string Email,string Name);
}
