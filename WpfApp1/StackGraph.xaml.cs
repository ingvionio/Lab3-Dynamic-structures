using Lab3_dynamic_structures;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Wpf;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Launcher
{
    /// <summary>
    /// Логика взаимодействия для StackGraph.xaml
    /// </summary>
    public partial class StackGraphWindow : Window
    {
        public StackGraphWindow()
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
                Title = "Мой стек", // Подпись серии
                Color = OxyColors.Blue // Цвет линии
            };
            var myStack = new MyStack<string>();
            var times1 = FileHandler.HandleDirectory("C:\\Users\\ingvion\\Downloads\\inputs", myStack);

            for (int i = 0; i < maxFile; i++)
            {
                series1.Points.Add(new DataPoint(i, times1[i].TotalMilliseconds));
            }

            // Вторая очередь и серия данных
            LineSeries series2 = new LineSeries
            {
                Title = "С# стек", // Подпись серии
                Color = OxyColors.Red // Цвет линии
            };
            var Stack = new StackAdapter<string>();
            var times2 = FileHandler.HandleDirectory("C:\\Users\\ingvion\\Downloads\\inputs", Stack);

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

