using System.Security.Cryptography;
using System.Text;

namespace Plugins.GameBoost.Core
{
    public class SHA2Function : IHashFunction
    {
        public byte[] Calculate(string source)
        {
            var stringBytes = Encoding.UTF8.GetBytes(source);
            var hashFunction = new SHA256Managed();
            return hashFunction.ComputeHash(stringBytes);
        }
    }
}
