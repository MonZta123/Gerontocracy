using System;
using System.Collections.Generic;
using System.Text;

namespace Gerontocracy.Core.BusinessObjects.News
{
    public class Parlament
    {
        public long Id { get; set; }

        public string Code { get; set; }

        public string Langtext { get; set; }

        public List<RssSource> RssSources { get; set; }
    }
}
