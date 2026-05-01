using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBattle.hitbox_view
{
    internal class Hitbox_view
    {
        private Texture2D _pixelTexture;

        public void LoadHitbox(GraphicsDevice graphicsDevice, Color color)
        {
            _pixelTexture = new Texture2D(graphicsDevice, 1, 1);
            _pixelTexture.SetData(new[] { color });
        }

        public void DrawHitbox(Rectangle rect, Color color, SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(_pixelTexture, new Rectangle(rect.X, rect.Y, rect.Width, 2), color);
            spriteBatch.Draw(_pixelTexture, new Rectangle(rect.X, rect.Y + rect.Height - 2, rect.Width, 2), color);
            spriteBatch.Draw(_pixelTexture, new Rectangle(rect.X, rect.Y, 2, rect.Height), color);
            spriteBatch.Draw(_pixelTexture, new Rectangle(rect.X + rect.Width - 2, rect.Y, 2, rect.Height), color);
        }
    }
}