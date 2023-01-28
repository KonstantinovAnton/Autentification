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

namespace Autentification
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        string login = "admin";
        string password = "admin";

        private void btnAuto_Click(object sender, RoutedEventArgs e)
        {
            if(textBoxLogin.Text == login && passBox.Password == password)
            {
                WindowForCode windowForCode = new WindowForCode();

                windowForCode.Show();


                Random rnd = new Random();
                int randomeCode = rnd.Next(10000, 99999);
                    
                MessageBox.Show("Код: " + randomeCode, "Аутентификация",
                    MessageBoxButton.OK, MessageBoxImage.Information); 
            }
            else
            {
                MessageBox.Show("Таких нет!", "Ошибка авторизации",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
