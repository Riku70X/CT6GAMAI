using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinersDog : Worker
{
    //public:

    // These values can be monitored and edited by our "states"
    public int m_Boredom;

    // These values cannot change at runtime
    public readonly int maxBoredom;

    public MinersDog()
    {
        // Set the initial state as SniffOutGold
        pState = new SniffOutGold();

        m_Boredom = 0;

        maxBoredom = 8;

        m_DesireToSniff = new SniffOutGoldDesire();
        m_DesireToBark = new BarkDesire();
        m_DesireToRun = new RunAroundDesire();
    }

    //protected:

    protected override void ChooseState()
    {
        // If running around, keep running around until no longer bored
        if (pState is RunAround && m_Boredom > 0)
        {
            return;
        }

        m_DesireToSniff.CalculateDesire(this);
        m_DesireToBark.CalculateDesire(this);
        m_DesireToRun.CalculateDesire(this);

        DesirePriorityQueue.Clear();
        DesirePriorityQueue.Enqueue(m_DesireToSniff);
        DesirePriorityQueue.Enqueue(m_DesireToBark);
        DesirePriorityQueue.Enqueue(m_DesireToRun);

        if (!DesirePriorityQueue.IsEmpty())
        {
            Desire GreatestDesire = DesirePriorityQueue.Peek();

            ChangeState(GreatestDesire.State);
        }
    }

    //private:

    // Desire system
    private readonly SniffOutGoldDesire m_DesireToSniff;
    private readonly BarkDesire m_DesireToBark;
    private readonly RunAroundDesire m_DesireToRun;
}
