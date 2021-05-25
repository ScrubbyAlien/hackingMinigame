using System.Collections.Generic;

namespace Utilities
{
    public struct ContentManager
    {
        public enum Thing
        {
            Pipe,
            Bomb,
            Start
        }

        public static IDictionary<string, Thing> stringToEnum = new Dictionary<string, Thing>()
        {
            {"pipe", Thing.Pipe},
            {"bomb", Thing.Bomb},
            {"start", Thing.Start}
        };
    }
}
