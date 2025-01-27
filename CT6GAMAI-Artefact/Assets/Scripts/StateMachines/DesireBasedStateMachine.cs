using System.Collections;
using Assets.Scripts.DataTypes;
using Assets.Scripts.Desires;
using Assets.Scripts.States;
using UnityEngine;

namespace Assets.Scripts.StateMachines
{
    public abstract class DesireBasedStateMachine : MonoBehaviour
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

        protected DesireBasedStateMachine()
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
}
