using System;
using System.Collections;

namespace Plugins.GameBoost
{
    public class AsyncResult<Params, Result>
    {
        public delegate void AsyncCommand(Params parameters, Action<Result> callMethod);
        
        private Params parameters;
        private Result commandResult;
        private Boolean isDone;
        private AsyncCommand command;
        
        public Result CommandResult => commandResult;
        public Boolean IsDone => isDone;
        
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