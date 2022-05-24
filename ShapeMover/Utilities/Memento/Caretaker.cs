using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeMover.Utilities.Memento
{
    public class Caretaker
    {
        private Stack<Memento> savedStates = new Stack<Memento>();
        private Stack<Memento> savedStatesRedo = new Stack<Memento>();

        public bool HasStates {
            get
            {
                return savedStates.Count > 0;
            }
        }

        public bool HasRedoStates {
            get
            {
                return savedStatesRedo.Count > 0;
            }
        }

        public void AddMemento(Memento memento)
        {
            savedStates.Push(memento);
            savedStatesRedo.Clear();
        }

        public Memento GetMemento()
        {
            var state = savedStates.Pop();
            savedStatesRedo.Push(state);
            return state;
        }

        public Memento RedoMemento()
        {
            var state = savedStatesRedo.Pop();
            savedStates.Push(state);
            return state;
        }

    }
}
