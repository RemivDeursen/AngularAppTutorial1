using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HeroesAPI.Models;

namespace HeroesAPI.Controllers
{
    [Route("api/Heroes")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        private readonly HeroContext _context;

        public HeroesController(HeroContext context)
        {
            _context = context;
        }

        // GET: api/Heroes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HeroDTO>>> GetHeroes()
        {
            return await _context.Heroes
                .Select(x=> HeroToDTO(x))
                .ToListAsync();
            //return await _context.Heroes.ToListAsync();
        }

        // GET: api/Heroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HeroDTO>> GetHero(long id)
        {
            var hero = await _context.Heroes.FindAsync(id);

            if (hero == null)
            {
                return NotFound();
            }

            return HeroToDTO(hero);
        }

        // PUT: api/Heroes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHero(long id, HeroDTO heroDTO)
        {
            if (id != heroDTO.Id)
            {
                return BadRequest();
            }

            //_context.Entry(hero).State = EntityState.Modified;
            var hero = await _context.Heroes.FindAsync(id);
            if (hero == null)
            {
                return NotFound();
            }

            hero.Name = heroDTO.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HeroExists(id))
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

        // POST: api/Heroes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Hero>> CreateHero(HeroDTO heroDTO)
        {
            var hero = new Hero
            {
                Name = heroDTO.Name
            };

            _context.Heroes.Add(hero);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetHero), 
                new { id = hero.Id }, 
                HeroToDTO(hero));
        }

        // DELETE: api/Heroes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Hero>> DeleteHero(long id)
        {
            var hero = await _context.Heroes.FindAsync(id);
            if (hero == null)
            {
                return NotFound();
            }

            _context.Heroes.Remove(hero);
            await _context.SaveChangesAsync();

            return hero;
        }

        private bool HeroExists(long id)
        {
            return _context.Heroes.Any(e => e.Id == id);
        }

        private static HeroDTO HeroToDTO(Hero hero) =>
            new HeroDTO
            {
                Id = hero.Id,
                Name = hero.Name
            };
    }
}
