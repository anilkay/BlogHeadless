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
using AutoMapper;
using BlogHeadless.Data.Dtos;

namespace BlogHeadless.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BlogDbContext _context;
        private readonly IMapper _mapper;

        public AuthorsController(BlogDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
        {
            var authors=await _context.Authors.ToListAsync();
            return _mapper.Map<List<AuthorDto>>(authors);
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthor(Guid id)
        {
            var author = await _context.Authors.FindAsync(new AuthorId(id));
            if (author == null)
            {
                return NotFound();
            }

            var authorDto = _mapper.Map<AuthorDto>(author);

            return authorDto;
        }

      
      
        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AuthorDto>> PostAuthor(PostAuthorRequest authorRequest)
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

            return _mapper.Map<AuthorDto>(author);
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
