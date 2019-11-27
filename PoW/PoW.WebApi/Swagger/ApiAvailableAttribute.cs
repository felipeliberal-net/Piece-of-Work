using Swashbuckle.Swagger.Annotations;
using System;

namespace PoW.WebApi.Swagger
{
    /// <summary>
    /// Adding this attribute to a method makes the method available on the public REST API.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ApiAvailableAttribute : SwaggerOperationAttribute
    {
        public ApiAvailableAttribute() : base()
        {
        }
        
        public string[] Groups
        {
            get
            {
                return Tags;
            }
            set
            {
                Tags = value;
            }
        }
    }
}
