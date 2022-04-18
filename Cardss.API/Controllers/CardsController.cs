using Cardss.API.Data;
using Cardss.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cardss.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCards()
        {
            var cards = await _context.Cards.ToListAsync();
            return Ok(cards);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetCard")]
        public async Task<IActionResult> GetCard([FromRoute] Guid id)
        {
            var cards = await _context.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if(cards != null)
            {
                return Ok(cards);
            }
            else
            {
                return NotFound("Card not found");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] Card card)
        {
            card.Id = Guid.NewGuid();
            await _context.Cards.AddAsync(card);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCard), new {id = card.Id}, card);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCard([FromRoute] Guid id, [FromBody] Card card)
        {
            var existingCard = await _context.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if(existingCard != null)
            {
                existingCard.CardHolderName = card.CardHolderName;
                existingCard.CardNumber = card.CardNumber;
                existingCard.ExpiryMonth = card.ExpiryMonth;
                existingCard.ExpiryYear = card.ExpiryYear;
                existingCard.CVC = card.CVC;
                await _context.SaveChangesAsync();
                return Ok(existingCard);
            }
            else
            {
                return NotFound("Card not found");
            }

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCard([FromRoute] Guid id)
        {
            var existingCard = await _context.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCard != null)
            {
                _context.Remove(existingCard);
                await _context.SaveChangesAsync();
                return Ok(existingCard);
            }
            else
            {
                return NotFound("Card not found");
            }

        }


    }
}
