using Assets.Scripts.StateMachines;
using UnityEngine;

namespace Assets.Scripts.States.MinerStates
{
    public class MiningForGold : State
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
            Debug.Log("Digging for gold!");

            // Increment the MinerStateMachine's gold amount
            MinerStateMachine.m_Gold++;

            // Working makes you tired and thirsty
            MinerStateMachine.m_Tiredness++;
            MinerStateMachine.m_Thirst++;
        }
    }
}
