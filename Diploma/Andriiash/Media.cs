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
    public partial class Media : Form
    {
        
        public Media()
        {
            InitializeComponent();
        }
        string[] links;

        private void Media_Load(object sender, EventArgs e)
        {

            links = new string[10];
            links[1] = "https://www.youtube.com/watch?v=VQMqpvR2ns8";
            links[2] = "http://edition.cnn.com/2016/01/06/health/video-games-addiction-gentile-feat/index.html";
            links[3] = "https://www.youtube.com/watch?v=EHmC2D0_Hdg";
            links[4] = "http://edition.cnn.com/2012/08/05/tech/gaming-gadgets/gaming-addiction-warning-signs/index.html";
            links[5] = "https://www.youtube.com/watch?v=V_qlumZ5K4I";
            links[6] = "https://www.youtube.com/watch?v=ym1VOVjuMBM";
            links[7] = "https://www.youtube.com/watch?v=En5D3Kpej5k";
            links[8] = "http://channel.nationalgeographic.com/taboo/videos/gaming-addiction/";
            links[9] = "https://www.youtube.com/watch?v=_-KV0l6o5FE";

            timer1.Start();
            timer1.Interval = 1500;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            listBox1.Visible = listBox2.Visible = listBox3.Visible = false;

            listBox1.Items.Add("Веб-серфінг");
            listBox1.Items.Add("Воля");
            listBox1.Items.Add("Депресія");
            listBox1.Items.Add("Ефект Google");
            listBox1.Items.Add("Залежність");
            listBox1.Items.Add("Ігри");
            listBox1.Items.Add("Кіберсексуалізм");
            listBox1.Items.Add("Кіберхондія");
            listBox1.Items.Add("Номофобія");
            listBox1.Items.Add("Психічне виснаження");
            listBox1.Items.Add("Самооцінка");
            listBox1.Items.Add("Сила волі");
            listBox1.Items.Add("Смерті через залежність");
            listBox1.Items.Add("Спілкування");
            listBox1.Items.Add("Фінансові розтрати");

            for (int i = 1; i <= 9; i++)
            {
                listBox2.Items.Add(i);
            }


            for (int i = 1; i <= 27; i++)
            {
                listBox3.Items.Add(i);
            }



        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webBrowser1.Visible = pictureBox1.Visible = listBox2.Visible=  listBox3.Visible = false;
            listBox1.Visible = richTextBox1.Visible = true; 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox2.Visible = webBrowser1.Visible = true;
            pictureBox1.Visible = richTextBox1.Visible = listBox3.Visible = listBox1.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox3.Visible = pictureBox1.Visible = true;
            webBrowser1.Visible = richTextBox1.Visible = listBox2.Visible = listBox1.Visible = false;
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            switch (listBox1.SelectedIndex)
            {
                case 0:
                    richTextBox1.LoadFile("../../words/веб-серфінг.rtf");
                    break;
                case 1:
                    richTextBox1.LoadFile("../../words/воля.rtf");
                    break;
                case 2:
                    richTextBox1.LoadFile("../../words/депресія.rtf");
                    break;
                case 3:
                    richTextBox1.LoadFile("../../words/ефект google.rtf");
                    break;
                case 4:
                    richTextBox1.LoadFile("../../words/залежність.rtf");
                    break;
                case 5:
                    richTextBox1.LoadFile("../../words/ігри.rtf");
                    break;
                case 6:
                    richTextBox1.LoadFile("../../words/кіберсуксуалізм.rtf");
                    break;
                case 7:
                    richTextBox1.LoadFile("../../words/кіберхондія.rtf");
                    break;
                case 8:
                    richTextBox1.LoadFile("../../words/номофобія.rtf");
                    break;
                case 9:
                    richTextBox1.LoadFile("../../words/психічне виснаження.rtf");
                    break;
                case 10:
                    richTextBox1.LoadFile("../../words/самооцінка.rtf");
                    break;
                case 11:
                    richTextBox1.LoadFile("../../words/сила волі.rtf");
                    break;
                case 12:
                    richTextBox1.LoadFile("../../words/смерть.rtf");
                    break;
                case 13:
                    richTextBox1.LoadFile("../../words/спілкування.rtf");
                    break;
                case 14:
                    richTextBox1.LoadFile("../../words/фінансові розтрати.rtf");
                    break;

            }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = "../../images/" + listBox3.SelectedItem + ".jpg";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (listBox3.SelectedIndex != 26) listBox3.SelectedIndex += 1;
                else listBox3.SelectedIndex = 0;
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {


            webBrowser1.Navigate(new Uri(links[listBox2.SelectedIndex+1]));
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }


    }
}
