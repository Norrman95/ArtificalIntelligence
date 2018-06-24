using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1
{
    class FuSM
    {
        List<State> allStates = new List<State>();
        List<State> activeStates = new List<State>();
        List<State> inactiveStates = new List<State>();

        Enemy enemy;

        public FuSM(Enemy enemy)
        {
            this.enemy = enemy;

            allStates.Add(FuSMAttacking.Instance());
            allStates.Add(FuSMReloading.Instance());
            allStates.Add(FuSMHealing.Instance());
            allStates.Add(FuSMApproaching.Instance());
            allStates.Add(FuSMDeparting.Instance());
        }

        public void UpdateMachine()
        {
            activeStates.Clear();
            inactiveStates.Clear();

            for (int i = 0; i < allStates.Count(); i++)
            {
                if (allStates[i].CalculateActivation(enemy) > 0)
                {
                    activeStates.Add(allStates[i]);
                }
                else
                {
                    if (allStates[i].active)
                    {
                        inactiveStates.Add(allStates[i]);
                    }
                }
            }
            foreach (State s in inactiveStates)
            {
                s.Exit();
            }
            foreach (State s in activeStates)
            {
                s.Execute(enemy);
            }
        }
    }
}
