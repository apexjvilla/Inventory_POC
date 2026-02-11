using Inventory.Domain.Entities;

namespace Inventory.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJson<T>(this IEnumerable<T> entities) where T : EntityBase
        { 
            return System.Text.Json.JsonSerializer.Serialize(entities, 
                new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true
            });
        }

        public static string ToJson(this EntityBase entity)
        {
            return System.Text.Json.JsonSerializer.Serialize(entity,
                new System.Text.Json.JsonSerializerOptions
                {
                    WriteIndented = true
                });
        }

    }
}
