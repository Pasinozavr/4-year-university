using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft;
using Microsoft.Win32;


namespace ritadraw
{
   

    public partial class Main : Form
    {
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Graphics g = e.Graphics;

            if (Parent != null)
            {
                // Take each control in turn
                int index = Parent.Controls.GetChildIndex(this);
                for (int i = Parent.Controls.Count - 1; i > index; i--)
                {
                    Control c = Parent.Controls[i];

                    // Check it visible and overlaps this control
                    if (c.Bounds.IntersectsWith(Bounds) && c.Visible)
                    {
                        // Load appearance of underlying control and redraw it on this background
                        Bitmap bmp = new Bitmap(c.Width, c.Height, g);
                        c.DrawToBitmap(bmp, c.ClientRectangle);
                        g.TranslateTransform(c.Left - Left, c.Top - Top);
                        g.DrawImageUnscaled(bmp, Point.Empty);
                        g.TranslateTransform(Left - c.Left, Top - c.Top);
                        bmp.Dispose();
                    }
                }
            }
        }
        //global
        Color clearColor=Color.White, drawColor;
        System.Drawing.Drawing2D.FillMode mode=System.Drawing.Drawing2D.FillMode.Alternate;
        Random rnd;
        bool reverseMode = false, showparts = false, beforethestart = true, draw=false,vpisanny = false,first=false, second=false, third=false,catchimage=false;
        bool[,] zanyato = new bool[45, 45];
        int width, height, widthclasters = 5, heightclasters = 5, size=3, k=3, brushfit=1, a=0, b=0, saving=0;
        float tension = 0.2F;
        Bitmap myBitmap, tempImage;
        Graphics fig;
        Pen redPen,
            greenPen,
            blackPen,
            whitePen, 
            currentPen;
        Brush fillBrush;
        Point lv, pn, kuda;
        //
        public Main()
        {
            InitializeComponent();
        }
        void createarrayofpoints(Point[] points, Point lefttop, Point rightbottom)
        {
            Point leftbottom = new Point(lefttop.X, rightbottom.Y);
            Point righttop = new Point(rightbottom.X, lefttop.Y);
            int n = points.Length, i;
            for (i = 0; i < n; i++)
            {
                points[i] = new Point(rnd.Next(leftbottom.X, righttop.X), rnd.Next(leftbottom.Y, righttop.Y));
            }
        }
        void clear()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (!reverseMode)
                    {
                        myBitmap.SetPixel(i, j, clearColor);
                    }
                    else
                    {
                        myBitmap.SetPixel(i, j, drawColor);
                    }
                }
            }
            pictureBox1.Image = myBitmap;
        }
        void showpartsdraw()
        {
            int widthclast = width / widthclasters, heightclast = height / heightclasters;
            if (showparts)
            {
                for (int i = 1; i < width; i++)
                {
                    for (int j = 1; j < height; j++)
                    {
                       // if (i % widthclast == 0 || j % heightclast == 0) { myBitmap.SetPixel(i, j, drawColor); }
                    }
                }
            }
            else
            {
                for (int i = 1; i < width; i++)
                {
                    for (int j = 1; j < height; j++)
                    {
                       // if (i % widthclast == 0 || j % heightclast == 0) { myBitmap.SetPixel(i, j, clearColor); }
                    }
                }
            }
            pictureBox1.Image = myBitmap;
        }
        void showpartsswitch()
        {
            if (!showparts)
            {
                showparts = true;
                label4.Text = "Видно";
            }
            else
            {
                showparts = false;
                label4.Text = "Не видно";
            }
            showpartsdraw();
        }
        void newbrushfit()
        {
            redPen = new Pen(Color.Red, brushfit);
            greenPen = new Pen(Color.Green, brushfit);
            blackPen = new Pen(Color.Black, brushfit);
            whitePen = new Pen(Color.White, brushfit);
            if(!beforethestart)currentPen.Width = brushfit;
        }
        Image ScaleImage(Image source, int width, int height)
        {
            Image dest = new Bitmap(width, height);
            using (Graphics gr = Graphics.FromImage(dest))
            {
                gr.FillRectangle(Brushes.White, 0, 0, width, height);  // Очищаем экран
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                float srcwidth = source.Width;
                float srcheight = source.Height;
                float dstwidth = width;
                float dstheight = height;

                if (srcwidth <= dstwidth && srcheight <= dstheight)  // Исходное изображение меньше целевого
                {
                    int left = (width - source.Width) / 2;
                    int top = (height - source.Height) / 2;
                    gr.DrawImage(source, left, top, source.Width, source.Height);
                }
                else if (srcwidth / srcheight > dstwidth / dstheight)  // Пропорции исходного изображения более широкие
                {
                    float cy = srcheight / srcwidth * dstwidth;
                    float top = ((float)dstheight - cy) / 2.0f;
                    if (top < 1.0f) top = 0;
                    gr.DrawImage(source, 0, top, dstwidth, cy);
                }
                else  // Пропорции исходного изображения более узкие
                {
                    float cx = srcwidth / srcheight * dstheight;
                    float left = ((float)dstwidth - cx) / 2.0f;
                    if (left < 1.0f) left = 0;
                    gr.DrawImage(source, left, 0, cx, dstheight);
                }

                return dest;
            }
        }
        Bitmap CopyBitmap(Image src, Rectangle rect)
        {
            var ret = new Bitmap(rect.Width, rect.Height);
            using (var g = Graphics.FromImage(myBitmap))
            {
                //MessageBox.Show("Натисність, куда вставити");
                g.DrawImage(src, kuda.X, kuda.Y, rect, GraphicsUnit.Pixel);
                
            }
            return ret;
        }
        //Close
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        //Start
        private void button2_Click(object sender, EventArgs e)
        {
            showpartsdraw();
            fig = Graphics.FromImage(myBitmap);

            bool doing=true;

            int p, l, numb=0;
            
            while (doing)
            {
                numb++;
                for (l = 0; l < heightclasters; l++)
                {
                    for (p = 0; p < widthclasters; p++)
                    {
                        if (!zanyato[l, p])
                        {
                            Point[] points = new Point[size];

                            Point lefttop = new Point((width / widthclasters) * (p), (height / heightclasters) * (l + 1)),
                                    rightbottom = new Point((width / widthclasters) * (p + 1), (height / heightclasters) * (l));

                            createarrayofpoints(points, lefttop, rightbottom);
                            if (!checkBox1.Checked)
                            {
                                if (!checkBox2.Checked)
                                {
                                    if (checkBox3.Checked)
                                    {
                                        fig.FillClosedCurve(fillBrush, points, mode, tension);
                                    }
                                    else
                                    {
                                        fig.DrawClosedCurve(currentPen, points, tension, mode);
                                    }
                                }
                                else
                                {
                                    if (checkBox3.Checked)
                                    {

                                        fig.FillClosedCurve(new SolidBrush(Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256))), points, mode, tension);
                                    }
                                    else
                                    {
                                        fig.DrawClosedCurve(new Pen(Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256))), points, tension, mode);
                                    }
                                }
                            }
                            else
                            {
                                if (!checkBox2.Checked)
                                {
                                    if (checkBox3.Checked)
                                    {
                                        fig.FillClosedCurve(fillBrush, points, mode, rnd.Next(10));
                                    }
                                    else
                                    {
                                        fig.DrawClosedCurve(currentPen, points, rnd.Next(10), mode);
                                    }
                                }
                                else
                                {
                                    if (checkBox3.Checked)
                                    {
                                        fig.FillClosedCurve(new SolidBrush(Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256))), points, mode, rnd.Next(5));
                                    }
                                    else
                                    {
                                        fig.DrawClosedCurve(new Pen(Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256))), points, rnd.Next(5), mode);

                                    }
                                }
                            }
                        }
                    }
                }
                
                if (numb == k) { doing = false; }
            }

            pictureBox1.Image = myBitmap;   
        }
        //Reverse
        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color c = myBitmap.GetPixel(i, j);
                    myBitmap.SetPixel(
                        i,
                        j,
                        Color.FromArgb(c.A, 255 - c.R, 255 - c.G, 255 - c.B));
                }
            }

            pictureBox1.Image = myBitmap;

            if (reverseMode)
            {
                label3.Text = "Викл";
                currentPen = blackPen;
                reverseMode = false;
            }
            else
            {
                label3.Text = "Вкл";
                currentPen = whitePen;
                reverseMode = true;
            }
        }
        //Background
        private void button4_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = false;
            MyDialog.ShowHelp = false;
           // MyDialog.Color = pictureBox1.BackColor;
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                clearColor = MyDialog.Color;
                label9.Visible = true;
                label9.Text = MyDialog.Color.Name;
            }
            drawColor = Color.FromArgb(clearColor.A, 255 - clearColor.R, 255 - clearColor.G, 255 - clearColor.B);
            currentPen.Color = drawColor;
            fillBrush = new SolidBrush(drawColor);
            //showpartsswitch();
            clear();
        }
        //Clear
        private void button5_Click(object sender, EventArgs e)
        {
            clear();
            showpartsdraw();
            for (int i = 0; i <= 44; i++) for (int j = 0; j <= 44; j++) zanyato[i, j] = false;
        }
        //Mode (CurverLine Close Mode)
        private void button6_Click(object sender, EventArgs e)
        {
            if (mode == System.Drawing.Drawing2D.FillMode.Alternate) 
            { 
                mode = System.Drawing.Drawing2D.FillMode.Winding; 
                label2.Text = "Крила"; 
            }
            else 
            { 
                mode = System.Drawing.Drawing2D.FillMode.Alternate; 
                label2.Text = "Альт"; 
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i <= 44; i++) for (int j = 0; j <= 44; j++) zanyato[i, j] = false;
            beforethestart = false;
            currentPen = new Pen(Color.Black, brushfit);
            newbrushfit();
           

            myBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            width = pictureBox1.Width;
            height = pictureBox1.Height;

            clear();

            textBox1.Text = "" + size;
            textBox2.Text = "" + k;
            textBox3.Text = "" + widthclasters;
            textBox4.Text = "" + heightclasters;
            textBox5.Text = "" + brushfit;

            rnd = new Random((int)DateTime.Now.Ticks);

            timer1.Interval = 1;
            timer1.Start();

            fillBrush = new SolidBrush(currentPen.Color);
            
        }
        private void button7_Click(object sender, EventArgs e)
        {
            showpartsswitch();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            rnd = new Random((int)DateTime.Now.Ticks);
            saving++;
            if (saving % 1000 == 0)
            {
                myBitmap.Save("temp.bmp");
            }
        }       
        private void button9_Click(object sender, EventArgs e)
        {
            MessageBox.Show("0 - только прямые углы, 1 - только окружности\nset делает либо круги, либо фигуры с острыми углами");
        }
        private void button10_Click(object sender, EventArgs e)
        {
            
        }
        private void button11_Click(object sender, EventArgs e)
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
                        myBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case "jpg":
                        myBitmap.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    default:
                        break;
                }
            }
        }
        private void button10_Click_1(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox3.Text) > 40 || Convert.ToInt32(textBox4.Text) > 40)
            {
                MessageBox.Show("Завелика кількість кластерів.");
            }
            else
            {
                widthclasters = Convert.ToInt32(textBox3.Text);
                heightclasters = Convert.ToInt32(textBox4.Text);
            }
            size = Convert.ToInt32(textBox1.Text);
            k = Convert.ToInt32(textBox2.Text);
            brushfit = Convert.ToInt32(textBox5.Text);
            newbrushfit();
            MessageBox.Show("Зміни введені");
            bool pars = true; 
            char[] newtext = textBox6.Text.ToCharArray();
            for (int i = 0; i < newtext.Length; i++)
            {
                if (newtext[i] == '.')
                {
                    pars = false;
                }
            }
            if (pars)
            {
                tension = (float)(Convert.ToDouble(textBox6.Text));
            }
            else
            {
                MessageBox.Show("Неправильно введені данні напруги.");
            }
            
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == 0)
            {
                reverseMode = false; label3.Text = "Вимк";
                mode = System.Drawing.Drawing2D.FillMode.Alternate; label2.Text = "Альт";
                showparts = false; label4.Text = "Не видно";
                checkBox1.Checked = true;
                checkBox2.Checked = true;
                checkBox3.Checked = true;
                size = 20; textBox1.Text = size.ToString();
                k = 1; textBox2.Text = k.ToString();
                brushfit = 1; newbrushfit(); textBox5.Text = brushfit.ToString();
                heightclasters = 5; textBox3.Text = heightclasters.ToString();
                widthclasters = 5; textBox4.Text = widthclasters.ToString();
                clearColor = Color.White; drawColor = Color.FromArgb(clearColor.A, 255 - clearColor.R, 255 - clearColor.G, 255 - clearColor.B); currentPen.Color = drawColor; fillBrush = new SolidBrush(drawColor); label9.Text = "White"; clear();
            }
            if (listBox1.SelectedIndex == 1)
            {
                reverseMode = false; label3.Text = "Вимк";
                mode = System.Drawing.Drawing2D.FillMode.Alternate; label2.Text = "Альт";
                showparts = false; label4.Text = "Не видно";
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = true;
                tension = 3; textBox6.Text = tension.ToString();
                size = 35; textBox1.Text = size.ToString();
                k = 1; textBox2.Text = k.ToString();
                brushfit = 1; newbrushfit(); textBox5.Text = brushfit.ToString();
                heightclasters = 5; textBox3.Text = heightclasters.ToString();
                widthclasters = 5; textBox4.Text = widthclasters.ToString();
                clearColor = Color.Blue; drawColor = Color.DarkSlateBlue; currentPen.Color = drawColor; fillBrush = new SolidBrush(drawColor); label9.Text = "Blue"; clear();
            }
            if (listBox1.SelectedIndex == 2)
            {
                reverseMode = false; label3.Text = "Вимк";
                mode = System.Drawing.Drawing2D.FillMode.Alternate; label2.Text = "Альт";
                showparts = false; label4.Text = "Не видно";
                checkBox1.Checked = true;
                checkBox2.Checked = false;
                checkBox3.Checked = true;
                size = 50; textBox1.Text = size.ToString();
                k = 1; textBox2.Text = k.ToString();
                brushfit = 1; newbrushfit(); textBox5.Text = brushfit.ToString();
                heightclasters = 2; textBox3.Text = heightclasters.ToString();
                widthclasters = 2; textBox4.Text = widthclasters.ToString();
                clearColor = Color.White; drawColor = Color.FromArgb(clearColor.A, 255 - clearColor.R, 255 - clearColor.G, 255 - clearColor.B); currentPen.Color = drawColor; fillBrush = new SolidBrush(drawColor); label9.Text = "White"; clear();
            }
            if (listBox1.SelectedIndex == 3)
            {
                reverseMode = false; label3.Text = "Вимк";
                mode = System.Drawing.Drawing2D.FillMode.Alternate; label2.Text = "Альт";
                showparts = false; label4.Text = "Не видно";
                checkBox1.Checked = true;
                checkBox2.Checked = true;
                checkBox3.Checked = true;
                size = 40; textBox1.Text = size.ToString();
                k = 3; textBox2.Text = k.ToString();
                brushfit = 1; newbrushfit(); textBox5.Text = brushfit.ToString();
                heightclasters = 40; textBox3.Text = heightclasters.ToString();
                widthclasters = 40; textBox4.Text = widthclasters.ToString();
                clearColor = Color.White; drawColor = Color.FromArgb(clearColor.A, 255 - clearColor.R, 255 - clearColor.G, 255 - clearColor.B); currentPen.Color = drawColor; fillBrush = new SolidBrush(drawColor); label9.Text = "White"; clear();
            }
            if (listBox1.SelectedIndex == 4)
            {
                reverseMode = false; label3.Text = "Вимк";
                mode = System.Drawing.Drawing2D.FillMode.Alternate; label2.Text = "Альт";
                showparts = false; label4.Text = "Не видно";
                checkBox1.Checked = true;
                checkBox2.Checked = true;
                checkBox3.Checked = true;
                size = 5; textBox1.Text = size.ToString();
                k = 3; textBox2.Text = k.ToString();
                brushfit = 1; newbrushfit(); textBox5.Text = brushfit.ToString();
                heightclasters = 12; textBox3.Text = heightclasters.ToString();
                widthclasters = 12; textBox4.Text = widthclasters.ToString();
                clearColor = Color.White; drawColor = Color.FromArgb(clearColor.A, 255 - clearColor.R, 255 - clearColor.G, 255 - clearColor.B); currentPen.Color = drawColor; fillBrush = new SolidBrush(drawColor); label9.Text = "White"; clear();
            }
        }
        private void button12_Click(object sender, EventArgs e)
        {

            int oldi = myBitmap.Width, oldj = myBitmap.Height;
            
            Bitmap lol = new Bitmap(Convert.ToInt32(textBox7.Text), Convert.ToInt32(textBox8.Text));

            width = Convert.ToInt32(textBox7.Text);
            height = Convert.ToInt32(textBox8.Text);

            fig = Graphics.FromImage(lol);
            fig.Clear(clearColor);
            bool doing = true;

            int p, l, numb = 0;

            while (doing)
            {
                numb++;
                for (l = 0; l < heightclasters; l++)
                {
                    for (p = 0; p < widthclasters; p++)
                    {
                        Point[] points = new Point[size];

                        Point lefttop = new Point((width / widthclasters) * (p), (height / heightclasters) * (l + 1)), rightbottom = new Point((width / widthclasters) * (p + 1), (height / heightclasters) * (l));

                        createarrayofpoints(points, lefttop, rightbottom);
                        if (!checkBox1.Checked)
                        {
                            if (!checkBox2.Checked)
                            {
                                if (checkBox3.Checked)
                                {
                                    fig.FillClosedCurve(fillBrush, points, mode, tension);
                                }
                                else
                                {
                                    fig.DrawClosedCurve(currentPen, points, tension, mode);
                                }
                            }
                            else
                            {
                                if (checkBox3.Checked)
                                {
                                    fig.FillClosedCurve(new SolidBrush(Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), 0)), points, mode, tension);
                                }
                                else
                                {
                                    fig.DrawClosedCurve(new Pen(Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), 0)), points, tension, mode);
                                }
                            }
                        }
                        else
                        {
                            if (!checkBox2.Checked)
                            {
                                if (checkBox3.Checked)
                                {
                                    fig.FillClosedCurve(fillBrush, points, mode, rnd.Next(10));
                                }
                                else
                                {
                                    fig.DrawClosedCurve(currentPen, points, rnd.Next(10), mode);
                                }
                            }
                            else
                            {
                                if (checkBox3.Checked)
                                {
                                    fig.FillClosedCurve(new SolidBrush(Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), 0)), points, mode, rnd.Next(5));
                                }
                                else
                                {
                                    fig.DrawClosedCurve(new Pen(Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), 0)), points, rnd.Next(5), mode);

                                }
                            }
                        }
                    }
                }

                if (numb == k) { doing = false; }

                Draw newdraw = new Draw(lol);
                newdraw.Width = Convert.ToInt32(textBox7.Text) + 15;
                newdraw.Height = Convert.ToInt32(textBox7.Text) + 15;
                newdraw.Show();

                width = oldi;
                height = oldj;
            }

        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (!vpisanny)
            {
                ColorDialog MyDialog = new ColorDialog();
                MyDialog.AllowFullOpen = false;
                MyDialog.ShowHelp = false;
                // MyDialog.Color = pictureBox1.BackColor;
                if (MyDialog.ShowDialog() == DialogResult.OK)
                {
                    fillBrush = new SolidBrush(MyDialog.Color);
                }
                clear();

                vpisanny = true;
                button13.Text = "Закінчити";
            }
            else
            {
                vpisanny = false;
                button13.Text = "Вписання";
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            draw = false;  
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            label14.Text = ""+myBitmap.GetPixel(e.X, e.Y);
            
            fig = Graphics.FromImage(myBitmap);
            if (third) { kuda.X = e.X; kuda.Y = e.Y; third = false; fig.DrawRectangle(new Pen(clearColor), lv.X, lv.Y, pn.X - lv.X, pn.Y - lv.Y); var res = CopyBitmap(pictureBox1.Image, new Rectangle(lv.X + 1, lv.Y + 1, pn.X - lv.X - 1, pn.Y - lv.Y - 1)); pictureBox1.Image = myBitmap; }
            else if (second) { pn.X = e.X; pn.Y = e.Y; second = false; fig.DrawRectangle(new Pen(Color.Red), lv.X, lv.Y, pn.X - lv.X, pn.Y - lv.Y); pictureBox1.Image = myBitmap; third = true; }
            else if (first) { lv.X = e.X; lv.Y = e.Y; first = false; second = true; }
            else if (catchimage) {

                kuda.X = e.X; kuda.Y = e.Y; catchimage = false; var res = CopyBitmap(ScaleImage(tempImage, Convert.ToInt32(textBox9.Text),Convert.ToInt32(textBox10.Text)), new Rectangle(0, 0, tempImage.Width, tempImage.Height)); pictureBox1.Image = myBitmap;



                for (int i = 0; i < Convert.ToInt32(textBox9.Text); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(textBox10.Text); j++)
                    {

                        for (int l = 0; l < heightclasters; l++)
                        {
                            for (int p = 0; p < widthclasters; p++)
                            {
                                Point lefttop = new Point((width / widthclasters) * (p), (height / heightclasters) * (l + 1)),
                                    rightbottom = new Point((width / widthclasters) * (p + 1), (height / heightclasters) * (l));
                                //ERROR
                                if (kuda.X + i > lefttop.X && kuda.X + i < rightbottom.X && kuda.Y + j < lefttop.Y && kuda.Y + j > rightbottom.Y && myBitmap.GetPixel(kuda.X + i, kuda.Y + j) != Color.FromArgb(255, 255, 255, 255)) zanyato[l, p] = true;
                            }
                        }
                    }
                }
            }
            else draw = true;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (draw && vpisanny)
            {
                fig = Graphics.FromImage(myBitmap);

                fig.FillEllipse(new SolidBrush(Color.Green), e.X, e.Y, brushfit+1, brushfit+1);

                pictureBox1.Image = myBitmap;

                for (int l = 0; l < heightclasters; l++)
                {
                    for (int p = 0; p < widthclasters; p++)
                    {
                        Point lefttop = new Point((width / widthclasters) * (p), (height / heightclasters) * (l + 1)),
                            rightbottom = new Point((width / widthclasters) * (p + 1), (height / heightclasters) * (l));

                        if (e.X >= lefttop.X && e.X <= rightbottom.X && e.Y < lefttop.Y && e.Y > rightbottom.Y && myBitmap.GetPixel(e.X, e.Y) != clearColor) zanyato[l, p] = true;
         
                    }
                }
            }
            
        }

        private void button14_Click(object sender, EventArgs e)
        {
            vpisanny = false;
            MessageBox.Show("Натисніть на лівий верхній край регіону, який хочете скопіювати, а потом - на правий нижній, і, в-третіх, куди");
            first = true;

        }

        private void button15_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Оберіть, яке зображення потрібно додати, і після цього натисніть на точку на холсті, куди");

            openFileDialog1.Title = "Open Image";
            openFileDialog1.Filter = "*.bmp|*.png|*.jpg|*.jpeg";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);

                tempImage=new Bitmap(openFileDialog1.FileName);
                
                sr.Close();
            }
           
            catchimage = true;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            pictureBox1.BackColor = pictureBox1.BackColor = Color.Transparent;
        }


 
    }
}

