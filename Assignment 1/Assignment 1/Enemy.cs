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
    class Enemy : GameObject
    {
        List<Missile> missiles = new List<Missile>();
        State currentState;
        Player target;

        public float targetDistance, maxDistance, distanceThreshold;
        public float currHealth, maxHealth, healthThreshold;
        public float currAmmo, maxAmmo, ammoThreshold;

        bool targetHostile;
        public bool TargetHostile
        {
            get { return targetHostile; }
            set { targetHostile = value; }
        }

        float tAttack, tHeal, tReload, reloadSpeed, attackSpeed;


        FuSM fusm;

        public Enemy(Vector2 pos, Player target) : base(TextureManager.enemy, pos)
        {
            this.pos = pos - Offset();
            this.target = target;

            ammoThreshold = 5;
            healthThreshold = 30;
            maxAmmo = 15;
            currAmmo = 15;
            maxHealth = 50;
            currHealth = 50;
            maxDistance = 300;
            distanceThreshold = 200;

            SetAttackSpeed(15);
            SetReloadSpeed(75);

            fusm = new FuSM(this);
            currentState = FSMAttacking.Instance();
        }


        public void ConstructAndRunDT()
        {
            CState rf = new CState(DTReloading.Instance(), DTAttacking.Instance(), this, EmptyAmmo());
            CState rf_a = new CState(rf, DTApproaching.Instance(), this, targetDistance < maxDistance);
            CState rfa_h = new CState(rf_a, DTHealing.Instance() , this, currHealth > healthThreshold);
            CState rfah_d = new CState(rfa_h, DTDeparting.Instance(), this, TargetHostile == false);

            rfah_d.Execute(this);
        }

        public void ChangeState(State newState)
        {
            Console.WriteLine(newState);
            currentState.Exit();
            currentState = newState;
            currentState.Enter();
        }
        public void Fire(float a)
        {
            tAttack += 0.5f * a;
            if (tAttack > attackSpeed)
            {
                Missile m = new Missile(pos + Offset(), target);
                missiles.Add(m);
                tAttack = 0;
                currAmmo--;
            }
        }
        public void Reload(float a)
        {
            tReload += 0.5f * a;
            if (tReload > reloadSpeed)
            {
                currAmmo = maxAmmo;
                tReload = 0;
            }
        }
        public void Approach(float a)
        {
            Vector2 dir = target.getPos() - pos + target.Offset();
            dir.Normalize();
            pos += dir * a;
        }
        public void Depart(float a)
        {
            Vector2 dir = target.getPos() - pos + target.Offset();
            dir.Normalize();

            if(a > 10)
            {
                a = 10;
            }
            pos -= dir * a;
        }
        public void Heal(float a)
        {
            tHeal += 0.5f * a;
            if (tHeal > attackSpeed)
            {
                tHeal = 0;
                currHealth += 1;
            }
        }


        public override void Update(GameTime gameTime)
        {
            targetDistance = Vector2.Distance(pos, target.getPos());
            
            foreach (Missile b in missiles)
            {
                b.Update(gameTime);
            }

            //if (currentState != null)
            //{
            //    currentState.Execute(this);
            //}

            //fusm.UpdateMachine();

            ConstructAndRunDT();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach (Missile m in missiles)
            {
                m.Draw(spriteBatch);
            }

            spriteBatch.DrawString(TextureManager.font, currHealth.ToString(), new Vector2(0, 0), Color.Black);
            spriteBatch.DrawString(TextureManager.font, currAmmo.ToString(), new Vector2(0, 15), Color.Black);
        }


        public bool EmptyAmmo()
        {
            return currAmmo <= 0;
        }
        public bool TargetWithinRange()
        {
            if (Range().Intersects(target.HitBox()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Rectangle Range()
        {
            return new Rectangle(HitBox().X - 100, HitBox().Y - 100, tex.Width + 200, tex.Height + 200);
        }
        public void SetAttackSpeed(float i)
        {
            attackSpeed = i;
        }
        public void SetReloadSpeed(float i)
        {
            reloadSpeed = i;
        }
    }
}
