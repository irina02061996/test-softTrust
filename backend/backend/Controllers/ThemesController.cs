using System.Collections.Generic;
using System.Linq;
using backend.Database;
using backend.Database.Models;
using Microsoft.AspNetCore.Mvc;


namespace backend.Controllers
{
    [Route("api/[controller]")]
    public class ThemesController : Controller
    {
        UserMessagesContext db;

        public ThemesController(UserMessagesContext context)
        {
            this.db = context;
        }



        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Theme> Get()
        {
            return db.Themes.ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Theme theme = db.Themes.FirstOrDefault(x => x.Id == id);
            if (theme == null)
                return NotFound();
            return new ObjectResult(theme);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Theme theme)
        {
            if (theme == null)
            {
                return BadRequest();
            }

            db.Themes.Add(theme);
            db.SaveChanges();
            return Ok(theme);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public IActionResult Put([FromBody]Theme theme)
        {
            if (theme == null)
            {
                return BadRequest();
            }
            if (!db.Themes.Any(x => x.Id == theme.Id))
            {
                return NotFound();
            }

            db.Update(theme);
            db.SaveChanges();
            return Ok(theme);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Theme theme = db.Themes.FirstOrDefault(x => x.Id == id);
            if (theme == null)
            {
                return NotFound();
            }
            db.Themes.Remove(theme);
            db.SaveChanges();
            return Ok(theme);
        }
    }
}
