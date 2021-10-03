using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class Player : Entity
    {
        protected static readonly Color[] playerColors = { new Color(255, 192, 64), new Color(64, 192, 240) };

        public int playerId;
        public Vector2 normal;
        protected float speed;
        public int lives = 3;

        public Player(int playerId, Vector2 position, Vector2 normal) : base(position)
        {
            this.playerId = playerId;
            this.normal = normal;
            this.sprite = Assets.playerTexture;
            speed = Prefs.baseSpeed;
        }

        public virtual void Reset()
        {
            speed = Prefs.baseSpeed;
        }

        public virtual void IncreaseDifficulty()
        {
            speed *= Prefs.speedMultiplier;
        }
        public virtual void Move(float dt)
        {
            position.Y += InputHandler.Instance.PlayerMovementInput[playerId] * speed * dt;
            ClampYPosition();
        }

        protected void ClampYPosition()
        {
            position.Y = MathHelper.Clamp(position.Y, sprite.Height / 2f, Prefs.screenSize.Y - sprite.Height / 2f);
        }
        public virtual void Draw()
        {
            Renderer.Instance.DrawSpriteCentered(sprite, position, playerColors[playerId], playerId == 1);
        }
    }
}