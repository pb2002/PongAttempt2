using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.States
{
    // TODO: this contains a lot of duplicate code, need to get rid of it
    public class CoopGameState : State
    {
        private Player player1;
        private Player player2;
        private Ball ball;
        
        private LifeCounter lifeCounter;
        
        
        public const float playerOffset = 100f;
        private int lives = 3;
        private int score = 0;
        public CoopGameState(PongGame game) : base(game)
        {
            ball = new Ball(PongGame.screenSize / 2, Vector2.UnitX);
            ball.Reset();
            
            player1 = new Player(0, new Vector2(playerOffset, PongGame.screenSize.Y / 2), Vector2.UnitX);
            player2 = new Player(1, new Vector2(PongGame.screenSize.X - playerOffset, PongGame.screenSize.Y / 2), -Vector2.UnitX);

            lifeCounter = new LifeCounter(PongGame.screenSize / 2, 16);
        }
        private void Reset()
        {
            player1.Reset();
            player2.Reset();
            ball.Reset();
        }
        public override void Update(GameTime gameTime)
        {
            var dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
            
            ball.Move(dt);
            player1.Move(dt);
            player2.Move(dt);

            if (ball.CheckPlayerCollision(player1) || ball.CheckPlayerCollision(player2))
            {
                ball.speed *= 1.02f;
                player1.IncreaseDifficulty();
                player2.IncreaseDifficulty();
                score++;
                
                Assets.playerHitSFX.Play();
            }

            if (ball.CheckWallCollision())
            {
                Assets.wallHitSFX.Play();
            }
            
            if (ball.position.X < 0 || ball.position.X > PongGame.screenSize.X)
            {
                lives--;
                if (lives == 0)
                {
                    game.SwitchState(new CoopGameOverState(game, score));
                    return;
                }
                Reset();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            lifeCounter.Draw(lives);
            player1.Draw();
            player2.Draw();
            ball.Draw();
        }
    }
}