using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Andriiash
{
    public partial class Result : Form
    {

        public Result(string[] answers, string[] quests)
        {
            InitializeComponent();
            this.answers = answers;
            this.quests = quests;

        }
        string[] answers = new string[17], quests = new string[17];
        private void Result_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            label1.Text = "Останнє питання: як ти вважаєш,\nякий коефіцієнт твоєї ігрової залежності?";
            for (int i = 1; i <= 16; i++)
            {
                listBox1.Items.Add(quests[i]);
                listBox2.Items.Add(answers[i]);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
        double addit(double a, double b, double c, string t)
        {
            double x = Convert.ToDouble(t);
            if (x == 0) { x = 1; }
            if (x == 100) { x = 99; }
            if (x >= a && x <= b) { return (1.0 - ((double)(b - x) / (double)(b - a))); }
            if (x >= b && x <= c) { return (1.0 - ((double)(x - b) / (double)(c - b))); }
            else return 0;
        }

        public double min(params double[] nums)
        {
            double min = 10000.0;
            foreach (double q in nums)
                if (q < min) { min = q; }

            return min;
        }
        public double max(params double[] nums)
        {
            double max = -10000.0;
            foreach (double q in nums)
                if (q > max) { max = q; }

            return max;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            double addiction, controllow, controlmiddle, controlhigh, addictionlow, addictionmiddle, addictionhigh, incomfortlow, incomfortmiddle, incomforthigh, outcomfortlow, outcomfortmiddle, outcomforthigh;

            double[][] fringe =
            {
                new double[]{500,500,500},
                new double[]{500,0,1,30},
                new double[]{500,20,35,50},
                new double[]{500,40,55,70},
                new double[]{500,60,75,90},
                new double[]{500,80,99,100}
            };

            double[][] table = new double[15][];
            for (int i = 0; i < 15; i++) table[i] = new double[6];
            string lines = "";
            for (int j = 1; j <= 5; j++)
            {
                table[1][j] = addit(fringe[j][1], fringe[j][2], fringe[j][3], answers[3]);
                table[2][j] = addit(fringe[j][1], fringe[j][2], fringe[j][3], answers[10]);
                table[3][j] = addit(fringe[j][1], fringe[j][2], fringe[j][3], answers[1]);
                table[4][j] = addit(fringe[j][1], fringe[j][2], fringe[j][3], answers[2]);
                table[5][j] = addit(fringe[j][1], fringe[j][2], fringe[j][3], answers[4]);
                table[6][j] = addit(fringe[j][1], fringe[j][2], fringe[j][3], answers[8]);
                table[7][j] = addit(fringe[j][1], fringe[j][2], fringe[j][3], answers[5]);
                table[8][j] = addit(fringe[j][1], fringe[j][2], fringe[j][3], answers[6]);
                table[9][j] = addit(fringe[j][1], fringe[j][2], fringe[j][3], answers[7]);
            }
            for (int i = 1; i <= 9; i++) { for (int j = 1; j <= 5; j++) { lines += table[i][j]; lines += " "; } lines += "\n"; }
            System.IO.File.WriteAllText("table.txt", lines);

            // for (int i = 1; i <= 9; i++) for (int j = 1; j <= 5; j++) MessageBox.Show("[" + i + "][" + j + "]=" + table[i][j] + ".");

            //           - нет - скорее нет - не знаю - скорее да - да
            //1 вместо дел
            //2 играть меньше
            //3 больше, чем хотел
            //4 отношения
            //5 физ упр
            //6 сила воли
            //7 самооценка
            //8 одиночество
            //9 депрессия
            //контроль
            //внешний
            //внутренний
            //предрасположенность

            if (textBox1.Text != "")
            {
                controllow = min(max(table[1][4], table[1][5]), max(table[2][4], table[2][5]), max(table[3][4], table[3][5]));
                controlmiddle = max(table[1][1], table[1][2], table[1][3], table[2][1], table[2][2], table[2][3], table[3][1], table[3][2], table[3][3]);
                controlhigh = min(table[1][1], table[2][1], table[3][1]);

                outcomfortlow = max(table[4][5], table[4][4], table[5][1], table[5][2]);
                outcomfortmiddle = min(max(table[4][3], table[4][2], table[4][1]), max(table[5][3], table[5][2], table[5][1]));
                outcomforthigh = min(table[4][1], max(table[5][5], table[5][4]));

                incomfortlow = max(table[6][1], table[6][2], table[7][1], table[7][2], table[8][4], table[8][5], table[9][4], table[9][5]);
                incomfortmiddle = min(max(table[6][3], table[6][4]), max(table[7][3], table[7][4]), max(table[8][3], table[8][2]), max(table[9][3], table[9][2]));
                incomforthigh = min(table[6][5], table[7][5], table[8][1], table[9][1]);

                addictionlow = min(controlhigh, incomforthigh);
                addictionmiddle = min(controlmiddle, max(incomfortmiddle, incomforthigh), max(outcomfortmiddle, outcomforthigh));
                addictionhigh = max(controllow, incomfortlow, outcomfortlow);
               
                addiction = (addictionlow * 30 + addictionmiddle * 70 + addictionhigh * 100) / (addictionlow + addictionmiddle + addictionhigh);
                if (addictionlow == 0 && addictionmiddle == 0) { addiction = addictionhigh * 125; }
                if (addiction >= 100) { addiction *= 0.8; }
                if (addiction >= 100) { addiction *= 0.8; }
                if (addiction >= 100) { addiction *= 0.8; }
                /*
                MessageBox.Show("controllow " + controllow);
                MessageBox.Show("controlmiddle) " + controlmiddle);
                MessageBox.Show("controlhigh " + controlhigh);

                MessageBox.Show("outcomfortlow " + outcomfortlow);
                MessageBox.Show("outcomfortmiddle " + outcomfortmiddle);
                MessageBox.Show("outcomforthigh " + outcomforthigh);

                MessageBox.Show("incomfortlow " + incomfortlow);
                MessageBox.Show("incomfortmiddle " + incomfortmiddle);
                MessageBox.Show("incomforthigh " + incomforthigh);

                MessageBox.Show("addictionlow " + addictionlow);
                MessageBox.Show("addictionmiddle " + addictionmiddle);
                MessageBox.Show("addictionhigh " + addictionhigh);
                
                MessageBox.Show("addiction " + addiction);
                 * */
                label3.Text = "Ваша схильність до ігрової залежності " + addiction + " %";
                
                label4.Text = "controllow " + controllow;
                label5.Text = "controlmiddle " + controlmiddle;
                label6.Text = "controlhigh " + controlhigh;
                label7.Text = "outcomfortlow " + outcomfortlow;
                label8.Text = "outcomfortmiddle " + outcomfortmiddle;
                label9.Text = "outcomforthigh " + outcomforthigh;
                label10.Text = "incomfortlow " + incomfortlow;
                label11.Text = "incomfortmiddle " + incomfortmiddle;
                label12.Text = "incomforthigh " + incomforthigh;
                label13.Text = "addictionlow " + addictionlow;
                label14.Text = "addictionmiddle " + addictionmiddle;
                label15.Text = "addictionhigh " + addictionhigh;
                label16.Text = "addiction " + addiction;
            }
            else
            {
                MessageBox.Show("Не введена ваша думка щодо залежності.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label4.Visible == false)
            {
                label4.Visible = label5.Visible = label6.Visible = label7.Visible = label8.Visible = label9.Visible = label10.Visible = label11.Visible = label12.Visible = label13.Visible = label14.Visible = label15.Visible = label16.Visible = true;
            }
            else
            {
                label4.Visible = label5.Visible = label6.Visible = label7.Visible = label8.Visible = label9.Visible = label10.Visible = label11.Visible = label12.Visible = label13.Visible = label14.Visible = label15.Visible = label16.Visible = false;
            }
        }
    }
}
