using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PongAttempt2.States
{
    public class TwoPlayerGameState : State
    {
        private Player player1;
        private Player player2;
        private Ball ball;

        private LifeCounter player1LifeCounter;
        private LifeCounter player2LifeCounter;
        
        public const float playerSpeed = 300f;
        public const float ballSpeed = 300f;
        
        
        public TwoPlayerGameState(PongGame game, ContentManager content) : base(game, content)
        {
            ball = new Ball(PongGame.screenSize / 2, Vector2.UnitX, ballSpeed);
            
            player1 = new Player(0, new Vector2(100, PongGame.screenSize.Y/2), Vector2.UnitX, playerSpeed);
            player2 = new Player(1, new Vector2(PongGame.screenSize.X-100, PongGame.screenSize.Y/2), -Vector2.UnitX, playerSpeed);

            player1LifeCounter = new LifeCounter(player1.position + 256 * Vector2.UnitX, 16);
            player2LifeCounter = new LifeCounter(player2.position - 256 * Vector2.UnitX, 16);
        }

        public override void LoadContent()
        {
            player1.sprite = content.Load<Texture2D>("player");
            ball.sprite = content.Load<Texture2D>("ball");
            player2.sprite = player1.sprite;
            player1LifeCounter.sprite = content.Load<Texture2D>("heart");
            player2LifeCounter.sprite = player1LifeCounter.sprite;
        }
        private void Reset()
        {
            ball.speed = ballSpeed;
            player1.speed = playerSpeed;
            player2.speed = playerSpeed;
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
                ball.speed *= 1.05f;
                player1.speed *= 1.05f;
                player2.speed *= 1.05f;
            }

            if (ball.CheckWallCollision())
            {
                
            }
            
            if (ball.position.X < 0)
            {
                player1.lives--;
                if (player1.lives == 0)
                    game.SwitchState(new GameOverState(game, content, 0));
                Reset();
            }
            else if (ball.position.X > PongGame.screenSize.X)
            {
                player2.lives--;
                if (player2.lives == 0)
                    game.SwitchState(new GameOverState(game, content, 1));
                Reset();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            player1LifeCounter.Draw(player1.lives);
            player2LifeCounter.Draw(player2.lives);
            player1.Draw();
            player2.Draw();
            ball.Draw();
        }
    }
}