using Microsoft.Xna.Framework;

namespace GameBattle.models
{
    public class Player
    {
        public float X { get; set; } = 190;
        public float Y { get; set; } = 280;
        public int Width { get; private set; } = 190;
        public int Height { get; private set; } = 175;
        public float Speed { get; set; } = 300f;
        public bool Direction { get; set; } = true;

        public int CurrentFrameX { get; set; } = 1;
        public int CurrentFrameY { get; set; } = 2;
        public int TotalFramesX { get; set; } = 6;
        public float FrameTimer { get; set; } = 0f;
        public float FrameInterval { get; set; } = 0.15f;
        public bool IsMoving { get; set; } = false;

        private float _Force = -600f;
        private float _gravity = 1200f;
        private float _velosity = 0f;
        private bool _isJumping = false;
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
        public Rectangle GetSourceRectangle()
        {
            return new Rectangle(
                CurrentFrameX * Width,
                CurrentFrameY * Height,
                Width,
                Height
            );
        }



        public void UpdateAnimation(float deltaTime)
        {
            if (IsMoving)
            {
                FrameTimer += deltaTime;
                if (FrameTimer >= FrameInterval)
                {
                    FrameTimer = 0f;
                    CurrentFrameX++;
                    CurrentFrameY = 2;
                    if (CurrentFrameX >= TotalFramesX)
                        CurrentFrameX = 1;
                }
            }
            else
            {
                CurrentFrameX = 1;
                CurrentFrameY = 0;
                FrameTimer = 0f;
            }
        }
    }
}