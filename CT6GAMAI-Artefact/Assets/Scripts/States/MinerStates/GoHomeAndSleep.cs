using Assets.Scripts.StateMachines;
using UnityEngine;

namespace Assets.Scripts.States.MinerStates
{
    public class GoHomeAndSleep : State
    {
        public override void Execute(DesireBasedStateMachine DesireBasedStateMachine)
        {
            MinerStateMachine MinerStateMachine = (MinerStateMachine)DesireBasedStateMachine;

            if (MinerStateMachine == null)
            {
                Debug.LogError("ERROR: Attempted to call State::Execute on something that does not implement this state");
                return;
            }

            // Print out information on what it is doing...
            Debug.Log("Sleeping...");

            // Decrease tiredness
            MinerStateMachine.m_Tiredness -= 2;
        }
    }
}
