using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Form4 detailForm;
        List<Double> dkArray = new List<Double>();
        Boolean show_information = false;
        double aci;
        double fck, fyk, gmc, gms, es, b, fcd, fyd, fykkarekok, fctd, c, a, paspayi, d, k1, h, dg;
        double esyd = 0.001825, ecu = 0.003, cb, eb, snrdgr, snrdgraci, yndgraci,
        ac, yenih, yenic, kk, yeniy;
        int kesitTipi = 0;
        List<Double> myLabelDegerleri, pmyLabelDegerleri, mzLabelDegerleri, pmzLabelDegerleri;
        Double p03h, p903h;

        public Form1()
        {
            InitializeComponent();
            detailForm = new Form4();
            myLabelDegerleri = new List<Double>();
            pmyLabelDegerleri = new List<Double>();
            mzLabelDegerleri = new List<Double>();
            pmzLabelDegerleri = new List<Double>();
            kesitTipleriCombo.Items.Add("Dikdörtgen");
            kesitTipleriCombo.Items.Add("Daire");
            kesitTipleriCombo.SelectedIndex = 0;
            textBox8.Text = "0";
            aciLabel.Text = "0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fck = Convert.ToDouble(textBox1.Text);
            fyk = Convert.ToDouble(textBox3.Text);
            gmc = Convert.ToDouble(textBox2.Text);
            gms = Convert.ToDouble(textBox4.Text);
            es = Convert.ToDouble(textBox5.Text);
            b = Convert.ToDouble(wval.Text);
            h = Convert.ToDouble(hval.Text);
            eb = Convert.ToDouble(textBox6.Text);
            aci = Double.Parse(textBox8.Text);

            paspayi = Convert.ToDouble(textBox15.Text);
            d = (h - paspayi);
            textBox14.Text = d.ToString();
            ac = h * b;
            textBox23.Text = ac.ToString();
            fcd = fck / gmc;
            fyd = fyk / gms;
            fykkarekok = (Math.Sqrt(fck));
            fctd = (fykkarekok / gmc) * 0.35;
            snrdgr= h/b;
            snrdgraci= Math.Atan(snrdgr);
            yndgraci = Math.Tan(Math.PI * aci / 180);

            textBox9.Text = fcd.ToString();
            textBox10.Text = fyd.ToString();
            textBox11.Text = fctd.ToString();

            k1 = (0.85 - 0.006 * (fck - 25));
            
            // cb = ecu * (h - paspayi) / (ecu + esyd);
            //textBox73.Text = cb.ToString();

            if (k1 > 0.64 && k1 < 0.85)
            {

                textBox16.Text = k1.ToString();
            }
            else if (k1 < 0.64)
            {
                k1 = 0.64;
                textBox16.Text = k1.ToString();
            }
            else
            {
                k1 = 0.85;
                textBox16.Text = k1.ToString();
            }

            int index = 0;
            for (double i = 0.1; i < 0.99; i += 0.1)
            {
                Double c = h * i;
                Double k1c = k1 * c;
                StaticLabels.cLabels.ElementAt(index).Text = c.ToString();
                StaticLabels.k1cLables.ElementAt(index).Text = k1c.ToString();

                Double bc = b * i;
                Double bk1c = k1 * bc;
                StaticLabels.bcLabels.ElementAt(index).Text = bc.ToString();
                StaticLabels.bk1cLables.ElementAt(index).Text = bk1c.ToString();
                index++;
            }

            for (int i = 1; i < 6; ++i)
            {
                Double c = h * i;
                Double kc = k1 * c;
                StaticLabels.cLabels.ElementAt(index).Text = c.ToString();
                StaticLabels.k1cLables.ElementAt(index).Text = kc.ToString();
                if (kc > h)
                    StaticLabels.k1cLables.ElementAt(index).Text = h.ToString();


                Double bc = b * i;
                Double bk1c = k1 * bc;
                StaticLabels.bcLabels.ElementAt(index).Text = bc.ToString();
                StaticLabels.bk1cLables.ElementAt(index).Text = bk1c.ToString();
                if (bk1c > b)
                    StaticLabels.bk1cLables.ElementAt(index).Text = b.ToString();

                index++;
            }

            Double cSonsuz = h * int.MaxValue;
            Double kcSonsuz = k1 * cSonsuz;
            StaticLabels.cLabels.ElementAt(index).Text = cSonsuz.ToString();
            StaticLabels.k1cLables.ElementAt(index).Text = kcSonsuz.ToString();
            if (kcSonsuz > h)
                StaticLabels.k1cLables.ElementAt(index).Text = h.ToString();

            Double bcSonsuz = b * int.MaxValue;
            Double bk1cSonsuz = k1 * bcSonsuz;
            StaticLabels.bcLabels.ElementAt(index).Text = bcSonsuz.ToString();
            StaticLabels.bk1cLables.ElementAt(index).Text = bk1cSonsuz.ToString();
            if (bk1cSonsuz > h)
                StaticLabels.bk1cLables.ElementAt(index).Text = b.ToString();

            calculateBBK();

        }

        private void calculateBBK()
        {
            Console.WriteLine("calculateBBK aci : " + aci);
            if (aci.Equals(0.0))
            {
                zeroDegreeCalculation();
            }
            else if (aci.Equals(90))
            {
                nintyDegreeCalculation();
            }
            else
            {
                otherCalculation(); 
            }
        }

        private void zeroDegreeCalculation()
        {
            for (int i = 0; i < StaticLabels.k1cLables.Count; ++i)
            {
                StaticLabels.bbkArray[i] = (0.85 * fcd * b * Double.Parse(StaticLabels.k1cLables.ElementAt(i).Text) * Math.Pow(10, -3));
                StaticLabels.pLabels[i].Text = String.Format("{0:0.00}", StaticLabels.bbkArray[i]);
                StaticLabels.myLabels[i].Text = String.Format("{0:0.00}", ((StaticLabels.bbkArray[i] * (h / 2 - ((Double.Parse(StaticLabels.k1cLables.ElementAt(i).Text) / 2)))) * Math.Pow(10, -3)));
                Console.WriteLine(i + ": StaticLabels.myLabels" + StaticLabels.myLabels[i].Text + " StaticLabels.bbkArray " + StaticLabels.bbkArray[i] + " k1clabel : " + StaticLabels.k1cLables[i].Text + " ->" + (h / 2 - ((Double.Parse(StaticLabels.k1cLables.ElementAt(i).Text) / 2))));
                StaticLabels.mzLabels[i].Text = "0";
                StaticLabels.mbLabels[i].Text = String.Format("{0:0.00}", ((StaticLabels.bbkArray[i] * (h / 2 - (Double.Parse(StaticLabels.k1cLables.ElementAt(i).Text) / 2))) * Math.Pow(10, -3)));
                dkArray.Add(0.0);
            }
        }

        private void nintyDegreeCalculation()
        {
            for (int i = 0; i < StaticLabels.k1cLables.Count; ++i)
            {

                StaticLabels.bbkArray[i] = (0.85 * fcd * h * Double.Parse(StaticLabels.bk1cLables.ElementAt(i).Text) * Math.Pow(10, -3));
                StaticLabels.pLabels[i].Text = String.Format("{0:0.00}", StaticLabels.bbkArray[i]);
                StaticLabels.mzLabels[i].Text = String.Format("{0:0.00}", ((StaticLabels.bbkArray[i] * (b / 2 - (Double.Parse(StaticLabels.bk1cLables.ElementAt(i).Text) / 2))) * Math.Pow(10, -3)));
                StaticLabels.myLabels[i].Text = "0";
                StaticLabels.mbLabels[i].Text = String.Format("{0:0.00}", ((StaticLabels.bbkArray[i] * (b / 2 - (Double.Parse(StaticLabels.bk1cLables.ElementAt(i).Text) / 2))) * Math.Pow(10, -3)));
                dkArray.Add(0.0);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            drawDonati(2, 2, 10, e);                        //?
        }

        private void drawDonati(int xcoor, int ycoor, int donati_r, PaintEventArgs e)
        {
            Pen redPen = new Pen(Color.Red, 2);
            Rectangle rect = new Rectangle(xcoor, ycoor, donati_r, donati_r);

            e.Graphics.DrawEllipse(redPen, rect);
            redPen.Dispose();
        }

        private List<Donati> donatiList = new List<Donati>();

        private void addBut_Click(object sender, EventArgs e)
        {
            int xCoord, yCoord;

            try
            {
                xCoord = Convert.ToInt32(xcoorTextBox.Text.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Girilen x koordinatı sayı olmalıdır.",
                                        "Donatı eklenemedi",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                yCoord = Int32.Parse(ycoorTextBox.Text.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Girilen y koordinatı sayı olmalıdır.",
                                        "Donatı eklenemedi",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int r = Int32.Parse(donatir.Text.ToString());
            if (r < 0)
            {
                MessageBox.Show("Girilen r negatif olamaz : ",
                        "Donatı eklenemedi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (kesitTipi == 0)
            {
                if (!checkRectangleCoordinate(xCoord, b, paspayi))
                {
                    MessageBox.Show("Girilen x koordinatı şu aralıkta olmalıdır : " + (paspayi) + ":" + (b - paspayi),
                            "Donatı eklenemedi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!checkRectangleCoordinate(yCoord, h, paspayi))
                {
                    MessageBox.Show("Girilen y koordinatı şu aralıkta olmalıdır : " + (paspayi) + ":" + (h - paspayi),
                            "Donatı eklenemedi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (kesitTipi == 1)
            {
                if (!checkCirclePoint(xCoord, yCoord, paspayi,r,b))
                {
                    MessageBox.Show("Girilen y koordinatı şu aralıkta olmalıdır : " + (paspayi) + ":" + (h - paspayi),
                           "Donatı eklenemedi",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }

            foreach (Donati donati in donatiList)
            {
                if (donati.koordinatlarAynı(xCoord, yCoord))
                {
                    MessageBox.Show("Aynı koordinatlara sahip bir donatı daha önce eklendi",
                        "Donatı eklenemedi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (kesitTipi == 0)
            {
                Console.WriteLine("Dikdörtgen");
                hesapla(xCoord, yCoord, r,true);
            }
            else if (kesitTipi == 1)
            {
                Console.WriteLine("Daire");
            }

            donatiList.Add(new Donati(xCoord, yCoord, r, dg, dkArray));
            donatiComboBox.Items.Add(xCoord + ":" + yCoord + ":" + r);
            donatiComboBox.SelectedIndex = donatiList.Count - 1;
            Double tempAci = aci;
            button5_Click(sender, e);
            ChangeDegree("90");
            button6_Click(sender, e);
            ChangeDegree("0");
            pictureBox5.Hide();
            ChangeDegree(tempAci.ToString());
            chart0.Show();
            chart90.Show();
            chart0.Show();
            chart90.Show();
            label140.Show();
            textBox8.Show();
            button8.Show();
            button2.Show();
            label1.Show();
            changeKesitButton.Show();
            kesitTipleriCombo.Show();
           
        }

        private bool checkCirclePoint(int xCoord, int yCoord, double paspayi,int donatiR, double r)
        {

            if (isCircleInCircle(xCoord, yCoord, donatiR, r))
            {
                Console.WriteLine("checkcircle point");
                double xDif = Math.Pow(xCoord - (r / 2), 2);
                Console.WriteLine(xDif);
                double yDif = Math.Pow(yCoord - (r / 2), 2);
                Console.WriteLine(xDif);
                double totalDif = xDif + yDif;

                Console.WriteLine(Math.Pow((r / 2), 2));
                return (totalDif < (Math.Pow((r / 2), 2) - paspayi));
            }
            else return false;
        }
        private bool isCircleInCircle(int x, int y, int donatiy, double r)
        {
            double xalt = x - donatiy / 2;
            double xust = x + donatiy / 2;
            double yalt = y - donatiy / 2;
            double yust = y + donatiy / 2;
            if (xalt > 0 && yalt > 0 && xust < r && yust < r)
                return true;
            else
                return false;
        }
        private void showBut_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(donatiList, kesitTipleriCombo.SelectedIndex, h, b);
            Int32 wForm = 100, hForm = 100;
            if (kesitTipi == 0)
            {
                wForm = Int32.Parse(wval.Text) + 75;
                hForm = Int32.Parse(hval.Text) + 75;
            }
           
            else if (kesitTipi == 1)
            {
                wForm = (Int32.Parse(wval.Text)) + 30;
                hForm = (Int32.Parse(wval.Text)) + 30;
            }

            form2.Size = new Size(wForm, hForm);
            form2.ShowDialog();
        }

        public int map(int x, double inMin, double inMax, double outMin, double outMax)  //?
        {

            return (int)((x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin);
        }

        public bool checkRectangleCoordinate(int coordinate, double limit, double pay)
        {
            Console.WriteLine("rect");
            return ((coordinate < pay) || (coordinate > (limit - pay))) ? false : true;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

            int selected = donatiComboBox.SelectedIndex;
            donatisil(selected);
            
        }
        public void donatisil(int selectedIndex)
        {
            if (selectedIndex != -1)
            {
                Console.WriteLine("dk array size : " + donatiList.ElementAt(selectedIndex).dk.Count);
                Console.WriteLine("p Label size : " + StaticLabels.pLabels.Count);
                for (int i = 0; i < StaticLabels.pLabels.Count; ++i)
                {
                    Double dk = donatiList.ElementAt(selectedIndex).dk[i];
                    int xCoord = donatiList.ElementAt(selectedIndex).xCoordinate;
                    int yCoord = donatiList.ElementAt(selectedIndex).yCoordinate;

                    Double pValue = Double.Parse(StaticLabels.pLabels[i].Text);
                    pValue -= dk;
                    StaticLabels.pLabels[i].Text = String.Format("{0:0.00}", pValue);

                    Double myValue = Double.Parse(StaticLabels.myLabels[i].Text);
                    if (!myValue.Equals(0))
                    {
                        myValue -= dk * (h / 2 - yCoord) * Math.Pow(10, -3);
                        StaticLabels.myLabels[i].Text = String.Format("{0:0.00}", myValue);
                    }

                    Double mzValue = Double.Parse(StaticLabels.mzLabels[i].Text);
                    if (!mzValue.Equals(0))
                    {
                        mzValue -= dk * (b / 2 - xCoord) * Math.Pow(10, -3);
                        StaticLabels.mzLabels[i].Text = String.Format("{0:0.00}", mzValue);
                    }

                    Double mbValue = Double.Parse(StaticLabels.mbLabels[i].Text);
                    if (!mbValue.Equals(0))
                    {
                        mbValue -= dk * ((h / 2 - yCoord) / Math.Cos(Math.PI * aci / 180)) * Math.Pow(10, -3);
                        StaticLabels.mbLabels[i].Text = String.Format("{0:0.00}", mbValue);
                    }
                }

                donatiList.RemoveAt(selectedIndex);
                donatiComboBox.Items.RemoveAt(selectedIndex);
            }
        }
        public void hesapla(int xCoord, int yCoord, int r, Boolean flag)
        {


            double da;
            da = (Math.Pow(r, 2) * Math.PI) / (4.0);
            //aci = Convert.ToDouble(textBox8.Text);
            Console.WriteLine(aci);
            Console.WriteLine("------");
            //------------------------------ACİ=0-----------------------------------------------

            if (aci == 0)
            {
                for (int j = 0; j < StaticLabels.cLabels.Count; j++)
                {
                    dg = eb * es * (1 - (yCoord / Double.Parse(StaticLabels.cLabels.ElementAt(j).Text)));
                    if (dg > 0)
                    {
                        if (dg >= fyd)
                        {
                            dg = fyd;
                        }
                        /*else if (dg < fyd)
                        {
                            dg = eb * es * (1 - (yCoord / Double.Parse(StaticLabels.cLabels.ElementAt(j).Text)));
                        }*/

                    }
                    else if (dg < 0)
                    {
                        if (dg < -fyd)
                        {
                            dg = -fyd;
                        }
                        else if (dg > -fyd)
                        {
                            dg = eb * es * (1 - (yCoord / Double.Parse(StaticLabels.cLabels.ElementAt(j).Text)));
                        }

                    }
                    dkArray[j] = dg * da * Math.Pow(10, -3);
                }


                for (int i = 0; i < StaticLabels.k1cLables.Count; ++i)
                {
                    //StaticLabels.bbkArray[i] = (0.85 * fcd * b * Double.Parse(StaticLabels.k1cLables.ElementAt(i).Text)*Math.Pow(10, -3));
                    //Console.WriteLine(StaticLabels.k1cLables[i].Text);

                    Double yeniPdeger = Double.Parse(StaticLabels.pLabels[i].Text) + dkArray[i];
                    StaticLabels.pLabels[i].Text = String.Format("{0:0.00}", yeniPdeger);

                    Math.Round(yeniPdeger, 2);
                    Double yeniMYdeger = Double.Parse(StaticLabels.myLabels[i].Text) + (dkArray[i] * (h / 2 - yCoord) * Math.Pow(10, -3));
                    StaticLabels.myLabels[i].Text = String.Format("{0:0.00}", yeniMYdeger);

                    Math.Round(yeniMYdeger, 2);
                    StaticLabels.mzLabels[i].Text = "0";
                    StaticLabels.mbLabels[i].Text = StaticLabels.myLabels[i].Text;

                }
            }



//-----------------------------ACİ=90------------------------------------



            else if (aci == 90)
            {

                for (int j = 0; j < StaticLabels.bcLabels.Count; j++)
                {

                    dg = 600 * (1 - (xCoord / Double.Parse(StaticLabels.bcLabels.ElementAt(j).Text)));
                    if (dg > 0)
                    {
                        if (dg > fyd)
                        {
                            dg = fyd;
                        }

                    }
                    else if (dg < 0)
                    {
                        if (dg < -fyd)
                        {
                            dg = -fyd;
                        }
                        else if (dg > -fyd)
                        {
                            dg = eb * es * (1 - (xCoord / Double.Parse(StaticLabels.bcLabels.ElementAt(j).Text)));
                        }

                    }
                    dkArray[j] = dg * da * Math.Pow(10, -3);
                }


                for (int i = 0; i < StaticLabels.bk1cLables.Count; ++i)
                {
                    //yenideger1 tanımlaması doğru mu? 0 derece için de yenideger idi aynı tanımlama olmaması adına 1 yazdım.

                    Double yeniPdeger1 = Double.Parse(StaticLabels.pLabels[i].Text) + dkArray[i];
                    Double yeniMZdeger = Double.Parse(StaticLabels.mzLabels[i].Text) + (dkArray[i] * (b / 2 - xCoord) * Math.Pow(10, -3));

                    StaticLabels.pLabels[i].Text = String.Format("{0:0.00}", yeniPdeger1);
                    StaticLabels.mzLabels[i].Text = String.Format("{0:0.00}", yeniMZdeger);
                    //StaticLabels.pLabels[i].Text = Math.Round(yeniPdeger, 2);
                    StaticLabels.myLabels[i].Text = "0";
                    StaticLabels.mbLabels[i].Text = String.Format("{0:0.00}", yeniMZdeger);


                }
            }





//------------------------------0<ACİ<90-----------------------------------------------

            else if (0 < aci && aci < 90)
            {
           /*     for (int j = 0; j < StaticLabels.cLabels.Count; j++){
                    yenih = h / Math.Cos(Math.PI * aci / 180);
                    yeniy =  yCoord / Math.Cos(Math.PI * aci / 180);

                    if (snrdgraci >= yndgraci)
                    {                      
                        yenih = h / Math.Cos(Math.PI * aci / 180);
                        yenic = (Double.Parse(StaticLabels.cLabels.ElementAt(j).Text) / Math.Cos(Math.PI * aci / 180));
                        yeniy = yCoord / Math.Cos(Math.PI * aci / 180);
                    }
                    else if (snrdgraci < yndgraci && yndgraci < 90)
                    {
                        yenih = h / Math.Sin(Math.PI * aci / 180);
                        yenic = (Double.Parse(StaticLabels.cLabels.ElementAt(j).Text) / Math.Sin(Math.PI * aci / 180));
                        yeniy = yCoord / Math.Sin(Math.PI * aci / 180);
                    }
                        
                    dg = eb * es * (1 - ((yeniy / yenic)));

                    if (dg > 0)
                    {
                        if (dg > fyd)
                        {
                            dg = fyd;
                        }
                    }
                    else if (dg < 0)
                    {
                        if (dg < -fyd)
                        {
                            dg = -fyd;
                        }
                        else if (dg > -fyd)
                        {
                            dg = eb * es * (1 - ((yeniy / yenic)));
                        }

                    }
                    dkArray[j] = dg * da * Math.Pow(10, -3);

                }*/
                if (flag)
                {
                    changeDegree();
                }
                writeStaticLabels();                  
           }
              
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            chart0.Hide();
            chart90.Hide();
            label140.Hide();
            textBox8.Hide();
            button8.Hide();
            button2.Hide();
            label1.Hide();
            changeKesitButton.Hide();
            kesitTipleriCombo.Hide();


        }

        private void button8_Click_1(object sender, EventArgs e) //AÇI BELİRLE BUTONU
        {
            ChangeDegree(textBox8.Text);
            if (aci < 90 && aci > 0)
            {
                changeDegree();
            }
        }

        private void writeStaticLabels()
        {
            for (int i = 0; i < pmyLabelDegerleri.Count; ++i)
            {
                double nr = (pmyLabelDegerleri.ElementAt(i) * pmzLabelDegerleri.ElementAt(i) * p03h) / ((pmzLabelDegerleri.ElementAt(i) * p03h) + (pmyLabelDegerleri.ElementAt(i) * p03h) - (pmyLabelDegerleri.ElementAt(i) * pmzLabelDegerleri.ElementAt(i)));
                double oran = nr / p03h;
                if (oran < 0.2)
                {
                    oran = 0.2;
                }
                else if (oran > 0.8)
                {
                    oran = 0.8;
                }

                double alfa = 0.67 + 1.67 * oran;

                Double temp = (myLabelDegerleri.ElementAt(i) * mzLabelDegerleri.ElementAt(i) / Math.Tan(Math.PI * aci / 180)) / Math.Pow((Math.Pow((mzLabelDegerleri.ElementAt(i) / Math.Tan(Math.PI * aci / 180)), alfa) + Math.Pow(myLabelDegerleri.ElementAt(i), alfa)), 1 / alfa);
                StaticLabels.myLabels.ElementAt(i).Text = String.Format("{0:0.00}", temp).ToString();
                StaticLabels.mzLabels.ElementAt(i).Text = String.Format("{0:0.00}", (temp * Math.Tan(Math.PI * aci / 180))).ToString();
                StaticLabels.mbLabels.ElementAt(i).Text = String.Format("{0:0.00}", (temp / Math.Tan(Math.PI * aci / 180))).ToString();
                drawChart(StaticLabels.mbLabels, chartOther);
            }
        }

        private void changeDegree()
        {
            Double tempAci = aci;
            ChangeDegree("0");
            myLabelDegerleri.Clear();
            foreach (Label label in StaticLabels.myLabels)
            {
                myLabelDegerleri.Add(Double.Parse(label.Text));
            }
            pmyLabelDegerleri.Clear();
            foreach (Label label in StaticLabels.pLabels)
            {
                pmyLabelDegerleri.Add(Double.Parse(label.Text));
            }

            p03h = Double.Parse(StaticLabels.pLabels.ElementAt(11).Text);

            ChangeDegree("90");
            mzLabelDegerleri.Clear();
            foreach (Label label in StaticLabels.mzLabels)
            {
                mzLabelDegerleri.Add(Double.Parse(label.Text));
            }
            pmzLabelDegerleri.Clear();
            foreach (Label label in StaticLabels.pLabels)
            {
                pmzLabelDegerleri.Add(Double.Parse(label.Text));
            }
            p903h = Double.Parse(StaticLabels.pLabels.ElementAt(11).Text);


            ChangeDegree(tempAci.ToString());
            aci = tempAci;
            writeStaticLabels();
            
        }

        private void ChangeDegree(string _aci)
        {
            Console.WriteLine("---->>>>Aci : " + _aci);
            aci = Double.Parse(_aci);
            calculateBBK();
            aciLabel.Text = _aci;
            foreach (Donati donati in donatiList)
            {
                hesapla(donati.xCoordinate, donati.yCoordinate, donati.r,false);
            }

        }


        private void button9_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();


            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //0
            drawChart(StaticLabels.myLabels, chart0);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            //90
            drawChart(StaticLabels.mzLabels, chart90);
        }

        public void drawChart(List<Label> mbilLabels, Chart chart)
        {
            chart.Visible = true;
            ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            chart.ChartAreas.Clear();
            chart.ChartAreas.Add(chartArea1);

            chart.Legends.Clear();
            chart.Legends.Add(legend1);

            series1.Legend = "Legend1";
            series1.Name = "Series1";

            chart.Series.Clear();
            chart.Series.Add(series1);
            chart.Series.Add(series2);

            for (int i = 0; i < StaticLabels.pLabels.Count; ++i)
            {
                Double y = Double.Parse(StaticLabels.pLabels[i].Text);
                Double x = Double.Parse(mbilLabels[i].Text);
                DataPoint dataPoint = new System.Windows.Forms.DataVisualization.Charting.DataPoint(x, y);
                series1.Points.AddXY(x, y);
            }
        }


        private void changeKesitButton_Click(object sender, EventArgs e)
        {
            kesitTipi = kesitTipleriCombo.SelectedIndex;
            if (kesitTipi == 0)
            {

                label8.Text = "B(mm)";
                label9.Show();
                hval.Show();


            }
            else if (kesitTipi == 1)
            {
                label8.Text = "R(mm)";
                label9.Hide();
                hval.Hide();

            }
           
            for (int i = 0; i < donatiComboBox.Items.Count; ++i)
            {
                donatisil(i+1);
            }

        }   

      
    }
}




