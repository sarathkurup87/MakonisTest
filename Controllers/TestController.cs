using Makonis.Interface;
using Makonis.Models;
using Microsoft.AspNetCore.Mvc;

namespace Makonis.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private IGet getInterface;
        private ISave saveInterface;
        public TestController(ILogger<TestController> logger, IGet _getInterface, ISave _saveInterface)
        {
            _logger = logger;
            getInterface = _getInterface;
            saveInterface = _saveInterface;
        }

        [Route("getData")]
        [HttpGet]
        public Task<List<User>> GetData()
        {
            return getInterface.ReadJson();
        }

        [HttpPost("saveData")]
        public async Task<IActionResult> SaveData([FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("Owner object is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await saveInterface.WriteJson(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
