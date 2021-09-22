using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PongAttempt2
{
    public class PongGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Vector2 screenSize = new Vector2(1280, 720);
        
        public Player player1;
        public Player player2;

        public Ball ball;
        
        public PongGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
            _graphics.PreferMultiSampling = true;
            
            _graphics.PreferredBackBufferWidth = (int)screenSize.X;
            _graphics.PreferredBackBufferHeight = (int)screenSize.Y;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player1 = new Player(new Vector2(100, screenSize.Y/2), 0);
            player2 = new Player(new Vector2(screenSize.X-100, screenSize.Y/2), 1);
            ball = new Ball(screenSize/2+new Vector2(0,30), Vector2.UnitX, 100);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Renderer.instance.spriteBatch = new SpriteBatch(GraphicsDevice);
            player1.sprite = Content.Load<Texture2D>("player");
            ball.sprite = Content.Load<Texture2D>("ball");
            player2.sprite = player1.sprite;
            
            // TODO: use this.Content to load your game content here
        }
        
        protected override void Update(GameTime gameTime)
        {
            var dt = (float) gameTime.ElapsedGameTime.TotalSeconds;
            InputHandler.instance.UpdateInput();
            
            if (InputHandler.instance.Exit)
                Exit();
            
            var player1Bounds = player1.sprite.Bounds;
            var player2Bounds = player2.sprite.Bounds;
            player1Bounds.Offset(player1.position);
            player2Bounds.Offset(player2.position);
            var ballBounds = ball.sprite.Bounds;
            ballBounds.Offset(ball.position);
            if (ballBounds.Intersects(player1Bounds))
            {
                var heightDiff = ball.position.Y - player1.position.Y;
                ball.direction.X = 1;
                ball.direction.Y = 1.5f * heightDiff / player1Bounds.Height;
            }

            if (ballBounds.Intersects(player2Bounds))
            {
                var heightDiff = ball.position.Y - player2.position.Y;
                ball.direction.X = -1;
                ball.direction.Y = 1.5f * heightDiff / player1Bounds.Height;
            }

            ball.position += ball.direction * ball.speed * dt;
            // TODO: Add your update logic here

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