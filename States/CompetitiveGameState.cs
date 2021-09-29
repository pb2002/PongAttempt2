using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.States
{
    public enum GameMode
    {
        OnePlayer,
        TwoPlayer,
        CPUBattle,
    }
    public class CompetitiveGameState : State
    {
        private Player player1;
        private Player player2;
        private Ball ball;

        private LifeCounter player1LifeCounter;
        private LifeCounter player2LifeCounter;
        
        private AudioClip playerHitSFX;
        private AudioClip wallHitSFX;

        private GameMode gameMode;
        public CompetitiveGameState(PongGame game, ContentManager content, GameMode gameMode) : base(game, content)
        {
            this.gameMode = gameMode;
            
            ball = new Ball(PongGame.screenSize / 2, Vector2.UnitX);
            ball.Reset();

            player1 = gameMode == GameMode.CPUBattle 
                ? new CPUPlayer(0, new Vector2(100, PongGame.screenSize.Y/2), Vector2.UnitX, ball) 
                : new Player(0, new Vector2(100, PongGame.screenSize.Y/2), Vector2.UnitX);
            
            player2 = gameMode == GameMode.TwoPlayer 
                ? new Player(1, new Vector2(PongGame.screenSize.X-100, PongGame.screenSize.Y/2), -Vector2.UnitX) 
                : new CPUPlayer(1, new Vector2(PongGame.screenSize.X-100, PongGame.screenSize.Y/2), -Vector2.UnitX, ball);

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

            playerHitSFX = new AudioClip(content.Load<SoundEffect>("player hit"));
            wallHitSFX = new AudioClip(content.Load<SoundEffect>("wall hit"));
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
                playerHitSFX.Play();
            }

            if (ball.CheckWallCollision())
            {
                wallHitSFX.Play();
            }
            
            if (ball.position.X < 0)
            {
                player1.lives--;
                if (player1.lives == 0)
                {
                    game.SwitchState(new GameOverState(game, content, 1));
                    return;
                }
                Reset();
            }
            else if (ball.position.X > PongGame.screenSize.X)
            {
                player2.lives--;
                if (player2.lives == 0)
                {
                    game.SwitchState(new GameOverState(game, content, 0));
                    return;
                }
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