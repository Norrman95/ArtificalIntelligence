using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1
{
    class GameObject
    {
        public Texture2D tex;
        public Vector2 pos;


        public GameObject(Texture2D tex, Vector2 pos)
        {
            this.tex = tex;
            this.pos = pos;
        }

        public virtual Rectangle HitBox()
        {
            return new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
        }

        public virtual Vector2 Offset()
        {
            return new Vector2(tex.Width / 2, tex.Height / 2);
        }

        public virtual void Update(GameTime gt)
        {
           
        }

        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(tex, pos, Color.White);
        }
    }
}
