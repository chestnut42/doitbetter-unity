using System.Collections.Generic;

namespace Plugins.GameBoost.Core
{
    public interface IHashFunction
    {
        byte[] Calculate(string source);
    }
}
