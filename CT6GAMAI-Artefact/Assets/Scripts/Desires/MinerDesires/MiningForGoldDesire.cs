public class MiningForGoldDesire : Desire
{
    public MiningForGoldDesire()
    {
        State = new MiningForGold();
    }

    public override void CalculateDesire(DesireBasedStateMachine DesireBasedStateMachine)
    {
        DesireVal = 0.9f; // Default action with a high desire
    }
}
