using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BorrowingService.Entities
{
    public class Borrowing
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Name")]
        public string Bookid { get; set; }
        public string Custumerid { get; set; }
        public string Datestart { get; set; }
        public string Dateend { get; set; }
    }
}
