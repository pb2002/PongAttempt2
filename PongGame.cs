using Microsoft.Xna.Framework;
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
        public Texture2D fade;
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
            IsFixedTimeStep = false;

            _graphics.PreferredBackBufferWidth = (int)screenSize.X;
            _graphics.PreferredBackBufferHeight = (int)screenSize.Y;
        }
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            
            currentState = new MenuState(this, Content);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Renderer.instance.spriteBatch = new SpriteBatch(GraphicsDevice);
            fade = Content.Load<Texture2D>("fade");
            
            currentState.LoadContent();
        }


        protected override void Update(GameTime gameTime)
        {
            fadeTimer.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            // Update Input
            InputHandler.instance.UpdateInput();

            if (nextState != null)
            {
                if (fadeTimer) return;
                fadeTimer.Reset();
                currentState = nextState;
                currentState.LoadContent();
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

                Renderer.instance.DrawSpriteScaled(fade, Vector2.Zero, screenSize, Color.White * alpha);
            }
                
            Renderer.instance.End();
            
            base.Draw(gameTime);
        }
    }
}