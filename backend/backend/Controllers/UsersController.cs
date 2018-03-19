using System;
using System.Collections.Generic;
using System.Linq;
using backend.Database;
using backend.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        UserMessagesContext db;

        public UsersController(UserMessagesContext context)
        {
            this.db = context;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return db.Users
                .ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            User user = db.Users
                       .FirstOrDefault(x => x.Id == id);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]CreateUser createuser)
        {
            if (createuser == null)
            {
                return BadRequest();
            }

            User user = new User()
            {
                Name = createuser.Name,
                Email = createuser.Email,
                Phone = createuser.Phone,
            };


            db.Users.Add(user);
            db.SaveChanges();
            return Ok(user);
        }




        // PUT api/<controller>/5
        [HttpPut]
        public IActionResult Put([FromBody]User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!db.Users.Any(x => x.Id == user.Id))
            {
                return NotFound();
            }

            db.Update(user);
            db.SaveChanges();
            return Ok(user);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            User user = db.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            db.Users.Remove(user);
            db.SaveChanges();
            return Ok(user);
        }
    }
}
