using System;
[Serializable]
public class TransitionConnection<Transition> where Transition : struct, Enum
{
        public Transition transition;
        public StateHandler from;
        public StateHandler to;
        
        public TransitionConnection(StateHandler from, StateHandler to, Transition transition)
        {
                this.from = from;
                this.to = to;
                this.transition = transition;
        }

}