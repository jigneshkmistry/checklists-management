using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChecklistsManagement.Domain
{
    public class Checklists
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = "";

        [BsonElement("title")]
        public string Title { get; set; } = "";

        [BsonElement("description")]
        public string? Description { get; set; }

        [BsonElement("items")]
        public List<ChecklistItem> Items { get; set; } = new();

        [BsonElement("status")]
        public ChecklistStatus Status { get; set; } = ChecklistStatus.Draft; // DRAFT, PUBLISHED, UNPUBLISHED

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("updatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }

    public class ChecklistItem
    {
        [BsonElement("id")]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("title")]
        public string Title { get; set; } = string.Empty;

        [BsonElement("description")]
        public string? Description { get; set; }
  
    }

    public enum ChecklistStatus
    {
        Draft,
        Published,
        Unpublished
    }
}
