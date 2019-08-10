using System.Threading.Tasks;

namespace Gerontocracy.Core.Interfaces
{
    public interface ISyncService
    {
        Task SyncPolitiker();

        Task SyncApa();
    }
}