using System.Collections.Generic;
using Plugins.GameBoost.Core;

namespace Plugins.GameBoost
{
    internal class JsonKeyGenerator : IKeyGenerator
    {
        private readonly IJsonSerializer jsonSerializer;
        private readonly IHashFunction hashFunction;
        private readonly IDataEncoder dataEncoder;
        private readonly IKeyHashStorage keyHashStorage;

        public JsonKeyGenerator(
            IJsonSerializer jsonSerializer,
            IHashFunction hashFunction,
            IDataEncoder dataEncoder,
            IKeyHashStorage keyHashStorage
        )
        {
            this.jsonSerializer = jsonSerializer;
            this.hashFunction = hashFunction;
            this.dataEncoder = dataEncoder;
            this.keyHashStorage = keyHashStorage;
        }

        public string GenerateKey(Dictionary<string, object> dataObject)
        {
            var jsonString = jsonSerializer.Serialize(dataObject, true);
            var hashBytes = hashFunction.Calculate(jsonString);
            var hashString = dataEncoder.Encode(hashBytes);
            
            keyHashStorage.AddKeyHash(jsonString, hashString);

            return hashString;
        }
    }
}
