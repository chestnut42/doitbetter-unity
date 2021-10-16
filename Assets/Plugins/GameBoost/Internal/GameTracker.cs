using System.Collections.Generic;
using Plugins.GameBoost.Core;

namespace Plugins.GameBoost
{
    internal class GameTracker : IGameTracker
    {
        private IKeyGenerator keyGenerator;

        private readonly IEventTracker eventTracker;

        private readonly IGameParamsRequest gameParamsRequest;


        public GameTracker(
            IEventTracker eventTracker,
            IGameParamsRequest gameParamsRequest,
            IKeyHashStorage keyHashStorage
        )
        {
            this.eventTracker = eventTracker;
            this.gameParamsRequest = gameParamsRequest;
            this.keyGenerator = new JsonKeyGenerator(
                new JsonSerializer(),
                new SHA2Function(),
                new Base64DataEncoder(),
                keyHashStorage
            );
        }


        public IGame CreateGame(
            Dictionary<string, object> balance
        )
        {
            return new GameInstance(
                keyGenerator,
                eventTracker,
                gameParamsRequest,
                keyGenerator.GenerateKey(balance));
        }
    }
}
