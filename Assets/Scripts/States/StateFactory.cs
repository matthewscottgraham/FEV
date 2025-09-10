using System;
using System.Collections.Generic;

namespace States
{
    public class StateFactory
    {
        private readonly List<IState> _states = new List<IState>();

        public IState GetState(Type type)
        {
            foreach (var state in _states)
            {
                if (state.GetType() == type)
                    return state;
            }

            return CreateState(type);
        }

        private IState CreateState(Type type)
        {
            var newState = Activator.CreateInstance(type) as IState;
            _states.Add(newState);
            return newState;
        }
    }
}