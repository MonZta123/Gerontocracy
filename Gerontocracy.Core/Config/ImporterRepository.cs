using Gerontocracy.Core.Strategies.Sync;
using System.Collections.Generic;

namespace Gerontocracy.Core.Config
{
    internal class ImporterRepository
    {
        public List<IParlamentImporter> Importers { get; } = new List<IParlamentImporter>() {
            new AustriaImporter()
        };
    }
}
