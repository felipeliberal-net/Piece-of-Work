using Swashbuckle.Swagger;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoW.WebApi.Swagger
{
    public sealed class SwaggerCachingProvider : ISwaggerProvider
    {
        private readonly ConcurrentDictionary<string, SwaggerDocument> cache = new ConcurrentDictionary<string, SwaggerDocument>();

        private ISwaggerProvider swaggerProvider;

        public SwaggerCachingProvider SetProvider(ISwaggerProvider swaggerProvider)
        {
            if (swaggerProvider == null)
                throw new ArgumentNullException();

            this.swaggerProvider = swaggerProvider;

            return this;
        }

        public SwaggerDocument GetSwagger(string rootUrl, string apiVersion)
        {
            if (swaggerProvider == null)
                throw new InvalidOperationException("Failed to initialize your swagger tool.");

            var cacheKey = $"{ rootUrl }_{ apiVersion }";

            try
            {
                return cache.GetOrAdd(cacheKey, (key) => swaggerProvider.GetSwagger(rootUrl, apiVersion));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Fail to generate the API documentation. { e.Message }");
                throw e;
            }
        }
    }
}
