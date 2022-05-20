using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Makonis.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private IWebHostEnvironment webHostEnvironment;
        private readonly ILogger<TestController> _logger;
        private readonly string _filePath = string.Empty;
        public TestController(ILogger<TestController> logger, IWebHostEnvironment _environment)
        {
            webHostEnvironment = _environment;
            _logger = logger;
            _filePath = Path.Combine(this.webHostEnvironment.WebRootPath, "_result.json");
        }

        [Route("getData")]
        [HttpGet]
        public Task<List<User>> GetData()
        {
            return ReadJson();
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
                    return BadRequest("Invalid model object");
                }

                await WriteJson(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        #region Read/Write Json
        public async Task<List<User>> ReadJson()
        {
            var _user = new List<User>();
            try
            {
                FileInfo file = new FileInfo(_filePath);
                if (file.Exists)
                {
                    using (StreamReader r = new StreamReader(_filePath))
                    {
                        string json = await r.ReadToEndAsync();
                        _user = JsonConvert.DeserializeObject<List<User>>(json);
                    }
                }
            }
            catch { }
            return _user;
        }
        public async Task WriteJson(User _user)
        {
            try
            {
                var users = new List<User>();
                users = ReadJson().Result; users.Add(_user);
                string jsondata = JsonConvert.SerializeObject(users);
                byte[] byteArray = Encoding.ASCII.GetBytes(jsondata);
                string strPath = Path.Combine(this.webHostEnvironment.WebRootPath, "_result.json");
                using (FileStream fs = new FileStream(strPath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    await fs.WriteAsync(byteArray, 0, byteArray.Length);
                }
            }
            catch { }
        }
        #endregion

    }
}
