using UnityEngine;

/// <summary>
/// The AIController for our shopper. It needs a ShopperBlackboard component.
/// </summary>
[RequireComponent(typeof(ShopperBlackboard))]
public class ShopperAIController : MonoBehaviour
{
    private BehaviourTreeNode RootNode;

    void Start()
    {
        ShopperBlackboard BlackboardComponent = GetComponent<ShopperBlackboard>();

        RootNode = new Selector(BlackboardComponent);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
