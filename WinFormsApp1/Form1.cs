using System;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // �������������� ������ ��� ��������
            textBox1.Text = Properties.Settings.Default.SideA;
            textBox2.Text = Properties.Settings.Default.SideB;
            textBox3.Text = Properties.Settings.Default.SideC;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TryGetSides(out int a, out int b, out int c))
            {
                if (Triangle.Exists(a, b, c))
                {
                    MessageBox.Show($"����������� �� ��������� {a}, {b}, {c} ����������", "�������� �������������");
                }
                else
                {
                    MessageBox.Show($"����������� �� ��������� {a}, {b}, {c} �� ����������", "������");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (TryGetSides(out int a, out int b, out int c))
            {
                if (Triangle.Exists(a, b, c))
                {
                    if (Triangle.IsRightTriangle(a, b, c))
                    {
                        MessageBox.Show("����������� �������� �������������", "�������� �� ���������������");
                    }
                    else
                    {
                        MessageBox.Show("����������� �� �������� �������������", "�������� �� ���������������");
                    }
                }
                else
                {
                    MessageBox.Show("����������� �� ����������, �������� �� ��������������� ����������", "������");
                }
            }
        }

        private bool TryGetSides(out int a, out int b, out int c)
        {
            // ���������� �������� �� ��������� ��� out-����������
            a = b = c = 0;

            bool isValid = int.TryParse(textBox1.Text, out a)
                        && int.TryParse(textBox2.Text, out b)
                        && int.TryParse(textBox3.Text, out c);

            if (!isValid || a <= 0 || b <= 0 || c <= 0)
            {
                MessageBox.Show("������� ���������� ������������� ����� �����", "������ �����");
                return false;
            }

            return true;
        }



        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // ���������� ������ ��� �������� �����
            Properties.Settings.Default.SideA = textBox1.Text;
            Properties.Settings.Default.SideB = textBox2.Text;
            Properties.Settings.Default.SideC = textBox3.Text;
            Properties.Settings.Default.Save();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }

    public class Triangle
    {
        public static bool Exists(int a, int b, int c)
        {
            return a > 0 && b > 0 && c > 0 && (a + b > c) && (a + c > b) && (b + c > a);
        }

        public static bool IsRightTriangle(int a, int b, int c)
        {
            int[] sides = { a, b, c };
            Array.Sort(sides);
            return Math.Pow(sides[0], 2) + Math.Pow(sides[1], 2) == Math.Pow(sides[2], 2);
        }
    }
}

