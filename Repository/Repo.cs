using Makonis.Interface;
using Makonis.Models;
using Newtonsoft.Json;
using System.Text;

namespace Makonis.Repository
{
    public class Repo : IGet, ISave
    {

        private IWebHostEnvironment webHostEnvironment;
        private readonly string _filePath = string.Empty;
        public Repo(IWebHostEnvironment _environment)
        {
            webHostEnvironment = _environment;
            _filePath = Path.Combine(this.webHostEnvironment.WebRootPath, "_result.json");
        }

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

    }
}
