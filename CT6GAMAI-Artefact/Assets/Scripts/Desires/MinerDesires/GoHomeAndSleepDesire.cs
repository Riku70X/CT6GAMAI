using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoHomeAndSleepDesire : Desire
{
    public GoHomeAndSleepDesire()
    {
        State = new GoHomeAndSleep();
    }

    public override void CalculateDesire(Worker worker)
    {
        Miner miner = (Miner)worker;

        if (miner == null)
        {
            Debug.LogError("ERROR: Attempted to call Desire::CalculateDesire on something that does not implement this desire");
            return;
        }

        DesireVal = Mathf.Clamp((float)miner.m_Tiredness / miner.maxTiredness, 0, 1);
    }
}
