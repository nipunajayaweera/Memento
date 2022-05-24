using ShapeMover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeMover.Utilities.Memento
{
    public class Originator
    {
        private Circle circle;

        public void Set(Circle circle)
        {
            this.circle = circle;
        }

        public Memento StoreInMemento()
        {
            return new Memento(this.circle);
        }

        public Circle RestoreFromMemento(Memento memento)
        {
            circle = memento.GetSavedCircle();
            return circle;
        }
    }
}
