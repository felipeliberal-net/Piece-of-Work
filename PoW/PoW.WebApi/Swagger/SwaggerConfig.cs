using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Xml.XPath;

namespace PoW.WebApi.Swagger
{
    public sealed class SwaggerConfig
    {
        private readonly SwaggerCachingProvider swaggerCachingProvider;

        public SwaggerConfig(SwaggerCachingProvider swaggerCachingProvider)
        {
            this.swaggerCachingProvider = swaggerCachingProvider;
        }

        public void Register(HttpConfiguration config)
        {
            config.EnableSwagger("api/{apiVersion}/swagger.json", c =>
            {
                c.IncludeXmlComments(GetXmlCommentPath());

                c.DocumentFilter<PublicApiFilter>();

                c.DocumentFilter<SwaggerAddEnumDescriptions>();

                c.DocumentFilter<AddTagsDescriptionFilter>();

                c.SingleApiVersion("v1", "Task Work API").Description("This is a tool for testing and prototyping the Task Work API.");

                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                c.OperationFilter<ResponseContentTypeOperationFilter>();

                c.PrettyPrint();

                c.CustomProvider((defaultProvider) => swaggerCachingProvider.SetProvider(defaultProvider));
            })
            .EnableSwaggerUi("api/help/{*assetPath}", c =>
            {
                c.DisableValidator();

                c.DocExpansion(DocExpansion.List);

                var thisAssembly = typeof(SwaggerConfig).Assembly;

                c.InjectJavaScript(thisAssembly, "swagger_customizations.js");
            });
        }

        private string GetXmlCommentPath()
        {
            return AppDomain.CurrentDomain.BaseDirectory + @"PoW.WebApi.xml";
        }
    }

    public sealed class ResponseContentTypeOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var requestAttributes = apiDescription.GetControllerAndActionAttributes<SwaggerContentTypeFilter>().FirstOrDefault();

            if (requestAttributes != null)
            {
                if (requestAttributes.Exclusive)
                    operation.produces.Clear();

                operation.produces.Add(requestAttributes.ResponseType);
            }
        }
    }

    /// <summary>
    /// Removes methods that are not explicitly declared as public from the API documentation.
    /// </summary>
    public sealed class PublicApiFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            foreach (var apiDescription in apiExplorer.ApiDescriptions)
            {
                // Get method attribute.
                var attribute = apiDescription.ActionDescriptor.GetCustomAttributes<ApiAvailableAttribute>().FirstOrDefault();

                // Removes methods that are not explicitly declared as public.
                if (attribute == null)
                {
                    var route = "/" + apiDescription.Route.RouteTemplate.TrimEnd('/').Replace("*", "");
                    if (swaggerDoc.paths.ContainsKey(route))
                    {
                        var path = swaggerDoc.paths[route];
                        foreach (var method in apiDescription.ActionDescriptor.SupportedHttpMethods)
                        {
                            if (method == HttpMethod.Get)
                            {
                                path.get = null;
                                continue;
                            }

                            if (method == HttpMethod.Post)
                            {
                                path.post = null;
                                continue;
                            }

                            if (method == HttpMethod.Put)
                            {
                                path.put = null;
                                continue;
                            }

                            if (method == HttpMethod.Delete)
                            {
                                path.delete = null;
                                continue;
                            }
                        }

                        if (path.get == null && path.post == null && path.put == null && path.delete == null)
                        {
                            swaggerDoc.paths.Remove(route);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Add tags description.
    /// </summary>
    public sealed class AddTagsDescriptionFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            if (swaggerDoc.tags == null)
            {
                swaggerDoc.tags = ApiGroups.Groups;
            }
        }
    }
}
