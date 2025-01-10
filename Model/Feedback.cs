using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.Model
{
    public class Feedback
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        
        public required string TrackingNum { get; set; }
        
        public required string Name { get; set; }
        
        public required string Surname { get; set; }
        
        public required string Email { get; set; }
        
        public string? Phone { get; set; }
        
        public string? Message { get; set; }
        
        public string? Province { get; set; }
        
        public string? District { get; set; }
        
        public int Rating { get; set; }
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Date { get; set; }
    }
}