using System.Collections.Generic;

namespace Morphius
{
    public class PostResult
    {
        public bool Success { get; set; }

        public object Data { get; set; }

        public IEnumerable<Fault> Errors { get; set; }
    }
}
