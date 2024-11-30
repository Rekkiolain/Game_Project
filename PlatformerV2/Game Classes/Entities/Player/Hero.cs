using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Animations;
using MonoGame.Extended.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Input.InputListeners;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Collisions;
using MonoGame.Extended;
using FontStashSharp;

namespace Project1.Entities.Player_Character
{
    internal class Hero : ICollisionActor
    {
        private SpriteSheet _spriteSheet;
        private AnimatedSprite _marine;
        private KeyboardListener _keyboardListener;
        private Vector2 _position = new Vector2(43,760);
        private SpriteBatch _spriteBatch;
        private GraphicsDevice _graphicsDevice;

        public Vector2 _velocity;

        public IShapeF Bounds { get; set; }

        public Hero(RectangleF bounds,string layerName)
        {
            Bounds = bounds;
        }

        public void LoadContent(ContentManager content)
        {
            Texture2D MarineTexture = content.Load<Texture2D>("Hero");
            Texture2DAtlas atlas = Texture2DAtlas.Create("Assets/Hero", MarineTexture, 75, 48);
            _spriteSheet = new SpriteSheet("SpriteSheet/Hero", atlas);

            _spriteSheet.DefineAnimation("Idle", builder =>
            {
                builder.IsLooping(true)
                    .AddFrame(regionIndex: 30, duration: TimeSpan.FromSeconds(0.1))
                    .AddFrame(31, TimeSpan.FromSeconds(0.1))
                    .AddFrame(32, TimeSpan.FromSeconds(0.1))
                    .AddFrame(33, TimeSpan.FromSeconds(0.1));
            });

            _spriteSheet.DefineAnimation("Jump", builder =>
            {
                builder.IsLooping(false)
                    .AddFrame(130, TimeSpan.FromSeconds(0.1))
                    .AddFrame(131, TimeSpan.FromSeconds(0.1))
                    .AddFrame(132, TimeSpan.FromSeconds(0.1))
                    .AddFrame(133, TimeSpan.FromSeconds(0.1))
                    .AddFrame(134, TimeSpan.FromSeconds(0.1))
                    .AddFrame(135, TimeSpan.FromSeconds(0.1));
            });

            _spriteSheet.DefineAnimation("Run", builder =>
            {
                builder.IsLooping(true)
                    .AddFrame(60, TimeSpan.FromSeconds(0.1))
                    .AddFrame(61, TimeSpan.FromSeconds(0.1))
                    .AddFrame(62, TimeSpan.FromSeconds(0.1))
                    .AddFrame(63, TimeSpan.FromSeconds(0.1))
                    .AddFrame(64, TimeSpan.FromSeconds(0.1))
                    .AddFrame(65, TimeSpan.FromSeconds(0.1))
                    .AddFrame(66, TimeSpan.FromSeconds(0.1))
                    .AddFrame(67, TimeSpan.FromSeconds(0.1))
                    .AddFrame(68, TimeSpan.FromSeconds(0.1))
                    .AddFrame(69, TimeSpan.FromSeconds(0.1));
            });

            _marine = new AnimatedSprite(_spriteSheet, "Idle");  // Start with the "Idle" animation
        }

        public void Initialize()
        {
            _keyboardListener = new KeyboardListener();


            bool isRunning = false;


            _keyboardListener.KeyPressed += (sender, eventArgs) =>
            {

                if (eventArgs.Key == Keys.Space && _marine.CurrentAnimation == "Idle")
                {
                    _marine.SetAnimation("Jump").OnAnimationEvent += (sender, trigger) =>
                    {
                        if (trigger == AnimationEventTrigger.AnimationCompleted)
                        {
                            _marine.SetAnimation("Idle");
                        }
                    };
                }

                if ((eventArgs.Key == Keys.Q || eventArgs.Key == Keys.D) && !isRunning)
                {
                    isRunning = true;
                    _marine.SetAnimation("Run");
                }
            };


            _keyboardListener.KeyReleased += (sender, eventArgs) =>
            {
                if ((eventArgs.Key == Keys.Q || eventArgs.Key == Keys.D) && isRunning)
                {
                    isRunning = false;
                    _marine.SetAnimation("Idle");
                }
            };

        }

        public void Update(GameTime gameTime)
        {
            float speed = 100f;
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                _velocity.X = speed * deltaTime;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                _velocity.X = -speed * deltaTime;
            }
            else
            {
                _velocity.X = 0;
            }

            _position += _velocity;
            Bounds.Position = _position;  

            if (_velocity.X != 0)
            {
                if (_marine.CurrentAnimation != "Run")
                {
                    _marine.SetAnimation("Run");
                }
            }
            else
            {
                if (_marine.CurrentAnimation != "Idle")
                {
                    _marine.SetAnimation("Idle");
                }
            }

            _marine.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            int scale = 2;
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(_marine, _position, 0, new Vector2(scale));
            spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Red, 3);  // To visualize collision bounds
            spriteBatch.End();
        }

        public void OnCollision(CollisionEventArgs collisionInfo)
        {
            _velocity.X *= -1;
            _velocity.Y *= -1;
            Bounds.Position -= collisionInfo.PenetrationVector;
        }
    }
}

