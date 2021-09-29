using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Pong.States
{
    public abstract class State
    {
        protected PongGame game;
        public State(PongGame game)
        {
            this.game = game;
            
        }
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
    }
}