using System.Windows;

namespace Launcher
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

       

        private void QueuButton_Click(object sender, RoutedEventArgs e)
        {
            var queue = new QueueGraphWindow();
            queue.Show();
        }

        private void StackButton_Click(object sender, RoutedEventArgs e)
        {
            var stack = new StackGraphWindow();
            stack.Show();
        }
    }
}