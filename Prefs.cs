using Microsoft.Xna.Framework;
using Pong.States;

namespace Pong
{
    public static class Prefs
    {
        public static readonly Vector2 screenSize = new Vector2(1600, 900);
        public static readonly float baseSpeed = 300f;
        public static readonly float speedMultiplier = 1.03f;
        public static int difficulty = 0;
        public static readonly float[] baseDeviationPresets = new[] { 0.85f, 0.8f, 1.1f, 0.3f };
        public static readonly float[] deviationMultiplierPresets = new[] { 1.015f, 1.0125f, 1.0032f, 1f };
        public static float BaseDeviation => baseDeviationPresets[difficulty];
        public static float DeviationMultiplier => deviationMultiplierPresets[difficulty];
    }
}