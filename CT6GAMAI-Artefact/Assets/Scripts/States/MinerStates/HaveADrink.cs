using UnityEngine;

public class HaveADrink : State
{
    public override void Execute(DesireBasedStateMachine DesireBasedStateMachine)
    {
        MinerStateMachine MinerStateMachine = (MinerStateMachine)DesireBasedStateMachine;

        if (MinerStateMachine == null)
        {
            Debug.LogError("ERROR: Attempted to call State::Execute on something that does not implement this state");
            return;
        }

        // Print out information on what it is doing...
        Debug.Log("Having a drink.");

        // Drink water
        MinerStateMachine.m_Thirst -= 5;

        // Short trip to the water fountain
        MinerStateMachine.m_Tiredness += 2;
    }
}
