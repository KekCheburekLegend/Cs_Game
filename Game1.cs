using GameBattle.controllers;
using GameBattle.hitbox_view;
using GameBattle.models;
using GameBattle.view;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;


namespace GameBattle
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _texture;
        private Texture2D _texture2;
        private Texture2D _bgtexture;
        private Controller _controller;
        private Controller2 _controller2;
        private Player _player;
        private Player _player2;
        private List<Floor> floors = new List<Floor>();
        
        // для отладки хитбокса
        private Hitbox_view _hitbox;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 450;
            _graphics.ApplyChanges();

            _player = new Player();
            _player2 = new Player { X = 800, Y = 290 };
            _player2.Direction = false;

            _controller = new Controller(_player);
            _controller2 = new Controller2(_player2);

            // для отладки хитбокса
            _hitbox = new Hitbox_view();
            floors.Add(new Floor(200, 280, 400, 10));
            floors.Add(new Floor(0, 450, 1200, 40));


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _texture = Content.Load<Texture2D>("5");
            _texture2 = Content.Load<Texture2D>("3");
            _bgtexture = Content.Load<Texture2D>("bg2");

            // для отладки хитбокса
            _hitbox.LoadHitbox(GraphicsDevice, Color.White);

        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Home))
                _bgtexture = Content.Load<Texture2D>("bkg");
            else if (Keyboard.GetState().IsKeyDown(Keys.End))
                _bgtexture = Content.Load<Texture2D>("bg2");

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _controller.Update(gameTime, Window);
            _controller2.Update(gameTime, Window);

            _player.UpdateJump(dt);
            _player2.UpdateJump(dt);

            _player.UpdateAttack(dt);
            _player2.UpdateAttack(dt);

            if (_player.CheckHit(_player2))
            {
                _player2.health -= 0.3f;
                Debug.WriteLine(_player2.health);
            }
            if (_player2.CheckHit(_player))
            {
                _player.health -= 0.3f;
                Debug.WriteLine(_player.health);
            }

            bool onPlatform1 = false;
            foreach (var floor in floors)
            {
                if (floor.isFloor(_player))
                {
                    _player.Y = floor.rectangle.Y - _player.Height;
                    _player.ResetJump();
                    onPlatform1 = true;
                    break;
                }
            }

            if (!onPlatform1 && !_player._isJumping)
            {
                _player._isJumping = true;
                _player._velosity = 0;
            }

            _player.UpdateAnimation(dt);
            _player2.UpdateAnimation(dt);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);


            _spriteBatch.Begin();

            // Фон
            _spriteBatch.Draw(_bgtexture, new Vector2(-100, 0), Color.White);


            SpriteEffects flip1 = _player.Direction
                ? SpriteEffects.None
                : SpriteEffects.FlipHorizontally;
            _spriteBatch.Draw(
                _texture,
                new Vector2(_player.X, _player.Y),
                _player.GetSourceRectangle(),
                Color.White, 0f, Vector2.Zero, 1f, flip1, 0f
            );

            SpriteEffects flip2 = _player2.Direction
                ? SpriteEffects.None
                : SpriteEffects.FlipHorizontally;
            _spriteBatch.Draw(
                _texture2,
                new Vector2(_player2.X, _player2.Y),
                _player2.GetSourceRectangle(),
                Color.White, 0f, Vector2.Zero, 1f, flip2, 0f
            );

            // для отладки хитбоксы
            _hitbox.DrawHitbox(_player.GetAttackHitbox(), Color.Red, _spriteBatch);
            _hitbox.DrawHitbox(_player2.GetAttackHitbox(), Color.Blue, _spriteBatch);
            _hitbox.DrawHitbox(new Rectangle((int)_player.X, (int)_player.Y, _player.Width, _player.Height), Color.Green, _spriteBatch);
            _hitbox.DrawHitbox(new Rectangle((int)_player2.X, (int)_player2.Y, _player2.Width, _player2.Height), Color.Yellow, _spriteBatch);
            foreach (var floor in floors)
            {
                _hitbox.DrawHitbox(floor.rectangle, Color.Gray, _spriteBatch);
            }
            _hitbox.DrawHitbox(new Rectangle((int)_player.X, (int)(_player.Y + _player.Height)-15, _player.Width, 5), Color.Blue, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}