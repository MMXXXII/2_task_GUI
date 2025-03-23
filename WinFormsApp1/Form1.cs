
namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private string filePath = "sides.txt"; // Путь к файлу для сохранения данных

        // Конструктор формы
        public Form1()
        {
            InitializeComponent(); // Инициализация компонентов формы
            LoadSavedSides();      // Загрузка сохранённых сторон из файла при запуске
        }

        // Обработчик кнопки для проверки существования треугольника и его прямоугольности
        private void button1_Click(object sender, EventArgs e)
        {
            int a, b, c;

            // Попытка получить значения сторон
            if (int.TryParse(textBox1.Text, out a) && int.TryParse(textBox2.Text, out b) && int.TryParse(textBox3.Text, out c))
            {
                // Проверка, что все стороны положительные
                if (a > 0 && b > 0 && c > 0)
                {
                    // Проверка существования треугольника
                    if (a + b > c && a + c > b && b + c > a)
                    {
                        // Проверка прямоугольности
                        if (Math.Pow(a, 2) + Math.Pow(b, 2) == Math.Pow(c, 2) || Math.Pow(a, 2) + Math.Pow(c, 2) == Math.Pow(b, 2) || Math.Pow(b, 2) + Math.Pow(c, 2) == Math.Pow(a, 2))

                        {
                            MessageBox.Show("Треугольник существует и является прямоугольным.", "Результат проверки");
                        }
                        else
                        {
                            MessageBox.Show("Треугольник существует, но не является прямоугольным.", "Результат проверки");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Треугольник не существует.", "Результат проверки");
                    }
                }
                else
                {
                    MessageBox.Show("Стороны должны быть положительными числами.", "Ошибка ввода");
                }
            }
            else
            {
                MessageBox.Show("Введите корректные целые числа для всех сторон.", "Ошибка ввода");
            }
        }

        // Метод для проверки свойств треугольника (существование и прямоугольность)
        private bool TryGetSides(out int a, out int b, out int c)
        {
            a = b = c = 0; // Инициализация сторон нулями

            // Попытка преобразования текстовых полей в числа
            bool validInput = int.TryParse(textBox1.Text, out a) &&
                              int.TryParse(textBox2.Text, out b) &&
                              int.TryParse(textBox3.Text, out c) &&
                              a > 0 && b > 0 && c > 0;

            // Вывод сообщения об ошибке при некорректных данных
            if (!validInput)
            {
                MessageBox.Show("Введите корректные положительные целые числа", "Ошибка ввода");
            }
            return validInput;
        }

        // Метод для загрузки сохранённых данных о сторонах из файла
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

        // Обработчик события закрытия формы
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Сохранение данных о сторонах в файл
            File.WriteAllLines(filePath, new string[] { textBox1.Text, textBox2.Text, textBox3.Text });
        }
    }

    // Класс для работы с треугольниками
    public class Triangle
    {
        // Проверка существования треугольника по неравенству треугольника
        public static bool Exists(int a, int b, int c) =>
            a > 0 && b > 0 && c > 0 && a + b > c && a + c > b && b + c > a;

        // Проверка, является ли треугольник прямоугольным (по теореме Пифагора)
        public static bool IsRightTriangle(int a, int b, int c)
        {
            int[] sides = { a, b, c };
            return sides[0] * sides[0] + sides[1] * sides[1] == sides[2] * sides[2];
        }
    }
}
