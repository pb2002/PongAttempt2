using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PongAttempt2.States
{
    public class GameOverState : State
    {
        private static readonly Color titleColor = new Color();
        private static readonly Color subtitleColor= new Color();
        
        private SpriteFont titleFont;
        private SpriteFont subtitleFont;
        private int winningPlayer;
        public GameOverState(PongGame game, ContentManager content, int winningPlayer) : base(game, content)
        {
            this.winningPlayer = winningPlayer;
        }

        public override void LoadContent()
        {
            titleFont = content.Load<SpriteFont>("titleFont");
            subtitleFont = content.Load<SpriteFont>("subtitleFont");
        }

        public override void Update(GameTime gameTime)
        {
            if(InputHandler.instance.Confirm)
                game.SwitchState(new MenuState(game, content));
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 screenCenter = PongGame.screenSize / 2;
            Renderer.instance.DrawText(titleFont, "game over", screenCenter, Color.White);
            Renderer.instance.DrawText(subtitleFont, $"player {winningPlayer+1} wins!", screenCenter + new Vector2(0,96), Color.White);
            Renderer.instance.DrawText(subtitleFont, "press A/Enter to continue", screenCenter + new Vector2(0,144), Color.White); 
        }
    }
}
