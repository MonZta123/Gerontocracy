using Microsoft.AspNetCore.Builder;
using System;

namespace Morphius
{
    public static class MorphiusBuilder
    {
        public static IApplicationBuilder UseMorphius(this IApplicationBuilder app, Action<MorphiusOptions> config)
        {
            var data = new MorphiusOptions();
            config(data);
            return app.UseMiddleware<Morphius>(data);
        }
    }
}
