﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CustumerServiceITS.Entities
{
    public class Custumer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Dateofbirth { get; set; }
        public string Email { get; set; }

    }
}
