using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Wl_labb2.Models
{
    public class Snus
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] // Gör att MongoDB kan hantera ID:t som en sträng
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}
