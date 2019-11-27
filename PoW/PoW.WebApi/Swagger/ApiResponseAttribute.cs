using Swashbuckle.Swagger.Annotations;
using System;
using System.Net;

namespace PoW.WebApi.Swagger
{
    /// <summary>
    /// Generic HTTP response.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ApiResponseAttribute : SwaggerResponseAttribute
    {
        public ApiResponseAttribute(HttpStatusCode statusCode, Type type = null, string description = null) : base(statusCode, description, type)
        {

        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class ApiResponseOkAttribute : ApiResponseAttribute
    {
        public ApiResponseOkAttribute(Type type = null, string description = null): base(HttpStatusCode.OK, type, description ?? GetDescription())
        {

        }
        
        private static string GetDescription()
        {
            return "200 OK - The request has succeeded.";
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class ApiResponseBadRequestAttribute : ApiResponseAttribute
    {
        public ApiResponseBadRequestAttribute(Type type = null, string description = null) : base(HttpStatusCode.BadRequest, type, description ?? GetDescription())
        {

        }

        private static string GetDescription()
        {
            return "400 Bad Request - The request is invalid.";
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class ApiResponseUnauthorizedAttribute : ApiResponseAttribute
    {
        public ApiResponseUnauthorizedAttribute() : base(HttpStatusCode.Unauthorized, null, GetDescription())
        {

        }

        private static string GetDescription()
        {
            return "401 Unauthorized - An authentication error occurred.";
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class ApiResponseNotFoundAttribute : ApiResponseAttribute
    {
        public ApiResponseNotFoundAttribute(Type type = null, string description = null) : base(HttpStatusCode.NotFound, null, description ?? GetDescription())
        {

        }

        private static string GetDescription()
        {
            return "404 Not Found - Entity not found.";
        }
    }
}
