using Phac.Utility;

namespace Phac.Character
{
    public class StateTransition : ITransition
    {
        public IState To { get; }

        public IPredicate Condition { get; }

        public StateTransition(IState to, IPredicate condition) {
            To = to;
            Condition = condition;
        }
    }
}