using GameBattle.models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static Community.CsharpSqlite.Sqlite3;

namespace GameBattle.controllers
{
    public class Controller2
    {
        private Player _player;
        private knife _knife;

        public Keys LeftKey { get; private set; } = Keys.Left;
        public Keys RightKey { get;private set; } = Keys.Right;
        public Keys Up { get; private set; } = Keys.Up;
        public Keys Attack { get; private set; } = Keys.NumPad1;
        
        public Keys KnifeKey { get; private set; } = Keys.RightControl;

        public Controller2(Player player)
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

            if (!_player.isStun)
            {

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

                if (keyboard.IsKeyDown(Attack))
                {
                    _player.Attack();
                }
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