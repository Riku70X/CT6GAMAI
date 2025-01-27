using Assets.Scripts.StateMachines;
using Assets.Scripts.States;

namespace Assets.Scripts.Desires
{
    public abstract class Desire
    {
        //public:

        // The state this Desire corresponds to
        public State State;

        //The value used by the comparator
        public float DesireVal;

        public Desire()
        {
            State = null; // in child classes, create a reference to an appropriate state
            DesireVal = 0.05f; // default value
        }

        // This sets the DesireVal in child classes (does nothing here)
        public abstract void CalculateDesire(DesireBasedStateMachine DesireBasedStateMachine);

        // Comparator (logic taken from https://visualstudiomagazine.com/articles/2012/11/01/priority-queues-with-c/listing2.aspx)
        public int CompareTo(Desire other)
        {
            if (DesireVal < other.DesireVal) return 1;
            else if (DesireVal > other.DesireVal) return -1;
            else return 0;
            // Swapped 1 and -1, so that higher values get higher priority in the queue
        }
    }
}
