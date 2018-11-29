using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace Andriiash
{
    public partial class Data : Form
    {
        public Data(bool admin)
        {
            InitializeComponent();
            this.admin = admin;
        }
        bool admin;
        private void Data_Load(object sender, EventArgs e)
        { 
            dataGridView1.RowCount = 17;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            if (!admin) button2.Enabled = false;
            else { dataGridView1.Enabled = true; }
            using (TextReader tr = new StreamReader("result.txt"))
            {
                while (tr.Peek() >= 0)
                {
                    for(int j=0; j!=100; )
                {
                    for (int i = 0; i < 17; i++)
                    {
                        string sr = tr.ReadLine();
                        dataGridView1.Rows[i].Cells[j].Value = sr;
                        if (i == 16) { j++; dataGridView1.ColumnCount++; }
                    }
                }
                }
            }

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
