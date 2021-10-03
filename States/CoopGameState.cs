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
        private Label scoreLabel;
        
        public const float playerOffset = 100f;
        private int lives = 3;
        private int score = 0;
        public CoopGameState(PongGame game) : base(game)
        {
            ball = new Ball(Prefs.screenSize / 2, Vector2.UnitX);
            ball.Reset();
            
            player1 = new Player(0, new Vector2(playerOffset, Prefs.screenSize.Y / 2), Vector2.UnitX);
            player2 = new Player(1, new Vector2(Prefs.screenSize.X - playerOffset, Prefs.screenSize.Y / 2), -Vector2.UnitX);

            lifeCounter = new LifeCounter(Prefs.screenSize / 2, 16);
            scoreLabel = new Label(Prefs.screenSize / 2 + new Vector2(0, 100), Vector2.Zero, "0", Renderer.buttonColor,
                Assets.subtitleFont);
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
                // increase ball speed & player speed (/ difficulty for CPU players)
                ball.speed *= Prefs.difficulty; 
                player1.IncreaseDifficulty(); 
                player2.IncreaseDifficulty();
                score++;
                scoreLabel.text = $"{score}";
                Assets.playerHitSFX.Play(); // Play sound effect
            }

            if (ball.CheckWallCollision())
            {
                Assets.wallHitSFX.Play(); // play sound effect
            }
            
            if (ball.position.X < 0 || ball.position.X > Prefs.screenSize.X)
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
            scoreLabel.Draw();
            player1.Draw();
            player2.Draw();
            ball.Draw();
        }
    }
}