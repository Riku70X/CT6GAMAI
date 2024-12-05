using UnityEngine;

public class RunAroundDesire : Desire
{
    public RunAroundDesire()
    {
        State = new RunAround();
    }

    public override void CalculateDesire(Worker worker)
    {
        MinersDog minersDog = (MinersDog)worker;

        if (minersDog == null)
        {
            Debug.LogError("ERROR: Attempted to call Desire::CalculateDesire on something that does not implement this desire");
            return;
        }

        DesireVal = Mathf.Clamp((float)minersDog.m_Boredom / minersDog.maxBoredom, 0, 1);
    }
}
