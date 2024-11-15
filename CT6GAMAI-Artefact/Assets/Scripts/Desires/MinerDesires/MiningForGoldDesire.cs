using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningForGoldDesire : Desire
{
    public MiningForGoldDesire()
    {
        State = new MiningForGold();
    }

    public override void CalculateDesire(Worker worker)
    {
        DesireVal = 0.9f; // Default action with a high desire
    }
}
