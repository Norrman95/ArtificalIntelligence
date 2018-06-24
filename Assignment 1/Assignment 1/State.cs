using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1
{
    abstract class State
    {
        public float activationLevel;
        public bool active;

        public virtual void Init()
        {
            this.activationLevel = 0.0f;
            this.active = false;
        }
        public virtual void Enter() { }
        public abstract void Execute(Enemy enemy);
        public virtual void Exit() { }
        public virtual float CalculateActivation(Enemy enemy)
        {
            return activationLevel;
        }

        //DT
        State state;
        public State(State state)
        {
            this.state = state;
        }
        public State() { }
    }
    class CState : State
    {
        State right, left;
        bool condition;

        public CState(State left, State right, Enemy enemy, bool condition)
        {
            this.condition = condition;
            this.left = left;
            this.right = right;
        }

        public override void Execute(Enemy enemy)
        {
            if (condition)
            {
                left.Execute(enemy);
            }
            else
            {
                right.Execute(enemy);
            }
        }
    }
}
