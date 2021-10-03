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

        // Fade effect timer
        public Timer fadeTimer = new Timer(0.5f);

        private State currentState;
        private State nextState;

        public void SwitchState(State state)
        {
            // set the next state
            nextState = state;
            // reset the timer to start fade effect
            fadeTimer.Reset();
        }
        
        public PongGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
            // set screen resolution
            _graphics.PreferredBackBufferWidth = (int)Prefs.screenSize.X;
            _graphics.PreferredBackBufferHeight = (int)Prefs.screenSize.Y;
        }
        
        protected override void Initialize()
        {
            base.Initialize();
            // start at menu state
            currentState = new MenuState(this);
        }

        protected override void LoadContent()
        {
            Renderer.Instance.spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // Load all assets
            Assets.playerTexture = Content.Load<Texture2D>("sprites/player");
            Assets.ballTexture = Content.Load<Texture2D>("sprites/ball");
            Assets.heartTexture = Content.Load<Texture2D>("sprites/heart");
            Assets.fadeTexture = Content.Load<Texture2D>("sprites/fade");

            Assets.titleFont = Content.Load<SpriteFont>("fonts/titleFont");
            Assets.subtitleFont = Content.Load<SpriteFont>("fonts/subtitleFont");

            Assets.playerHitSFX = new AudioClip(Content.Load<SoundEffect>("sfx/player hit"));
            Assets.wallHitSFX = new AudioClip(Content.Load<SoundEffect>("sfx/wall hit"));
            Assets.music = new AudioClip(Content.Load<SoundEffect>("music/music"));
        }


        protected override void Update(GameTime gameTime)
        {
            // update the timer (gameTime.ElapsedGameTime.TotalSeconds = time of last frame)
            fadeTimer.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            // Update Input
            InputHandler.Instance.UpdateInput();

            // If there is a state to be loaded
            if (nextState != null)
            {
                // wait for the fade effect to finish
                if (fadeTimer) return;
                // update the current state
                currentState = nextState;
                // reset the timer to start fade in effect
                fadeTimer.Reset();
                nextState = null;
            }
            else
            {
                // update the current state
                // don't update during the fade out effect so the game pauses when switching states
                currentState.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            Renderer.Instance.Begin();
            currentState.Draw(gameTime);
            // fade effect (check if the fade timer is running)
            if (fadeTimer)
            {
                // if the next state is null, we have changed states already, so we should
                // apply a fade out effect.
                // if not, we should apply a fade in effect instead.
                float alpha = nextState == null 
                    ? fadeTimer.Time / fadeTimer.MaxTime // divide by MaxTime to get a number between 0 and 1
                    : 1f - fadeTimer.Time / fadeTimer.MaxTime;
                
                // draw the fade rectangle with transparency
                Renderer.Instance.DrawSpriteScaled(Assets.fadeTexture, Vector2.Zero, Prefs.screenSize, Color.White * alpha);
                
            }
            
            Renderer.Instance.End();
            
            base.Draw(gameTime);
        }
    }
}