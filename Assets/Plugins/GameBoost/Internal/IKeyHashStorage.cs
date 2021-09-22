using System.Collections.Generic;

namespace Plugins.GameBoost
{
    internal interface IKeyHashStorage
    {
        void AddKeyHash(string keyValue, string hashValue, KeyHashType type);
    }
}