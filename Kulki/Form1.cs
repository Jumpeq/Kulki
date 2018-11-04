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
        private Kulka[] kulka;
        private Thread[] Threads;

        public Form1()
        {
            InitializeComponent();
            kulka = new Kulka[6];
            Threads = new Thread[7];

            //for (int i = 0; i < 7; ++i) Threads[i] = new Thread();
            kulka[0] = new Kulka(new PointF(220, 130), Brushes.Blue);
            kulka[1] = new Kulka(new PointF(320, 280), Brushes.Blue);
            kulka[2] = new Kulka(new PointF(220, 430), Brushes.Blue);
            kulka[3] = new Kulka(new PointF(630, 130), Brushes.Red);
            kulka[4] = new Kulka(new PointF(535, 280), Brushes.Red);
            kulka[5] = new Kulka(new PointF(630, 430), Brushes.Red);



        }


        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            for (int i = 0; i < 6; i++)
            {
                g.FillRectangle(kulka[i].Kolor, new RectangleF(kulka[i].Wsp.X, kulka[i].Wsp.Y, 50, 50));
                //g.FillEllipse(k[i].Kolor, new RectangleF(k[i].Wsp.X, k[i].Wsp.Y, 50, 50));
            }

            //Thread.Sleep(2000);
            kulka[0].ZmienY(400);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Threads[0].ThreadState.Equals(ThreadState.Unstarted))
            {
                for (int i = 0; i < 6; ++i) Threads[i].Start(kulka[i]);
            }
            else if (Threads[0].ThreadState.Equals(ThreadState.Running) ||
                     Threads[0].ThreadState.Equals(ThreadState.WaitSleepJoin))
            {
                //nic nie rob
            }
            else
            {
                for (int i = 0; i < 7; ++i) Threads[i].Resume();

            }
        }
    }
}
