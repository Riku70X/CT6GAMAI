public class SniffOutGoldDesire : Desire
{
    public SniffOutGoldDesire()
    {
        State = new SniffOutGold();
    }

    public override void CalculateDesire(DesireBasedStateMachine DesireBasedStateMachine)
    {
        DesireVal = 0.9f; // Default action with a high desire
    }
}
