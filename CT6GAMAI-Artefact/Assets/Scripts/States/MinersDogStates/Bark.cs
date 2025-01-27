using Assets.Scripts.StateMachines;
using UnityEngine;

namespace Assets.Scripts.States.MinersDogStates
{
    public class Bark : State
    {
        public override void Execute(DesireBasedStateMachine DesireBasedStateMachine)
        {
            MinersDogStateMachine MinersDogStateMachine = (MinersDogStateMachine)DesireBasedStateMachine;

            if (MinersDogStateMachine == null)
            {
                Debug.LogError("ERROR: Attempted to call State::Execute on something that does not implement this state");
                return;
            }

            // Print out information on what it is doing...
            Debug.Log("Barking!");

            // Increment the miner's dog's boredom
            MinersDogStateMachine.m_Boredom++;
        }
    }
}
