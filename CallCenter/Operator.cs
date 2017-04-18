using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Windows.Threading;

namespace CallCenter
{
    public class Operator : INotifyPropertyChanged
    {
        public int OperatorId { get; set; }
        public string Name { get; set; }
        public Sex OperatorSex { get; set; }

        private int? _currentClientId;
        public int? CurrentClientId
        {
            get { return _currentClientId; }
            set
            {
                _currentClientId = value;
                OnPropertyChanged("CurrentClientId");
            }
        }

        private TimeSpan _sessionTime;
        public TimeSpan SessionTime
        {
            get { return _sessionTime; }
            set
            {
                _sessionTime = value;
                OnPropertyChanged("SessionTime");
            }
        }

        private bool _status;
        public bool Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }

        private int _salary;
        public int Salary
        {
            get { return _salary; }
            set
            {
                _salary = value;
                OnPropertyChanged("Salary");
            }
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
