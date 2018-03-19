using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Database.Models
{
    public class CreateMessage
    {
        public string Content { get; set; }

        public int UserId { get; set; }
        public int ThemeId { get; set; }
    }
}
