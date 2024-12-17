using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions.Layers;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Input.InputListeners;

namespace PlatformerV3.Entities.Sprites
{
    public class BaseSprite
    {
        public RectangleF Bounds;
        public Rectangle HitBox;
        public Vector2 Velocity;
        public Vector2 Position;
        public float DeltaTime;

        public bool isIdle;
        public bool isFalling;
        public bool isOnGround;

        public float _speed = 1f;
        public float _jumpForce = -3.5f;
        public float _gravity = 0.1f;
        public float _maxFallSpeed = 3f;

        public SpriteSheet SpriteSheet;
        public SpriteEffects SpriteEffects = SpriteEffects.None;

        private const float Scale = 0.5f;

        public BaseSprite(int x, int y, int width, int heigth, RectangleF hitBox)
        {
            Bounds = new Rectangle(x, y, width, heigth);
            HitBox = hitBox.ToRectangle();
        }

        public void LoadContent(ContentManager content, string fileName, int width, int height)
        {
            var texture = content.Load<Texture2D>(fileName);
            var atlas = Texture2DAtlas.Create(fileName, texture, width, height);
            SpriteSheet = new SpriteSheet(fileName, atlas);
        }
        public virtual void Draw(SpriteBatch spriteBatch, int width, int height, Vector2 offset, AnimatedSprite _sprite)
        {
            var hitBoxCenter = HitBox.Center.ToVector2();
            var spriteSize = new Vector2(width, height) * Scale;
            var spritePosition = hitBoxCenter - spriteSize / 2 + offset;

            spriteBatch.DrawRectangle(Bounds, Color.Red, 1);

            var currentTextureRegion = _sprite.TextureRegion;
            spriteBatch.Draw(
                texture: currentTextureRegion.Texture,
                position: spritePosition,
                sourceRectangle: currentTextureRegion.Bounds,
                color: Color.White,
                rotation: 0f,
                origin: Vector2.Zero,
                scale: Scale,
                effects: SpriteEffects,
                layerDepth: 0f
            );
        }
        public void SetStartPosition(Vector2 startPosition)
        {
            Position = startPosition;
            Bounds.Position = startPosition;
        }

    }
}
