namespace Status
{
    public class StateChange
    {
        public State CurrentState { get; }

        public State NextState { get; }

        public StateChange(
            State currentState, 
            State nextState)
        {
            this.CurrentState = currentState;
            this.NextState = nextState;
        }

        public override string ToString()
        {
            return $"{this.CurrentState}->{this.NextState}";
        }
    }
}
