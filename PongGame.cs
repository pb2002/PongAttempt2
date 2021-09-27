using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongAttempt2
{
    public class PongGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static Vector2 screenSize = new Vector2(1280, 720);
        public static float playerSpeed = 300f;
        public static float ballSpeed = 300f;
        
        private static Rectangle screenBounds;
        
        private Player player1;
        private Player player2;

        private Ball ball;
        
        public PongGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
            _graphics.PreferMultiSampling = true;
            
            _graphics.PreferredBackBufferWidth = (int)screenSize.X;
            _graphics.PreferredBackBufferHeight = (int)screenSize.Y;
            
            screenBounds = new Rectangle(Point.Zero, screenSize.ToPoint());
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ball = new Ball(screenSize / 2, Vector2.UnitX, ballSpeed);
            
            player1 = new Player(0, new Vector2(100, screenSize.Y/2), playerSpeed);
            player2 = new CPUPlayer(1, new Vector2(screenSize.X-100, screenSize.Y/2), playerSpeed, ball);
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Renderer.instance.spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // Load textures
            player1.sprite = Content.Load<Texture2D>("player");
            ball.sprite = Content.Load<Texture2D>("ball");
            player2.sprite = player1.sprite;
        }

        private void Reset()
        {
            ball.speed = ballSpeed;
            player1.speed = playerSpeed;
            player2.speed = playerSpeed;
            ball.Reset();
        }
        protected override void Update(GameTime gameTime)
        {
            var dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
            
            // Update Input
            InputHandler.instance.UpdateInput();
            
            if (InputHandler.instance.Exit)
                Exit();
            
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
                
                Reset();
            }
            else if (ball.position.X > screenSize.X)
            {
                player2.lives--;
                
                Reset();
            }
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            Renderer.instance.Begin();

            player1.Draw();
            player2.Draw();
            ball.Draw();
            Renderer.instance.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}