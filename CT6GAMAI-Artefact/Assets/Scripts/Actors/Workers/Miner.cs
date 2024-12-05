public class Miner : Worker
{
    //public:

    // These values can be monitored and edited by our "states"
    public int m_Gold;
    public int m_BankedGold;
    public int m_Tiredness;
    public int m_Thirst;

    // These values cannot change at runtime
    public readonly int maxGoldStorage;
    public readonly int maxTiredness;
    public readonly int maxThirst;

    public Miner()
    {
        // Set the intial state as MiningForGold
        pState = new MiningForGold();

        m_Gold = 0;
        m_BankedGold = 0;
        m_Tiredness = 0;
        m_Thirst = 0;

        maxGoldStorage = 5;
        maxTiredness = 15;
        maxThirst = 7;

        m_DesireToMine = new MiningForGoldDesire();
        m_DesireToBank = new BankingGoldDesire();
        m_DesireToDrink = new HaveADrinkDesire();
        m_DesireToSleep = new GoHomeAndSleepDesire();
    }

    //protected:

    protected override void ChooseState()
    {
        // If sleeping, stay sleeping until no longer tired
        if (pState is GoHomeAndSleep && m_Tiredness > 0)
        {
            return;
        }

        m_DesireToMine.CalculateDesire(this);
        m_DesireToBank.CalculateDesire(this);
        m_DesireToDrink.CalculateDesire(this);
        m_DesireToSleep.CalculateDesire(this);

        DesirePriorityQueue.Clear();
        DesirePriorityQueue.Enqueue(m_DesireToMine);
        DesirePriorityQueue.Enqueue(m_DesireToBank);
        DesirePriorityQueue.Enqueue(m_DesireToDrink);
        DesirePriorityQueue.Enqueue(m_DesireToSleep);

        if (!DesirePriorityQueue.IsEmpty())
        {
            Desire GreatestDesire = DesirePriorityQueue.Peek();

            ChangeState(GreatestDesire.State);
        }
    }

    //private:

    // Desire system
    private readonly MiningForGoldDesire m_DesireToMine;
    private readonly BankingGoldDesire m_DesireToBank;
    private readonly HaveADrinkDesire m_DesireToDrink;
    private readonly GoHomeAndSleepDesire m_DesireToSleep;
}
