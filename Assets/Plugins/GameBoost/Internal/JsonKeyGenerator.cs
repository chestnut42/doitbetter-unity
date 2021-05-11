using System.Collections.Generic;
using Plugins.GameBoost.Core;

namespace Plugins.GameBoost
{
    internal class JsonKeyGenerator : IKeyGenerator
    {
        private readonly IJsonSerializer jsonSerializer;
        private readonly IHashFunction hashFunction;
        private readonly IDataEncoder dataEncoder;

        public JsonKeyGenerator(
            IJsonSerializer jsonSerializer,
            IHashFunction hashFunction,
            IDataEncoder dataEncoder
        )
        {
            this.jsonSerializer = jsonSerializer;
            this.hashFunction = hashFunction;
            this.dataEncoder = dataEncoder;
        }

        public string GenerateKey(Dictionary<string, object> dataObject)
        {
            var jsonString = jsonSerializer.Serialize(dataObject, true);
            var hashBytes = hashFunction.Calculate(jsonString);
            return dataEncoder.Encode(hashBytes);
        }
    }
}
