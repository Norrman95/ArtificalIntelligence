using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence;

namespace Steering
{
    class Player
    {
        Vector2 pos;
        Vector2 velocity;

        public Player(Vector2 pos)
        {
            this.pos = pos;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            velocity = Vector2.Zero;

            if (state.IsKeyDown(Keys.W))
            {
                velocity.Y = -8;
            }
            if (state.IsKeyDown(Keys.A))
            {
                velocity.X = -8;
            }
            if (state.IsKeyDown(Keys.D))
            {
                velocity.X = 8;
            }
            if (state.IsKeyDown(Keys.S))
            {
                velocity.Y = 8;
            }
            pos += velocity;
        }

        public Vector2 getPos()
        {
            return pos;
        }

        public Vector2 getDir()
        {
            return pos + (velocity * 10);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.missile, pos, Color.White);
        }
    }
}