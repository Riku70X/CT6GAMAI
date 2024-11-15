using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankingGoldDesire : Desire
{
    public BankingGoldDesire()
    {
        State = new BankingGold();
    }

    public override void CalculateDesire(Worker worker)
    {
        Miner miner = (Miner)worker;

        if (miner == null) 
        {
            Debug.LogError("ERROR: Attempted to call Desire::CalculateDesire on something that does not implement this desire");
            return;
        }

        DesireVal = Mathf.Clamp((float)miner.m_Gold / miner.maxGoldStorage, 0, 1);
    }
}
