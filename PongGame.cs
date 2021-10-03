
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Pong.States;


namespace Pong
{
    public class PongGame : Game
    {
        private readonly GraphicsDeviceManager graphics;        

        // Fade effect timer
        public readonly Timer fadeTimer = new Timer(0.5f);
        public const float musicFadeTime = 2f;

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
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = false;
            graphics.SynchronizeWithVerticalRetrace = true;
            
            
            // set screen resolution
            graphics.PreferredBackBufferWidth = (int)Prefs.screenSize.X;
            graphics.PreferredBackBufferHeight = (int)Prefs.screenSize.Y;
        }
        
        protected override void Initialize()
        {
            base.Initialize();
            // start at menu state
            
            Assets.musicTopLayer.Play(0f, true);
            Assets.musicBaseLayer.Play(0f, true);
            Assets.musicTopLayer.FadeIn(musicFadeTime);
            Assets.musicBaseLayer.FadeIn(musicFadeTime);
            
            currentState = new MenuState(this);
        }

        protected override void LoadContent()
        {
            Renderer.Instance.spriteBatch = new SpriteBatch(GraphicsDevice);
            Renderer.Instance.graphicsDevice = GraphicsDevice;
            // Load all assets
            Assets.playerTexture = Content.Load<Texture2D>("sprites/player");
            Assets.ballTexture = Content.Load<Texture2D>("sprites/ball");
            Assets.heartTexture = Content.Load<Texture2D>("sprites/heart");
            Assets.fadeTexture = Content.Load<Texture2D>("sprites/fade");

            Assets.titleFont = Content.Load<SpriteFont>("fonts/titleFont");
            Assets.subtitleFont = Content.Load<SpriteFont>("fonts/subtitleFont");
            Assets.smallFont = Content.Load<SpriteFont>("fonts/smallFont");

            Assets.playerHitSFX = new AudioClip(Content.Load<SoundEffect>("sfx/player-hit"));
            Assets.wallHitSFX = new AudioClip(Content.Load<SoundEffect>("sfx/wall-hit"));
            Assets.clickSFX = new AudioClip(Content.Load<SoundEffect>("sfx/click"));
            Assets.musicBaseLayer = new AudioClip(Content.Load<SoundEffect>("music/music-base-layer"));
            Assets.musicTopLayer = new AudioClip(Content.Load<SoundEffect>("music/music-top-layer"));
        }


        protected override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            // update the timer
            fadeTimer.Update(dt);

            Assets.musicBaseLayer.Update(dt);
            Assets.musicTopLayer.Update(dt);
            
            // Update Input (only when window is in focus)
            if(IsActive) InputHandler.Instance.UpdateInput();

            // If there is a state to be loaded and fade transition has finished
            if (nextState != null && !fadeTimer.IsRunning)
            {

                // update the current state
                currentState = nextState;
                // reset the timer to start fade in effect
                fadeTimer.Reset();
                nextState = null;
            }
            // update the current state
            currentState.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Renderer.Instance.DrawBG((float)gameTime.ElapsedGameTime.TotalSeconds);
            Renderer.Instance.Begin();
            
            currentState.Draw(gameTime);
            // fade effect (check if the fade timer is running)
            if (fadeTimer.IsRunning)
            {
                // if the next state is null, we have changed states already, so we should
                // apply a fade out effect.
                // if not, we should apply a fade in effect instead.
                float alpha = nextState == null 
                    ? fadeTimer.Time / fadeTimer.MaxTime // divide by MaxTime to get a number between 0 and 1
                    : 1f - fadeTimer.Time / fadeTimer.MaxTime;
                
                // draw the fade rectangle with transparency
                Renderer.Instance.DrawSpriteScaled(Assets.fadeTexture, Vector2.Zero, Prefs.screenSize, Renderer.Instance.currentBackgroundColor * alpha);
                
            }
            Renderer.Instance.End();
            base.Draw(gameTime);
        }
    }
}