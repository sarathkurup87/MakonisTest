using Makonis.Models;

namespace Makonis.Interface
{
    public interface IGet
    {
        Task<List<User>> ReadJson();
    }
}
