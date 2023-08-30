﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Application.Models
{
    public class Media
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
        public string Extension { get; set; }
    }
}