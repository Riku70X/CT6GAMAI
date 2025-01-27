using Assets.Scripts.StateMachines;
using Assets.Scripts.States.MinersDogStates;
using UnityEngine;

namespace Assets.Scripts.Desires.MinersDogDesires
{
    public class BarkDesire : Desire
    {
        public BarkDesire()
        {
            State = new Bark();
        }

        public override void CalculateDesire(DesireBasedStateMachine DesireBasedStateMachine)
        {
            MinersDogStateMachine MinersDogStateMachine = (MinersDogStateMachine)DesireBasedStateMachine;

            if (MinersDogStateMachine == null)
            {
                Debug.LogError("ERROR: Attempted to call Desire::CalculateDesire on something that does not implement this desire");
                return;
            }

            // 20% chance to "find gold" every turn, and therefore bark
            if (Random.Range(0, 5) == 0)
            {
                DesireVal = 1;
            }
            else
            {
                DesireVal = 0;
            }
        }
    }
}
