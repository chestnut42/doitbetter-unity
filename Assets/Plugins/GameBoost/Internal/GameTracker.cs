using System.Collections.Generic;
using Plugins.GameBoost.Core;

namespace Plugins.GameBoost
{
    internal class GameTracker : IGameTracker
    {
        private readonly IKeyGenerator keyGenerator = new JsonKeyGenerator(
            new JsonSerializer(),
            new SHA2Function(),
            new Base64DataEncoder()
        );

        private readonly IEventTracker eventTracker;

        public GameTracker(
            IEventTracker eventTracker
        )
        {
            this.eventTracker = eventTracker;
        }


        public IGame CreateGame(
            Dictionary<string, object> balance
        )
        {
            return new GameInstance(
                keyGenerator,
                eventTracker,
                keyGenerator.GenerateKey(balance));
        }
    }
}
