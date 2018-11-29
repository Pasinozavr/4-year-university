using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ritadraw
{
    public partial class Draw : Form
    {
        Bitmap myBmp;
        public Draw(Bitmap myBitmap)
        {
            InitializeComponent();
            myBmp = myBitmap;
        }

        private void Draw_Load(object sender, EventArgs e)
        {
            pictureBox1.Height = this.Height - 10;
            pictureBox1.Width = this.Width - 10;
            pictureBox1.Image = myBmp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog savedialog = new SaveFileDialog();
            //saveFileDialog savedialog = new SaveFileDialog();
            savedialog.Title = "Save as";
            savedialog.OverwritePrompt = true;
            savedialog.CheckPathExists = true;
            savedialog.Filter =
                "Bitmap File(*.bmp)|*.bmp|" +
                "GIF File(*.gif)|*.gif";
            savedialog.ShowHelp = true;
            if (savedialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = savedialog.FileName;
                string strFilExtn =
                    fileName.Remove(0, fileName.Length - 3);
                switch (strFilExtn)
                {
                    case "bmp":
                        myBmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case "jpg":
                        myBmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
