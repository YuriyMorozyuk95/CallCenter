using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CallCenter
{
    public enum ClientStatus : byte
    {
        None, Wating, Connected, Missed
    }

    public class Client : INotifyPropertyChanged
    {
        public int ClientId { get; set; }
        public string Name { get; set; }

        private int? _currentOperatorId;

        public int? CurrentOperatorId
        {
            get { return _currentOperatorId; }
            set
            {
                _currentOperatorId = value;
                OnPropertyChanged("CurrentOperatorId");
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

        private ClientStatus _status;
        public ClientStatus Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
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
