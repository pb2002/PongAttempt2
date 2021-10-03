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
            IsFixedTimeStep = false;
            _graphics.SynchronizeWithVerticalRetrace = true;
            
            
            // set screen resolution
            _graphics.PreferredBackBufferWidth = (int)Prefs.screenSize.X;
            _graphics.PreferredBackBufferHeight = (int)Prefs.screenSize.Y;
        }
        
        protected override void Initialize()
        {
            base.Initialize();
            // start at menu state
            currentState = new MenuState(this);
            
            Assets.musicTopLayer.Play(true, 0f);
            Assets.musicTopLayer.FadeIn(5f);
            Assets.musicBaseLayer.Play(true, 0f);
            Assets.musicBaseLayer.FadeIn(5f);
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

            Assets.playerHitSFX = new AudioClip(Content.Load<SoundEffect>("sfx/player hit"));
            Assets.wallHitSFX = new AudioClip(Content.Load<SoundEffect>("sfx/wall hit"));
            Assets.clickSFX = new AudioClip(Content.Load<SoundEffect>("sfx/click"));
            Assets.musicTopLayer = new AudioClip(Content.Load<SoundEffect>("music/music-top-layer"));
            Assets.musicBaseLayer = new AudioClip(Content.Load<SoundEffect>("music/music-base-layer"));
        }


        protected override void Update(GameTime gameTime)
        {
            // update the timer (gameTime.ElapsedGameTime.TotalSeconds = time of last frame)
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            fadeTimer.Update(dt);
            //Assets.musicTopLayer.Update(dt);
            Assets.musicBaseLayer.Update(dt);
            Assets.musicTopLayer.Update(dt);
            // Update Input
            InputHandler.Instance.UpdateInput();

            // If there is a state to be loaded and fade transition has finished
            if (nextState != null && !fadeTimer)
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
            if (fadeTimer)
            {
                // if the next state is null, we have changed states already, so we should
                // apply a fade out effect.
                // if not, we should apply a fade in effect instead.
                float alpha = nextState == null 
                    ? fadeTimer.Time / fadeTimer.MaxTime // divide by MaxTime to get a number between 0 and 1
                    : 1f - fadeTimer.Time / fadeTimer.MaxTime;
                
                // draw the fade rectangle with transparency
                Renderer.Instance.DrawSpriteScaled(Assets.fadeTexture, Vector2.Zero, Prefs.screenSize, Renderer.Instance.BGColor * alpha);
                
            }
            Renderer.Instance.End();
            base.Draw(gameTime);
        }
    }
}