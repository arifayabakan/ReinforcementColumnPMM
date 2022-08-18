using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        private List<Donati> donatiListesi;
        private int kesitTipi;
        private double h;
        private double b;
        public Form2(List<Donati> donati,int kesitTipi,double h,double b)
        {
            this.donatiListesi = donati;
            this.kesitTipi = kesitTipi;
            this.h = h;
            this.b = b;
            InitializeComponent();
        }

        public void draw(int x, int y, int r, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Red,2);
            Rectangle rect = new Rectangle(x, y, r, r);
            e.Graphics.DrawEllipse(pen, rect);
            pen.Dispose();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.Size = this.Size;
            Pen pen = new Pen(Color.Black, 4);
            foreach (Donati donati in donatiListesi)
            {
                Console.WriteLine(donati.toString());
                int xDraw = donati.xCoordinate;
                int yDraw = donati.yCoordinate;
                if (kesitTipi == 0)
                {
                    Rectangle rect = new Rectangle(0, 0, panel1.Size.Width - 65, panel1.Size.Height - 65);
                    e.Graphics.DrawRectangle(pen, rect);
                    draw(xDraw, yDraw, donati.r, e);
                }
               
                else if (kesitTipi == 1)
                {
                    Console.WriteLine(h);
                    Rectangle rect = new Rectangle(0, 0, (int)h, (int)h);
                    e.Graphics.DrawEllipse(pen, rect);
                    draw(xDraw, yDraw, donati.r, e);
                }
            }

        }
        public int map(int x, double inMin, double inMax, double outMin, double outMax)
        {
            return (int)((x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin);
        }



    }
}
