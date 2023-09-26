using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heros = new List<SuperHero>
        {
            new SuperHero { Id = 1,
            Name = "Spider Man",
            FirstName = "Peter",
            LastName = "Parker",
            Place = "New York"},

            new SuperHero { Id = 2,
            Name = "Iron Man",
            FirstName = "Tony",
            LastName = "Stark",
            Place = "Long Island"}
        };

        private readonly DataContex _context;

        public SuperHeroController(DataContex contex)
        {
            _context = contex;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {

            return Ok(await _context.superHeroes.ToListAsync());

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetById(int id)
        {
            var hero = await _context.superHeroes.FindAsync(id);

            if (hero == null)
                return BadRequest("hero not found");
            return Ok(await _context.superHeroes.ToListAsync());
        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddNew(SuperHero newHero)
        {
            _context.superHeroes.Add(newHero);
            await _context.SaveChangesAsync();

            return Ok(await _context.superHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var dbHero = await _context.superHeroes.FindAsync(request.Id);
            if (dbHero == null)
                return BadRequest("hero not found");

            dbHero.Name = request.Name;
            dbHero.FirstName = request.FirstName;
            dbHero.LastName = request.LastName;
            dbHero.Place = request.Place;

            await _context.SaveChangesAsync();
                     
            return Ok(await _context.superHeroes.ToListAsync());
        }
        [HttpDelete]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var dbHero = await _context.superHeroes.FindAsync(id);
            if (dbHero == null)
                return BadRequest("hero not found");
            _context.superHeroes.Remove(dbHero);
            await _context.SaveChangesAsync();

            return Ok(await _context.superHeroes.ToListAsync());
        }
    }
}
