using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBattle.models
{
    public class knife
    {
        public bool isKnife { get; private set; } = false;
        private int kWidth = 45;
        private int kheight = 5;
        private float kX;
        private float kY;
        private bool direction;
        private float _knifeTimer = 0f;
        private float _knifeDuration = 0.2f;
        private float _knifeCooldown = 1.0f;
        private float _kcooldownTimer = 0f;
        private float _knifevelocity = 700f;
        public int _knifeCount = 5;
        public float lifedt = 0f;
        public float lifetime = 3f;
        public Texture2D Texture;

        public void attackeknife(Player player)
        {
            if (!isKnife && _kcooldownTimer <= 0f && _knifeCount > 0)
            {
                isKnife = true;
                _knifeTimer = _knifeDuration;
                _kcooldownTimer = _knifeCooldown;

                kX = player.Direction ? player.X + player.Width - 20 : player.X - 30;
                kY = player.Y + player.Height / 2 - kheight / 2; 

                direction = player.Direction;

                _knifeCount--;
                lifedt = 0;
            }
        }

        public Rectangle GetHitboxknife()
        {
            if (!isKnife) return Rectangle.Empty;
            return new Rectangle((int)kX, (int)kY, kWidth, kheight);
        }

        public void move(float dt)
        {
            if (!isKnife) return;

            float der = direction ? 1f : -1f; 
            kX += _knifevelocity * dt * der;
            lifedt += dt; 

            if (lifedt >= lifetime || kX < -100 || kX > 2200)
                isKnife = false;
        }

        public bool CheckHit(Player other)
        {
            if (!isKnife) return false;
            return GetHitboxknife().Intersects(
                new Rectangle((int)other.X, (int)other.Y, other.Width, other.Height));
        }

        public void UpdateCooldown(float dt)
        {
            if (_kcooldownTimer > 0f)
                _kcooldownTimer -= dt;
        }

        public bool CanAttack()
        {
            return !isKnife && _kcooldownTimer <= 0f && _knifeCount > 0;
        }

        public float GetCooldownProgress()
        {
            if (_kcooldownTimer <= 0) return 1f;
            return 1f - (_kcooldownTimer / _knifeCooldown);
        }

        public void loadknife(GraphicsDevice graphicsDevice)
        {
            Texture = new Texture2D(graphicsDevice, 1, 1);
            Texture.SetData(new[] { Color.Gray });
        }

        public void Reset()
        {
            isKnife = false;
            _knifeTimer = 0f;
            _kcooldownTimer = 0f;
            lifedt = 0f;
            _knifeCount = 5;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isKnife)
            {
                spriteBatch.Draw(Texture, new Rectangle((int)kX, (int)kY, kWidth, kheight), Color.White);
            }
        }
    }
}