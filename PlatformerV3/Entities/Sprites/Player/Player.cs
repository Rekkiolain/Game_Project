using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Animations;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Input;
using MonoGame.Extended.Input.InputListeners;
using PlatformerV3.Entities.Sprites;
using System;

namespace PlatformerV3.Entities
{
    public class Player : IEntity
    {
        public BaseSprite _marineBase;
        private AnimatedSprite _marine;
        private float _jumpHoldTime = 0f;
        private const float _maxJumpTime = 0.3f;

        private KeyboardListener _keyboardListener;

        public BaseSprite MarineBase => _marineBase;

        public Player()
        {
        }

        public IShapeF Bounds => throw new NotImplementedException();

        public void Initialize()
        {
            _marineBase = new BaseSprite(0, 0, 12, 18, new RectangleF(new Vector2(0, 0), new SizeF(12, 18)));
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

        public void LoadContent(ContentManager content)
        {
            _marineBase.LoadContent(content, "Spritesheet", width: 75, height: 48);

            _marineBase.SpriteSheet.DefineAnimation("Idle", builder =>
            {
                builder.IsLooping(true)
                    .AddFrame(regionIndex: 30, duration: TimeSpan.FromSeconds(0.1))
                    .AddFrame(31, TimeSpan.FromSeconds(0.1))
                    .AddFrame(32, TimeSpan.FromSeconds(0.1))
                    .AddFrame(33, TimeSpan.FromSeconds(0.1));
            });
            _marineBase.SpriteSheet.DefineAnimation("Run", builder =>
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
            _marineBase.SpriteSheet.DefineAnimation("Jump", builder =>
            {
                builder.IsLooping(false)
                    .AddFrame(130, TimeSpan.FromSeconds(0.1))
                    .AddFrame(131, TimeSpan.FromSeconds(0.1))
                    .AddFrame(132, TimeSpan.FromSeconds(0.1))
                    .AddFrame(133, TimeSpan.FromSeconds(0.1))
                    .AddFrame(134, TimeSpan.FromSeconds(0.1))
                    .AddFrame(135, TimeSpan.FromSeconds(0.1));
            });

            _marine = new AnimatedSprite(_marineBase.SpriteSheet, "Idle");
        }

        public void Update(GameTime gameTime)
        {
            _marineBase.DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _keyboardListener.Update(gameTime);
            _marine.Update(gameTime);
            HandleMovement();
        }

        public void OnCollision(CollisionEventArgs collisionInfo) { }

        public void Draw(SpriteBatch spriteBatch)
        {
            _marineBase.Draw(spriteBatch, 75, 48, new Vector2(0, -3),_marine);
        }

        private void HandleMovement()
        {
            KeyboardExtended.Update();
            KeyboardStateExtended keyboardState = KeyboardExtended.GetState();

            if (!_marineBase.isOnGround) 
            {
                _marineBase.Velocity.Y += _marineBase._gravity;
                if (_marineBase.Velocity.Y > _marineBase._maxFallSpeed)
                    _marineBase.Velocity.Y = _marineBase._maxFallSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Space) && _marineBase.isOnGround)
            {
                _jumpHoldTime += _marineBase.DeltaTime;

                if (_jumpHoldTime > _maxJumpTime)
                {
                    _jumpHoldTime = _maxJumpTime;
                }

                _marineBase.Velocity.Y = _marineBase._jumpForce * (1 + _jumpHoldTime * 2);
                _marineBase.isOnGround = false;
                _marineBase.isFalling = true;
                _marine.SetAnimation("Jump");
            }
            else if (keyboardState.IsKeyDown(Keys.Space) && !_marineBase.isOnGround)
            {
                _marineBase.Velocity.Y = Math.Max(_marineBase.Velocity.Y, _marineBase._jumpForce);
            }
            else
            {
                _jumpHoldTime = 0f;
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                _marineBase.Position.X += _marineBase._speed;
                if (_marine.CurrentAnimation != "Run")
                {
                    _marine.SetAnimation("Run");
                }
                _marineBase.SpriteEffects = SpriteEffects.None;
            }
            else if (keyboardState.IsKeyDown(Keys.Q))
            {
                _marineBase.Position.X -= _marineBase._speed;
                if (_marine.CurrentAnimation != "Run")
                {
                    _marine.SetAnimation("Run");
                }
                _marineBase.SpriteEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                if (_marine.CurrentAnimation != "Idle")
                {
                    _marine.SetAnimation("Idle");
                }
            }

            _marineBase.Position.Y += _marineBase.Velocity.Y;
        }
    }
}
