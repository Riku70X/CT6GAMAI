using UnityEngine;

[RequireComponent(typeof(Miner))]
public class ShopperBlackboard : Blackboard
{
    public Miner ShopperStateMachine;

    private void Start()
    {
        ShopperStateMachine = GetComponent<Miner>();
    }
}
