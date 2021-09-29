using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.States
{
    public class MenuState : State
    {
        private Label titleLabel;
        private Label subtitleLabel;

        private Button onePlayerButton;
        private Button twoPlayerButton;
        private Button cpuBattleButton;
        private Button coopButton;
        private Button difficultyButton;

        private readonly string[] difficultyNames = {"easy", "normal", "hard", "impossible"};
        public MenuState(PongGame game) : base(game)
        {
            titleLabel = new Label(PongGame.screenSize / 2 + new Vector2(0, -50), Vector2.Zero, "PONG", Renderer.titleColor, Assets.titleFont);
            subtitleLabel = new Label(PongGame.screenSize / 2 + new Vector2(0, 50), Vector2.Zero, "By Pepijn & Tigo", Renderer.subtitleColor, Assets.subtitleFont);
            
            onePlayerButton = new Button(PongGame.screenSize / 2 + new Vector2(0, 130), new Vector2(360, 48), "PLAYER VS CPU", Assets.subtitleFont);
            twoPlayerButton = new Button(PongGame.screenSize / 2 + new Vector2(0, 190), new Vector2(380, 48), "PLAYER VS PLAYER", Assets.subtitleFont);
            coopButton = new Button(PongGame.screenSize / 2 + new Vector2(0, 250), new Vector2(260, 48), "COOP MODE", Assets.subtitleFont);
            cpuBattleButton = new Button(PongGame.screenSize / 2 + new Vector2(0, 310), new Vector2(300, 48), "CPU VS CPU", Assets.subtitleFont);
            
            difficultyButton = new Button(PongGame.screenSize / 2 + new Vector2(0, 400), new Vector2(500, 48),
                $"AI difficulty: {difficultyNames[Prefs.difficulty]}", Assets.subtitleFont);
        }

        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            onePlayerButton.Update(dt);
            twoPlayerButton.Update(dt);
            cpuBattleButton.Update(dt);
            coopButton.Update(dt);
            difficultyButton.Update(dt);
            
            if (onePlayerButton.IsPressed)
                game.SwitchState(new CompetitiveGameState(game, GameMode.OnePlayer));
            
            if (twoPlayerButton.IsPressed)
                game.SwitchState(new CompetitiveGameState(game, GameMode.TwoPlayer));
            
            if (cpuBattleButton.IsPressed)
                game.SwitchState(new CompetitiveGameState(game, GameMode.CPUBattle));
            
            if (coopButton.IsPressed)
                game.SwitchState(new CoopGameState(game));

            if (difficultyButton.IsPressed)
            {
                Prefs.difficulty = (Prefs.difficulty + 1) % 4;
                difficultyButton.text = $"AI difficulty: {difficultyNames[Prefs.difficulty]}";
            }
            
            titleLabel.position = PongGame.screenSize / 2 
                                  + MathF.Sin((float)gameTime.TotalGameTime.TotalSeconds) * Vector2.UnitY * 9
                                  - 70 * Vector2.UnitY;
            
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 screenCenter = PongGame.screenSize / 2;
            
            titleLabel.Draw();
            subtitleLabel.Draw();
            
            onePlayerButton.Draw();
            twoPlayerButton.Draw();
            coopButton.Draw();
            cpuBattleButton.Draw();
            difficultyButton.Draw();
        }
    }
}