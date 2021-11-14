using System;
using System.Collections;

namespace Plugins.GameBoost
{
    internal class AsyncResult<Params, Result> : IAsyncResult<Result>
    {
        public delegate void AsyncCommand(Params parameters, Action<Result> callMethod);

        private Params parameters;
        private Result commandResult;
        private bool isDone;
        private AsyncCommand command;

        public Result CommandResult => commandResult;
        public bool IsDone => isDone;

        public AsyncResult(Params parameters, AsyncCommand command)
        {
            this.parameters = parameters;
            this.command = command;
        }
        public IEnumerator Run()
        {
            if (!isDone)
            {
                command(parameters, result =>
                {
                    commandResult = result;
                    isDone = true;
                });

                while (!isDone)
                {
                    yield return null;
                }
            }
        }
    }
}
