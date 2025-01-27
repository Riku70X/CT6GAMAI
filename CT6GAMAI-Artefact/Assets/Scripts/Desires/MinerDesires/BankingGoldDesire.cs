using Assets.Scripts.StateMachines;
using Assets.Scripts.States.MinerStates;
using UnityEngine;

namespace Assets.Scripts.Desires.MinerDesires
{
    public class BankingGoldDesire : Desire
    {
        public BankingGoldDesire()
        {
            State = new BankingGold();
        }

        public override void CalculateDesire(DesireBasedStateMachine DesireBasedStateMachine)
        {
            MinerStateMachine MinerStateMachine = (MinerStateMachine)DesireBasedStateMachine;

            if (MinerStateMachine == null) 
            {
                Debug.LogError("ERROR: Attempted to call Desire::CalculateDesire on something that does not implement this desire");
                return;
            }

            DesireVal = Mathf.Clamp((float)MinerStateMachine.m_Gold / MinerStateMachine.maxGoldStorage, 0, 1);
        }
    }
}
