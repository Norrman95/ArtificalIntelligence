using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1
{
    class FSMAttacking : State
    {
        public static State Instance()
        {
            return new FSMAttacking();
        }
        public override void Execute(Enemy enemy)
        {
            if (enemy.EmptyAmmo())
            {
                enemy.ChangeState(FSMReloading.Instance());
            }
            else
            {
                if (enemy.TargetWithinRange())
                {
                    enemy.Fire(1);
                }
                else
                {
                    enemy.ChangeState(FSMApproaching.Instance());
                }
            }
        }
    }
    class FSMReloading : State
    {
        public static State Instance()
        {
            return new FSMReloading();
        }
        public override void Execute(Enemy enemy)
        {
            if (enemy.EmptyAmmo())
            {
                enemy.Reload(1);
            }
            else
            {
                enemy.ChangeState(FSMAttacking.Instance());
            }
        }
    }

    class FSMApproaching : State
    {
        public static State Instance()
        {
            return new FSMApproaching();
        }
        public override void Execute(Enemy enemy)
        {
            if (enemy.TargetWithinRange())
            {
                enemy.ChangeState(FSMAttacking.Instance());
            }
            else
            {
                enemy.Approach(1);
            }
        }
    }
}
