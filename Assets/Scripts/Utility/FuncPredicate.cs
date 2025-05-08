using System;

namespace Phac.Utility
{
    public class FuncPredicate : IPredicate
    {
        private readonly Func<bool> m_Func;
        public bool Evaulate() => m_Func.Invoke();
        public FuncPredicate(Func<bool> func) {
            m_Func = func;
        }
    }
}