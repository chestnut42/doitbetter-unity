using System;

namespace Plugins.GameBoost.Core
{
    public class Base64DataEncoder : IDataEncoder
    {
        public string Encode(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }
    }
}
