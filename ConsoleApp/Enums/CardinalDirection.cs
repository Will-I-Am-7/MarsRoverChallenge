using System.ComponentModel;

namespace MarsRoverChallenge.ConsoleApp.Enums
{
    public enum CardinalDirection
    {
        [Description("North")]
        N = 0,

        [Description("East")]
        E = 90,

        [Description("South")]
        S = 180,

        [Description("West")]
        W = 270
    }
}
