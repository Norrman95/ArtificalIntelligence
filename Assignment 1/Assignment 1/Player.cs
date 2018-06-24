using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Render;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence;

namespace Assignment_1
{
    class Player : GameObject
    {
        public bool Aggressive = false;
        Enemy enemy;

        public Player(Vector2 pos) : base(TextureManager.player, pos)
        {
            
        }

        public override void Update(GameTime gt)
        {
            pos = KeyMouseReader.GetMousePosition() - Offset();

            if(KeyMouseReader.LeftPressed())
            {
                enemy.TargetHostile = true;
                if(enemy.currHealth > 1)
                enemy.currHealth -= 0.5f;
            }
            else
            {
                enemy.TargetHostile = false;
            }
        }

        public void Enemy(Enemy e)
        {
            enemy = e;
        }

        public Vector2 getPos()
        {
            return pos;
        }
    }
}