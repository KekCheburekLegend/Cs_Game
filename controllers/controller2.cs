using GameBattle.models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameBattle.controllers
{
    public class Controller2
    {
        private Player _player;

        public Keys LeftKey { get; private set; } = Keys.Left;
        public Keys RightKey { get;private set; } = Keys.Right;
        public Keys Up { get; private set; } = Keys.Up;
        public Controller2(Player player)
        {
            _player = player;
        }

        public void Update(GameTime gameTime, GameWindow window)
        {
            var keyboard = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _player.IsMoving = false;

            if (keyboard.IsKeyDown(LeftKey))
            {
                _player.X -= _player.Speed * dt;
                _player.Direction = false;
                _player.IsMoving = true;
            }
            if (keyboard.IsKeyDown(RightKey))
            {
                _player.X += _player.Speed * dt;
                _player.Direction = true;
                _player.IsMoving = true;
            }

            if (keyboard.IsKeyDown(Up))
            {
                _player.Jump();
            }

            if (_player.X < 0) _player.X = 0;
            if (_player.X > window.ClientBounds.Width - _player.Width)
                _player.X = window.ClientBounds.Width - _player.Width;
        }
    }
}