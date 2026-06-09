using GameBattle.models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameBattle.controllers
{
    public class Controller
    {
        private Player _player;
        private knife _knife;
        public Keys LefKey { get; private set; } = Keys.A;
        public Keys RightKey { get; private set; } = Keys.D;
        public Keys Up { get; private set; } = Keys.W;
        public Keys Attack { get; private set; } = Keys.E;
        public Keys KnifeKey { get; private set; } = Keys.Q;

        public Controller(Player player)
        {
            _player = player;
            _knife = new knife();
        }

        public void InitializeKnife(GraphicsDevice graphicsDevice)
        {
            _knife.loadknife(graphicsDevice);
        }

        public void Reset()
        {
            _knife.Reset();
        }

        public void Update(GameTime gameTime, GameWindow window)
        {
            var keyboard = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _player.IsMoving = false;

            if (keyboard.IsKeyDown(LefKey))
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

            if (keyboard.IsKeyDown(Attack))
            {
                _player.Attack();
            }

            if (keyboard.IsKeyDown(KnifeKey) && _knife.CanAttack())
            {
                _knife.attackeknife(_player);
            }

            _knife.move(dt);
            _knife.UpdateCooldown(dt);

            if (_player.X < 0) _player.X = 0;
            if (_player.X > window.ClientBounds.Width - _player.Width)
                _player.X = window.ClientBounds.Width - _player.Width;
        }

        public knife GetKnife()
        {
            return _knife;
        }
    }
}