using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookServiceITS.Entities
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Name")]
        public string Titolo { get; set; }
        public string Autore { get; set; }
        public int Pagine { get; set; }
        public int NumeroCopie { get; set; }
    }
}
