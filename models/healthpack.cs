using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBattle.models
{
    public class healthpack
    {
        public Vector2 pos {get; set;}
        private int widthHeight = 30;
        public int health = 10;
        public bool Active = true;

        private Texture2D _texture;
        private float _respawnTimer = 0f;
        private const float RESPAWN_TIME = 15f;

        public healthpack(Vector2 vector)
        {
            pos = vector;
        }

        public void loadContent(GraphicsDevice graphics)
        {
            _texture = new Texture2D(graphics, widthHeight, widthHeight);
            var data = new Color[widthHeight * widthHeight];
            for (int i = 0; i < data.Length; i++)
            {
                int x = i % widthHeight;
                int y = i / widthHeight;

                data[i] = Color.Transparent;

                if ((x >= widthHeight / 3 && x <= 2 * widthHeight / 3) || (y >= widthHeight / 3 && y <= 2 * widthHeight / 3))
                {
                    data[i] = Color.Red;
                }
            }
            _texture.SetData(data);
        }

        public void Update(float dt)
        {
            if (!Active)
            {
                _respawnTimer -= dt;
                if (_respawnTimer <= 0)
                {
                    Active = true;
                    _respawnTimer = 0f;
                }
            }
        }



        public void collect(Player player)
        {
            if (Active)
            {
                player.health = MathF.Min(100.0f, health + player.health);
                Active = false;
                _respawnTimer = RESPAWN_TIME;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active && _texture != null)
            {
                spriteBatch.Draw(_texture, new Rectangle((int)pos.X, (int)pos.Y, widthHeight, widthHeight), Color.White);
            }
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)pos.X, (int)pos.Y, widthHeight, widthHeight);
        }


        public bool takepack(Player player)
        {
            return GetBounds().Intersects(
                new Rectangle((int)player.X, (int)player.Y, player.Width, player.Height));
        }


    }
}
