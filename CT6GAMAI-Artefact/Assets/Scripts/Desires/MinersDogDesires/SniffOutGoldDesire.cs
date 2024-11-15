using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniffOutGoldDesire : Desire
{
    public SniffOutGoldDesire()
    {
        State = new SniffOutGold();
    }

    public override void CalculateDesire(Worker worker)
    {
        DesireVal = 0.9f; // Default action with a high desire
    }
}
