using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Threading;

namespace PowerSideDock.WPF.EventEmiters {
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT {
        public int X;
        public int Y;

        public static implicit operator Point(POINT point) {
            return new Point(point.X, point.Y);
        }
    }

    public class MouseTracker {
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);
        public Action<double, double, PositionRectangle> OnCursorInside { get; set; }
        public Action<double, double, PositionRectangle> OnCursorOutside { get; set; }

        private DispatcherTimer dispatcherTimer;
        private List<PositionRectangle> positionsToCheck;

        public MouseTracker() {
            positionsToCheck = new List<PositionRectangle>();
        }

        public void RegisterPositionRectangle(PositionRectangle rectangle) {
            positionsToCheck.Add(rectangle);
        }

        public void RegisterPositionRectangle(double xMin, double xMax, double yMin, double yMax) {
            positionsToCheck.Add(new PositionRectangle(xMin, xMax, yMin, yMax));
        }

        public void StartTracking() {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(HandleMousePosition);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            dispatcherTimer.Start();
        }

        public void EndTracking() {
            dispatcherTimer.Stop();
            dispatcherTimer = null;
        }

        private void HandleMousePosition(object sender, EventArgs e) {
            var mousePoint = GetCursorPosition();

            foreach (var position in positionsToCheck) {
                if (mousePoint.X > position.XMin &&
                    mousePoint.X < position.XMax &&
                    mousePoint.Y > position.YMin &&
                    mousePoint.Y < position.YMax) {
                    OnCursorInside?.Invoke(mousePoint.X, mousePoint.Y, position);
                    return;
                } else {
                    OnCursorOutside?.Invoke(mousePoint.X, mousePoint.Y, position);
                }
            }
        }

        private Point GetCursorPosition() {
            GetCursorPos(out POINT lpPoint);

            return lpPoint;
        }
    }
}
