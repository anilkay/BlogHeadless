using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlogHeadless.Api.Models.Context;
using BlogHeadless.Data.Models.Subscriber;
using AutoMapper;
using BlogHeadless.Data.Dtos;

namespace BlogHeadless.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribersController : ControllerBase
    {
        private readonly BlogDbContext _context;
        private readonly IMapper _mapper;

        public SubscribersController(BlogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Subscribers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubsCriberDto>>> Getsubscribers()
        {
            var susbcribers=await _context.subscribers.ToListAsync();
            return _mapper.Map<List<SubsCriberDto>>(susbcribers);
        }

        // GET: api/Subscribers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Subscriber>> GetSubscriber(string id)
        {
            Ulid possibleId;

            bool isParsed=Ulid.TryParse(id, out possibleId);

            if(!isParsed)
            {
                return NotFound();
            }

            var subscriber = await _context.subscribers.FindAsync(possibleId);

            if (subscriber == null)
            {
                return NotFound();
            }

            return subscriber;
        }

        // PUT: api/Subscribers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
       

        // POST: api/Subscribers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Subscriber>> PostSubscriber(PostSubscriberRequest subscriberRequest)
        {
            Subscriber subscriber = new(subscriberRequest.Email, subscriberRequest.SubsriptionSource);

            _context.subscribers.Add(subscriber);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SubscriberExists(subscriber.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSubscriber", new { id = subscriber.Id }, subscriber);
        }

        // DELETE: api/Subscribers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscriber(string id)
        {
            Ulid possibleId;

            bool isParsed = Ulid.TryParse(id, out possibleId);
            if (!isParsed)
            {
                return NotFound();
            }
            var subscriber = await _context.subscribers.FindAsync(possibleId);
            if (subscriber == null)
            {
                return NotFound();
            }

            _context.subscribers.Remove(subscriber);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubscriberExists(Ulid id)
        {
            return _context.subscribers.Any(e => e.Id == id);
        }
    }
    public record PostSubscriberRequest(string Email,string SubsriptionSource);
}
