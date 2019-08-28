using System.Threading.Tasks;

namespace Gerontocracy.Core.Interfaces
{
    public interface ISyncService
    {
        Task SyncPolitiker();
        
        Task SyncSource(long id);

        Task SyncSources();
    }
}