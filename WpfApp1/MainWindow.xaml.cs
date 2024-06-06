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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Коллекция для привязки к списку
        /// </summary>

        ObservableCollection<Notice> Notices;
        public MainWindow()
        {
            Notices = new ObservableCollection<Notice>();
            InitializeComponent();
            lBox.DataContext = Notices;
        }


        /// <summary>
        /// Заполнение коллекции данными
        /// </summary>
        void FillData()
        {
            Notices.Clear();
            foreach (var item in Notice.GetAllNotices())
            {
                Notices.Add(item);
            }
        }


        private void btnFill_Click(object sender, RoutedEventArgs e)
        {
            FillData();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var notice = new Notice()
            {
                Date = DateTime.Today.ToString(),
                AccountNumber = "159753",
                Client = "Test",
                Summa = (System.Data.SqlTypes.SqlMoney)1000.0                
            };
            notice.Insert();
            FillData();
        }




        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var notice = (Notice)lBox.SelectedItem;
            if(notice != null) { 
           // notice.Date = "0000-00-00";
           //notice.AccountNumber = "987";
            notice.Client = "Измененное имя";
           // notice.Summa = (System.Data.SqlTypes.SqlMoney)0.0;
            notice.Update();
            FillData();
            }
            /*
            var selectedPaymentSlip = (PaymentSlip)lBox.SelectedItem;
            if (selectedPaymentSlip != null)
            {
                selectedPaymentSlip.Client = "Измененный клиент";
                selectedPaymentSlip.Update();
                FillData();
            }
            */




        }


        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            var id = ((Notice)lBox.SelectedItem).NoticeId;
            Notice.Delete(id);
            FillData();
        }




















    }
}
