using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.States;


namespace Pong
{
    public class PongGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static Vector2 screenSize = new Vector2(1600, 900);

        public Timer fadeTimer = 0.5f;
        
        
        private State currentState;
        private State nextState;

        public void SwitchState(State state)
        {
            nextState = state;
            fadeTimer.Reset();
        }
        
        public PongGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = (int)screenSize.X;
            _graphics.PreferredBackBufferHeight = (int)screenSize.Y;
        }
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            currentState = new MenuState(this);
        }

        protected override void LoadContent()
        {
            Renderer.instance.spriteBatch = new SpriteBatch(GraphicsDevice);
            
            Assets.playerTexture = Content.Load<Texture2D>("sprites/player");
            Assets.ballTexture = Content.Load<Texture2D>("sprites/ball");
            Assets.heartTexture = Content.Load<Texture2D>("sprites/heart");
            Assets.fadeTexture = Content.Load<Texture2D>("sprites/fade");

            Assets.titleFont = Content.Load<SpriteFont>("fonts/titleFont");
            Assets.subtitleFont = Content.Load<SpriteFont>("fonts/subtitleFont");

            Assets.playerHitSFX = new AudioClip(Content.Load<SoundEffect>("sfx/player hit"));
            Assets.wallHitSFX = new AudioClip(Content.Load<SoundEffect>("sfx/wall hit"));
        }


        protected override void Update(GameTime gameTime)
        {
            fadeTimer.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            // Update Input
            InputHandler.instance.UpdateInput();

            if (nextState != null)
            {
                if (fadeTimer) return;
                currentState = nextState;
                fadeTimer.Reset();
                nextState = null;
            }
            else
            {
                currentState.Update(gameTime);
            }
            
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            Renderer.instance.Begin();
            currentState.Draw(gameTime);
            if (fadeTimer)
            {
                float alpha = nextState == null 
                    ? fadeTimer.Time / fadeTimer.MaxTime 
                    : 1f - fadeTimer.Time / fadeTimer.MaxTime;
                Renderer.instance.DrawSpriteScaled(Assets.fadeTexture, Vector2.Zero, screenSize, Color.White * alpha);
                
            }
            Renderer.instance.DrawText(Assets.subtitleFont, $"{fadeTimer.Time:0.00}", new Vector2(100, 100), Color.Black);    
            Renderer.instance.End();
            
            base.Draw(gameTime);
        }
    }
}