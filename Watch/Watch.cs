using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Watch
{
    class Watch:Control
    {
        private Timer timer;
        private double SecTickCount;
        private double MinTickCount;
        private double HrTickCount;
        public Watch() : base()
        {
            this.Size = new System.Drawing.Size(200, 200);
            this.Paint += ClockControl_Paint;
            timer = new Timer();
            timer.Interval = 1000;
            timer.Enabled = true;
            timer.Tick += Timer_Tick;
            timer.Start();
            SecTickCount = 4.71239 + (0.0174533 * 6) * DateTime.Now.Second;
            MinTickCount = 4.71239 + (0.0174533 * 6) * DateTime.Now.Minute;
            HrTickCount = 4.71239 + (0.0174533 * 12) * DateTime.Now.Hour;
            this.SizeChanged += WatchClass_SizeChanged;


        }

        private void WatchClass_SizeChanged(object sender, EventArgs e)
        {
            if (this.Size.Width != this.Size.Height)
            {
                this.Size = new Size(100, 100);
            }
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            SecTickCount += (0.017 * 6);
            MinTickCount += (0.017 * 6) / 60;
            HrTickCount += (0.017 * 6) / 60 / 60;

            this.Invalidate();
        }

        private void ClockControl_Paint(object sender, PaintEventArgs e)
        {
            var g = this.CreateGraphics();

            g.DrawLine(Pens.Black, new Point(this.Width / 2, this.Height / 2), new Point((int)(this.Width / 2 + Math.Cos(SecTickCount) * this.Width / 2), (int)(this.Width / 2.4 + Math.Sin(SecTickCount) * this.Height / 2.4)));
            g.DrawLine(Pens.DarkKhaki, new Point(this.Width / 2, this.Height / 2), new Point((int)(this.Width / 2 + Math.Cos(MinTickCount) * this.Width / 2.8), (int)(this.Width / 2 + Math.Sin(MinTickCount) * this.Height / 2.8)));
            g.DrawLine(Pens.Red, new Point(this.Width / 2, this.Height / 2), new Point((int)(this.Width / 2 + Math.Cos(HrTickCount) * this.Width / 3), (int)(this.Width / 2 + Math.Sin(HrTickCount) * this.Height / 3)));


            for (double i = 4.71; i < 10.5; i += 0.52)
            {
                if (i != 6.28333 && i != 4.7199999 && i != 7.85441 && i != 9.4)
                {
                    g.FillEllipse(Brushes.Black, new Rectangle(new Point((int)(this.Width / 2.1 + Math.Cos(i) * this.Width / 2.1), (int)(this.Width / 2.1 + Math.Sin(i) * this.Height / 2.1)), new Size(10, 10)));
                }

            }
            Font font = new Font("Times New Roman", 12, FontStyle.Bold);


            g.DrawString("12", font, Brushes.Black, new Point(this.Width / 2 - 12, 10));
            g.DrawString("3", font, Brushes.Black, new Point(this.Width - 24, this.Height / 2 - 6));
            g.DrawString("6", font, Brushes.Black, new Point(this.Width / 2 - 6, this.Height - 24));
            g.DrawString("9", font, Brushes.Black, new Point(16, this.Height / 2 - 6));


        }
    }
}
