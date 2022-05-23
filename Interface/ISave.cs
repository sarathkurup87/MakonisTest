using Makonis.Models;

namespace Makonis.Interface
{
    public interface ISave
    {
        Task WriteJson(User _user);
    }
}
