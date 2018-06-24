using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1
{
    class DTAttacking : State
    {
        public static State Instance()
        {
            return new DTAttacking();
        }
        public override void Execute(Enemy enemy)
        {
            enemy.Fire(1);
        }
    }

    class DTReloading : State
    {
        public static State Instance()
        {
            return new DTReloading();
        }
        public override void Execute(Enemy enemy)
        {
            enemy.Reload(1);
        }
    }

    class DTHealing : State
    {
        public static State Instance()
        {
            return new DTHealing();
        }
        public override void Execute(Enemy enemy)
        {
            enemy.Heal(1);
        }
    }

    class DTApproaching : State
    {
        public static State Instance()
        {
            return new DTApproaching();
        }
        public override void Execute(Enemy enemy)
        {
            enemy.Approach(1);
        }
    }
    class DTDeparting : State
    {
        public static State Instance()
        {
            return new DTDeparting();
        }
        public override void Execute(Enemy enemy)
        {
            enemy.Depart(1);
        }
    }
}
