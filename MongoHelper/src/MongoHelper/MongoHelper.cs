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
        protected static ILogger _logger;

        #region Constructors
        public MongoHelper(string dbname)
        {
            _client = new MongoClient();
            _database = _client.GetDatabase(dbname);
        }

        public MongoHelper(string dbname, ILogger log)
        {
            _client = new MongoClient();
            _database = _client.GetDatabase(dbname);
            _logger = log;
        }
        #endregion

        #region Select
        #region Select Count
        /// <summary>
        /// Returns the count of all recors in the specified collection
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public long? SelectCount(string collectionName)
        {
            try
            {
                var collection = _database.GetCollection<BsonDocument>(collectionName);
                return collection.Find(new BsonDocument()).Count();
            }
            catch(Exception ex)
            {
                this.WriteError("SelectCount", "SelectCount(string collectionName)", ex.Message);
                return null; 
            }
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
        /// <summary>
        /// Insert a BsonDocument 
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Insert an object of any type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        public bool Insert<T>(string collectionName, T doc)
        {
            try
            {
                var collection = _database.GetCollection<BsonDocument>(collectionName);
                collection.InsertOne(doc.ToBsonDocument());
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Insert multiple objects into a collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="documents"></param>
        /// <returns></returns>
        public bool InsertMany<T>(string collectionName, IEnumerable<T> documents)
        {
            try
            {
                List<BsonDocument> docs = new List<BsonDocument>();
                for (int i = 0; i < documents.Count(); i++)
                {
                    docs[i] = documents.ElementAt(i).ToBsonDocument();
                }
                var collection = _database.GetCollection<BsonDocument>(collectionName);
                collection.InsertMany(docs);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Update
        /// <summary>
        /// Update an object
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Update an Array inside an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName"></param>
        /// <param name="arrayField"></param>
        /// <param name="list"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
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

        #region private
        private void WriteError(string title, string function, string message)
        {
            if (_logger != null)
                _logger.WriteError(title, function, message);
        }
        #endregion

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
