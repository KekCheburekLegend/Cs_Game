using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBattle.models
{
    internal class Floor
    {
        public Rectangle rectangle;

        public Floor(int x, int y, int Width, int Height)
        {
            rectangle = new Rectangle(x, y, Width, Height);
        }

        public bool isFloor(Player player)
        {
            return rectangle.Intersects(new Rectangle((int)player.X, (int)(player.Y + player.Height)-15, player.Width, 20));
        }

        
    }
}
