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
    class Enemy
    {
        SteeringModes current = SteeringModes.Wander;
        Random rnd = new Random();

        Vector2 pos;
        Vector2 cPos;

        float speed = 5;
        Vector2 velocity;
        Vector2 maxVelocity = new Vector2(5, 5);

        float arriveRange = 150;
        float radius = 200;
        float diamater = 400;
        float force = 0.1f;

        public Enemy(Vector2 pos)
        {
            this.pos = pos;

            velocity = new Vector2(1, 1);
        }

        void TruncateSpeed(Vector2 a)
        {
            if(a.X > speed)
            {
                a.X = speed;
            }
            if (a.Y > speed)
            {
                a.Y = speed;
            }
        }

        public void Approach(Vector2 dest)
        {
            Vector2 diseredVel = dest - pos;
            diseredVel.Normalize();
            diseredVel *= speed;

            Vector2 steering = diseredVel - velocity;
            if (steering.Length() > force)
            {
                steering *= force;
            }

            velocity += steering;
            TruncateSpeed(velocity);

            pos += velocity;
        }

        public void Pursuit(Player target)
        {
            cPos = target.getDir();
            Approach(cPos);
        }
        public void Arrive(Vector2 dest)
        {
            Vector2 subVector = dest - pos;
            double distance = Math.Sqrt((subVector.X * subVector.X) + (subVector.Y * subVector.Y));

            if (distance < arriveRange)
            {
                subVector.Normalize();
                subVector *= (float)(speed * (distance / arriveRange));
            }
            else
            {
                subVector.Normalize();
                subVector *= speed;
            }

            pos += subVector;
        }
        public void Wander(GameTime gameTime)
        {
            float rndX = (float)rnd.Next(0, 360);
            float rndY = (float)rnd.Next(0, 360);
            Vector2 rndDir = new Vector2((float)Math.Cos(rndX) * diamater, (float)Math.Sin(rndY) * diamater);

            velocity.Normalize();
            cPos = pos + (velocity * radius);

            Vector2 dest = cPos + rndDir;
            Approach(dest);
        }
        public void Update(GameTime gameTime, Player target)
        {
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.F1))
            {
                current = SteeringModes.Approach;
            }
            if (state.IsKeyDown(Keys.F2))
            {
                current = SteeringModes.Pursuit;
            }
            if (state.IsKeyDown(Keys.F3))
            {
                current = SteeringModes.Arrive;
            }
            if (state.IsKeyDown(Keys.F4))
            {
                current = SteeringModes.Wander;
            }
            if (state.IsKeyDown(Keys.P))
            {
                current = SteeringModes.Paused;
            }

            switch (current)
            {
                case SteeringModes.Approach:
                    Approach(target.getPos());
                    break;
                case SteeringModes.Pursuit:
                    Pursuit(target);
                    break;
                case SteeringModes.Arrive:
                    Arrive(target.getPos());
                    break;
                case SteeringModes.Wander:
                    Wander(gameTime);
                    break;
                case SteeringModes.Paused:
                    break;
                default:
                    break;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.enemy, pos - new Vector2(TextureManager.enemy.Width / 2, TextureManager.enemy.Height / 2), Color.White);
            spriteBatch.Draw(TextureManager.missile, cPos, Color.Red);
        }
    }
}
