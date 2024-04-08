using CalculatorWebAPI.DTO;
using CalculatorWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorWebAPI.Controllers
{
    [Route("monkey")]
    [ApiController]
    public class MonkeyAPIController: ControllerBase
    {
        static MonkeyList list = new MonkeyList();
        // GET monkey/ReadAllMonkeys
        [HttpGet("ReadAllMonkeys")]
        public IActionResult ReadAllMonkeys()
        {
            MonkeyListDto mon = new MonkeyListDto();
            
            mon.Monkeys = new List<MonkeyDto>();
            foreach (Monkey m in list.Monkeys)
            {
                mon.Monkeys.Add(new MonkeyDto()
                {
                    Name = m.Name,
                    Location = m.Location,
                    Details = m.Details,
                    ImageUrl = m.ImageUrl,
                    IsFavorite = m.IsFavorite,

                });
            }

            return Ok(mon);
        }

        // GET monkey/ReadMonkey
        [HttpGet("ReadMonkey")]
        public IActionResult ReadMonkey([FromQuery] string MonkeyName)
        {
                foreach(Monkey m in list.Monkeys)
                {
                    if(m.Name == MonkeyName)
                    {
                        MonkeyDto monkeyDto = new MonkeyDto()
                        {
                            Name = m.Name,
                            Location = m.Location,
                            Details = m.Details,
                            ImageUrl = m.ImageUrl,
                            IsFavorite = m.IsFavorite,
                        };
                        return Ok(monkeyDto);
                    }
                }
                NotFoundResult result = new NotFoundResult();
                return result;
        }

        // POST monkey/AddMonkey
        [HttpPost("AddMonkey")]
        public IActionResult AddMonkey([FromBody] Monkey monkey)
        {
           try
            {
                bool isNotOk = false;
                foreach(Monkey m in list.Monkeys)
                {
                    if( m.Name == monkey.Name)
                    {
                        isNotOk = true;
                    }
                }
                if (!isNotOk)
                {
                    Monkey mon = new Monkey()
                    {
                        Name = monkey.Name,
                        Location = monkey.Location,
                        Details = monkey.Details,
                        ImageUrl = monkey.ImageUrl,
                        IsFavorite = monkey.IsFavorite,

                    };
                    list.Monkeys.Add(mon);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
