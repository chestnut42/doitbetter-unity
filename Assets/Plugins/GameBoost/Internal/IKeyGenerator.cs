using System.Collections.Generic;

namespace Plugins.GameBoost
{
    internal interface IKeyGenerator
    {
        string GenerateKey(Dictionary<string, object> dataObject);
    }
}
