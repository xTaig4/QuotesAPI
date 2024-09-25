using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using QuoteAPI.Data;
using QuoteAPI.Models;

namespace QuoteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private readonly QuoteContext _context;

        public QuotesController(QuoteContext context)
        {
            _context = context;
        }

        [EnableRateLimiting("fixed")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quote>>> GetQuotes()
        {
            return await _context.Quotes.ToListAsync();
        }

        [EnableRateLimiting("fixed")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Quote>> GetQuote(int id)
        {
            var quote = await _context.Quotes.FindAsync(id);

            if (quote == null)
            {
                return NotFound();
            }

            return quote;
        }

        [EnableRateLimiting("fixed")]
        [HttpGet("Random/Quote")]
        public async Task<ActionResult<Quote>> GetRandomQuote()
        {
            int quotesTotals = _context.Quotes.Count();

            int quoteId = new Random().Next(1, quotesTotals + 1);

            var quote = await _context.Quotes.FindAsync(quoteId);

            if (quote == null)
            {
                return NotFound();
            }

            return quote;
        }

        [EnableRateLimiting("fixed")]
        [HttpGet("Random/QuoteDTO")]
        public async Task<ActionResult<QuoteDTO>> GetRandomQuote3()
        {
            int quotesTotals = _context.Quotes.Count();

            int quoteId = new Random().Next(1, quotesTotals + 1);

            var quote = await _context.Quotes.FindAsync(quoteId);

            if (quote == null)
            {
                return NotFound();
            }

            return new QuoteDTO() { 
                FirstName = quote.FirstName, 
                LastName = quote.LastName, 
                _Quote = quote._Quote,
                Image = quote.Image
            };
        }

        [EnableRateLimiting("fixed")]
        [HttpGet("Random/Quote/{firstName}")]
        public Task<ActionResult<Quote>> GetRndQuoteFromCharacter(string firstName)
        {
            Dictionary<int, Quote> quotesCollection = new Dictionary<int, Quote>();
            int tmpId = 0;

            foreach (var quote in _context.Quotes)
            {
                if (quote.FirstName == firstName)
                {
                    tmpId++;
                    quotesCollection.Add(tmpId, quote);
                }
                else if (quote.FirstName == null)
                {
                    return Task.FromResult<ActionResult<Quote>>(NotFound());
                }
            }

            int quoteTmpId = new Random().Next(1, quotesCollection.Count + 1);
            Quote quoteDrawn = quotesCollection[quoteTmpId];

            return Task.FromResult<ActionResult<Quote>>(quoteDrawn);
        }

        // PUT: api/Quotes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuote(int id, Quote quote)
        {
            if (id != quote.Id)
            {
                return BadRequest();
            }

            _context.Entry(quote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuoteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Quotes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Quote>> PostQuote(Quote quote)
        {
            _context.Quotes.Add(quote);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuote", new { id = quote.Id }, quote);
        }

        // DELETE: api/Quotes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuote(int id)
        {
            var quote = await _context.Quotes.FindAsync(id);
            if (quote == null)
            {
                return NotFound();
            }

            _context.Quotes.Remove(quote);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuoteExists(int id)
        {
            return _context.Quotes.Any(e => e.Id == id);
        }
    }
}
