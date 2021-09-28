using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Pong.States
{
    public abstract class State
    {
        protected PongGame game;
        protected ContentManager content;
        public State(PongGame game, ContentManager content)
        {
            this.game = game;
            this.content = content;
        }

        public abstract void LoadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
    }
}