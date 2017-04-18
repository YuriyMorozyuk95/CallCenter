using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CallCenter
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private SupportService _support = new SupportService();
        public SupportService Support
        {
            get { return _support; }
            set
            {
                _support = value;
                OnPropertyChanged("Support");
            }
        }

        public Client SelectedClient { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            MakeCallButton.Click += MakeCallButtonOnClick;
            TerminateButton.Click += TerminateButtonOnClick;

            var operatorFactory = new OperatorFactory();
            Support.Operators = new ObservableCollection<Operator>(operatorFactory.GetOperators(5));

            var clientFactory = new ClientFactory();
            Support.Clients = new ObservableCollection<Client>(clientFactory.GetClients(10));
        }

        private void TerminateButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            if (SelectedClient != null && SelectedClient.CurrentOperatorId != null)
            {
                Support.Terminate(SelectedClient);
            }
        }

        private void MakeCallButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            if (SelectedClient == null || SelectedClient.CurrentOperatorId != null) return;

            Support.MakeCall(SelectedClient);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
