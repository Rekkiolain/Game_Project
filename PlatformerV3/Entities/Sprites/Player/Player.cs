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
using MonoGame.Extended.Timers;
using System;
using System.Diagnostics;
using static System.Formats.Asn1.AsnWriter;

namespace PlatformerV3.Entities
{
    public class Player : IEntity
    {
        private float _speed = 1f;
        private float _deltaTime;
        private float _jumpForce = -3.5f; // Initial jump force (negative for upward movement)
        private float _gravity = 0.1f; // Gravity pull
        private float _maxFallSpeed = 3f; // Max speed for falling
        private float _scale = 0.5f;

        private float _jumpHoldTime = 0f; // Track how long the jump button is held
        private const float _maxJumpTime = 0.3f; // Max time for jump hold (controls max jump height)

        private SpriteEffects _spriteEffects = SpriteEffects.None;
        private SpriteSheet _spriteSheet;
        private AnimatedSprite _marine;
        private KeyboardStateExtended _keyBoardState;
        private KeyboardListener _keyboardListener;

        public bool _isOnGround = false;
        public bool _isFalling = true;
        public bool _isIdle = false;

        public Vector2 _velocity;
        public Vector2 _position;
        public RectangleF _bounds;
        public Rectangle _hitBox;
        public IShapeF Bounds => _bounds;

        public Player(RectangleF rectangleF)
        {
            _bounds = new Rectangle((int)_position.X, (int)_position.Y, 12, 18);
            _position = rectangleF.Position;
            _hitBox = rectangleF.ToRectangle();
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

        public void LoadContent(ContentManager content)
        {
            Texture2D MarineTexture = content.Load<Texture2D>("Spritesheet");
            Texture2DAtlas atlas = Texture2DAtlas.Create("Spritesheet", MarineTexture, 75, 48);
            _spriteSheet = new SpriteSheet("Spritesheet", atlas);

            _spriteSheet.DefineAnimation("Idle", builder =>
            {
                builder.IsLooping(true)
                    .AddFrame(regionIndex: 30, duration: TimeSpan.FromSeconds(0.1))
                    .AddFrame(31, TimeSpan.FromSeconds(0.1))
                    .AddFrame(32, TimeSpan.FromSeconds(0.1))
                    .AddFrame(33, TimeSpan.FromSeconds(0.1));
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

            _marine = new AnimatedSprite(_spriteSheet, "Idle");
        }

        public void Update(GameTime gameTime)
        {
            _keyboardListener.Update(gameTime);

            _deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _marine.Update(gameTime);



            HandleMovement();
        }

        public void OnCollision(CollisionEventArgs collisionInfo) { }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Vector2 hitboxCenter = _hitBox.Center.ToVector2();
            Vector2 spriteSize = new Vector2(75, 48) * _scale;
            Vector2 spritePosition = hitboxCenter - spriteSize / 2;
            Vector2 offset = new Vector2(0, -3);
            spritePosition += offset;

            spriteBatch.DrawRectangle(_bounds, Color.Red, 1);

            var currentTextureRegion = _marine.TextureRegion;
            spriteBatch.Draw(
                texture: currentTextureRegion.Texture,
                position: spritePosition,
                sourceRectangle: currentTextureRegion.Bounds,
                color: Color.White,
                rotation: 0f,
                origin: Vector2.Zero,
                scale: _scale,
                effects: _spriteEffects,
                layerDepth: 0f
            );
        }

        private void HandleMovement()
        {
            KeyboardExtended.Update();
            KeyboardStateExtended keyboardState = KeyboardExtended.GetState();

            // Apply gravity
            if (!_isOnGround)
            {
                _velocity.Y += _gravity;
                if (_velocity.Y > _maxFallSpeed) // Cap max fall speed
                    _velocity.Y = _maxFallSpeed;
            }

            // Handle jumping
            if (keyboardState.IsKeyDown(Keys.Space) && _isOnGround)
            {
                _jumpHoldTime += _deltaTime;

                // Limit the jump force scaling
                if (_jumpHoldTime > _maxJumpTime)
                {
                    _jumpHoldTime = _maxJumpTime;
                }

                // Increase upward velocity while holding jump key
                _velocity.Y = _jumpForce * (1 + _jumpHoldTime * 2); // Upward force increases with time held

                _marine.SetAnimation("Jump");
            }
            else if (keyboardState.IsKeyDown(Keys.Space) && !_isOnGround)
            {
                // If the player is already in the air, stop applying further jump force
                // Just allow gravity to take control
                _velocity.Y = Math.Max(_velocity.Y, _jumpForce); // Keep upward velocity from increasing
            }
            else
            {
                // Reset jump hold time when jump is released
                _jumpHoldTime = 0f;
            }

            // Horizontal movement
            if (keyboardState.IsKeyDown(Keys.D))
            {
                _position.X += _speed;
                if (_marine.CurrentAnimation != "Run")
                {
                    _marine.SetAnimation("Run");
                }
                _spriteEffects = SpriteEffects.None;
            }
            else if (keyboardState.IsKeyDown(Keys.Q))
            {
                _position.X -= _speed;
                if (_marine.CurrentAnimation != "Run")
                {
                    _marine.SetAnimation("Run");
                }
                _spriteEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                if (_marine.CurrentAnimation != "Idle")
                {
                    _marine.SetAnimation("Idle");
                }
            }

            // Update vertical position based on velocity
            _position.Y += _velocity.Y;
        }
    }


}