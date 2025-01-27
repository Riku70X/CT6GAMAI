using Assets.Scripts.StateMachines;
using UnityEngine;

namespace Assets.Scripts.BehaviourTree.Blackboards
{
    [RequireComponent(typeof(ShopperStateMachine))]
    public class ShopperBlackboard : Blackboard
    {
        public ShopperStateMachine ShopperStateMachine;

        private void Start()
        {
            ShopperStateMachine = GetComponent<ShopperStateMachine>();
        }
    }
}
