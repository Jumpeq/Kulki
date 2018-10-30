using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kulki
{
    class Kulka
    {
        private PointF wsp;
        private Brush kolor;
      
        public PointF Wsp
        {
            get
            {
                return wsp;
            }
            set
            {
                wsp = value;
            }
        }

        //public PointF Srodek
        //{
        //    get
        //    {
        //        return new PointF(wsp.X + 25, wsp.Y + 25);
        //    }
        //}

        public Brush Kolor
        {
            get
            {
                return kolor;
            }
            set
            {
                kolor = value;
            }
        }
       
        public Kulka(PointF wsp, Brush kolor)
        {
            this.kolor = kolor;
            this.wsp = wsp;
        }
        public void ZmienY(int Y)
        {
            this.wsp.Y = Y;
        }
    }
}
