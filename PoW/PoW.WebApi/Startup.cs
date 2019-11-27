using Microsoft.Owin;
using Newtonsoft.Json.Serialization;
using Owin;
using PoW.WebApi.Swagger;
using Swashbuckle.Application;
using System;
using System.Linq;
using System.Web.Http;
using System.Xml.XPath;

[assembly: OwinStartup(typeof(PoW.WebApi.Startup))]

namespace PoW.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            SwaggerCachingProvider swaggerProvider = new SwaggerCachingProvider();
            SwaggerConfig swaggerConfig = new SwaggerConfig(swaggerProvider);
            
            swaggerConfig.Register(config);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            var jsonFormater = config.Formatters.JsonFormatter;
            jsonFormater.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            jsonFormater.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Formatters.Add(jsonFormater);

            
            //config
            //    .EnableSwagger(c =>
            //    {
            //        c.SingleApiVersion("v1", "Task Work API").Description("This is a tool for testing and prototyping the Task Work API.");

            //        c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

            //        c.IncludeXmlComments(GetXmlCommentPath());
                    
            //        c.PrettyPrint();
            //    })
            //    .EnableSwaggerUi("api/help/{*assetPath}", c =>
            //    {
            //        c.DisableValidator();

            //        c.DocExpansion(DocExpansion.List);
            //    });

            app.UseWebApi(config);
        }

        private string GetXmlCommentPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory + @"PoW.WebApi.xml";
        }
    }
}
