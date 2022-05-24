using ShapeMover.Models;
using ShapeMover.Utilities;
using ShapeMover.Utilities.Memento;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ShapeMover.ViewModels
{
    public class ShapeMoverViewModel : NotifyPropertyChangedBase
    {
        readonly Caretaker caretaker = new Caretaker();
        readonly Originator originator = new Originator();

        #region Commands
        private ICommand _addShapeCommand;
        private ICommand _undoCommand;
        private ICommand _redoCommand;
        private ICommand _mouseLeftButtonDownCommand;
        private ICommand _mouseMoveCommand;
        private ICommand _mouseUpCommand;

        public ICommand AddShapeCommand
        {
            get
            {
                if(_addShapeCommand == null)
                {
                    _addShapeCommand = new RelayCommand(AddShape);
                }
                return _addShapeCommand;
            }
        }

        public ICommand MouseLeftButtonDownCommand
        {
            get
            {
                if(_mouseLeftButtonDownCommand == null)
                {
                    _mouseLeftButtonDownCommand = new RelayCommand(MouseLeftButtonDown);
                }
                return _mouseLeftButtonDownCommand;
            }
        }

        public ICommand MouseUpCommand
        {
            get
            {
                if(_mouseUpCommand == null)
                {
                    _mouseUpCommand = new RelayCommand(MouseUp);
                }
                return _mouseUpCommand;
            }
        }


        public ICommand UndoCommand
        {
            get
            {
                if(_undoCommand == null)
                {
                    _undoCommand = new RelayCommand(param => this.Undo(), param => this.IsUndoBtnEnable);
                }
                return _undoCommand;
            }
        }

        public ICommand RedoCommand
        {
            get
            {
                if (_redoCommand == null)
                {
                    _redoCommand = new RelayCommand(param => this.Redo(), param => this.IsRedoBtnEnable);
                }
                return _redoCommand;
            }
        }

        public ICommand MouseMoveCommand
        {
            get
            {
                if (_mouseMoveCommand == null)
                {
                    _mouseMoveCommand = new RelayCommand(MouseMove);
                }
                return _mouseMoveCommand;
            }
        }

        #endregion

        Random r = new Random();
        private ObservableCollection<Circle> _circles = new ObservableCollection<Circle>();
        public ObservableCollection<Circle> Circles
        {
            get { return _circles; }
            set { _circles = value; }
        }

        public Circle selectedItem { get; set; }
        public double selectetItemPosX { get; set; }
        public double selectetItemPosY { get; set; }


        private double _panelX = 100;
        
        public double PanelX
        {
            get { return _panelX; }
            set
            {
                if (value.Equals(_panelX)) return;
                _panelX = value;
                RaisePropertyChanged("PanelX");
            }
        }
        private double _panelY;
        public double PanelY
        {
            get { return _panelY; }
            set
            {
                if (value.Equals(_panelY)) return;
                _panelY = value;
                RaisePropertyChanged("PanelY");
            }
        }

        public bool IsUndoBtnEnable
        {
            get
            {
                return caretaker.HasStates;
            }
        }

        public bool IsRedoBtnEnable
        {
            get
            {
                return caretaker.HasRedoStates;
            }
        }
        

        private void AddShape(object obj)
        {
            var randomColor = new SolidColorBrush(Color.FromRgb((byte)r.Next(1, 255), (byte)r.Next(1, 255), (byte)r.Next(1, 255)));
            var newCircle = new Circle()
            {
                Id = Guid.NewGuid().ToString(),
                Position = new Point(r.Next(40, 360), r.Next(40, 360)),
                Color = randomColor

            };
            Circles.Add(newCircle);
            originator.Set(new Circle(newCircle));
            caretaker.AddMemento(originator.StoreInMemento());

        }

        private void Undo()
        {
            var savedState = originator.RestoreFromMemento(caretaker.GetMemento());
            var circle = Circles.FirstOrDefault(o => o.Id == savedState.Id);

            if (savedState.PositionOld.Equals(new Point()) && circle.Position.Equals(savedState.Position))
            {
                Circles.Remove(circle);
            }
            else
            {
                circle.Position = savedState.PositionOld;
            }
        }

        private void Redo()
        {
            var savedState = originator.RestoreFromMemento(caretaker.RedoMemento());
            var circle = Circles.FirstOrDefault(o => o.Id == savedState.Id);
            if (circle == null)
            {
                Circles.Add(savedState);
            }
            else
            {
                circle.Position = savedState.Position;
            }
        }

        private void MouseLeftButtonDown(object item)
        {
            selectedItem = item as Circle;
            selectetItemPosX = selectedItem.Position.X;
            selectetItemPosY = selectedItem.Position.Y;


        }

        private void MouseUp(object obj)
        {
            if (selectedItem != null)
            {
                var cir = new Circle(selectedItem)
                {
                    PositionOld = new Point(selectetItemPosX, selectetItemPosY)
                };
                originator.Set(cir);
                caretaker.AddMemento(originator.StoreInMemento());
            }
            selectedItem = null;
        }

        private void MouseMove(object obj)
        {
            if (selectedItem != null)
            {
                var LocX = PanelX - 20;
                var LocY = PanelY - 20;
                selectedItem.Position = new Point(LocX, LocY);

            }
        }
    }
}
