using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CalculatorWindow : Window
    {
        public CalculatorWindow()
        {
            InitializeComponent();
        }

        // Переменные для отслеживания нажатых клавиш
        private bool isZPressed = false;

        //
        //Функция вычисления 10,2,8,16 систем счисления
        //
        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            CreateHistoryFile();
            string TenthSys = TenthNumSysBox.Text;
            string SecondSys = SecondNumSysBox.Text;
            string EightSys = EightNumSysBox.Text;
            string Sixteenth = SixteenthNumSysBox.Text;

            //Открытие файла history.txt для записи истории
            using (StreamWriter writer = new StreamWriter("history.txt", true))
            {
                //Перевод из десятичной системы
                if (long.TryParse(TenthSys, out long TenthNum))
                {
                    // Если введено число в десятичной системе, то переводим его в другие системы
                    SecondNumSysBox.Text = Convert.ToString(TenthNum, 2); // Двоичная система
                    EightNumSysBox.Text = Convert.ToString(TenthNum, 8);  // Восьмеричная система
                    SixteenthNumSysBox.Text = Convert.ToString(TenthNum, 16).ToUpper(); // Шестнадцатеричная система
                }

                //Перевод из двоичной системы
                if (!string.IsNullOrEmpty(SecondSys) && IsBinary(SecondSys))
                {
                    try
                    {
                        long SecondNum = Convert.ToInt64(SecondSys, 2); // Преобразуем в long
                                                                        // Если введено число в двоичной системе, то переводим его в другие системы
                        TenthNumSysBox.Text = SecondNum.ToString(); // Десятичная система
                        EightNumSysBox.Text = Convert.ToString(SecondNum, 8); // Восьмеричная система
                        SixteenthNumSysBox.Text = Convert.ToString(SecondNum, 16).ToUpper(); // Шестнадцатеричная система
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Ошибка: Некорректное значение для двоичной системы.");
                    }
                }

                //Перевод из восьмеричной системы
                if (!string.IsNullOrEmpty(EightSys) && IsOctal(EightSys))
                {
                    try
                    {
                        long EightNum = Convert.ToInt64(EightSys, 8); // Преобразуем в long
                                                                      // Если введено число в восьмеричной системе, то переводим его в другие системы
                        TenthNumSysBox.Text = EightNum.ToString(); // Десятичная система
                        SecondNumSysBox.Text = Convert.ToString(EightNum, 2); // Двоичная система
                        SixteenthNumSysBox.Text = Convert.ToString(EightNum, 16).ToUpper(); // Шестнадцатеричная система
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Ошибка: Некорректное значение для восьмеричной системы.");
                    }
                }

                //Перевод из шестнадцатеричной системы
                if (long.TryParse(Sixteenth, System.Globalization.NumberStyles.HexNumber, null, out long SixteenthNum))
                {
                    // Если введено число в шестнадцатеричной системе, то переводим его в другие системы
                    TenthNumSysBox.Text = SixteenthNum.ToString(); // Десятичная система
                    SecondNumSysBox.Text = Convert.ToString(SixteenthNum, 2); // Двоичная система
                    EightNumSysBox.Text = Convert.ToString(SixteenthNum, 8); // Восьмеричная система
                }
                else
                {
                    Console.WriteLine("Ошибка перевода");
                }
                //Запись в history.txt
                writer.WriteLine($"Десятичное число: {TenthNumSysBox.Text}");
                writer.WriteLine($"Двоичное число: {SecondNumSysBox.Text}");
                writer.WriteLine($"Восьмеричное число: {EightNumSysBox.Text}");
                writer.WriteLine($"Шестнадцатеричное число: {SixteenthNumSysBox.Text}");
                writer.WriteLine("---------------------------------------");

                Console.WriteLine($"Десятичное число: {TenthNumSysBox.Text}");
                Console.WriteLine($"Двоичное число: {SecondNumSysBox.Text}");
                Console.WriteLine($"Восьмеричное число: {EightNumSysBox.Text}");
                Console.WriteLine($"Шестнадцатеричное число: {SixteenthNumSysBox.Text}");
                Console.WriteLine("---------------------------------------");
            }
        }

        //Метод для проверки, является ли строка двоичным числом
        private bool IsBinary(string value)
        {
            foreach (char c in value)
            {
                if (c != '0' && c != '1') return false;
            }
            return true;
        }

        //Метод для проверки, является ли строка восьмеричным числом
        private bool IsOctal(string value)
        {
            foreach (char c in value)
            {
                if (c < '0' || c > '7') return false;
            }
            return true;
        }


        //Файл хранящий в себе историю вычислений с датой

        private void CreateHistoryFile()
        {
            string filePath = "history.txt";

            Console.WriteLine("Current Working Directory: " + Directory.GetCurrentDirectory());

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
            else
            {
                using (StreamWriter Datatime = new StreamWriter(filePath, append: true))
                {
                    Datatime.WriteLine($"     {DateTime.Now}");
                }
                
            }
        }



        //
        //Вспомогатели ввода и кнопки
        //
        //Кнопка закрытия
        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //Открытие истории(скрытие текущего окна и отображение Истрории)
        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            HistoryWindow historyWindow = new();
            historyWindow.Show();
            Console.WriteLine("История открыта");
            Console.WriteLine("---------------------------------------");
        }

        //Отслеживание события закрытия окна, если было закрыто то приложение ложится
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

       //Ввод только чисел и backspace для 10 системы счисления
        private void TenthNumSysBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Разрешаем клавишу Backspace
            if (e.Key == Key.Back) { return; } // Разрешаем удаление символов

            if (!char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)))
            {
                // Блокируем все остальные клавиши если это не числа или backspace
                e.Handled = true;
            }
        }

        //Ввод только 0,1, backspace для 2 системы счисления
        private void SecondNumSysBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Разрешаем клавишу Backspace
            if (e.Key == Key.Back) { return; } // Разрешаем удаление символов

            //Проверяем, является ли нажата клавиша  "0" "1" на основной клавиатуре или NumPad
            if (e.Key == Key.D0 || e.Key == Key.NumPad0 || e.Key == Key.D1 || e.Key == Key.NumPad1)
            {
                return; //Разрешаем ввод символов "0" "1"
            }

            // Блокируем все остальные клавиши
            e.Handled = true;
        }

        private void EightNumSysBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Разрешаем клавишу Backspace
            if (e.Key == Key.Back) { return; } // Разрешаем удаление символов

            //Проверяем, является ли нажата клавиша от "0" до "7" на основной клавиатуре
            if (e.Key >= Key.D0 && e.Key <= Key.D7)
            {
                return; //Разрешаем ввод символов "0" до "7"
            }

            //Проверяем, является ли нажата клавиша от "0" до "7" на NumPad
            if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad7)
            {
                return; //Разрешаем ввод символов "0" до "7" на NumPad
            }

            //Блокируем все остальные клавиши
            e.Handled = true;
        }

        private void SixteenthNumSysBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Разрешаем клавишу Backspace
            if (e.Key == Key.Back) { return; } // Разрешаем удаление символов

            //Проверяем, является ли нажата клавиша от "0" до "9" на основной клавиатуре
            if (e.Key >= Key.D0 && e.Key < Key.D9)
            {
                return; //Разрешаем ввод символов "0" до "9"
            }

            //Проверяем, является ли нажата клавиша от "0" до "9" на NumPad
            if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad8)
            {
                return; //Разрешаем ввод символов "0" до "9" на NumPad
            }

            //Проверяем, является ли нажата клавиша от "A" до "F"
            if ( e.Key >= Key.A && e.Key <= Key.F)
            {
                return; //Разрешаем ввод символов "A" до "F"
            }

            //Блокируем все остальные клавиши
            e.Handled = true;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // Проверяем, какие клавиши нажаты
            if (e.Key == System.Windows.Input.Key.Z)
            {
                isZPressed = true;
            }

            if (isZPressed)
            {
                OpenConsole();
            }
        }
        private void OpenConsole()
        {
            // Открытие консоли вручную
            AllocConsole();
            Console.WriteLine("Консоль открыта!");

            // Сбрасываем флаги, чтобы не открывалась консоль снова при повторном нажатии этих клавиш
            isZPressed = false;
        }

        // Импортируем функцию для открытия консоли
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            TenthNumSysBox.Text = "";
            SecondNumSysBox.Text = "";
            EightNumSysBox.Text = "";
            SixteenthNumSysBox.Text = "";
            Console.WriteLine("Сброс");
            Console.WriteLine("---------------------------------------");
        }
    }
}