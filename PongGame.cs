using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PongAttempt2.States;


namespace PongAttempt2
{
    public class PongGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static Vector2 screenSize = new Vector2(1280, 720);
        
        private State currentState;
        private State nextState;

        public void SwitchState(State state)
        {
            nextState = state;
        }
        
        public PongGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = false;
            
            _graphics.PreferMultiSampling = true;
            
            _graphics.PreferredBackBufferWidth = (int)screenSize.X;
            _graphics.PreferredBackBufferHeight = (int)screenSize.Y;
        }
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here


            currentState = new TwoPlayerGameState(this, Content);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Renderer.instance.spriteBatch = new SpriteBatch(GraphicsDevice);
            
            currentState.LoadContent();
        }


        protected override void Update(GameTime gameTime)
        {
            
            
            // Update Input
            InputHandler.instance.UpdateInput();

            if (nextState != null)
            {
                currentState = nextState;
                currentState.LoadContent();
                nextState = null;
            }
            
            currentState.Update(gameTime);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            Renderer.instance.Begin();
            currentState.Draw(gameTime);
            Renderer.instance.End();
            
            base.Draw(gameTime);
        }
    }
}