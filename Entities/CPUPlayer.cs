using System;
using Microsoft.Xna.Framework;

namespace Pong
{
    public class CPUPlayer : Player
    {
        private float deviation;
        private static Random random = new Random();
        private Ball ball;
        private Vector2 renderPosition;
        private float seed;
        public CPUPlayer(int playerId, Vector2 position, Vector2 normal, Ball ball) : base(playerId, position, normal)
        {
            this.ball = ball;
            renderPosition = position;
            
            deviation = Prefs.BaseDeviation;

            seed = (float)random.NextDouble() * 231.85837f + 197.435f;
        }
        // pseudo RNG mapping
        private float Rand(float f) => ((f * seed % 1) * 3284.381f) % 1;
        public override void Move(float dt)
        {
            // don't move when the ball is moving away from this player
            if (Vector2.Dot(ball.direction, normal) > 0) return;
            
            // generate a random offset based on the Y direction of the ball.
            float steepness = MathF.Abs(ball.direction.Y);
            float offset = Bounds.Height * deviation * (Rand(steepness) - 0.5f);
            
            // calculate the height difference between the ball and the target hit point
            // (target hit point = player position + offset)
            float deltaY = ball.position.Y - position.Y + offset;
            
            // scales down fine adjustment speed based on the height difference
            float speedScale = deltaY / 30;
            // make large adjustments when height difference is larger than or opposing the dy vector of the ball
            if (deltaY > 30)
                position.Y += speed * dt;
            else if (deltaY < -30)
                position.Y -= speed * dt;
            // otherwise make fine adjustments
            else
                position.Y += MathHelper.Clamp(MathF.Abs(ball.direction.Y) * ball.speed * speedScale, -speed, speed) * dt;
            // smooth out rendered position to remove jitter using accumulative lerp
            renderPosition = Vector2.Lerp(renderPosition, position, 30 * dt);
            ClampYPosition();
        }

        public override void IncreaseDifficulty()
        {
            deviation *= Prefs.DeviationMultiplier;
            base.IncreaseDifficulty();
        }

        public override void Reset()
        {
            deviation = Prefs.BaseDeviation;
            base.Reset();
        }

        public override void Draw()
        {
            Renderer.instance.DrawSpriteCentered(sprite, renderPosition, playerColors[playerId], playerId == 1);
        }
    }
}