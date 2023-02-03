using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Autentification
{
    /// <summary>
    /// Логика взаимодействия для WindowForCode.xaml
    /// </summary>
    public partial class WindowForCode : Window
    {

        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public WindowForCode()
        {
            InitializeComponent();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
            dispatcherTimer.Tick += new EventHandler(DisTimer_Tick);
            runTimer();   
        }

       
        private void runTimer()
        {
            dispatcherTimer.Start();      
        }

    private void DisTimer_Tick(object sender, EventArgs e)
        {
            dispatcherTimer.Stop();

            try
            {
                int code = Convert.ToInt32(textBoxCode.Text);
                if (code == Global.randomCode)
                {
                    MessageBox.Show("Добро пожаловать ", "Аутентификация",
                   MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                    MessageBox.Show("Код не соответствует ", "Аутентификация",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {
                MessageBox.Show("Код не соответствует ", "Аутентификация",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Global.failedAttempt++;
            

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
