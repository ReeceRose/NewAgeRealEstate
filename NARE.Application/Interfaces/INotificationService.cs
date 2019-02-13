using System.Threading.Tasks;

namespace NARE.Application.Interfaces
{
    public interface INotificationService
    {
        Task<bool> SendNotificationAsync(string toName,
            string toEmailAddress,
            string subject,
            string message);
    }
}
