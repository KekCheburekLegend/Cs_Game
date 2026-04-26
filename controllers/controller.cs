using GameBattle.models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameBattle.controllers
{
    public class Controller
    {
        private Player _player;

        public Keys Left { get;private set; } = Keys.A;
        public Keys Right { get; private set; } = Keys.D;
        public Keys Up { get; private set; } = Keys.W;

        public Controller(Player player)
        {
            _player = player;
        }

        public void Update(GameTime gameTime, GameWindow window)
        {
            var keyboard = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _player.IsMoving = false;

            if (keyboard.IsKeyDown(Left))
            {
                _player.X -= _player.Speed * dt;
                _player.Direction = false;
                _player.IsMoving = true;
            }
            if (keyboard.IsKeyDown(Right))
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