//***********************************************
// based on a https://www.codeproject.com/articles/492473/using-xinput-to-access-an-xbox-controller-in-m
//**********************************************
using System;

namespace MaximStartsev.GamePadRemoteControl.XInput
{
    public sealed class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point() { }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public override string ToString()
        {
            return String.Format("{0};{1}", X, Y);
        }
        public override bool Equals(object obj)
        {
            var point = obj as Point;
            if (point == null) return false;
            return point.X == X && point.Y == Y;
        }
    }
}
