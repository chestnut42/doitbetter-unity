using System.Collections;

namespace Plugins.GameBoost
{
    public interface IAsyncResult<Result>
    {
        public Result CommandResult { get; }
        public bool IsDone { get; }

        IEnumerator Run();
    }
}
