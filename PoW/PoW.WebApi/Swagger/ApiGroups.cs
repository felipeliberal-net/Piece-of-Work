using Swashbuckle.Swagger;
using System.Collections.Generic;

namespace PoW.WebApi.Swagger
{
    public static class ApiGroups
    {
        public static readonly List<Tag> Groups;

        static ApiGroups()
        {
            Groups = new List<Tag>()
            {
                new Tag() { name = "Authentication", description= "Authentication methods." }
            };
        }
    }
}
