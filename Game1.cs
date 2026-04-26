using GameBattle.controllers;
using GameBattle.models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
            _player2 = new Player { X = 600, Y = 290 };
            _player2.Direction = false;

            _controller = new Controller(_player);
            _controller2 = new Controller2(_player2);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _texture = Content.Load<Texture2D>("5");
            _texture2 = Content.Load<Texture2D>("3");
            _bgtexture = Content.Load<Texture2D>("bg2");
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _controller.Update(gameTime, Window);
            _controller2.Update(gameTime, Window);

            _player.UpdateJump(dt);
            _player2.UpdateJump(dt);

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

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}