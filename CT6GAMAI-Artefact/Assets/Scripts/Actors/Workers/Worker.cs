using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public abstract class Worker : MonoBehaviour
{    
    //protected:

    // State Machine. Runs every two seconds.
    protected abstract void ChooseState();

    protected void ChangeState(State newState)
    {
        pState = newState;
    }

    // Our state
    protected State pState;

    // Our priority queue
    protected readonly PriorityQueue<Desire> DesirePriorityQueue;

    protected Worker()
    {
        DesirePriorityQueue = new PriorityQueue<Desire>();
    }

    //private:

    private void Start()
    {
        StartCoroutine(UpdateWithDelay());
    }

    private IEnumerator UpdateWithDelay()
    {
        while (true)
        {
            ChooseState();

            pState.Execute(this);

            yield return new WaitForSeconds(2.0f);
        }
    }
}
