using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
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

        public SpriteSheet SpriteSheet;
        public SpriteEffects SpriteEffects = SpriteEffects.None;

        private const float Scale = 0.5f;

        public BaseSprite(Rectangle bounds, RectangleF hitBox)
        {
            Bounds = bounds;
            HitBox = hitBox.ToRectangle();
        }

        public void LoadContent(ContentManager content, string fileName, int width, int height)
        {
            var texture = content.Load<Texture2D>(fileName);
            var atlas = Texture2DAtlas.Create(fileName, texture, width, height);
            SpriteSheet = new SpriteSheet(fileName, atlas);
        }

        public void Update(GameTime gameTime, AnimatedSprite _sprite)
        {
            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _sprite.Update(gameTime);
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
    }
}
