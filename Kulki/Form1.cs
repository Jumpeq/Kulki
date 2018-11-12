using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kulki
{
    public partial class Form1 : Form
    {
        private Random losuj;
        private Kulka[] kulka;
        private Thread[] Threads;
        private object o;
        private Barrier bariera1;
        private Barrier bariera2;

        public Form1()
        {
            InitializeComponent();
            losuj = new Random();
            kulka = new Kulka[12];
            Threads = new Thread[13];
            o = new object();
            bariera2 = new Barrier(13);
            bariera1 = new Barrier(13, (b) => { pictureBox1.Invalidate(); });

            for (int i = 0; i < 13; ++i) Threads[i] = new Thread(odswiez);
            kulka[0] = new Kulka(new PointF(100, 100), Brushes.Blue, losuj.Next(360));
            kulka[1] = new Kulka(new PointF(100, 400), Brushes.Gold, losuj.Next(360));
            kulka[2] = new Kulka(new PointF(700, 400), Brushes.Black, losuj.Next(360));
            kulka[3] = new Kulka(new PointF(400, 400), Brushes.Red, losuj.Next(360));
            kulka[4] = new Kulka(new PointF(700, 100), Brushes.Green, losuj.Next(360));
            kulka[5] = new Kulka(new PointF(250, 320), Brushes.Purple, losuj.Next(360));
            kulka[6] = new Kulka(new PointF(400, 100), Brushes.Aqua, losuj.Next(360));
            kulka[7] = new Kulka(new PointF(550, 320), Brushes.DarkGray, losuj.Next(360));
            kulka[8] = new Kulka(new PointF(250, 180), Brushes.DarkOrange, losuj.Next(360));
            kulka[9] = new Kulka(new PointF(550, 180), Brushes.Fuchsia, losuj.Next(360));
            kulka[10] = new Kulka(new PointF(50, 250), Brushes.DarkGoldenrod, losuj.Next(360));
            kulka[11] = new Kulka(new PointF(800, 250), Brushes.PaleGreen, losuj.Next(360));
        }
        public static double odleglosc(PointF p, PointF q)
        {
            double a = p.X - q.X;
            double b = p.Y - q.Y;
            double distance = Math.Sqrt(a * a + b * b);
            return distance;
        }
        private void odswiez(object a)
        {
            while (true)
            {
                if (a is Kulka)
                {
                    ((Kulka)a).zmienWsp();
                    try { bariera1.SignalAndWait(); }
                    catch (BarrierPostPhaseException e) { }
                    for (int i = 0; i < 12; ++i)
                    {
                        if (odleglosc(kulka[i].Srodek, ((Kulka)a).Srodek) < 50 && odleglosc(kulka[i].Srodek, ((Kulka)a).Srodek) != 0)
                        {
                            lock (o)
                            {
                                if (((Kulka)a).Kat != kulka[i].Kat)
                                {
                                    ((Kulka)a).Temp = ((Kulka)a).Kat;
                                    ((Kulka)a).Kat = kulka[i].Kat;
                                    ((Kulka)a).Kolor = kulka[i].Kolor;
                                }
                                else
                                {
                                    ((Kulka)a).Kat = kulka[i].Temp;
                                }
                            }
                        }
                    }
                }
                else
                {
                    try { bariera1.SignalAndWait(); }
                    catch (BarrierPostPhaseException e) { }
                }

                bariera2.SignalAndWait();
                Thread.Sleep(5);
            }
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            for (int i = 0; i < 12; ++i) g.FillEllipse(kulka[i].Kolor, new RectangleF(kulka[i].Wsp.X, kulka[i].Wsp.Y, 50, 50));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Threads[0].ThreadState.Equals(ThreadState.Unstarted))
            {
                for (int i = 0; i < 12; ++i) Threads[i].Start(kulka[i]);
                Threads[12].Start();
            }
            else if (Threads[0].ThreadState.Equals(ThreadState.Running) || Threads[0].ThreadState.Equals(ThreadState.WaitSleepJoin))
            {
                //nic nie rob
            }
            else
            {
                for (int i = 0; i < 13; ++i) Threads[i].Resume();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            for (int i = 0; i < 13; ++i) Threads[i].Abort();
        }
    }
}
