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
    public partial class Main : Form
    {
        bool login = false, admin, visible = false;
        string rightpass = "admin";
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            radioButton1.Checked = true;
            label1.Text = "Дипломна работа\nна тему";
            label2.Text = "\"Аналіз особистісних складових ігрової залежності\"";
            label3.Text = "студента групи ПК-14-2\nАндріяша Павла";
            textBox1.UseSystemPasswordChar = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            login = true;
            if (radioButton1.Checked) 
            { 
                admin = false; MessageBox.Show("Ви увійшли як гість.");
                label2.Visible = label3.Visible = groupBox1.Visible = false;
                label1.Text = "Ласкаво прошу, госте";
                label1.Visible = false;
                pictureBox1.ImageLocation = "../../images/18.jpg";
                pictureBox1.Visible = true;
            }
            if (radioButton2.Checked) 
            {
                string pass = textBox1.Text;
                if (pass == rightpass)
                {
                    admin = true;
                    MessageBox.Show("Ви увійшли як адміністатор.");
                    textBox1.Visible = label4.Visible = visible = label2.Visible = label3.Visible = groupBox1.Visible = false;
                    label1.Text = "Ласкаво прошу, адміне";
                    label1.Visible = false;
                    pictureBox1.ImageLocation = "../../images/18.jpg";
                    pictureBox1.Visible = true;
                }
                else
                {
                    MessageBox.Show("Пароль введено неправильно.");
                }
            }
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (!visible)
            {
                textBox1.Visible = label4.Visible = visible = true;
            }
            else
            {
                textBox1.Visible = label4.Visible = visible = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Media newform= new Media();
            newform.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (login)
            {
                Test newtest = new Test();
                newtest.Show();
            }
            else
            {
                MessageBox.Show("Спочатку треба пройти авторизацію (увійти).");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Data newdata = new Data(admin);
            newdata.Show();
        }
    }
}
