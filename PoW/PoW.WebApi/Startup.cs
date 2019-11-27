﻿using Microsoft.Owin;
using Microsoft.Owin.Cors;
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
            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            var jsonFormater = config.Formatters.JsonFormatter;
            jsonFormater.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            jsonFormater.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Formatters.Add(jsonFormater);

            config
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "Task Work API");
                    c.PrettyPrint();
                })
                .EnableSwaggerUi(c => c.DisableValidator());

            app.UseWebApi(config);
        }
    }
}
