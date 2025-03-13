using System;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Восстановление данных при открытии
            textBox1.Text = Properties.Settings.Default.SideA;
            textBox2.Text = Properties.Settings.Default.SideB;
            textBox3.Text = Properties.Settings.Default.SideC;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckTriangle((a, b, c) => Triangle.Exists(a, b, c), "существует");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (TryGetSides(out int a, out int b, out int c) && Triangle.Exists(a, b, c))
            {
                MessageBox.Show(Triangle.IsRightTriangle(a, b, c) ? "Треугольник является прямоугольным" : "Треугольник не является прямоугольным", "Проверка на прямоугольность");
            }
            else
            {
                MessageBox.Show("Треугольник не существует, проверка на прямоугольность невозможна", "Ошибка");
            }
        }

        private void CheckTriangle(Func<int, int, int, bool> triangleCheck, string checkType)
        {
            if (TryGetSides(out int a, out int b, out int c))
            {
                string message = triangleCheck(a, b, c)
                    ? $"Треугольник со сторонами {a}, {b}, {c} {checkType}"
                    : $"Треугольник со сторонами {a}, {b}, {c} не {checkType}";
                MessageBox.Show(message, $"Проверка {checkType}");
            }
        }

        private bool TryGetSides(out int a, out int b, out int c)
        {
            a = b = c = 0;
            bool isValid = int.TryParse(textBox1.Text, out a) && int.TryParse(textBox2.Text, out b) && int.TryParse(textBox3.Text, out c);
            if (!isValid || a <= 0 || b <= 0 || c <= 0)
            {
                MessageBox.Show("Введите корректные положительные целые числа", "Ошибка ввода");
                return false;
            }
            return true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Сохранение данных при закрытии формы
            Properties.Settings.Default.SideA = textBox1.Text;
            Properties.Settings.Default.SideB = textBox2.Text;
            Properties.Settings.Default.SideC = textBox3.Text;
            Properties.Settings.Default.Save();
        }
    }

    public class Triangle
    {
        public static bool Exists(int a, int b, int c) =>
            a > 0 && b > 0 && c > 0 && (a + b > c) && (a + c > b) && (b + c > a);

        public static bool IsRightTriangle(int a, int b, int c)
        {
            int[] sides = { a, b, c };
            Array.Sort(sides);
            return Math.Pow(sides[0], 2) + Math.Pow(sides[1], 2) == Math.Pow(sides[2], 2);
        }
    }
}
