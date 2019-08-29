using System.Threading.Tasks;

namespace Gerontocracy.Core.Interfaces
{
    public interface ISyncService
    {
        void SyncPolitiker();
        
        Task SyncSource(long id);

        Task SyncSources();
    }
}