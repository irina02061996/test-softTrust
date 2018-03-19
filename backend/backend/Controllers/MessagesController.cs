using System;
using System.Collections.Generic;
using System.Linq;
using backend.Database;
using backend.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    public class MessagesController : Controller
    {
        UserMessagesContext db;

        public MessagesController(UserMessagesContext context)
        {
            this.db = context;
        }



        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Message> Get()
        {
            return db.Messages
                .Include(m => m.User)
                .Include(m => m.Theme)
                .ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Message message = db.Messages
                                .Include(m => m.User)
                                .Include(m => m.Theme)
                                .FirstOrDefault(x => x.Id == id);
            if (message == null)
                return NotFound();

            return new ObjectResult(message);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]CreateMessage createMessage)
        {
            if (createMessage == null)
            {
                return BadRequest();
            }

            var user = db.Users.FirstOrDefault(data => data.Id == createMessage.UserId);
            var theme = db.Themes.FirstOrDefault(data => data.Id == createMessage.ThemeId);

            Message message = null;
            if (user != null && theme != null)
            {
                message = new Message()
                {
                    Content = createMessage.Content,
                    Theme = theme,
                    User = user
                };
            }

            db.Messages.Add(message);
            db.SaveChanges();

            return Ok(message);
        }




        // PUT api/<controller>/5
        [HttpPut]
        public IActionResult Put([FromBody]Message message)
        {
            if (message == null)
            {
                return BadRequest();
            }
            if (!db.Themes.Any(x => x.Id == message.Id))
            {
                return NotFound();
            }

            db.Update(message);
            db.SaveChanges();
            return Ok(message);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Message message = db.Messages.FirstOrDefault(x => x.Id == id);
            if (message == null)
            {
                return NotFound();
            }
            db.Messages.Remove(message);
            db.SaveChanges();
            return Ok(message);
        }
    }
}
