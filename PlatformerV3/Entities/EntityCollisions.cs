using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions.Layers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerV3.Entities
{
    public class EntityCollisionsV2
    {
        public List<Rectangle> _collisionRectangles; // Use Rectangle for consistency
        public Texture2D _redTexture;

        public void GenerateCollisionRectangles(Dictionary<Vector2, int> _collisions)
        {
            const int tileSize = 16;

            _collisionRectangles = new List<Rectangle>();

            foreach (var collision in _collisions)
            {
                Vector2 position = collision.Key;
                int x = (int)position.X * tileSize;
                int y = (int)position.Y * tileSize;

                Rectangle rectangle = new Rectangle(x, y, tileSize, tileSize);

                _collisionRectangles.Add(rectangle);
            }
        }

        public void addEntity(Rectangle entityHitBox)
        {
            _collisionRectangles.Add(entityHitBox);
        }

        public void removeEntity(Rectangle entityHitBox)
        {
            _collisionRectangles.Remove(entityHitBox);
        }

        public void CreateRedTexture(GraphicsDevice graphicsDevice)
        {
            _redTexture = new Texture2D(graphicsDevice, 1, 1);
            _redTexture.SetData(new[] { Color.Red });
        }

        public void Collision(Rectangle playerHitbox, ref Vector2 playerPosition, ref bool isIdle, ref bool isFalling, ref bool isOnGround)
        {
            var initPosition = playerPosition;
            isOnGround = false;
            const int margin = 2; // Small margin to prevent tight tile snapping

            // Create a ground-check rectangle (slightly above the ground)
            Rectangle groundCheck = new Rectangle(playerHitbox.X + margin, playerHitbox.Bottom, playerHitbox.Width - margin * 2, 1);

            // Create a ceiling-check rectangle (slightly below the top of the player)
            Rectangle ceilingCheck = new Rectangle(playerHitbox.X + margin, playerHitbox.Y - 1, playerHitbox.Width - margin * 2, 1);

            foreach (var rect in _collisionRectangles)
            {
                // Wall detection - Left side
                Rectangle leftWallCheck = new Rectangle(playerHitbox.X - margin, playerHitbox.Y + margin, 1 + margin, playerHitbox.Height - margin * 2);
                if (rect.Intersects(leftWallCheck))
                {
                    // Only snap if the player is moving right into the wall
                    if (playerPosition.X < rect.Right)
                    {
                        playerPosition.X = rect.Right; // Snap to the right edge of the wall
                        isIdle = true;
                    }
                }

                // Wall detection - Right side
                Rectangle rightWallCheck = new Rectangle(playerHitbox.Right, playerHitbox.Y + margin, 1 + margin, playerHitbox.Height - margin * 2);
                if (rect.Intersects(rightWallCheck))
                {
                    // Only snap if the player is moving left into the wall
                    if (playerPosition.X > rect.Left - playerHitbox.Width)
                    {
                        playerPosition.X = rect.Left - playerHitbox.Width; // Snap to the left edge of the wall
                        isIdle = true;
                    }
                }

                // Ground detection
                if (rect.Intersects(groundCheck))
                {
                    isOnGround = true;
                    isFalling = false;
                    if (playerPosition.Y + playerHitbox.Height > rect.Top)
                    {
                        playerPosition.Y = rect.Top - playerHitbox.Height; // Align player with the floor
                    }
                }

                // Ceiling detection
                if (rect.Intersects(ceilingCheck))
                {
                    // Prevent upward movement if the player is moving up and hits the ceiling
                    if (playerPosition.Y < rect.Bottom)
                    {
                        playerPosition.Y = rect.Bottom; // Snap player to the ceiling
                        isFalling = true; // If the player hits the ceiling, they should stop going up
                    }
                }
            }

            // If no ground detected, mark as falling
            if (!isOnGround)
            {
                isFalling = true;
            }
        }







    }
}
