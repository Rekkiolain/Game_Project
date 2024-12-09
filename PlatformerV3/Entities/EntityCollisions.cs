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
        public List<Rectangle> _collisionRectangles; 
        public Texture2D _redTexture;

        public void GenerateCollisionRectangles(Dictionary<Vector2, int> _collisions, Dictionary<Vector2, int> _mapBounds)
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

            
            foreach (var bounds in _mapBounds)
            {
                Vector2 offset = new Vector2(-16, 16);
                Vector2 position = bounds.Key;
                int x = (int)position.X * tileSize + (int)offset.X;
                int y = (int)position.Y * tileSize - (int)offset.Y;

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
            const int margin = 2;

           
            Rectangle groundCheck = new Rectangle(playerHitbox.X + margin, playerHitbox.Bottom, playerHitbox.Width - margin * 2, 1);

            
            Rectangle ceilingCheck = new Rectangle(playerHitbox.X + margin, playerHitbox.Y - 1, playerHitbox.Width - margin * 2, 1);

            foreach (var rect in _collisionRectangles)
            {
               
                Rectangle leftWallCheck = new Rectangle(playerHitbox.X - margin, playerHitbox.Y + margin, 1 + margin, playerHitbox.Height - margin * 2);
                if (rect.Intersects(leftWallCheck))
                {
                   
                    if (playerPosition.X < rect.Right)
                    {
                        playerPosition.X = rect.Right; 
                        isIdle = true;
                    }
                }

                Rectangle rightWallCheck = new Rectangle(playerHitbox.Right, playerHitbox.Y + margin, 1 + margin, playerHitbox.Height - margin * 2);
                if (rect.Intersects(rightWallCheck))
                {
                    
                    if (playerPosition.X > rect.Left - playerHitbox.Width)
                    {
                        playerPosition.X = rect.Left - playerHitbox.Width;
                        isIdle = true;
                    }
                }

                if (rect.Intersects(groundCheck))
                {
                    isOnGround = true;
                    isFalling = false;
                    if (playerPosition.Y + playerHitbox.Height > rect.Top)
                    {
                        playerPosition.Y = rect.Top - playerHitbox.Height; 
                    }
                }

                if (rect.Intersects(ceilingCheck))
                {
                   
                    if (playerPosition.Y < rect.Bottom)
                    {
                        playerPosition.Y = rect.Bottom; 
                        isFalling = true; 
                    }
                }
            }

            
            if (!isOnGround)
            {
                isFalling = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var rect in _collisionRectangles)
            {
               
                spriteBatch.Draw(_redTexture, rect, Color.Red); 
            }
        }





    }
}
