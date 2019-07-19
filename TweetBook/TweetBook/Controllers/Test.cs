using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetBook.Controllers
{
    public class Test : Controller
    {
        [HttpGet("root")]
        public IActionResult Get()
        {
            return Ok(new { Name = "dum" });
        }
    }
}
