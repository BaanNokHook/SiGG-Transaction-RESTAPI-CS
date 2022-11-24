using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace WEPAPI_Microservice.DatabaseModels
{
    public class DbToken
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_userId")]
        public string UserId { get; set; } 
        public string Type { get; set; }
        public string Value { get; set; }

        [BsonRepresentation(BsonType.DateTime)]  
        public DateTime Created { get; set; }

        [BsonElement("expiredAt")]
        [JsonIgnore]  
        public DateTime ExpireAt { get; set; }   

    }
}



