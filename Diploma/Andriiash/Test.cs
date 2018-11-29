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
    public partial class Test : Form
    {
        private System.Windows.Forms.TrackBar trackBar1;
        public Test()
        {
            InitializeComponent();
        }

        int quest = 0, test = 0, howmuch, intro, sex, age;
        string gamelike, games;
        string[] variable = new string[3], answer = new string[5], answers = new string[17], temperam = new string[4], quests=new string[17];
        bool first = false, second = false;

        private void Test_Load(object sender, EventArgs e)
        {
            
            
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;


            variable[0] = "низька";
            variable[1] = "середня";
            variable[2] = "висока";

            answer[0] = "так";
            answer[1] = "скоріше так, ніж ни";
            answer[2] = "не знаю";
            answer[3] = "скоріше ні, ніж так";
            answer[4] = "ні";

            temperam[0]="меланхолік";
            temperam[1]="флегматик";
            temperam[2]="сангвіник";
            temperam[3]="холерик";


            quests[1] = "Буває, що ти граєш більше часу, ніж хотів спочатку?";
            quests[2] = "Чи впливають ігри на твої відносини із сім'єю/друзями?";
            quests[3] = "Чи буває, що ти граєш замість того, щоб займатися важливими справами?";
            quests[4] = "Чи займаєшся ти фізичними вправами?";
            quests[5] = "Чи нормальна в тебе самооцінка?";
            quests[6] = "Чи відчуваєш ти себе самотнім часто?";
            quests[7] = "Чи перебуваєш ти у стані депресії часто?";
            quests[8] = "Чи нормальна в тебе сила волі?";
            quests[9] = "Ви інтроверт?";
            quests[10] = "Чи вважаш ти, що тобі треба грати менше?";
            quests[11] = label1.Text;
            quests[12] = label3.Text;
            quests[13] = label4.Text;
            quests[14] = label5.Text;
            quests[15] = label6.Text;
            quests[16] = label7.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            switch (quest)
            {
                case 0:
                    string str = textBox1.Text;
            if (Int32.TryParse(str, out howmuch) && howmuch<24 && howmuch>-1)
            {
                textBox2.Visible = label3.Visible = true;
                quest++;
                progressBar1.Value++;
            }
            else
            {
                textBox2.Visible = label3.Visible = false;
                MessageBox.Show("Невірно введені дані.");
            }
            
            break;
                case 1:
            gamelike = textBox2.Text;
            quest++;progressBar1.Value++;
            textBox3.Visible = label4.Visible = true;
            break;
                case 2:
            games = textBox3.Text;
            quest++;progressBar1.Value++;
            comboBox2.Visible = label5.Visible = true;
            break;
                case 3:
            if (comboBox2.Text == "жіноча" || comboBox2.Text == "чоловіча")
            {
                quest++; progressBar1.Value++;
                if (comboBox2.Text == "чоловіча") sex = 0;
                else sex = 1;
                textBox5.Visible = label6.Visible = true;
            }
            else
            {
                MessageBox.Show("Невірно введені дані.");
            }
            break;
                case 4:
                    str = textBox5.Text;
            if (Int32.TryParse(str, out age) && age>14 && age<60)
            {
                textBox6.Visible = label7.Visible = true;
                quest++; progressBar1.Value++;
            }
            else
            {
                 textBox6.Visible = label7.Visible = false;
                MessageBox.Show("Невірно введені дані.");
            }
            break;
                case 5:
                    str = textBox5.Text;
            if (Int32.TryParse(str, out intro) && intro<101 && intro>-1)
            {
                quest++; progressBar1.Value++;
                button3.Visible = false;
                MessageBox.Show("Ця частина тесту завершена.");
                second = true;
                checkBox1.Checked = true;
                groupBox2.Enabled = false;

                answers[11] = howmuch.ToString();
                answers[12] = gamelike;
                answers[13] = games;
                if (sex==0) { answers[14] = "ч"; } else { answers[14] = "ж"; }
                answers[15]=age.ToString();
                answers[16]=intro.ToString();

                if (first)
                {
                    System.IO.File.AppendAllLines("result.txt", answers);
                    Result res = new Result(answers,quests);
                    res.Show();
                }

            }
            else
            {
                MessageBox.Show("Невірно введені дані.");
            }
            break;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
           switch(test)

            {
               case 0:
                button1.Text = "Наступне";
                question.Text = quests[1];
                test++;
                question.Visible = trackBar2.Visible = true;
                break;
               case 1:
                
                    answers[1] = Convert.ToString(trackBar2.Value);
                    progressBar1.Value++;
                    question.Text = quests[2];

                    test++;
                

                break;
               case 2:

                answers[2] = Convert.ToString(trackBar2.Value);
                    progressBar1.Value++;
                    question.Text = quests[3];

                    test++;

                break;
               case 3:

                answers[3] = Convert.ToString(trackBar2.Value);
                    progressBar1.Value++;
                    question.Text = quests[4];

                    test++;
                

                break;
               case 4:

                answers[4] = Convert.ToString(trackBar2.Value);
                    progressBar1.Value++;
                    question.Text = quests[5];
              
                    test++;
             
                break;
               case 5:

                answers[5] = Convert.ToString(trackBar2.Value);
                    progressBar1.Value++;
                    question.Text = quests[6];
                 
                    test++;

                break;
               case 6:

                answers[6] = Convert.ToString(trackBar2.Value);
                    progressBar1.Value++;
                    question.Text = quests[7];
                   
                    test++;

                break;
               case 7:

                answers[7] = Convert.ToString(trackBar2.Value);
                    progressBar1.Value++;
                    question.Text = quests[8];
                    
                    test++;
             
                break;
               case 8:

                answers[8] = Convert.ToString(trackBar2.Value);
                    progressBar1.Value++;
                    question.Text = quests[9];
                   
                    test++;
          
                break;
               case 9:

                answers[9] = Convert.ToString(trackBar2.Value);
                    progressBar1.Value++;
                    question.Text = quests[10];
                   
                    test++;
                    
              
                break;
               case 10:
                answers[10] = Convert.ToString(trackBar2.Value);
                progressBar1.Value++;
                   
                   button1.Visible = false;
                    checkBox2.Checked = true; 
                    groupBox1.Enabled = false;
                    MessageBox.Show("Ця частина тесту завершена.");
                    first = true;
                    if (second)
                    {
                        System.IO.File.AppendAllLines("result.txt", answers);
                        Result res = new Result(answers,quests);
                        res.Show();
                    }
                break;
            }
        }

 



    }
}
