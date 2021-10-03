using System;
using Microsoft.Xna.Framework;

namespace Pong.States
{
    /// <summary>
    /// Defines a Game state
    /// Each game state has an Update and Draw loop. the PongGame class will call those
    /// of the currently active game state.
    /// </summary>
    public abstract class State
    {
        protected event Action<GameTime> OnUpdate;
        protected event Action<GameTime> OnDraw;
        protected PongGame game;
        protected State(PongGame game)
        {
            this.game = game;
        }
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
    }
}