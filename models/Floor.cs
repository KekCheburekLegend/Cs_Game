using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBattle.models
{
    internal class Floor
    {
        public Rectangle rectangle;
        public Texture2D texture;

        public Floor(int x, int y, int Width, int Height)
        {
            rectangle = new Rectangle(x, y, Width, Height);
        }

        public bool isFloor(Player player)
        {
            return rectangle.Intersects(new Rectangle((int)player.X, (int)(player.Y + player.Height)-15, player.Width, 20));
        }

        public void LoadFloor(GraphicsDevice graphicsDevice)
        {
            texture = new Texture2D(graphicsDevice, 1, 1);
            texture.SetData(new[] { Color.Gray });
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.Gray);
        }
    }
}
