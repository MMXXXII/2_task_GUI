namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        // Конструктор формы, инициализация компонентов
        public Form1()
        {
            InitializeComponent();
            // Восстановление данных при открытии формы из настроек
            textBox1.Text = Properties.Settings.Default.SideA;
            textBox2.Text = Properties.Settings.Default.SideB;
            textBox3.Text = Properties.Settings.Default.SideC;
        }

        // Обработчик кнопки для проверки существования треугольника
        private void button1_Click(object sender, EventArgs e)
        {
            // Вызов обобщённой функции проверки треугольника на существование
            CheckTriangle((a, b, c) => Triangle.Exists(a, b, c), "существует");
        }

        // Обработчик кнопки для проверки на прямоугольность
        private void button2_Click(object sender, EventArgs e)
        {
            // Попытка получить стороны треугольника и проверить существование
            if (TryGetSides(out int a, out int b, out int c) && Triangle.Exists(a, b, c))
            {
                // Если треугольник существует, проверяем его прямоугольность
                MessageBox.Show(Triangle.IsRightTriangle(a, b, c) ?
                    "Треугольник является прямоугольным" :
                    "Треугольник не является прямоугольным", "Проверка на прямоугольность");
            }
            else
            {
                // Если треугольник не существует, сообщаем, что проверка на прямоугольность невозможна
                MessageBox.Show("Треугольник не существует, проверка на прямоугольность невозможна", "Ошибка");
            }
        }

        // Обобщённая функция для проверки свойств треугольника
        // triangleCheck — это делегат, который определяет, какой тип проверки будет выполнен
        private void CheckTriangle(Func<int, int, int, bool> triangleCheck, string checkType)
        {
            // Получение сторон треугольника и проверка их корректности
            if (TryGetSides(out int a, out int b, out int c))
            {
                // Если проверка проходит, показываем сообщение с результатом
                string message = triangleCheck(a, b, c)
                    ? $"Треугольник со сторонами {a}, {b}, {c} {checkType}"
                    : $"Треугольник со сторонами {a}, {b}, {c} не {checkType}";
                MessageBox.Show(message, $"Проверка {checkType}");
            }
        }

        // Функция для получения сторон треугольника из текстовых полей
        private bool TryGetSides(out int a, out int b, out int c)
        {
            // Инициализация значений сторон
            a = b = c = 0;

            // Попытка преобразовать введённые строки в целые числа
            bool isValid = int.TryParse(textBox1.Text, out a) &&
                           int.TryParse(textBox2.Text, out b) &&
                           int.TryParse(textBox3.Text, out c);

            // Если хотя бы одно значение некорректно или меньше или равно нулю, выводим сообщение об ошибке
            if (!isValid || a <= 0 || b <= 0 || c <= 0)
            {
                MessageBox.Show("Введите корректные положительные целые числа", "Ошибка ввода");
                return false;
            }

            // Если данные корректны, возвращаем true
            return true;
        }

        // Обработчик события закрытия формы
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Сохранение данных о сторонах в настройки при закрытии формы
            Properties.Settings.Default.SideA = textBox1.Text;
            Properties.Settings.Default.SideB = textBox2.Text;
            Properties.Settings.Default.SideC = textBox3.Text;
            Properties.Settings.Default.Save();
        }
    }

    // Класс для работы с треугольниками
    public class Triangle
    {
        // Метод для проверки существования треугольника
        public static bool Exists(int a, int b, int c) =>
            a > 0 && b > 0 && c > 0 && (a + b > c) && (a + c > b) && (b + c > a);

        // Метод для проверки, является ли треугольник прямоугольным
        public static bool IsRightTriangle(int a, int b, int c)
        {
            int[] sides = { a, b, c };
            // Сортировка сторон для того, чтобы гипотенуза была последней стороной
            Array.Sort(sides);
            // Проверка теоремы Пифагора для прямоугольного треугольника
            return Math.Pow(sides[0], 2) + Math.Pow(sides[1], 2) == Math.Pow(sides[2], 2);
        }
    }
}
