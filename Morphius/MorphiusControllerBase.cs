using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Morphius
{
    public class MorphiusControllerBase : ControllerBase
    {
        public OkObjectResult PostOk(object obj) => Ok(new PostResult()
        {
            Success = true,
            Data = obj,
        });

        public OkObjectResult PostOk() => PostOk(null);
    }
}
