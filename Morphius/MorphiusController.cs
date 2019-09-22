using Microsoft.AspNetCore.Mvc;

namespace Morphius
{
    public class MorphiusController : ControllerBase
    {
        [NonAction]
        public OkObjectResult PostOk(object obj) => Ok(new PostResult()
        {
            Success = true,
            Data = obj,
        });

        [NonAction]
        public OkObjectResult PostOk() => PostOk(null);
    }
}
