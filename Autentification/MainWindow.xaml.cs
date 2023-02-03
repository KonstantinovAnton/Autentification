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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Autentification
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private int time = 15;
        private DispatcherTimer timer;



        public MainWindow()
        {
            InitializeComponent();
            textBoxLogin.Text = Global.login;
            checkInitialize();

            
        }



        string login = "admin";      
        string password = "admin";

        private void btnAuto_Click(object sender, RoutedEventArgs e)
        {
            if(textBoxLogin.Text == "")
            {
                MessageBox.Show("Введите логин", "Аутентификация",
                  MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (passBox.Password == "")
            {
                MessageBox.Show("Введите пароль", "Аутентификация",
                  MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (textBoxLogin.Text == login && passBox.Password == password)
            {
                WindowForCode windowForCode = new WindowForCode();

                windowForCode.Show();
                this.Close();

                Random rnd = new Random();
                int randomeCode = rnd.Next(10000, 99999);

                Global.randomCode = randomeCode;
                Global.login = textBoxLogin.Text;
                    
                MessageBox.Show("Код: " + randomeCode, "Аутентификация",
                    MessageBoxButton.OK, MessageBoxImage.Information); 
            }
            else
            {
                MessageBox.Show("Таких нет!", "Ошибка авторизации",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void checkInitialize()
        {
            if(Global.failedAttempt == 1)
            {
                btnAuto.Visibility = Visibility.Hidden;
                timerStart();
            }
            else if(Global.failedAttempt == 2)
            {

            }

        }

        private void timerStart()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            if (time == 0)
            {
                timer.Stop();
                textBoxAccessIn.Text = "";
                btnAuto.Visibility = Visibility.Visible;
           
            }
            else
            {            
                textBoxAccessIn.Text = "Получить новый код можно будет через " + time + " секунд";
                time--;
            }
        }


        // CAPCHA

        //https://www.youtube.com/watch?v=rAnAud2sCgc

    }
}
