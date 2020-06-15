using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using CompanyEmployee.Model;

namespace CompanyEmployee
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Employee> _employeeData = new ObservableCollection<Employee>();
        public ObservableCollection<Employee> EmployeeData => _employeeData;
    
        public MainWindow()
        {
            InitializeComponent();
            FillLists();
        }
        void FillLists()
        {
            _employeeData.Add(new Employee("Василий", "Лазарев", 22, 3000, "Маркетинг"));
            _employeeData.Add(new Employee("Петр", "Жуков", 25, 6000, "Сбыт"));
            _employeeData.Add(new Employee("Николай", "Орехов", 38, 8000, "Инновации"));
            _employeeData.Add(new Employee("Анастасия", "Журавлева", 21, 3000, "Производство"));
            _employeeData.Add(new Employee("Дмитрий", "Панов", 19, 2500, "Закупки"));
            _employeeData.Add(new Employee("Алексей", "Зуев", 30, 7000, "Финансы"));
            _employeeData.Add(new Employee("Екатерина","Боброва", 29, 6500, "Связи с общественностью"));
            _employeeData.Add(new Employee("Мария", "Лапина", 32, 7500, "Кадры"));
            _employeeData.Add(new Employee("Степан", "Рыбаков", 40, 10000, "Администрация"));

            //lbEmployee.ItemsSource = _employeeData;
            //listView.ItemsSource = _employeeData;
        }

        private void lbEmployee_Selected(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(e.Source.ToString());
        }

        private void lbEmployee_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            MessageBox.Show(e.AddedItems[0].ToString());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _employeeData.Add(new Employee("Сергей", "Кудрявцев", 26, 7000, "Маркетинг"));
        }
        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            MessageBox.Show(selectedItem.Content?.ToString());
        }
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            ChildWindow childWindow = new ChildWindow();
            // Теперь MainWindow главное окно для childWindow
            childWindow.Owner = this;
            childWindow.ViewModel = "ViewModel";
            childWindow.Show();
            childWindow.ShowViewModel();
            foreach (Window window in this.OwnedWindows)
            {
                window.Background = new SolidColorBrush(Colors.Aquamarine);
                if (window is ChildWindow)
                    window.Title = "Новый заголовок";

            }

        }

       


    }
}

