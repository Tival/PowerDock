namespace Psd.Wpf.EventEmiters {
    public class PositionRectangle {
        public double XMin { get; set; }
        public double XMax { get; set; }
        public double YMin { get; set; }
        public double YMax { get; set; }

        public PositionRectangle(double xMin, double xMax, double yMin, double yMax) {
            XMin = xMin;
            XMax = xMax;
            YMin = yMin;
            YMax = yMax;
        }
    }
}
