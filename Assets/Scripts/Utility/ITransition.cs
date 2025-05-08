using Phac.Character;

namespace Phac.Utility
{
    public interface ITransition
    {
        IState To { get; }
        IPredicate Condition { get; }
    }
}