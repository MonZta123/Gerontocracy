using System.Collections.Generic;
using Gerontocracy.Core.BusinessObjects.Sync;

namespace Gerontocracy.Core.Utilities
{
    internal class ExternalIdComparer : IEqualityComparer<Politiker>
    {
        #region Methods

        public bool Equals(Politiker x, Politiker y) => y != null && x != null && x.ExternalId == y.ExternalId;
        public int GetHashCode(Politiker obj) => obj.ExternalId.GetHashCode();

        #endregion Methods
    }
}
