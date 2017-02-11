using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoHelper
{
    public static class Builder
    {
        public static FilterDefinition<BsonDocument> FilterEq(string field, string value)
        {
            return Builders<BsonDocument>.Filter.Eq(field, value);
        }

        public static FilterDefinition<BsonDocument> FilterEq<T>(string field, T value)
        {
            return Builders<BsonDocument>.Filter.Eq(field, value);
        }

        public static FilterDefinition<BsonDocument> FilterEq(string field, ObjectId id)
        {
            return Builders<BsonDocument>.Filter.Eq(field, id);
        }

        public static UpdateDefinition<BsonDocument> Update<T>(string field, T value)
        {
            return Builders<BsonDocument>.Update.Push(field, value);
        }
        
    }
}
