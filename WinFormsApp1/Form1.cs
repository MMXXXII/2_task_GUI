namespace WinFormsApp1
{
    // Основной класс формы
    public partial class Form1 : Form
    {
        // Конструктор формы
        public Form1()
        {
            InitializeComponent(); // Инициализация компонентов формы
            LoadSavedSides();      // Загрузка сохранённых сторон при запуске
        }

        // Обработчик кнопки для проверки существования треугольника
        private void button1_Click(object sender, EventArgs e) =>
            CheckTriangle(Triangle.Exists, "существует");

        // Обработчик кнопки для проверки на прямоугольность
        private void button2_Click(object sender, EventArgs e)
        {
            // Получение сторон и проверка существования треугольника
            if (TryGetSides(out int a, out int b, out int c) && Triangle.Exists(a, b, c))
            {
                // Сообщение о прямоугольности треугольника
                MessageBox.Show(Triangle.IsRightTriangle(a, b, c) ?
                    "Треугольник является прямоугольным" :
                    "Треугольник не является прямоугольным", "Проверка на прямоугольность");
            }
            else
            {
                // Сообщение об ошибке, если треугольник не существует
                MessageBox.Show("Треугольник не существует, проверка на прямоугольность невозможна", "Ошибка");
            }
        }

        // Метод для проверки свойств треугольника (существование или прямоугольность)
        private void CheckTriangle(Func<int, int, int, bool> check, string checkType)
        {
            // Проверяем корректность ввода и существование треугольника
            if (TryGetSides(out int a, out int b, out int c))
            {
                MessageBox.Show(check(a, b, c) ?
                    $"Треугольник со сторонами {a}, {b}, {c} {checkType}" :
                    $"Треугольник со сторонами {a}, {b}, {c} не {checkType}", $"Проверка {checkType}");
            }
        }

        // Метод для получения сторон треугольника из текстовых полей
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

        // Метод для загрузки сохранённых данных о сторонах треугольника
        private void LoadSavedSides()
        {
            textBox1.Text = Properties.Settings.Default.SideA;
            textBox2.Text = Properties.Settings.Default.SideB;
            textBox3.Text = Properties.Settings.Default.SideC;
        }

        // Обработчик события закрытия формы
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Сохранение данных о сторонах в настройки
            Properties.Settings.Default.SideA = textBox1.Text;
            Properties.Settings.Default.SideB = textBox2.Text;
            Properties.Settings.Default.SideC = textBox3.Text;
            Properties.Settings.Default.Save(); // Сохранение настроек
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
            Array.Sort(sides); // Сортировка для корректного определения гипотенузы
            return sides[0] * sides[0] + sides[1] * sides[1] == sides[2] * sides[2];
        }
    }
}