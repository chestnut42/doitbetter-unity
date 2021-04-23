using System.Collections.Generic;

namespace Plugins.GameBoost.Core
{
    public interface IJsonSerializer
    {
        string Serialize(Dictionary<string, object> json, bool doSort);
    }
}
