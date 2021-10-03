using Microsoft.Xna.Framework;

namespace Pong
{
    public static class Prefs
    {
        /// <summary>
        /// The desired screen size
        /// </summary>
        public static readonly Vector2 screenSize = new Vector2(1600, 900);
        /// <summary>
        /// The starting speed of the player and ball
        /// </summary>
        public const float baseSpeed = 300f;
        /// <summary>
        /// The value by which the player and ball speed gets multiplied on every player collision
        /// </summary>
        public const float speedMultiplier = 1.05f;
        /// <summary>
        /// The currently selected difficulty
        /// </summary>
        public static int difficulty = 0;
        
        private static readonly float[] baseDeviationPresets = { 0.87f, 0.8f, 1.0f, 0.95f };
        private static readonly float[] deviationMultiplierPresets = { speedMultiplier * 0.9998f, speedMultiplier * 0.995f, speedMultiplier * 0.98f, 1f };
        /// <summary>
        /// The starting deviation (clumsiness) of the AI player
        /// </summary>
        public static float BaseDeviation => baseDeviationPresets[difficulty];
        public static float DeviationMultiplier => deviationMultiplierPresets[difficulty];
    }
}