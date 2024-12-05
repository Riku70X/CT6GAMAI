using UnityEngine;

public class GoHomeAndSleep : State
{
    public override void Execute(Worker worker)
    {
        Miner miner = (Miner)worker;

        if (miner == null)
        {
            Debug.LogError("ERROR: Attempted to call State::Execute on something that does not implement this state");
            return;
        }

        // Print out information on what it is doing...
        Debug.Log("Sleeping...");

        // Decrease tiredness
        miner.m_Tiredness -= 2;
    }
}
