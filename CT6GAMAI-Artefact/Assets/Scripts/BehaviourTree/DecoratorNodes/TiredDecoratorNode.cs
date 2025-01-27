using Assets.Scripts.BehaviourTree.Blackboards;

namespace Assets.Scripts.BehaviourTree.DecoratorNodes
{
    public class TiredDecorator : ConditionalDecorator
    {
        ShopperBlackboard ShopperBlackboard;
        public TiredDecorator(BehaviourTreeNode WrappedNode, Blackboard Blackboard) : base(WrappedNode, Blackboard)
        {
            ShopperBlackboard = (ShopperBlackboard)Blackboard;
        }

        public override bool CheckStatus()
        {
            //ShopperBlackboard.ShopperStateMachine.m_BankedGold = 2;

            throw new System.NotImplementedException();
        }
    }
}
