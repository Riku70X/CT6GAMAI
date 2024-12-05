using UnityEngine;

public class BankingGold : State
{
    public override void Execute(DesireBasedStateMachine DesireBasedStateMachine)
    {
        MinerStateMachine MinerStateMachine = (MinerStateMachine)DesireBasedStateMachine;

        if (MinerStateMachine == null) 
        {
            Debug.LogError("ERROR: Attempted to call State::Execute on something that does not implement this state");
            return;
        }

        // Move gold to the bank
        MinerStateMachine.m_BankedGold += MinerStateMachine.m_Gold;
        MinerStateMachine.m_Gold = 0;

        // Print out information on what it is doing...
        Debug.Log("Banking gold! Current balance: " + MinerStateMachine.m_BankedGold + " gold.");

        // Long trip to the bank
        MinerStateMachine.m_Tiredness += 3;
        MinerStateMachine.m_Thirst += 2;
    }
}
