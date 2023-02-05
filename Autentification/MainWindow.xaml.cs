using System;
using System.Collections.Generic;
using System.IO;
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

        private int time = 60;
        private DispatcherTimer timer;



        public MainWindow()
        {
            InitializeComponent();

            textBoxCaptcha.Visibility = Visibility.Collapsed;
            captchaImage.Visibility = Visibility.Collapsed;
            btnLoadCaptcha.Visibility = Visibility.Collapsed;
            spCaptcha.Visibility = Visibility.Collapsed;

            textBoxLogin.Text = Global.login;
            checkInitialize();
        }

        string login = "admin";
        string password = "admin";

        private void btnAuto_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxLogin.Text == "")
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
            if (!(textBoxLogin.Text == login && passBox.Password == password))
            {
                MessageBox.Show("Таких нет!", "Ошибка авторизации",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (Global.failedAttempt < 2)
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
                if (textBoxCaptcha.Text == Global.captchaText)
                {
                    MessageBox.Show("Добро пожаловать ", "Аутентификация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else {
                    if (Global.failedAttempt == 2)
                    {
                        MessageBox.Show("Неверный текст CAPTCHA, у Вас осталась одна попытка. Вводите заглавными буквами ", "Аутентификация",
                      MessageBoxButton.OK, MessageBoxImage.Information);
                        Global.failedAttempt++;
                        loadCaptcha();
                    }
                    else if (Global.failedAttempt == 3)
                    {
                        MessageBox.Show("Неверный текст CAPTCHA, попыток не осталось ", "Аутентификация",
                     MessageBoxButton.OK, MessageBoxImage.Information);

                        btnAuto.Visibility = Visibility.Collapsed;
                    }
                }
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
                textBoxCaptcha.Visibility = Visibility.Visible;
                captchaImage.Visibility = Visibility.Visible;
                btnLoadCaptcha.Visibility = Visibility.Visible;
                spCaptcha.Visibility = Visibility.Visible;
                loadCaptcha();
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

        public void loadCaptcha()
        {
            int width = 230;
            int height = 80;
            var captchaCode = Captcha.generateCaptcha();
            var result = Captcha.GetCaptchaImage(width, height, captchaCode);

            Stream stream = new MemoryStream(result.captchaByteCode);

            captchaImage.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);

        }

        private void btnLoadCaptcha_Click(object sender, RoutedEventArgs e)
        {
            loadCaptcha();
        }


        // CAPCHA

        //https://www.youtube.com/watch?v=rAnAud2sCgc

    }
}
