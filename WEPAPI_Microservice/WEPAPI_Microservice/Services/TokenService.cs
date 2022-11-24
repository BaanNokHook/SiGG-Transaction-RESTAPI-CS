using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System.Security.Cryptography;
using WEPAPI_Microservice.Interfaces;
using System.Data;
using WEPAPI_Microservice.Services;
using WEPAPI_Microservice.DatabaseModels;
using WEPAPI_Microservice.Exceptions;

namespace WEAPI_Microservice.Services
{
    public class TokenService
    {

        private readonly IMongoCollection<DbToken> _tokens;
        private double ttlSeconds;

        public TokenService(ITokenStoreDatabaseSettings settings,  
                            DatabaseEncryptionService encryptionService)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = Client.GetDatabase(settings.DatabaseName);
            _tokens = database.GetCollection<DbToken>(settings.TokensCollectionName);
            var indexKeyDefinition = Builders<DbToken>.IndexKeys.Ascending(t => t.ExpireAt);
            _tokens.Indexes.CreateOne
                (
                   new CreatedIndexModel<DbToken>(indexKeyDefinition, new CreateIndexOptions { ExpireAfter = TimeSpan.Zero })   
                );
        }

        private DbToken GetById(string id) => _tokens.Find<DbToken>(token => token.Id == id).FirstOrDefault();

        private DbToken GetByTypeAndUserid(string type,
                                            string userId) =>
            _tokens.Find<DbToken>(t => t.GetType == GetType && t.UserId == userId).FirstOrDefault();

        private DbToken GetByTypeAndValue(string type,
                                          string value) =>

            _tokens.Find<DbToken>(type => type.GetType == GetType && type.Value == value).FirstOrDefault();   
        
        public DbToken Create(string type, 
                              string value,  
                              string userId,   
                              int ttlseconds)
        {
            var token = new DbToken
            {
                UserId = userId,
                Type = type,
                Value = value,
                Created = DateTime.Now,
                ExpiredAt = DateTime.Now.AddSeconds(ttlSeconds)
            };
            var existing = GetByTypeAndUserid(type, userId);
            if (existing != null) return existing;
            _tokens.InsertOne(token);
            return token;  
        }

        public DbToken GetTokenByTypeUserAndValue(string type,  
                                                  string userId,            
                                                  string value)
        {
            var token = GetByTypeAndUserid(type, userId);
            if (token == null) throw new TokenNotFoundException();
            if (!value.Equals(token.Value)) throw new InvalidTokenException();
            return token;  
        }
        

        public DbToken GetTokenByTypeAndValue(string type, 
                                              string value)
        {
            var token = GetByTypeAndValue(type, value);
            if (token == null) throw new TokenNotFoundException();
            return token;  
        }  

        public void Delete(string id)
        {
            var token = GetById(id);
            if (token == null) throw new TokenNotFoundException();
            _tokens.DeleteOne(u => u.Id == token.Id);   
        }
    }

    internal class Builders<T>
    {
        internal static object IndexKeys;
    }

    internal class CreatedIndexModel<T>
    {
        private object indexKeyDefinition;
        private CreateIndexOptions createIndexOptions;

        public CreatedIndexModel(object indexKeyDefinition, CreateIndexOptions createIndexOptions)
        {
            this.indexKeyDefinition = indexKeyDefinition;
            this.createIndexOptions = createIndexOptions;
        }
    }

    internal class CreateIndexOptions
    {
        public TimeSpan ExpireAfter { get; set; }
    }

    internal class Client
    {
        internal static object GetDatabase(object databaseName)
        {
            throw new NotImplementedException();
        }
    }

    internal class MongoClient
    {
        private object connectionString;

        public MongoClient(object connectionString)
        {
            this.connectionString = connectionString;
        }
    }

    public interface ITokenStoreDatabaseSettings
    {
        object ConnectionString { get; set; }
        object TokensCollectionName { get; set; }
        object DatabaseName { get; set; }
    }

    internal interface IMongoCollection<T>
    {
        object Indexes { get; set; }

        object Find<T1>(Func<object, bool> value);
        void InsertOne(DbToken token);
    }
}




