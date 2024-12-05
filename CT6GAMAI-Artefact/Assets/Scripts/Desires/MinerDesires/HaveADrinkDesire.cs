using UnityEngine;

public class HaveADrinkDesire : Desire
{
    public HaveADrinkDesire()
    {
        State = new HaveADrink();
    }

    public override void CalculateDesire(Worker worker)
    {
        Miner miner = (Miner)worker;

        if (miner == null)
        {
            Debug.LogError("ERROR: Attempted to call Desire::CalculateDesire on something that does not implement this desire");
            return;
        }

        DesireVal = Mathf.Clamp((float)miner.m_Thirst / miner.maxThirst, 0, 1);
    }
}
