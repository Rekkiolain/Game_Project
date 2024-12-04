using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PlatformerV3.Entities
{
    public class Player
    {
        // Properties for the player
        private Texture2D _texture;
        private Texture2D rectangleTexture;
        public Rectangle _rect;
        public Vector2 _velocity;
        public Rectangle _srect; // Source rectangle for sprite animation (if needed)

        public Player(Texture2D texture, Rectangle rect)
        {
            _texture = texture;
            _rect = rect;
            _srect = new Rectangle(0, 0, 75, 48); // Example source rect
            _velocity = Vector2.Zero;
        }

        // Load content and setup textures
        public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            rectangleTexture = new Texture2D(graphicsDevice, 1, 1);
            rectangleTexture.SetData(new Color[] { new Color(255, 0, 0, 255) }); // Red color for hitboxes
        }

        // Update the player state based on keyboard input
        public void Update(KeyboardState keystate)
        {
            _velocity = Vector2.Zero;

            if (keystate.IsKeyDown(Keys.Right))
            {
                _velocity.X = 5;
            }
            if (keystate.IsKeyDown(Keys.Left))
            {
                _velocity.X = -5;
            }
            if (keystate.IsKeyDown(Keys.Up))
            {
                _velocity.Y = -5;
            }
            if (keystate.IsKeyDown(Keys.Down))
            {
                _velocity.Y = 5;
            }

            // Update the player's position based on velocity
            _rect.X += (int)_velocity.X;
            _rect.Y += (int)_velocity.Y;
        }

        // Draw the player sprite and hitbox
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the player sprite
            spriteBatch.Draw(
                _texture,
                _rect,
                _srect,
                Color.White
            );

            // Optionally draw a hitbox (hollow rectangle)
            DrawRectHollow(spriteBatch, _rect, 4);
        }

        // Method to draw a hollow rectangle for hitbox visualization
        public void DrawRectHollow(SpriteBatch spriteBatch, Rectangle rect, int thickness)
        {
            spriteBatch.Draw(
                rectangleTexture,
                new Rectangle(
                    rect.X,
                    rect.Y,
                    rect.Width,
                    thickness
                ),
                Color.White
            );
            spriteBatch.Draw(
                rectangleTexture,
                new Rectangle(
                    rect.X,
                    rect.Bottom - thickness,
                    rect.Width,
                    thickness
                ),
                Color.White
            );
            spriteBatch.Draw(
                rectangleTexture,
                new Rectangle(
                    rect.X,
                    rect.Y,
                    thickness,
                    rect.Height
                ),
                Color.White
            );
            spriteBatch.Draw(
                rectangleTexture,
                new Rectangle(
                    rect.Right - thickness,
                    rect.Y,
                    thickness,
                    rect.Height
                ),
                Color.White
            );
        }
    }
}
