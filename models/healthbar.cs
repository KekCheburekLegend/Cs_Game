using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBattle.models
{
    internal class healthbar
    {
        private int Width = 300;
        private int Height = 20;
        public int X = 0;
        public int Y = 20;
        private Texture2D FrameTexture;
        private Texture2D HealthTexture;

        public void LoadHealthBar(GraphicsDevice graphicsDevice)
        {
            FrameTexture = new Texture2D(graphicsDevice, 1, 1);
            FrameTexture.SetData(new[] { Color.Gray });

            HealthTexture = new Texture2D(graphicsDevice, 1, 1);
            HealthTexture.SetData(new[] { Color.Red });
        }

        public void Draw(SpriteBatch spriteBatch, Player player)
        {
            int healthProcent = (int)(player.health / 100 * Width);
            spriteBatch.Draw(FrameTexture, new Rectangle(X, Y, Width, Height), Color.Black);
            spriteBatch.Draw(HealthTexture, new Rectangle(X, Y, healthProcent, Height), Color.Red);
        }
    }
}
