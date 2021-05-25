using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoogleGame.Models;
using BoogleGame.GameLogic;
using Microsoft.AspNetCore.Mvc;

namespace BoogleGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoogleGameController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            BoogleGameHelper bgh = new BoogleGameHelper();
            return Ok(bgh.cubicDice);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/BoogleGame
        [HttpPost]
        public ActionResult Post([FromBody] List<In> input)
        {
            try
            {
                var result = new List<Out>();
                BoogleGameHelper bgh = new BoogleGameHelper();
                result = bgh.GetNumberOfPointsMP(input);

                return Ok(result);
            }
            catch (Exception ex)
            {
                //TODO logging...
                return BadRequest("Error occured..");
            }
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
