using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Web.Http.Description;

namespace PoW.WebApi.Swagger
{
    public class SwaggerAddEnumDescriptions : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            // Add enum descriptions to result models.
            foreach (KeyValuePair<string, Schema> schemaDictionaryItem in swaggerDoc.definitions)
            {
                Schema schema = schemaDictionaryItem.Value;
                foreach (KeyValuePair<string, Schema> propertyDictionaryItem in schema.properties)
                {
                    Schema property = propertyDictionaryItem.Value;
                    IList<object> propertyEnums = property.@enum;
                    if (propertyEnums != null && propertyEnums.Count > 0)
                    {
                        if (property.description == null)
                            property.description = "";

                        if (property.description.Length > 0 && !property.description.EndsWith(" "))
                            property.description += " ";

                        property.description += DescribeEnum(propertyEnums);
                        property.@enum = null;
                    }
                }
            }

            // Add enum descriptions to input parameters.
            if (swaggerDoc.paths.Count > 0)
            {
                foreach (PathItem pathItem in swaggerDoc.paths.Values)
                {
                    DescribeEnumParameters(pathItem.parameters);

                    List<Operation> possibleParameterisedOperations = new List<Operation> { pathItem.get, pathItem.post, pathItem.put };
                    possibleParameterisedOperations.FindAll(x => x != null).ForEach(x => DescribeEnumParameters(x.parameters));
                }
            }
        }

        private void DescribeEnumParameters(IList<Parameter> parameters)
        {
            if (parameters != null)
            {
                foreach (Parameter param in parameters)
                {
                    IList<object> paramEnums = param.@enum;
                    if (paramEnums != null && paramEnums.Count > 0)
                    {
                        if (param.description == null)
                            param.description = "";

                        if (!param.description.EndsWith(" "))
                            param.description += " ";

                        param.description += DescribeEnum(paramEnums);
                    }
                }
            }
        }

        private string DescribeEnum(IList<object> values)
        {
            var items = new List<string>(values.Count);

            foreach (object value in values)
            {
                int str = Convert.ToInt32(value); // Can't use the casting operator (int) because the underlying type of some Enums is byte.
                var name = value.ToString();
                items.Add($"{ str } = { name }");
            }

            return "Values: " + string.Join(";", items);
        }
    }
}