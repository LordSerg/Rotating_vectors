using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rotating_vectors
{
    public partial class Form1 : Form
    {
        //int ID;
        vector []vctr;
        ListViewItem[] lvi;
        public Form1()
        {
            InitializeComponent();
        }

        class vector
        {
            public static int num_of_vectors=0;//переменная для всего класса
            public int id=0;
            public bool is1,is2, right;
            public double x0, y0, length, a, b, x, y;
            public double alpha;
            public vector(int ID, double ALPHA, double BETTA, double LENGTH, bool is_first, bool is_last, vector[] v)
            {
                id = ID;
                b = BETTA;//скорость поворота
                a = (ALPHA * Math.PI) / 180;//начальный угол поворота
                //alpha = (ALPHA * Math.PI) / 180;
                alpha = 0;
                length = LENGTH;//длинна вектора
                is1 = is_first;
                is2 = is_last;
                if (is1)
                {//закрепляем вектор в начале координат
                    x0 = 0;
                    y0 = 0;
                }
                else
                {
                    x0 = v[id - 1].x0 + v[id - 1].length * Math.Cos(v[id - 1].a);
                    y0 = v[id - 1].y0 + v[id - 1].length * Math.Sin(v[id - 1].a);
                }
                if (is2)
                {//прикрепляем к концу вектора индикаторы позиции (кисть)
                    x = x0 + length * Math.Cos(a);
                    y = y0 + length * Math.Sin(a);
                }
                else
                {
                    x = 0;
                    y = 0;
                }
            }
            public void next_action(vector []v)
            {
                //присваеваем новые начальные координаты
                if (!is1)
                {
                    alpha += ((Math.PI * b) / 180);
                    a = v[id - 1].a + alpha;
                    re:
                    if (a > Math.PI * 2)//ставим ограничения на него
                    {
                        a -= Math.PI * 2;
                        goto re;
                    }
                    if (a < Math.PI * (-2))//ставим ограничения на него
                    {
                        a += Math.PI * 2;
                        goto re;
                    }
                    x0 = v[id - 1].x0 + v[id - 1].length * Math.Cos(v[id - 1].a);
                    y0 = v[id - 1].y0 + v[id - 1].length * Math.Sin(v[id - 1].a);
                    //if (is2)
                    //{//если этот вектор последний:
                    //    x = x0 + length * Math.Cos(a);
                    //    y = y0 + length * Math.Sin(a);
                    //}
                }
                else
                {
                        a += ((Math.PI * b) / 180);
                    rea:
                    if (a > Math.PI * 2)//ставим ограничения на него
                    {
                        a -= Math.PI * 2;
                        goto rea;
                    }
                    if (a < Math.PI * (-2))//ставим ограничения на него
                    {
                        a += Math.PI * 2;
                        goto rea;
                    }
                    //if (is2)
                    //{//если этот вектор последний:
                    //    x = x0 + length * Math.Cos(a);
                    //    y = y0 + length * Math.Sin(a);
                    //}
                }
                if(is2)
                {
                    x = x0 + length * Math.Cos(a);
                    y = y0 + length * Math.Sin(a);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //string s = textBox1.Text;
            //string str="";
            //foreach (char c in s)
            //    if (c >= '0' && c <= '9')
            //        str += c;
            //textBox1.Text = str;
            if (listView1.SelectedItems.Count != 0/*&&*/)
            {
                listView1.SelectedItems[0].SubItems[1].Text = textBox1.Text;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //string s = textBox2.Text;
            //string str = "";
            //foreach (char c in s)
            //    if (c >= '0' && c <= '9')
            //        str += c;
            //textBox2.Text = str;
            if (listView1.SelectedItems.Count != 0)
            {
                listView1.SelectedItems[0].SubItems[3].Text = textBox2.Text;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            vctr = new vector[100000];
            lvi = new ListViewItem[100000];
            //Add_vector(Convert.ToInt32(textBox1.Text), Convert.ToInt32(numericUpDown1.Value), Convert.ToDouble(textBox2.Text), true, true);//автоматически создаем первый вектор
            p_image = new Pen(Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value));
            trackBar1.Value =250;//r
            trackBar2.Value =250;//g
            trackBar3.Value =250;//b
            p_vectors = new Pen(Color.FromArgb(trackBar7.Value, trackBar6.Value, trackBar5.Value));
            trackBar7.Value =1;//r
            trackBar6.Value =180;//g
            trackBar5.Value =1;//b
            toolTip1.SetToolTip(label3, "Длинна вектора");
            toolTip1.SetToolTip(textBox1, "Длинна вектора");
            toolTip1.SetToolTip(label4, "Градус, на который вектору\nможно откланяться\n(если 360 - то безограничений)");
            toolTip1.SetToolTip(textBox4, "Градус, на который вектору\nможно откланяться\n(если 360 - то безограничений)");
            toolTip1.SetToolTip(textBox2, "(Градусов в секунду)");
            toolTip1.SetToolTip(label5, "(Градусов в секунду)");
        }

        public void Add_vector(double lng, double a, double spd, bool isfirst, bool islast)
        {
            vector.num_of_vectors++;
            ListViewItem lv = new ListViewItem(vector.num_of_vectors.ToString());
            lv.SubItems.Add(lng.ToString());//длинна вектора
            lv.SubItems.Add(a.ToString());//диапазон
            lv.SubItems.Add(spd.ToString());//скорость
            //if (to_right)//направление
            //    lv.SubItems.Add("Направо");
            //else
            //    lv.SubItems.Add("Налево");
            listView1.Items.Add(lv);
            lvi[vector.num_of_vectors - 1] = lv;
            vector v = new vector(vector.num_of_vectors - 1, Math.PI * a / 180, Math.PI * spd / 180, lng, isfirst, islast, vctr);
            vctr[vector.num_of_vectors - 1] = v;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {//выделяем вектор
            if (listView1.SelectedItems.Count > 0)
            {
                button1.Enabled = true;
                label2.Text = listView1.SelectedItems[0].SubItems[0].Text;//индекс
                textBox1.Text = listView1.SelectedItems[0].SubItems[1].Text;//длинна
                textBox4.Text = listView1.SelectedItems[0].SubItems[2].Text;//диапазон
                textBox2.Text = listView1.SelectedItems[0].SubItems[3].Text;//скорость
                //заполняем изменения в массив векторов:
                for (int i = 0; i < vector.num_of_vectors; i++)
                {
                    vctr[Convert.ToInt32(listView1.Items[i].SubItems[0].Text) - 1].length = Convert.ToDouble(listView1.Items[i].SubItems[1].Text);//длинна
                    vctr[Convert.ToInt32(listView1.Items[i].SubItems[0].Text) - 1].a = (Math.PI * Convert.ToDouble(listView1.Items[i].SubItems[2].Text))/180;//начальный угол
                    vctr[Convert.ToInt32(listView1.Items[i].SubItems[0].Text) - 1].b = (Math.PI * Convert.ToDouble(listView1.Items[i].SubItems[3].Text)) / 180;//скорость
                }
            }
            else
                label2.Text = "_";
        }

        private void button1_Click(object sender, EventArgs e)
        {//удалить вектор
            if (listView1.SelectedItems.Count != 0)
            {
                vector.num_of_vectors--;
                for (int i = listView1.SelectedItems[0].Index; i < vector.num_of_vectors; i++)
                {
                    listView1.Items[i].SubItems[0].Text = (Convert.ToInt32(listView1.Items[i + 1].SubItems[0].Text) - 1).ToString();
                    vctr[i] = vctr[i + 1];
                    for (int j = 1; j < 4; j++)
                        listView1.Items[i].SubItems[j].Text = listView1.Items[i + 1].SubItems[j].Text;
                }
                listView1.Items[vector.num_of_vectors].Remove();
                vctr[vector.num_of_vectors] = null;
                vctr[vector.num_of_vectors - 1].is2 = true;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {//Начать\Остановить нарисовку
            if (listView1.Items.Count != 0)//огранечитель на случай удаления всех векторов
            {
                if (button2.Text == "Начать")
                {//
                    button2.Text = "Остановить";
                    b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                    g1 = Graphics.FromImage(b);
                    g = Graphics.FromImage(b);
                    for (int i = 0; i < vector.num_of_vectors; i++)
                    {
                        vctr[i].a = 0;
                        vctr[i].alpha = (Math.PI*Convert.ToDouble(listView1.Items[i].SubItems[2].Text))/180;
                    }
                    timer1.Interval = 1;
                    restart = true;
                    timer1.Enabled = true;
                }
                else
                {
                    button2.Text = "Начать";
                    timer1.Enabled = false;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {//добавить вектор
            bool first=false;
            
            if (vector.num_of_vectors == 0)
                first = true;
            Add_vector(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox4.Text), Convert.ToDouble(textBox2.Text), first, true);
            for(int i=0;i<vector.num_of_vectors-1;i++)
                vctr[i].is2 = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {//стираем всё
            timer1.Enabled = false;
            listView1.Items.Clear();
            //listView1.Items.Insert();
            vctr = new vector[10000];
            vector.num_of_vectors = 0;
            //Add_vector(Convert.ToInt32(textBox1.Text), Convert.ToInt32(numericUpDown1.Value), Convert.ToDouble(textBox2.Text), true, true);
        }

        

        private void button5_Click(object sender, EventArgs e)
        {//сьемная часть
            //138→23
            if (panel1.Height > 130)
                while (panel1.Height>24)
                    panel1.Height-=6;
            else 
                while (panel1.Height<130)
                    panel1.Height+=6;
        }

        bool CancelEdit = false;
        ListViewItem.ListViewSubItem CurrentSubItem = default(ListViewItem.ListViewSubItem);
        ListViewItem CurrentItem = default(ListViewItem);

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Get current item of ListView
            CurrentItem = listView1.GetItemAt(e.X, e.Y);
            if (CurrentItem == null)
                return;

            // Get sub item of current item
            CurrentSubItem = CurrentItem.GetSubItemAt(e.X, e.Y);
            int SubItembIndex = CurrentItem.SubItems.IndexOf(CurrentSubItem);

            // Check that we try edit column "Value"
            switch (SubItembIndex)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    return;
            }

            // Set params for TextBox, show it and set focus
            int lLeft = CurrentSubItem.Bounds.Left + 2;
            int lWidth = CurrentSubItem.Bounds.Width - 2;
            textBox3.SetBounds(lLeft + listView1.Left, CurrentSubItem.Bounds.Top + listView1.Top, lWidth, CurrentSubItem.Bounds.Height);
            textBox3.Text = CurrentSubItem.Text;
            textBox3.Show();
            textBox3.Focus();
            textBox3.Location = new Point(e.X - textBox3.Width / 2, e.Y);
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            textBox3.Hide();
            if (CancelEdit == false)
            {
                double azaza;
                if (double.TryParse(textBox3.Text, out azaza) == true)
                    CurrentSubItem.Text = textBox3.Text;
            }
            else
                CancelEdit = false;
            listView1.Focus();
        
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                // If you press Enter than copy data from TextBox to ListView cell
                case (char)Keys.Return:
                    CancelEdit = false;
                    e.Handled = true;
                    textBox3.Hide();
                    break;

                // If you press Escape than data in ListView cell stay without changes
                case (char)Keys.Escape:
                    CancelEdit = true;
                    e.Handled = true;
                    textBox3.Hide();
                    break;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //string s = textBox3.Text;
            //string str = "";
            //foreach (char c in s)
            //    if (c >= '0' && c <= '9')
            //        str += c;
            //textBox3.Text = str;
        }
        bool restart;
        private void Timer1_Tick(object sender, EventArgs e)
        {
            //1.очистка
            //2.смещение каждого вектора на скорость вектора
            //3.провести прямую от конечной точки к предыдущей конечной точкой
            //pictureBox1.Refresh();
            if (radioButton2.Checked)
                g.Clear(Color.Transparent);
            
            if (restart)
            {
                for (int i = 0; i < vector.num_of_vectors; i++)
                    vctr[i].next_action(vctr);
                p1.X = (float)(vctr[vector.num_of_vectors - 1].x0 + vctr[vector.num_of_vectors - 1].length * Math.Cos(vctr[vector.num_of_vectors - 1].a));
                p1.Y = (float)(vctr[vector.num_of_vectors - 1].y0 + vctr[vector.num_of_vectors - 1].length * Math.Sin(vctr[vector.num_of_vectors - 1].a));
                restart = false;
            }
            else
            {
                p1.X = (float)vctr[vector.num_of_vectors - 1].x;
                p1.Y = (float)vctr[vector.num_of_vectors - 1].y;
            }
            for (int i = 0; i < vector.num_of_vectors ; i++)
                vctr[i].next_action(vctr);
            draw_vector(vctr);
        }
        Bitmap b;
        Graphics g, g1;
        PointF p0,p1;

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            change_color();
        }

        private void TrackBar2_Scroll(object sender, EventArgs e)
        {
            change_color();
        }

        private void TrackBar3_Scroll(object sender, EventArgs e)
        {
            change_color();
        }

        private void TrackBar5_Scroll(object sender, EventArgs e)
        {
            change_color();
        }

        private void TrackBar6_Scroll(object sender, EventArgs e)
        {
            change_color();
        }

        private void TrackBar7_Scroll(object sender, EventArgs e)
        {
            change_color();
        }

        void change_color()
        {
            p_image.Color = Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value);
            label6.BackColor = Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value);
            label6.ForeColor = Color.FromArgb(250 - trackBar1.Value, 250 - trackBar2.Value, 250 - trackBar3.Value);
            p_vectors.Color = Color.FromArgb(trackBar7.Value, trackBar6.Value, trackBar5.Value);
            label8.BackColor = Color.FromArgb(trackBar7.Value, trackBar6.Value, trackBar5.Value);
            label8.ForeColor = Color.FromArgb(250 - trackBar7.Value, 250 - trackBar6.Value, 250 - trackBar5.Value);
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
                listView1.SelectedItems[0].SubItems[2].Text = textBox4.Text;
        }

        Pen p_vectors, p_image;
        void draw_vector(vector []v)
        {
            v[0].x0 = pictureBox1.Width / 2;
            v[0].y0 = pictureBox1.Height / 2;
            v[vector.num_of_vectors - 1].x = vctr[vector.num_of_vectors - 1].x0 + vctr[vector.num_of_vectors - 1].length * Math.Cos(vctr[vector.num_of_vectors - 1].a);
            v[vector.num_of_vectors - 1].y = vctr[vector.num_of_vectors - 1].y0 + vctr[vector.num_of_vectors - 1].length * Math.Sin(vctr[vector.num_of_vectors - 1].a);
            p0.X = (float)v[vector.num_of_vectors - 1].x;
            p0.Y = (float)v[vector.num_of_vectors - 1].y;
            p_image.Color = Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value);
            p_vectors.Color = Color.FromArgb(trackBar7.Value, trackBar6.Value, trackBar5.Value);
            g1.DrawLine(p_image, p0, p1);
            //отрисовка векторов:
            if (radioButton2.Checked)
            {
                for (int i = 0; i < vector.num_of_vectors - 1; i++)
                {
                    g.DrawLine(p_vectors, (float)v[i].x0, (float)v[i].y0, (float)v[i + 1].x0, (float)v[i + 1].y0);
                }
                g.DrawLine(p_vectors, (float)v[vector.num_of_vectors - 1].x0, (float)v[vector.num_of_vectors - 1].y0, (float)v[vector.num_of_vectors - 1].x, (float)v[vector.num_of_vectors - 1].y);
            }
            //отрисовка графика:
            
            pictureBox1.Image = b;
        }
    }
}
