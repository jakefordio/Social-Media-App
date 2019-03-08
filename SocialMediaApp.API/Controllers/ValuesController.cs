using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.API.Data;

namespace SocialMediaApp.API.Controllers
{
    // http://localhost:5000/api/values
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext context;

        public ValuesController(DataContext context)
        {
            this.context = context;

        }
        // GET api/values
        // [HttpGet] OLD SYNCHRONOUS METHOD
        // public IActionResult GetValues() //IActionResult allows you to return HTTP Responses to the client
        // {
        //     var values = context.Values.ToList(); //This gives values to EF (Entity Framework) methods, as well as DB Sets (Values)
        //     return Ok(values);
        // }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetValues() //IActionResult allows you to return HTTP Responses to the client
        {
            var values = await context.Values.ToListAsync(); //This gives values to EF (Entity Framework) methods, as well as DB Sets (Values)
            return Ok(values);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            //FirstOrDefault returns null if no matching value is found, while First() just returns an exception.
            //The 1st "x" in the Lambda expression represents the value we are returning.
            //x.Id is going to match the value that is being passed in(x.Id == id)
            var value = await context.Values.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(value);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
