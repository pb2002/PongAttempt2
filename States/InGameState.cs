using Microsoft.Xna.Framework;

namespace Pong.States
{
    public enum GameMode
    {
        OnePlayer,
        TwoPlayer,
        AIBattle,
    }
    public class InGameState : State
    {
        private const float playerHorizontalOffset = 100f;
        
        private Player player1;
        private Player player2;
        private Ball ball;

        private LifeCounter player1LifeCounter;
        private LifeCounter player2LifeCounter;

        private Label matchPointLabel;

        private Label pauseLabel;
        private Button continueButton;
        private Button quitButton;
        
        private bool matchPoint = false;
        private bool paused = false;
        public InGameState(PongGame game, GameMode gameMode) : base(game)
        {
            ball = new Ball(Prefs.screenSize / 2, Vector2.UnitX);
            ball.Reset();
            
            // player 1 is only an AI player in AI battle mode
            player1 = gameMode == GameMode.AIBattle 
                ? new AIPlayer(0, new Vector2(playerHorizontalOffset, Prefs.screenSize.Y / 2), Vector2.UnitX, ball) 
                : new Player(0, new Vector2(playerHorizontalOffset, Prefs.screenSize.Y / 2), Vector2.UnitX);
            
            // player 2 is only a regular player in 2 player mode
            player2 = gameMode == GameMode.TwoPlayer 
                ? new Player(1, new Vector2(Prefs.screenSize.X - playerHorizontalOffset, Prefs.screenSize.Y / 2), -Vector2.UnitX) 
                : new AIPlayer(1, new Vector2(Prefs.screenSize.X - playerHorizontalOffset, Prefs.screenSize.Y / 2), -Vector2.UnitX, ball);

            player1LifeCounter = new LifeCounter(player1.position + 256 * Vector2.UnitX, player1);
            player2LifeCounter = new LifeCounter(player2.position - 256 * Vector2.UnitX, player2);
            
            matchPointLabel = new Label(Prefs.screenSize / 2, Vector2.Zero, "MATCH POINT", Renderer.heartColor,
                Assets.subtitleFont);
            
            pauseLabel = new Label(Prefs.screenSize / 2, Vector2.Zero, "PAUSED", Renderer.buttonColor,
                Assets.subtitleFont);
            
            continueButton = new Button(
                Prefs.screenSize / 2 + new Vector2(0,120),
                new Vector2(200, 32),
                "CONTINUE",
                Assets.smallFont
            );
            quitButton = new Button(
                Prefs.screenSize / 2 + new Vector2(0,170),
                new Vector2(110, 32),
                "QUIT",
                Assets.smallFont
            );

            continueButton.OnButtonPressed += () => paused = !paused;
            quitButton.OnButtonPressed += () => game.SwitchState(new MenuState(game));
        }
        public override void Update(GameTime gameTime)
        {
            float dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
            // check for exit input
            if (InputHandler.Instance.Exit)
            {
                // toggle pause menu
                paused = !paused;
                Assets.clickSFX.Play(0.5f);
            }
            if (paused)
            {
                // only update buttons, then return
                continueButton.Update(dt);
                quitButton.Update(dt);
                return;
            }
            ball.Move(dt);
            player1.Move(dt);
            player2.Move(dt);

            if (ball.CheckPlayerCollision(player1) || ball.CheckPlayerCollision(player2))
            {
                // increase ball speed & player speed (/ difficulty for CPU players)
                ball.speed *= Prefs.speedMultiplier;
                player1.IncreaseDifficulty();
                player2.IncreaseDifficulty();
                Assets.playerHitSFX.Play(); // play sound effect
            }

            if (ball.CheckWallCollision())
            {
                Assets.wallHitSFX.Play(); // play sound effect
            }
            CheckGameOver();
        }
        
        public override void Draw(GameTime gameTime)
        {
            // check if the game is paused to prevent overlap of paused title and match point text
            if (matchPoint && !paused)
            {
                matchPointLabel.Draw();
            }
            
            player1LifeCounter.Draw();
            player2LifeCounter.Draw();
            
            player1.Draw();
            player2.Draw();
            ball.Draw();
            
            if (paused)
            {
                // draw a transparent backdrop on top of everything else
                Renderer.Instance.DrawSpriteScaled(Assets.fadeTexture, Vector2.Zero, Prefs.screenSize, Renderer.Instance.currentBackgroundColor * 0.78f);
                
                // draw the pause menu
                pauseLabel.Draw();
                continueButton.Draw();
                quitButton.Draw();
            }
        }
        private void Reset()
        {
            CheckMatchPoint();
            
            player1.Reset();
            player2.Reset();
            ball.Reset();
        }
        private void CheckMatchPoint()
        {
            matchPoint = player1.lives == 1 && player2.lives == 1;
        }
        private void CheckGameOver()
        {
            // check if the ball has fully passed the left side of the screen
            if (ball.position.X < -ball.Size.X/2)
            {
                // player 1 missed
                player1.lives--;
                if (player1.lives == 0)
                {
                    // player 2 (index = 1) wins
                    game.SwitchState(new GameOverState(game, 1));
                    return;
                }
                Reset();
            }
            // check if the ball has fully passed the right side of the screen
            else if (ball.position.X > Prefs.screenSize.X + ball.Size.X/2)
            {
                // player 2 missed
                player2.lives--;
                if (player2.lives == 0)
                {
                    // player 1 (index = 0) wins
                    game.SwitchState(new GameOverState(game, 0));
                    return;
                }
                Reset();
            }
        }
    }
}