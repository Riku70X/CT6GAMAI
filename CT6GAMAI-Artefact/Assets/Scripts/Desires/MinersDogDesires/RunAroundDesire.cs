using Assets.Scripts.StateMachines;
using Assets.Scripts.States.MinersDogStates;
using UnityEngine;

namespace Assets.Scripts.Desires.MinersDogDesires
{
    public class RunAroundDesire : Desire
    {
        public RunAroundDesire()
        {
            State = new RunAround();
        }

        public override void CalculateDesire(DesireBasedStateMachine DesireBasedStateMachine)
        {
            MinersDogStateMachine MinersDogStateMachine = (MinersDogStateMachine)DesireBasedStateMachine;

            if (MinersDogStateMachine == null)
            {
                Debug.LogError("ERROR: Attempted to call Desire::CalculateDesire on something that does not implement this desire");
                return;
            }

            DesireVal = Mathf.Clamp((float)MinersDogStateMachine.m_Boredom / MinersDogStateMachine.maxBoredom, 0, 1);
        }
    }
}
