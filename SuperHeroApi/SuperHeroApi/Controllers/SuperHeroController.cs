using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroApi.Controllers
{



   


    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;
        public SuperHeroController(DataContext context)
        {
            _context = context;
        }






        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }


        [HttpGet("{id}")]
        public async  Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero=await _context.SuperHeroes.FindAsync(id);
            if(hero == null)
            {
                return BadRequest("Hero Not Found");
            }
            else
            {
                return Ok(hero);
            }
        }







        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
           await _context.SaveChangesAsync();
           
            return Ok(await _context.SuperHeroes.ToListAsync());

        }

        [HttpDelete]
        public async  Task<ActionResult<List<SuperHero>>> Delete(SuperHero req)
        {
            var hero =  _context.SuperHeroes.Remove(req);
            _context.SaveChanges();
      
            if (hero == null)
            {
                return BadRequest("Hronot found");
            }
            else
            {
              
                return Ok(_context.SuperHeroes);
            }
        }

        [HttpPut]

        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero req)
        {

            var hero = await _context.SuperHeroes.FindAsync(req.Id);
            if (hero == null)
            {
                return BadRequest("Hero Not Found");
            }
            else
            {


                hero.Name = req.Name;
                hero.Firstname = req.Firstname;
                hero.Lastname = req.Lastname;
                hero.Place = req.Place;
                await _context.SaveChangesAsync();
                return Ok(_context.SuperHeroes);
            }


        }





    }
}
