using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Heroes.Data.Models;

namespace Heroes.Services
{
    public class HeroesService
    {
        private readonly ProjectContext _context;

        public HeroesService(ProjectContext context)
        {
            _context = context;
        }

        public async Task<IList<HeroResponseModel>> GetItems()
        {
            var Items = await _context.Heroes
                .Select(x => new HeroResponseModel
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();
            return Items;
        }

        public async Task<HeroResponseModel> GetHero(long id)
        {
            var hero = await _context.Heroes.FindAsync(id);

            /*if (hero == null)
            {
                return NotFound();
            }*/

            return new HeroResponseModel
            {
                Name = hero.Name
            };
        }

        public async Task<HeroResponseModel> UpdateHero(long id, HeroResponseModel heroIn)
        {
            var heroDTO = new Hero
            {
                Name = heroIn.Name,
                Id = id
            };

            if (id != heroDTO.Id)
            {
                //return BadRequest();
            }

            //_context.Entry(hero).State = EntityState.Modified;
            var hero = await _context.Heroes.FindAsync(id);
            if (hero == null)
            {
                //return NotFound();
            }

            hero.Name = heroDTO.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                /*if (!HeroExists(id))
                {
                    //return NotFound();
                }
                else
                {
                    throw;
                }*/
            }
            return new HeroResponseModel
            {
                Name = hero.Name
            };
            //return NoContent();
        }

        public async Task<HeroResponseModel> CreateHero(HeroResponseModel heroDTO)
        {
            var hero = new Heroes.Data.Models.Hero
            {
                Name = heroDTO.Name
            };

            _context.Heroes.Add(hero);
            await _context.SaveChangesAsync();

            return new HeroResponseModel
            {
                Name = heroDTO.Name
            };
        }

        public async Task<HeroResponseModel> DeleteHero(long id)
        {
            var hero = await _context.Heroes.FindAsync(id);
            if (hero == null)
            {
                //return NotFound();
            }

            _context.Heroes.Remove(hero);
            await _context.SaveChangesAsync();

            return new HeroResponseModel
            {
                Name = hero.Name
            };
        }

        private bool HeroExists(long id)
        {
            return _context.Heroes.Any(e => e.Id == id);
        }
    }
}
