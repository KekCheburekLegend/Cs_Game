using GameBattle.controllers;
using GameBattle.hitbox_view;
using GameBattle.models;
using GameBattle.view;
using Gum.Forms;
using Gum.Forms.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameGum;
using MonoGameGum.GueDeriving;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
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
        private healthbar healthbar;
        private healthbar healthbar2;


        private Button button = new Button();
        private Button back1;
        private Button back2;
        private Button reset;
        private TextRuntime winText;


        private List<healthpack> healthpacks = new List<healthpack>();


        private List<Floor> floors = new List<Floor>();

        private bool GameStart = false;
        private bool gameOver = false;
        GumService GumUI => GumService.Default;

        // для отладки хитбокса
        //private Hitbox_view _hitbox;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            GumService.Default.Initialize(this, DefaultVisualsVersion.V3);
            button = new Button();
            back1 = new Button();
            back2 = new Button();
            reset = new Button();
            winText = new TextRuntime();
            winText.AddToRoot();
            reset.AddToRoot();
            button.AddToRoot();
            back1.AddToRoot();
            back2.AddToRoot();
            button.X = 500;
            button.Y = 225;
            button.Width = 275;
            button.Height = 10;
            button.Text = "START GAME";
            button.Click += (_, _) =>
            {
                ResetGameToStart();

                GameStart = true;
                gameOver = false;

                button.Visual.Visible = false;
                back1.Visual.Visible = false;
                back2.Visual.Visible = false;
                reset.Visual.Visible = false;
                winText.Visible = false;

            };
            back1.X = 500;
            back1.Y = 300;
            back1.Text = "training";
            back1.Click += (_, _) =>
            {
                _bgtexture = Content.Load<Texture2D>("bg2");
                floors.Clear();
                floors.Add(new Floor(400, 280, 400, 10));
                floors.Add(new Floor(0, 450, 1200, 40));
                foreach (var floor in floors)
                {
                    floor.LoadFloor(GraphicsDevice);
                }

                healthpacks.Clear();
                healthpacks.Add(new healthpack(new Vector2(500, 240)));
                healthpacks.Add(new healthpack(new Vector2(700, 240)));

                foreach (var pack in healthpacks)
                {
                    pack.loadContent(GraphicsDevice);
                }
            };

            back2.X = 650;
            back2.Y = 300;
            back2.Text = "ruins";
            back2.Click += (_, _) =>
            {
                _bgtexture = Content.Load<Texture2D>("bkg");
                floors.Clear();
                floors.Add(new Floor(0, 450, 1200, 40));
                floors.Add(new Floor(0, 280, 400, 10));
                floors.Add(new Floor(800, 280, 400, 10));
                foreach (var floor in floors)
                {
                    floor.LoadFloor(GraphicsDevice);
                }

                healthpacks.Clear();
                healthpacks.Add(new healthpack(new Vector2(200, 240)));
                healthpacks.Add(new healthpack(new Vector2(900, 240)));

                foreach (var pack in healthpacks)
                {
                    pack.loadContent(GraphicsDevice);
                }
            };

            reset.X = 500;
            reset.Y = 225;
            reset.Width = 275;
            reset.Height = 10;

            reset.Click += (_, _) =>
            {
                ResetGameToStart();

                GameStart = false;
                gameOver = false;

                button.Visual.Visible = true;
                back1.Visual.Visible = true;
                back2.Visual.Visible = true;
                reset.Visual.Visible = false;
                winText.Visible = false;
            };

            winText.X = 550;
            winText.Y = 150;
            winText.Width = 400;
            winText.Height = 300;
            winText.FontSize = 50;
            winText.Color = Color.Black;
            reset.Text = "RESET GAME";
            reset.Visual.Visible = false;
            winText.Visible = false;

            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 450;
            _graphics.ApplyChanges();

            _player = new Player();
            _player2 = new Player { X = 1000, Y = 290 };
            _player2.Direction = false;

            _controller = new Controller(_player);
            _controller2 = new Controller2(_player2);

            // для отладки хитбокса
            //_hitbox = new Hitbox_view();

            //добавление пола + плотформ без текстур
            floors.Add(new Floor(400, 280, 400, 10));
            floors.Add(new Floor(0, 450, 1200, 40));

            healthpacks.Add(new healthpack(new Vector2(500, 240)));
            healthpacks.Add(new healthpack(new Vector2(700, 240)));

            //healthbar
            healthbar = new healthbar();
            healthbar2 = new healthbar { X = 900 };


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _texture = Content.Load<Texture2D>("5");
            _texture2 = Content.Load<Texture2D>("3");
            _bgtexture = Content.Load<Texture2D>("bg2");

            _controller.InitializeKnife(GraphicsDevice);
            _controller2.InitializeKnife(GraphicsDevice);
            // для отладки хитбокса
            //_hitbox.LoadHitbox(GraphicsDevice, Color.White);
            // загрузка цвета здоровья
            healthbar.LoadHealthBar(GraphicsDevice);
            healthbar2.LoadHealthBar(GraphicsDevice);

            // загрузка цвета платформы
            foreach (var floor in floors)
            {
                floor.LoadFloor(GraphicsDevice);
            }

            foreach (var pack in healthpacks)
            {
                pack.loadContent(GraphicsDevice);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            GumUI.Update(gameTime);

            if (GameStart && !gameOver)
            {
                float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

                _controller.Update(gameTime, Window);
                _controller2.Update(gameTime, Window);

                _player.UpdateJump(dt);
                _player2.UpdateJump(dt);

                _player.UpdateAttack(dt);
                _player2.UpdateAttack(dt);

                if (_player.CheckHit(_player2))
                {
                    _player2.health -= 0.4f;
                    //Debug.WriteLine(_player2.health);
                }
                if (_player2.CheckHit(_player))
                {
                    _player.health -= 0.4f;
                    //Debug.WriteLine(_player.health);
                }

                if (_controller.GetKnife().CheckHit(_player2))
                {
                    _player2.health -= 0.4f;
                }

                if (_controller2.GetKnife().CheckHit(_player))
                {
                    _player.health -= 0.4f;
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

                bool onPlatform2 = false;
                foreach (var floor in floors)
                {
                    if (floor.isFloor(_player2))
                    {
                        _player2.Y = floor.rectangle.Y - _player2.Height;
                        _player2.ResetJump();
                        onPlatform2 = true;
                        break;
                    }
                }

                if (!onPlatform2 && !_player2._isJumping)
                {
                    _player2._isJumping = true;
                    _player2._velosity = 0;
                }

                for (int i = 0; i < healthpacks.Count; i++)
                {
                    var temp = healthpacks[i];
                    temp.Update(dt);

                    if (temp.Active)
                    {
                        if (temp.takepack(_player)) temp.collect(_player);
                        if (temp.takepack(_player2)) temp.collect(_player2);
                    }
                }

                healthpacks.RemoveAll(p => !p.Active && p != null && healthpacks.Count > 7);

                _player.UpdateAnimation(dt);
                _player2.UpdateAnimation(dt);


                if (_player.health <= 0)
                {
                    gameOver = true;
                    GameStart = false;
                    winText.Text = "PLAYER 2 WINS!";
                    winText.Visible = true;
                    reset.Visual.Visible = true;
                }

                else if (_player2.health <= 0)
                {
                    gameOver = true;
                    GameStart = false;
                    winText.Text = "PLAYER 1 WINS!";
                    winText.Visible = true;
                    reset.Visual.Visible = true;
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);


            _spriteBatch.Begin();

            // Фон
            _spriteBatch.Draw(_bgtexture, new Vector2(-100, 0), Color.White);


            if (GameStart)
            {
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

                healthbar.Draw(_spriteBatch, _player);
                healthbar2.Draw(_spriteBatch, _player2);
                foreach (var floor in floors)
                {
                    floor.Draw(_spriteBatch);
                }

                _controller.GetKnife().Draw(_spriteBatch);
                _controller2.GetKnife().Draw(_spriteBatch);

                foreach (var pack in healthpacks)
                {
                    pack.Draw(_spriteBatch);
                }

                // для отладки хитбоксы
                //_hitbox.DrawHitbox(_player.GetAttackHitbox(), Color.Red, _spriteBatch);
                //_hitbox.DrawHitbox(_player2.GetAttackHitbox(), Color.Blue, _spriteBatch);
                //_hitbox.DrawHitbox(new Rectangle((int)_player.X, (int)_player.Y, _player.Width, _player.Height), Color.Green, _spriteBatch);
                //_hitbox.DrawHitbox(new Rectangle((int)_player2.X, (int)_player2.Y, _player2.Width, _player2.Height), Color.Yellow, _spriteBatch);
                //foreach (var floor in floors)
                //{
                //    _hitbox.DrawHitbox(floor.rectangle, Color.Gray, _spriteBatch);
                //}
                //_hitbox.DrawHitbox(new Rectangle((int)_player.X, (int)(_player.Y + _player.Height) - 15, _player.Width, 5), Color.Blue, _spriteBatch);

            }
            _spriteBatch.End();

            GumUI.Draw();

            base.Draw(gameTime);
        }

        private void ResetGameToStart()
        {
            _player.health = 100f;
            _player2.health = 100f;

            _player.X = 100;
            _player.Y = 290;
            _player2.X = 1000;
            _player2.Y = 290;

            _player.Direction = true;
            _player2.Direction = false;

            _player._isJumping = false;
            _player2._isJumping = false;
            _player._velosity = 0;
            _player2._velosity = 0;

            _controller = new Controller(_player);
            _controller2 = new Controller2(_player2);

            _controller.InitializeKnife(GraphicsDevice);
            _controller2.InitializeKnife(GraphicsDevice);
        }
    }
}