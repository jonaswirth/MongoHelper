using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoHelper
{
    public class MongoHelper : IDisposable
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        #region Constructors
        public MongoHelper(string dbname)
        {
            _client = new MongoClient();
            _database = _client.GetDatabase(dbname);
        }
        #endregion

        #region Select
        #region Select Count
        /// <summary>
        /// Returns the count of all recors in the specified collection
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public long SelectCount(string collectionName)
        {
            var collection = _database.GetCollection<BsonDocument>(collectionName);
            return collection.Find(new BsonDocument()).Count();
        }
        /// <summary>
        /// Select count 
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public long SelectCount(string collectionName, FilterDefinition<BsonDocument> filter)
        {
            var collection = _database.GetCollection<BsonDocument>(collectionName);
            return collection.Find(filter).Count();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="field"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public long SelectCount(string collectionName, string field, string value)
        {
            var collection = _database.GetCollection<BsonDocument>(collectionName);
            return collection.Find(Builder.FilterEq(field, value)).Count();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long SelectCount(string collectionName, string field, ObjectId id)
        {
            var collection = _database.GetCollection<BsonDocument>(collectionName);
            return collection.Find(Builder.FilterEq(field, id)).Count();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long SelectCount<T>(string collectionName, string field, T value)
        {
            var collection = _database.GetCollection<BsonDocument>(collectionName);
            return collection.Find(Builder.FilterEq<T>(field, value)).Count();
        }
        #endregion
        /// <summary>
        /// Returns a list of the given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<T> Select<T>(string collectionName, FilterDefinition<BsonDocument> filter)
        {
            var collection = _database.GetCollection<BsonDocument>(collectionName);
            var result = collection.Find(filter).ToList();
            List<T> returnList = new List<T>();
            foreach (var l in result)
            {
                returnList.Add(BsonSerializer.Deserialize<T>(l));
            }
            return returnList;
        }
        /// <summary>
        /// Select a single record of the given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public T SelectOne<T>(string collectionName, FilterDefinition<BsonDocument> filter)
        {
            var collection = _database.GetCollection<BsonDocument>(collectionName);
            var result = collection.Find(filter).ToList();
            if (result.Count > 1)
            {
                throw new Exception("To many results");
            }
            return BsonSerializer.Deserialize<T>(result.ElementAt(0));
        }
        #endregion

        #region Insert
        public bool Insert(string collectionName, BsonDocument doc)
        {
            try
            {
                var collection = _database.GetCollection<BsonDocument>(collectionName);
                collection.InsertOne(doc);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Update
        public bool UpdateOne(string collectionName, FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            try
            {
                var collection = _database.GetCollection<BsonDocument>(collectionName);
                collection.UpdateOne(filter, update);
                return true;
            }
            catch
            {
                return false;
            }
        }
        

        public bool UpdateArray<T>(string collectionName, string arrayField, List<T> list, FilterDefinition<BsonDocument> filter)
        {
            try
            {
                var collection = _database.GetCollection<BsonDocument>(collectionName);
                var update = Builders<BsonDocument>.Update.PushEach(arrayField, list);
                collection.FindOneAndUpdate<BsonDocument>(filter, update);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
