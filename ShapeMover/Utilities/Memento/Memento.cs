using ShapeMover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeMover.Utilities.Memento
{
    public class Memento
    {
        private readonly Circle circle;
        public Memento(Circle circleSave)
        {
            circle = circleSave;
        }
        public Circle GetSavedCircle()
        {
            return circle;
        }

    }
}
