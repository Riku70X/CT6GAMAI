using UnityEngine;

[RequireComponent(typeof(ShopperStateMachine))]
public class ShopperBlackboard : Blackboard
{
    public ShopperStateMachine ShopperStateMachine;

    private void Start()
    {
        ShopperStateMachine = GetComponent<ShopperStateMachine>();
    }
}
