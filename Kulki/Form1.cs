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
        private Random random;
        private Ball[] ball;
        private Thread[] Threads;
        private object o;
        private Barrier barier1;
        private Barrier barier2;

        public Form1()
        {
            InitializeComponent();
            random = new Random();
            ball = new Ball[12];
            Threads = new Thread[13];
            o = new object();
            barier2 = new Barrier(13);
            barier1 = new Barrier(13, (b) => { pictureBox1.Invalidate(); });

            for (int i = 0; i < 13; ++i) Threads[i] = new Thread(refresh);
            ball[0] = new Ball(new PointF(100, 100), Brushes.Blue, random.Next(360));
            ball[1] = new Ball(new PointF(100, 400), Brushes.Gold, random.Next(360));
            ball[2] = new Ball(new PointF(700, 400), Brushes.Black, random.Next(360));
            ball[3] = new Ball(new PointF(400, 400), Brushes.Red, random.Next(360));
            ball[4] = new Ball(new PointF(700, 100), Brushes.Green, random.Next(360));
            ball[5] = new Ball(new PointF(250, 320), Brushes.Purple, random.Next(360));
            ball[6] = new Ball(new PointF(400, 100), Brushes.Aqua, random.Next(360));
            ball[7] = new Ball(new PointF(550, 320), Brushes.DarkGray, random.Next(360));
            ball[8] = new Ball(new PointF(250, 180), Brushes.DarkOrange, random.Next(360));
            ball[9] = new Ball(new PointF(550, 180), Brushes.Fuchsia, random.Next(360));
            ball[10] = new Ball(new PointF(50, 250), Brushes.DarkGoldenrod, random.Next(360));
            ball[11] = new Ball(new PointF(800, 250), Brushes.PaleGreen, random.Next(360));
        }
        public static double Distance(PointF p, PointF q)
        {
            double a = p.X - q.X;
            double b = p.Y - q.Y;
            double distance = Math.Sqrt(a * a + b * b);
            return distance;
        }
        private void refresh(object a)
        {
            while (true)
            {
                if (a is Ball)
                {
                    ((Ball)a).ChangeCoord();
                    try { barier1.SignalAndWait(); }
                    catch (BarrierPostPhaseException e) { }
                    for (int i = 0; i < 12; ++i)
                    {
                        if (Distance(ball[i].Middle, ((Ball)a).Middle) < 50 && Distance(ball[i].Middle, ((Ball)a).Middle) != 0)
                        {
                            lock (o)
                            {
                                if (((Ball)a).Angle != ball[i].Angle)
                                {
                                    ((Ball)a).AngleTmp = ((Ball)a).Angle;
                                    ((Ball)a).Angle = ball[i].Angle;
                                    ((Ball)a).Color = ball[i].Color;
                                }
                                else
                                {
                                    ((Ball)a).Angle = ball[i].AngleTmp;
                                }
                            }
                        }
                    }
                }
                else
                {
                    try { barier1.SignalAndWait(); }
                    catch (BarrierPostPhaseException e) { }
                }

                barier2.SignalAndWait();
                Thread.Sleep(5);
            }
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            for (int i = 0; i < 12; ++i) g.FillEllipse(ball[i].Color, new RectangleF(ball[i].Cord.X, ball[i].Cord.Y, 50, 50));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Threads[0].ThreadState.Equals(ThreadState.Unstarted))
            {
                for (int i = 0; i < 12; ++i) Threads[i].Start(ball[i]);
                Threads[12].Start();
            }
            else if (Threads[0].ThreadState.Equals(ThreadState.Running) || Threads[0].ThreadState.Equals(ThreadState.WaitSleepJoin))
            {
                
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
