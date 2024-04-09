using Microsoft.AspNetCore.Mvc;
using Producer.Interfaces;

namespace Producer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitMqController(IRabbitMqService mqService) : ControllerBase
    {
        [Route("[action]/{message}")]
        [HttpGet]
        public IActionResult SendMessage(string message)
        {
            mqService.SendMessage(message);

            return Ok("Сообщение отправлено");
        }
    }
}
