using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Heroes.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Heroes.API.Controllers
{
    [Route("api/Heroes")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        private readonly HeroesService _heroesService;

        public HeroesController(HeroesService heroesService)
        {
            _heroesService = heroesService;
        }

        // GET: api/Heroes
        [HttpGet("list")]
        public async Task<IActionResult> GetHeroes()
        {
            var allHeroes = await _heroesService.GetHeroes();
            return Ok(allHeroes);
        }

        // GET: api/Heroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetHero(long id)
        {
            var hero = await _heroesService.GetHero(id);

            if (hero == null)
            {
                return NotFound();
            }

            return Ok(hero);
        }

        // PUT: api/Heroes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHero(long id, HeroResponseModel hero)
        {
            var heroupdate = await _heroesService.UpdateHero(id, hero);
            return Ok(heroupdate);
        }

        // POST: api/Heroes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<HeroResponseModel>> CreateHero(HeroResponseModel hero)
        {
            var newHero = await _heroesService.CreateHero(hero);

            return Ok();
        }

        // DELETE: api/Heroes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHero(long id)
        {
            var heroRemove = await _heroesService.DeleteHero(id);

            return Ok(heroRemove);
        }
    }
}
