using System;
using System.Collections.Generic;
using Phac.Utility;

namespace Phac.Character
{
    public class StateMachine
    {
        StateNode m_Current;
        Dictionary<Type, StateNode> m_Nodes;
        HashSet<ITransition> m_AnyTransitions;

        public void Update()
        {
            m_Current.State?.Update();
        }

        public void FixedUpdate()
        {
            m_Current.State?.FixedUpate();
        }

        private void SetState(IState state)
        {
            m_Current = m_Nodes[state.GetType()];
            m_Current.State?.OnEnter();
        }

        private void ChangeState(IState state)
        {
            if (state == m_Current) return;

            IState previousState = m_Current.State;
            IState nextState = m_Nodes[state.GetType()].State;

            previousState?.OnExit();
            nextState?.OnEnter();
            m_Current = m_Nodes[state.GetType()];
        }

        ITransition GetTransition()
        {
            foreach (var transition in m_AnyTransitions)
            {
                if (transition.Condition.Evaulate())
                {
                    return transition;
                }
            }

            foreach (var transition in m_Current.Transitions)
            {
                if (transition.Condition.Evaulate())
                {
                    return transition;
                }
            }

            return null;
        }

        public void AddTransition(IState from, IState to, IPredicate condition)
        {
            GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
        }

        public void AddAnyTransition(IState to, IPredicate condition)
        {
            m_AnyTransitions.Add(new StateTransition(GetOrAddNode(to).State, condition));
        }

        StateNode GetOrAddNode(IState state)
        {
            var node = m_Nodes.GetValueOrDefault(state.GetType());

            if (node == null)
            {
                node = new StateNode(state);
                m_Nodes.Add(state.GetType(), node);
            }

            return node;
        }

        private class StateNode
        {
            public IState State { get; }
            public HashSet<ITransition> Transitions { get; }

            public StateNode(IState state)
            {
                State = state;
                Transitions = new HashSet<ITransition>();
            }

            public void AddTransition(IState to, IPredicate condition)
            {
                Transitions.Add(new StateTransition(to, condition));
            }
        };
    }
}