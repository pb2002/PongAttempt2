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
        private Button difficultyButton;
        private Button darkModeButton;

        private readonly string[] difficultyNames = {"EASY", "NORMAL", "HARD", "IMPOSSIBLE"};
        public MenuState(PongGame game) : base(game)
        {
            titleLabel = new Label(Prefs.screenSize / 2 + new Vector2(0, -50), Vector2.Zero, "PONG", Renderer.titleColor, Assets.titleFont);
            subtitleLabel = new Label(Prefs.screenSize / 2 + new Vector2(0, 50), Vector2.Zero, "By Pepijn & Tigo", Renderer.subtitleColor, Assets.subtitleFont);
            
            onePlayerButton = new Button(Prefs.screenSize / 2 + new Vector2(0, 130), new Vector2(360, 48), "PLAYER VS CPU", Assets.subtitleFont);
            twoPlayerButton = new Button(Prefs.screenSize / 2 + new Vector2(0, 190), new Vector2(380, 48), "PLAYER VS PLAYER", Assets.subtitleFont);
            cpuBattleButton = new Button(Prefs.screenSize / 2 + new Vector2(0, 250), new Vector2(300, 48), "CPU VS CPU", Assets.subtitleFont);
            
            difficultyButton = new Button(Prefs.screenSize / 2 + new Vector2(0, 340), new Vector2(500, 48),
                $"AI DIFFICULTY : {difficultyNames[Prefs.difficulty]}", Assets.smallFont); 
            darkModeButton = new Button(Prefs.screenSize / 2 + new Vector2(0, 380), new Vector2(300, 32), "DARK MODE : OFF", Assets.smallFont);
        }

        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            
            onePlayerButton.Update(dt);
            twoPlayerButton.Update(dt);
            cpuBattleButton.Update(dt);
            difficultyButton.Update(dt);
            darkModeButton.Update(dt);

            if (onePlayerButton.IsPressed)
            {
                game.SwitchState(new InGameState(game, GameMode.OnePlayer));
                Assets.musicBaseLayer.FadeOut(2f);
            }


            if (twoPlayerButton.IsPressed)
            {
                game.SwitchState(new InGameState(game, GameMode.TwoPlayer));
                Assets.musicBaseLayer.FadeOut(2f);
            }


            if (cpuBattleButton.IsPressed)
            {
                game.SwitchState(new InGameState(game, GameMode.CPUBattle));
                Assets.musicBaseLayer.FadeOut(2f);
            }

            if (difficultyButton.IsPressed)
            {
                Prefs.difficulty = (Prefs.difficulty + 1) % 4;
                difficultyButton.text = $"AI DIFFICULTY : {difficultyNames[Prefs.difficulty]}";
            }

            if (darkModeButton.IsPressed)
            {
                Renderer.Instance.darkMode = !Renderer.Instance.darkMode;
                darkModeButton.text = $"DARK MODE : {(Renderer.Instance.darkMode ? "ON" : "OFF")}";
            }
            
            titleLabel.position = Prefs.screenSize / 2 
                                  + MathF.Sin((float)gameTime.TotalGameTime.TotalSeconds) * Vector2.UnitY * 9
                                  - 70 * Vector2.UnitY;
            
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 screenCenter = Prefs.screenSize / 2;
            
            titleLabel.Draw();
            subtitleLabel.Draw();
            
            onePlayerButton.Draw();
            twoPlayerButton.Draw();
            cpuBattleButton.Draw();
            difficultyButton.Draw();
            darkModeButton.Draw();
            
        }
    }
}