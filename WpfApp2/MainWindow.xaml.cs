using System;
using System.Windows;
using System.Windows.Controls;
using ExamCalculatorWPF.Logic;

namespace ExamCalculatorWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработчик кнопки расчета
        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!double.TryParse(Module1TextBox.Text, out double m1) ||
                    !double.TryParse(Module2TextBox.Text, out double m2) ||
                    !double.TryParse(Module3TextBox.Text, out double m3) ||
                    !double.TryParse(Module4TextBox.Text, out double m4) ||
                    !double.TryParse(Module5TextBox.Text, out double m5))
                {
                    MessageBox.Show(
                        "Введите корректные числа",
                        "Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);

                    return;
                }

                string level =
                    ((ComboBoxItem)LevelComboBox.SelectedItem)
                    .Content
                    .ToString();

                int total = ExamCalculator.CalculateTotal(
                    level,
                    m1,
                    m2,
                    m3,
                    m4,
                    m5);

                int max = ExamCalculator.GetMaxScore(level);

                double percent = ExamCalculator.CalculatePercent(total, max);

                int grade = ExamCalculator.GetGrade(percent);

                ResultTextBlock.Text =
                    $"Сумма баллов: {total}\n" +
                    $"Процент: {percent:F2}%\n" +
                    $"Оценка: {grade}";
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Неизвестная ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}