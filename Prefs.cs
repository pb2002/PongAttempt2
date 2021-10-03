using Microsoft.Xna.Framework;
using Pong.States;

namespace Pong
{
    public static class Prefs
    {
        public static readonly Vector2 screenSize = new Vector2(1600, 900);
        public static readonly float baseSpeed = 300f;
        public static readonly float speedMultiplier = 1.02f;
        public static int difficulty = 0;
        public static readonly float[] baseDeviationPresets = new[] { 0.85f, 0.8f, 1.0f, 0.95f };
        public static readonly float[] deviationMultiplierPresets = new[] { 1.0132f, 1.01f, 1.0025f, 1f };
        public static float BaseDeviation => baseDeviationPresets[difficulty];
        public static float DeviationMultiplier => deviationMultiplierPresets[difficulty];
    }
}