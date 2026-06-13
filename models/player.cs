using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameBattle.models
{
    public class Player
    {
        public float X { get; set; } = 100;
        public float Y { get; set; } = 290;
        public int Width { get; private set; } = 95;
        public int Height { get; private set; } = 170;
        public float Speed { get; set; } = 300f;
        public bool Direction { get; set; } = true;

        public float health = 100;
        public float damage = 0.4f;


        public bool IsAttacking { get; private set; } = false;
        private float _attackTimer = 0f;
        private float _attackDuration = 0.2f;
        private float _attackCooldown = 0.5f;
        private float _cooldownTimer = 0f;


        public int CurrentFrameX { get; set; } = 1;
        public int CurrentFrameY { get; set; } = 2;
        public int TotalFramesX { get; set; } = 6;
        public float FrameTimer { get; set; } = 0f;
        public float FrameInterval { get; set; } = 0.15f;
        public bool IsMoving { get; set; } = false;


        private float _Force = -650f;
        private float _gravity = 1200f;
        public float _velosity = 0f;
        public bool _isJumping = false;
        private int _floor = 290;

        public void Jump()
        {
            if (!_isJumping)
            {
                _isJumping = true;
                _velosity = _Force;
            }
        }

        public void UpdateJump(float dt)
        {
            if (_isJumping)
            {
                _velosity += _gravity * dt;
                Y += _velosity * dt;
            }
            if (Y >= _floor)
            {
                Y = _floor;
                _isJumping = false;
                _velosity = 0f;
            }
        }

        public void ResetJump()
        {
            _isJumping = false;
            _velosity = 0;
        }

        public void Attack()
        {
            if (!IsAttacking && _cooldownTimer <= 0f)
            {
                IsAttacking = true;
                _attackTimer = _attackDuration;
                _cooldownTimer = _attackCooldown;
                CurrentFrameX = 1;
                CurrentFrameY = 4;
            }
        }

        public Rectangle GetAttackHitbox()
        {
            if (!IsAttacking) return Rectangle.Empty;
            int aX = 40;
            int aY = 20;
            int attackX = Direction ? (int)X + (Width - 30) : (int)X + 30 - aX;
            int attackY = (int)Y + Height / 2 - aY / 2;

            return new Rectangle(attackX, attackY, aX, aY);
        }

        public bool CheckHit(Player other)
        {
            if (!IsAttacking) return false;
            return GetAttackHitbox().Intersects(
                new Rectangle((int)other.X, (int)other.Y, other.Width, other.Height));
        }

        public void UpdateAttack(float dt)
        {
            if (IsAttacking)
            {
                _attackTimer -= dt;
                if (_attackTimer <= 0f)
                {
                    IsAttacking = false;
                    CurrentFrameX = 1;
                    CurrentFrameY = 0;
                }
            }

            if (_cooldownTimer > 0f)
                _cooldownTimer -= dt;
        }

    }
}