using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1
{
    class FuSMAttacking : State
    {
        public static State Instance()
        {
            return new FuSMAttacking();
        }
        public override void Enter()
        {
            active = true;
        }
        public override void Execute(Enemy enemy)
        {
            enemy.Fire(activationLevel);
        }
        public override void Exit()
        {
            active = false;
        }
        public override float CalculateActivation(Enemy enemy)
        {
            activationLevel = enemy.currAmmo / enemy.maxAmmo;

            if (!enemy.TargetWithinRange())
            {
                activationLevel = 0;
            }
            if (enemy.EmptyAmmo())
            {
                activationLevel = 0;
            }

            return activationLevel;
        }
    }

    class FuSMReloading : State
    {
        public static State Instance()
        {
            return new FuSMReloading();
        }
        public override void Enter()
        {
            active = true;
        }
        public override void Execute(Enemy enemy)
        {
            enemy.Reload(activationLevel);
        }
        public override void Exit()
        {
            active = false;
        }
        public override float CalculateActivation(Enemy enemy)
        {
            activationLevel = enemy.currAmmo > enemy.ammoThreshold ? 0 : enemy.ammoThreshold / enemy.currAmmo;
            return activationLevel;
        }
    }

    class FuSMHealing : State
    {
        public static State Instance()
        {
            return new FuSMHealing();
        }
        public override void Enter()
        {
            active = true;
        }
        public override void Execute(Enemy enemy)
        {
            enemy.Heal(activationLevel);
        }
        public override void Exit()
        {
            active = false;
        }
        public override float CalculateActivation(Enemy enemy)
        {
            if (enemy.TargetHostile)
            {
                activationLevel = 0;
            }
            else
            {
                if (enemy.currHealth >= enemy.maxHealth)
                {
                    activationLevel = 0;
                    enemy.currHealth = enemy.maxHealth;
                }
                else
                {
                    activationLevel = enemy.currHealth > enemy.healthThreshold ? enemy.currHealth / enemy.maxHealth : enemy.maxHealth / enemy.currHealth;
                }
            }
            return activationLevel;
        }
    }

    class FuSMApproaching : State
    {
        public static State Instance()
        {
            return new FuSMApproaching();
        }
        public override void Enter()
        {
            active = true;
        }
        public override void Execute(Enemy enemy)
        {
            enemy.Approach(activationLevel);
        }
        public override void Exit()
        {
            active = false;
        }
        public override float CalculateActivation(Enemy enemy)
        {
            activationLevel = enemy.targetDistance / enemy.maxDistance;
            return activationLevel;
        }
    }
    class FuSMDeparting : State
    {
        public static State Instance()
        {
            return new FuSMDeparting();
        }
        public override void Enter()
        {
            active = true;
        }
        public override void Execute(Enemy enemy)
        {
            enemy.Depart(activationLevel);
        }
        public override void Exit()
        {
            active = false;
        }
        public override float CalculateActivation(Enemy enemy)
        {
            activationLevel = enemy.currHealth > enemy.healthThreshold ? 0 : enemy.maxHealth / enemy.currHealth;
            return activationLevel;
        }
    }
}