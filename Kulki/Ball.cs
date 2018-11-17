using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kulki
{
    class Ball
    {
        private int angleTmp;
        private PointF cord;
        private Brush color;
        private int r = 1;
        private int angle;

        public PointF Cord
        {
            get { return cord; }
            set { cord = value; }
        }

        public Brush Color
        {
            get { return color; }
            set { color = value; }
        }

        public int Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        public Ball(PointF cord, Brush color, int angle)
        {
            this.color = color;
            this.cord = cord;
            this.angle = angle;
            this.angleTmp = angle;
        }
        public Ball(Brush color)
        {
            this.color = color;
        }
        public void ChangeCoord()
        {
            if (cord.X > 817 || cord.X < 25)
            {
                if (angle > 0 & angle < 90) angle = 180 - angle;
                else if (angle > 90 & angle < 180) angle = 180 - angle;
                else if (angle > 180 & angle < 270) angle = 270 - angle + 270;
                else angle = 360 - angle + 180;
            }
            if (cord.Y > 527 || cord.Y < 15)
            {
                if (angle > 0 & angle < 90) angle = 360 - angle;
                else if (angle > 90 & angle < 180) angle = 360 - angle;
                else if (angle > 180 & angle < 270) angle = 360 - angle;
                else angle = 360 - angle;
            }
            cord.X += (float)(r * Math.Cos((angle * Math.PI) / 180));
            cord.Y += (float)(r * Math.Sin((angle * Math.PI) / 180));
        }

        public PointF Middle
        {
            get { return new PointF(cord.X + 25, cord.Y + 25); }
        }
        public int AngleTmp
        {
            get { return angleTmp; }
            set { angleTmp = value; }
        }
    }
}
