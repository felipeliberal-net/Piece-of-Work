using System;

namespace PoW.WebApi.Swagger
{
    /// <summary>
    /// SwaggerResponseContentTypeAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class SwaggerContentTypeFilter : Attribute
    {
        /// <summary>
        /// SwaggerResponseContentTypeAttribute
        /// </summary>
        /// <param name="responseType"></param>
        /// <param name="exclusive"></param>
        public SwaggerContentTypeFilter(string responseType, bool exclusive = true)
        {
            ResponseType = responseType;
            Exclusive = exclusive;
        }

        /// <summary>
        /// Response Content Type
        /// </summary>
        public string ResponseType { get; private set; }

        /// <summary>
        /// Remove all other Response Content Types
        /// </summary>
        public bool Exclusive { get; private set; }
    }
}
