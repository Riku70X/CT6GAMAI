using Assets.Scripts.StateMachines;

namespace Assets.Scripts.States
{
    public abstract class State
    {
        public abstract void Execute(DesireBasedStateMachine DesireBasedStateMachine);
    }
}
