using System;
using System.Windows;
using System.Windows.Media;

namespace TimeApp
{
    public partial class MainWindow : Window
    {
        private Time _time1;
        private Time _time2;
        private Time _tempTime;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddResult(string message, bool isError = false)
        {
            txtResult.Text += message + Environment.NewLine;
            txtResult.ScrollToEnd();

            if (isError)
            {
                lblStatus.Foreground = Brushes.Red;
                lblStatus.Text = "Ошибка: " + message; 
            }
            else
            {
                lblStatus.Foreground = Brushes.Green;
                lblStatus.Text = "Успешно выполнен";   
            }
        }

        private void ClearStatus()
        {
            lblStatus.Foreground = Brushes.Green;
            lblStatus.Text = "Готов";                   
        }

        private bool ValidateHoursMinutes(string hoursStr, string minutesStr, out byte hours, out byte minutes)
        {
            hours = 0;
            minutes = 0;

            if (string.IsNullOrWhiteSpace(hoursStr))
            {
                AddResult("Ошибка: Введите часы!", true);
                return false;
            }

            if (string.IsNullOrWhiteSpace(minutesStr))
            {
                AddResult("Ошибка: Введите минуты!", true);
                return false;
            }

            if (!byte.TryParse(hoursStr, out hours))
            {
                AddResult("Ошибка: Часы должны быть целым числом!", true);
                return false;
            }

            if (!byte.TryParse(minutesStr, out minutes))
            {
                AddResult("Ошибка: Минуты должны быть целым числом!", true);
                return false;
            }

            if (hours > 23)
            {
                AddResult("Ошибка: Часы должны быть в диапазоне 0-23!", true);
                return false;
            }

            if (minutes > 59)
            {
                AddResult("Ошибка: Минуты должны быть в диапазоне 0-59!", true);
                return false;
            }

            return true;
        }

        private void BtnCreate1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateHoursMinutes(txtHours1.Text, txtMinutes1.Text, out byte hours, out byte minutes))
                {
                    _time1 = new Time(hours, minutes);
                    lblTime1.Content = _time1.ToString();
                    AddResult($"Создано время №1: {_time1}");
                    ClearStatus();
                }
            }
            catch (Exception ex)
            {
                AddResult($"Ошибка при создании времени №1: {ex.Message}", true);
            }
        }

        private void BtnCreate2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateHoursMinutes(txtHours2.Text, txtMinutes2.Text, out byte hours, out byte minutes))
                {
                    _time2 = new Time(hours, minutes);
                    lblTime2.Content = _time2.ToString();
                    AddResult($"Создано время №2: {_time2}");
                    ClearStatus();
                }
            }
            catch (Exception ex)
            {
                AddResult($"Ошибка при создании времени №2: {ex.Message}", true);
            }
        }

        private void BtnCreateFromMinutes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMinutes.Text))
                {
                    AddResult("Ошибка: Введите количество минут!", true);
                    return;
                }

                if (!int.TryParse(txtMinutes.Text, out int minutes))
                {
                    AddResult("Ошибка: Минуты должны быть целым числом!", true);
                    return;
                }

                _tempTime = new Time(minutes);
                lblMinutesResult.Content = _tempTime.ToString();
                AddResult($"Создано время из {minutes} минут: {_tempTime}");
                ClearStatus();
            }
            catch (Exception ex)
            {
                AddResult($"Ошибка при создании времени из минут: {ex.Message}", true);
            }
        }

        private void BtnSubtract_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_time1 == null)
                {
                    AddResult("Ошибка: Время №1 не задано!", true);
                    return;
                }

                if (_time2 == null)
                {
                    AddResult("Ошибка: Время №2 не задано!", true);
                    return;
                }

                Time result = _time1 - _time2;
                AddResult($"Результат вычитания {_time1} - {_time2} = {result}");
                ClearStatus();
            }
            catch (Exception ex)
            {
                AddResult($"Ошибка при вычитании: {ex.Message}", true);
            }
        }

        private void BtnInc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_time1 == null)
                {
                    AddResult("Ошибка: Время №1 не задано!", true);
                    return;
                }

                _time1++;
                lblTime1.Content = _time1.ToString();
                AddResult($"После инкремента: {_time1}");
                ClearStatus();
            }
            catch (Exception ex)
            {
                AddResult($"Ошибка при инкременте: {ex.Message}", true);
            }
        }

        private void BtnDec_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_time1 == null)
                {
                    AddResult("Ошибка: Время №1 не задано!", true);
                    return;
                }

                _time1--;
                lblTime1.Content = _time1.ToString();
                AddResult($"После декремента: {_time1}");
                ClearStatus();
            }
            catch (Exception ex)
            {
                AddResult($"Ошибка при декременте: {ex.Message}", true);
            }
        }

        private void BtnCompare_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_time1 == null || _time2 == null)
                {
                    AddResult("Ошибка: Оба времени должны быть заданы для сравнения!", true);
                    return;
                }

                AddResult($"Сравнение времен:");
                AddResult($"{_time1} < {_time2}: {(_time1 < _time2)}");
                AddResult($"{_time1} > {_time2}: {(_time1 > _time2)}");
                AddResult($"{_time1} == {_time2}: {_time1.Equals(_time2)}");
                ClearStatus();
            }
            catch (Exception ex)
            {
                AddResult($"Ошибка при сравнении: {ex.Message}", true);
            }
        }

        private void BtnToBool_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_time1 == null)
                {
                    AddResult("Ошибка: Время №1 не задано!", true);
                    return;
                }

                bool result = (bool)_time1;
                AddResult($"Приведение {_time1} к bool: {(result ? "true (не равно 00:00)" : "false (равно 00:00)")}");
                ClearStatus();
            }
            catch (Exception ex)
            {
                AddResult($"Ошибка при приведении к bool: {ex.Message}", true);
            }
        }

        private void BtnToInt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_time1 == null)
                {
                    AddResult("Ошибка: Время №1 не задано!", true);
                    return;
                }

                int minutes = _time1;
                AddResult($"Приведение {_time1} к int: {minutes} минут");
                ClearStatus();
            }
            catch (Exception ex)
            {
                AddResult($"Ошибка при приведении к int: {ex.Message}", true);
            }
        }

        private void BtnDefault_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Time defaultTime = new Time();
                AddResult($"Конструктор по умолчанию: {defaultTime}");
                ClearStatus();
            }
            catch (Exception ex)
            {
                AddResult($"Ошибка: {ex.Message}", true);
            }
        }

        private void BtnCopy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_time1 == null)
                {
                    AddResult("Ошибка: Время №1 не задано для копирования!", true);
                    return;
                }

                Time copyTime = new Time(_time1);
                AddResult($"Конструктор копирования: {copyTime} (копия {_time1})");
                ClearStatus();
            }
            catch (Exception ex)
            {
                AddResult($"Ошибка при копировании: {ex.Message}", true);
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            txtResult.Clear();
            ClearStatus();
            AddResult("Результаты очищены");
        }
    }
}