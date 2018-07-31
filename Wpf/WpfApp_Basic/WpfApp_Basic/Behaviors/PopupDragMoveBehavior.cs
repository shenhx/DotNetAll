using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace KDY.IP.DOC.Uc
{
    public class PopupDragMoveBehavior : Behavior<Popup>
    {
        private bool _mouse_down;
        private Point _oldPos;
        private double maxVerticalOffset;
        private Point _orignalPos;

        public PopupDragMoveBehavior(double deftHorizontalOffset,double defVerizontalOffset)
        {
            this.DefaultHorizontalOffset = deftHorizontalOffset;
            this.DefaultVerticalOffset = defVerizontalOffset;
        }

        public double DefaultHorizontalOffset { get; }
        public double DefaultVerticalOffset { get; }

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.MouseLeftButtonDown += (sender, e) => {
                LeftMouseButton_Down(sender, e);
            };
            AssociatedObject.MouseLeftButtonUp += (sender, e) => {
                LeftMouseButton_Up(sender, e);
            };
            AssociatedObject.MouseMove += (sender, e) => {
                Mouse_Move(sender, e);
            };
            AssociatedObject.Closed += (sender, e) => {
                Popup_Close(sender, e);
            };
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.MouseLeftButtonDown -= (sender, e) => {
                LeftMouseButton_Down(sender, e);
            };
            AssociatedObject.MouseLeftButtonUp -= (sender, e) => {
                LeftMouseButton_Up(sender, e);
            };
            AssociatedObject.MouseMove -= (sender, e) => {
                Mouse_Move(sender, e);
            };
            AssociatedObject.Closed -= (sender, e) => {
                Popup_Close(sender, e);
            };
        }

        void LeftMouseButton_Down(object sender,MouseButtonEventArgs e)
        {
            _mouse_down = true;
            if(AssociatedObject.VerticalOffset == 0)
            {
                _orignalPos = AssociatedObject.Child.PointToScreen(new Point(AssociatedObject.ActualWidth, 0));
                maxVerticalOffset = 0 - _orignalPos.Y;
            }
            _oldPos = AssociatedObject.Child.PointToScreen(e.GetPosition(AssociatedObject.Child));
            AssociatedObject.Child.CaptureMouse();
        }

        void Mouse_Move(object sender,MouseEventArgs e)
        {
            if (!_mouse_down)
            {
                return;
            }
            var childPos = e.GetPosition(AssociatedObject.Child);
            var newPos = AssociatedObject.Child.PointToScreen(childPos);
            var offset = newPos - _oldPos;
            _oldPos = newPos;
            AssociatedObject.HorizontalOffset += offset.X;
            var newVerticalOffset = AssociatedObject.VerticalOffset + offset.Y;
            if(newVerticalOffset < maxVerticalOffset)
            {
                newVerticalOffset = maxVerticalOffset;
            }
            AssociatedObject.VerticalOffset = newVerticalOffset;
        }

        void LeftMouseButton_Up(object sender,MouseButtonEventArgs e)
        {
            _mouse_down = false;
            AssociatedObject.Child.ReleaseMouseCapture();
        }

        void Popup_Close(object sender,EventArgs e)
        {
            AssociatedObject.HorizontalOffset = DefaultHorizontalOffset;
            AssociatedObject.VerticalOffset = DefaultVerticalOffset;
        }
    }
}
