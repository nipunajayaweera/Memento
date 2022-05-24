using ShapeMover.Utilities;
using System;
using System.Windows;
using System.Windows.Media;

namespace ShapeMover.Models
{
    public class Circle: NotifyPropertyChangedBase, ICloneable
    {
        public Circle()
        {

        }
        public Circle(Circle circle)
        {
            Id = circle.Id;
            Color = circle.Color;
            Position = circle.Position;
        }
        public string Id { get; set; }

        private Point _position;
        public Point Position {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                RaisePropertyChanged(nameof(Position));
            }
        }

        private Point _positionOld;
        public Point PositionOld {
            get
            {
                return _positionOld;
            }
            set
            {
                _positionOld = value;
                RaisePropertyChanged(nameof(PositionOld));
            }
        }
        public SolidColorBrush Color { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
