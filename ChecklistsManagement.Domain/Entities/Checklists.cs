using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChecklistsManagement.Domain
{
    public class Checklists
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = "";

        public string Title { get; set; } = "";

        public string? Description { get; set; }
    }
}
