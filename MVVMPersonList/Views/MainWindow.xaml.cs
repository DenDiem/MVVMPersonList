using System.Windows;
using WpfSimpleMVVM.ViewModels;

namespace WpfSimpleMVVM
{
    //Можна змінити ім'я та фамільне ім'я, а також емайл(перевіряється на валідність), фільтр по всім полям(Contains),сериалізація вид JSON, видалення виділеного об'єкту

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

    }
}
