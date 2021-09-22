using System.Collections.Generic;
using Plugins.GameBoost.Core;

namespace Plugins.GameBoost
{
    internal class GameTracker : IGameTracker
    {
        private IKeyGenerator keyGenerator;

        private readonly IEventTracker eventTracker;

        public GameTracker(
            IEventTracker eventTracker,
            IKeyHashStorage keyHashStorage
        )
        {
            this.eventTracker = eventTracker;
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
                keyGenerator.GenerateKey(balance, KeyHashType.Balance));
        }
    }
}
