using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Calculator
{
    public partial class CalculatorWindow : Window
    {
        public CalculatorWindow()
        {
            InitializeComponent();
        }

        private void LoadHistory()
        {
            try
            {
                string historyFilePath = "history.txt";

                if (File.Exists(historyFilePath))
                {
                    // Читаем содержимое файла
                    string historyContent = File.ReadAllText(historyFilePath);

                    // Отображаем содержимое в TextBox
                    HistoryTextBox.Text = historyContent;
                }
                else
                {
                    HistoryTextBox.Text = "История пуста.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке истории: {ex.Message}");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadHistory();
        }

        // Функция для перевода числа между системами счисления
        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем входные значения
                string inputNumber = InputNumberBox.Text;
                int inputBase, outputBase;

                // Проверяем, является ли входное основание числом и в пределах от 2 до 36
                if (!int.TryParse(InputBaseBox.Text, out inputBase) || inputBase < 2)
                {
                    MessageBox.Show("Основание входной системы счисления должно быть числом больше или равно 2.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Проверяем, является ли выходное основание числом и в пределах от 2 до 36
                if (!int.TryParse(OutputBaseBox.Text, out outputBase) || outputBase < 2)
                {
                    MessageBox.Show("Основание выходной системы счисления должно быть числом больше или равно 2.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Проверка корректности введенного числа для исходной системы счисления
                if (!IsValidNumberInBase(inputNumber, inputBase))
                {
                    MessageBox.Show($"Число \"{inputNumber}\" не является допустимым в системе счисления с основанием {inputBase}.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Преобразуем число из входной системы в десятичную
                long decimalValue = ConvertToDecimal(inputNumber, inputBase);

                // Преобразуем число из десятичной системы в выходную систему счисления
                string result = ConvertFromDecimal(decimalValue, outputBase);

                // Выводим результат
                ResultBox.Text = result;

                // Записываем результат в файл истории
                CreateHistoryFile(inputNumber, inputBase, outputBase, result);
                LoadHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Проверка, является ли число допустимым для заданной системы счисления
        private bool IsValidNumberInBase(string number, int baseValue)
        {
            const string validChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string allowedChars = validChars.Substring(0, baseValue); // Формируем строку допустимых символов для данной системы счисления

            foreach (char c in number.ToUpper())
            {
                if (!allowedChars.Contains(c)) // Проверяем, что символ из числа входит в допустимые для этой системы
                {
                    return false;
                }
            }
            return true;
        }

        // Преобразование числа из произвольной системы счисления в десятичную
        private long ConvertToDecimal(string number, int fromBase)
        {
            number = number.ToUpper();
            long decimalValue = 0;
            int power = 0;

            // Читаем число справа налево
            for (int i = number.Length - 1; i >= 0; i--)
            {
                char digitChar = number[i];
                int digitValue = (digitChar >= '0' && digitChar <= '9') ? digitChar - '0' : digitChar - 'A' + 10;

                if (digitValue >= fromBase)
                {
                    throw new FormatException($"Символ '{digitChar}' не допустим в системе счисления с основанием {fromBase}.");
                }

                decimalValue += digitValue * (long)Math.Pow(fromBase, power);
                power++;
            }

            return decimalValue;
        }

        // Преобразование числа из десятичной системы в произвольную систему счисления
        private string ConvertFromDecimal(long decimalValue, int toBase)
        {
            if (decimalValue == 0) return "0";

            StringBuilder result = new StringBuilder();

            while (decimalValue > 0)
            {
                int remainder = (int)(decimalValue % toBase);
                result.Insert(0, (remainder < 10) ? (char)(remainder + '0') : (char)(remainder - 10 + 'A'));
                decimalValue /= toBase;
            }

            return result.ToString();
        }

        // Обработчик для сброса полей
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            InputNumberBox.Clear();
            InputBaseBox.Clear();
            OutputBaseBox.Clear();
            ResultBox.Clear();
            Console.WriteLine("Сброс");
        }

        // Создание файла истории
        private void CreateHistoryFile(string inputNumber, int inputBase, int outputBase, string result)
        {
            string filePath = "history.txt";

            // Проверяем, существует ли файл
            if (!File.Exists(filePath))
            {
                // Если файл не существует, создаем его и записываем заголовок
                using (StreamWriter writer = new StreamWriter(filePath, append: true))
                {
                    writer.WriteLine("---- История вычислений ----");
                    writer.WriteLine($"     {DateTime.Now}");
                }
            }

            // Записываем данные о вычислениях в файл
            using (StreamWriter writer = new StreamWriter(filePath, append: true))
            {
                writer.WriteLine($"Число: {inputNumber}, Основание ввода: {inputBase}, Основание вывода: {outputBase}, Результат: {result}");
            }
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Проверка, является ли символ русской буквой
            if (IsRussianLetter(e.Text))
            {
                // Отклоняем ввод русской буквы
                e.Handled = true;
            }
        }

        private bool IsRussianLetter(string input)
        {
            // Проверка, является ли символ русским
            return input.Any(c => (c >= 'А' && c <= 'Я') || (c >= 'а' && c <= 'я'));
        }

        // Обработчик для кнопки "Выйти"
        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Обработчик закрытия окна
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // Открытие консоли
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Z)
            {
                OpenConsole();
            }
        }

        // Открытие консоли
        private void OpenConsole()
        {
            AllocConsole();
            Console.WriteLine("Консоль открыта!");
        }

        // Импортируем функцию для открытия консоли
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();

        private void InputNumberBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }
    }
}
