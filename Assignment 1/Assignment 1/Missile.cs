using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence;

namespace Assignment_1
{
    class Missile : GameObject
    {
        Vector2 dir;

        public Missile(Vector2 pos, Player target) : base(TextureManager.missile, pos)
        {
            this.pos = pos;
            dir = target.getPos() - pos + target.Offset();
        }
        public override void Update(GameTime gt)
        {
            dir.Normalize();
            pos += dir * 10;
        }
    }
}
