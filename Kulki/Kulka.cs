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
        private int temp;
        private PointF wsp;
        private Brush kolor;
        private int r = 1;
        private int kat;

        public PointF Wsp
        {
            get { return wsp; }
            set { wsp = value; }
        }

        public Brush Kolor
        {
            get { return kolor; }
            set { kolor = value; }
        }

        public int Kat
        {
            get { return kat; }
            set { kat = value; }
        }

        public Kulka(PointF wsp, Brush kolor, int kat)
        {
            this.kolor = kolor;
            this.wsp = wsp;
            this.kat = kat;
            this.temp = kat;
        }
        public Kulka(Brush kolor)
        {
            this.kolor = kolor;
        }
        public void zmienWsp()
        {
            //zmiana katow
            if (wsp.X > 817 || wsp.X < 25)
            {
                if (kat > 0 & kat < 90) kat = 180 - kat;
                else if (kat > 90 & kat < 180) kat = 180 - kat;
                else if (kat > 180 & kat < 270) kat = 270 - kat + 270;
                else kat = 360 - kat + 180;
            }
            if (wsp.Y > 527 || wsp.Y < 15)
            {
                if (kat > 0 & kat < 90) kat = 360 - kat;
                else if (kat > 90 & kat < 180) kat = 360 - kat;
                else if (kat > 180 & kat < 270) kat = 360 - kat;
                else kat = 360 - kat;
            }
            //zmiana wsp
            wsp.X += (float)(r * Math.Cos((kat * Math.PI) / 180));
            wsp.Y += (float)(r * Math.Sin((kat * Math.PI) / 180));
        }

        public PointF Srodek
        {
            get { return new PointF(wsp.X + 25, wsp.Y + 25); }
        }
        public int Temp
        {
            get { return temp; }
            set { temp = value; }
        }
    }
}
