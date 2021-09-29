using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.States
{
    
    public class CoopGameOverState : State
    {

        private Label titleLabel;
        private Label resultLabel;
        private Button continueButton;

        private int score;
        
        public CoopGameOverState(PongGame game, ContentManager content, int score) : base(game, content)
        {
            this.score = score;
            titleLabel = new Label(PongGame.screenSize / 2, Vector2.Zero, "GAME OVER", Renderer.titleColor, Assets.titleFont);
            resultLabel = new Label(PongGame.screenSize / 2 + new Vector2(0, 120), Vector2.Zero, $"score: {score}", Renderer.subtitleColor, Assets.subtitleFont);
            continueButton = new Button(PongGame.screenSize / 2 + new Vector2(0, 184), new Vector2(200, 48), "CONTINUE", Assets.subtitleFont);
        }

        public override void Update(GameTime gameTime)
        {
            if(InputHandler.instance.Confirm)
                game.SwitchState(new MenuState(game, content));
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 screenCenter = PongGame.screenSize / 2;
            titleLabel.Draw();
            resultLabel.Draw();

        }
    }
}
