using System;
using Microsoft.Xna.Framework;

namespace PongAttempt2
{
    public class CPUPlayer : Player
    {
        private Random random = new Random();
        private Ball ball;

        public CPUPlayer(int playerId, Vector2 position, Vector2 normal, float speed, Ball ball) : base(playerId, position, normal, speed)
        {
            this.ball = ball;
        }
        private float rand(float f) => ((f * 1040.18774f % 1) * 3284.381f) % 1;
        public override void Move(float dt)
        {
            float steepness = MathF.Abs(ball.direction.Y);
            float offset = rand(steepness) - 0.5f;
            
            float deltaY = ball.position.Y - position.Y + Bounds.Height * offset * 1.1f;
            float speedScale = deltaY / 10;
            if (deltaY > 0)
                position.Y += MathHelper.Clamp(ball.direction.Y * ball.speed * speedScale, 0, speed) * dt;
            else if (deltaY < 0)
                position.Y -= MathHelper.Clamp(-ball.direction.Y * ball.speed * -speedScale, 0, speed) * dt;
            ClampYPosition();
        }
    }
}