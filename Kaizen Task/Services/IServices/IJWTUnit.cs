using ModelsRepository.Models;

namespace Kaizen_Task.Services.IServices
{
    public interface IJWTUnit
    {
        string GenerateToken(User user);
    }
}
