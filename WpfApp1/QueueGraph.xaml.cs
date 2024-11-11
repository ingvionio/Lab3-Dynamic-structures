using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Lab3_dynamic_structures;

namespace Launcher
{
    public partial class QueueGraphWindow : Window
    {
        public QueueGraphWindow()
        {
            InitializeComponent();
            DataContext = this;

            // Создаем пустой график с заголовком и осями
            PlotModel model = new PlotModel { Title = "Зависимость времени от числа операций в файле" };
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Число файлов" });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Время (мс)" });
            PlotView.Model = model;
        }

        private void MeasurementsButton_Click(object sender, RoutedEventArgs e)
        {
            MeasurementsButton.IsEnabled = false;

            // Получаем максимальное количество файлов из TextBox
            if (!int.TryParse(MaxDisksTextBox.Text, out int maxFile) || maxFile <= 0)
            {
                MessageBox.Show("Введите корректное максимальное количество файлов (положительное число).");
                MeasurementsButton.IsEnabled = true;
                return;
            }

            // Очищаем предыдущие серии данных
            PlotView.Model.Series.Clear();

            // Первая очередь и серия данных
            LineSeries series1 = new LineSeries
            {
                Title = "Моя очередь", // Подпись серии
                Color = OxyColors.Blue // Цвет линии
            };
            var myQueu = new MyQueue<string>();
            var times1 = FileHandler.HandleDirectory("C:\\Users\\ingvion\\Downloads\\inputs", myQueu);

            for (int i = 0; i < maxFile; i++)
            {
                series1.Points.Add(new DataPoint(i, times1[i].TotalMilliseconds));
            }

            // Вторая очередь и серия данных
            LineSeries series2 = new LineSeries
            {
                Title = "С# очередь", // Подпись серии
                Color = OxyColors.Red // Цвет линии
            };
            var queue = new QueueAdapter<string>();
            var times2 = FileHandler.HandleDirectory("C:\\Users\\ingvion\\Downloads\\inputs", queue);

            for (int i = 0; i < maxFile; i++)
            {
                series2.Points.Add(new DataPoint(i, times2[i].TotalMilliseconds));
            }

            // Добавляем серии на график
            PlotView.Model.Series.Add(series1);
            PlotView.Model.Series.Add(series2);

            // Обновляем график
            PlotView.InvalidatePlot(true);

            // Разблокируем кнопку после завершения измерений
            MeasurementsButton.IsEnabled = true;
        }
    }
}
