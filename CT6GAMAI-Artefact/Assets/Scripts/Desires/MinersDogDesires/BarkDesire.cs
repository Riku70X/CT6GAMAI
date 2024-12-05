using UnityEngine;

public class BarkDesire : Desire
{
    public BarkDesire()
    {
        State = new Bark();
    }

    public override void CalculateDesire(Worker worker)
    {
        MinersDog minersDog = (MinersDog)worker;

        if (minersDog == null)
        {
            Debug.LogError("ERROR: Attempted to call Desire::CalculateDesire on something that does not implement this desire");
            return;
        }

        // 20% chance to "find gold" every turn, and therefore bark
        if (Random.Range(0, 5) == 0)
        {
            DesireVal = 1;
        }
        else
        {
            DesireVal = 0;
        }
    }
}
