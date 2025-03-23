
namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private string filePath = "sides.txt"; // ���� � ����� ��� ���������� ������

        // ����������� �����
        public Form1()
        {
            InitializeComponent(); // ������������� ����������� �����
            LoadSavedSides();      // �������� ���������� ������ �� ����� ��� �������
        }

        // ���������� ������ ��� �������� ������������� ������������ � ��� ���������������
        private void button1_Click(object sender, EventArgs e)
        {
            int a, b, c;

            // ������� �������� �������� ������
            if (int.TryParse(textBox1.Text, out a) && int.TryParse(textBox2.Text, out b) && int.TryParse(textBox3.Text, out c))
            {
                // ��������, ��� ��� ������� �������������
                if (a > 0 && b > 0 && c > 0)
                {
                    // �������� ������������� ������������
                    if (a + b > c && a + c > b && b + c > a)
                    {
                        // �������� ���������������
                        if (Math.Pow(a, 2) + Math.Pow(b, 2) == Math.Pow(c, 2) || Math.Pow(a, 2) + Math.Pow(c, 2) == Math.Pow(b, 2) || Math.Pow(b, 2) + Math.Pow(c, 2) == Math.Pow(a, 2))

                        {
                            MessageBox.Show("����������� ���������� � �������� �������������.", "��������� ��������");
                        }
                        else
                        {
                            MessageBox.Show("����������� ����������, �� �� �������� �������������.", "��������� ��������");
                        }
                    }
                    else
                    {
                        MessageBox.Show("����������� �� ����������.", "��������� ��������");
                    }
                }
                else
                {
                    MessageBox.Show("������� ������ ���� �������������� �������.", "������ �����");
                }
            }
            else
            {
                MessageBox.Show("������� ���������� ����� ����� ��� ���� ������.", "������ �����");
            }
        }

        // ����� ��� �������� ������� ������������ (������������� � ���������������)
        private bool TryGetSides(out int a, out int b, out int c)
        {
            a = b = c = 0; // ������������� ������ ������

            // ������� �������������� ��������� ����� � �����
            bool validInput = int.TryParse(textBox1.Text, out a) &&
                              int.TryParse(textBox2.Text, out b) &&
                              int.TryParse(textBox3.Text, out c) &&
                              a > 0 && b > 0 && c > 0;

            // ����� ��������� �� ������ ��� ������������ ������
            if (!validInput)
            {
                MessageBox.Show("������� ���������� ������������� ����� �����", "������ �����");
            }
            return validInput;
        }

        // ����� ��� �������� ���������� ������ � �������� �� �����
        private void LoadSavedSides()
        {
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                if (lines.Length >= 3)
                {
                    textBox1.Text = lines[0];
                    textBox2.Text = lines[1];
                    textBox3.Text = lines[2];
                }
            }
        }

        // ���������� ������� �������� �����
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // ���������� ������ � �������� � ����
            File.WriteAllLines(filePath, new string[] { textBox1.Text, textBox2.Text, textBox3.Text });
        }
    }

    // ����� ��� ������ � ��������������
    public class Triangle
    {
        // �������� ������������� ������������ �� ����������� ������������
        public static bool Exists(int a, int b, int c) =>
            a > 0 && b > 0 && c > 0 && a + b > c && a + c > b && b + c > a;

        // ��������, �������� �� ����������� ������������� (�� ������� ��������)
        public static bool IsRightTriangle(int a, int b, int c)
        {
            int[] sides = { a, b, c };
            return sides[0] * sides[0] + sides[1] * sides[1] == sides[2] * sides[2];
        }
    }
}
