using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace CallCenter
{
    public class SupportService : INotifyPropertyChanged
    {
        private ObservableCollection<Operator> _operators;
        public ObservableCollection<Operator> Operators
        {
            get { return _operators; }
            set
            {
                _operators = value;
                OnPropertyChanged("Operators");
            }
        }

        private ObservableCollection<Client> _clients;
        public ObservableCollection<Client> Clients
        {
            get { return _clients; }
            set
            {
                _clients = value;
                OnPropertyChanged("Clients");
            }
        }

        private readonly List<Connection> _connections = new List<Connection>();
        private readonly List<Waiting> _notServicedClients = new List<Waiting>();

        public void RemoveConnection(Connection connection)
        {
            _connections.Remove(connection);
        }

        public void RemoveWaiting(Waiting waiting)
        {
            _notServicedClients.Remove(waiting);
        }

        public void AddConnection(Connection connection)
        {
            _connections.Add(connection);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void MakeCall(Client client)
        {
            var freeOperator = Operators.FirstOrDefault(o => o.Status);//лінкю повертає першого оператора в якого Status = true

            if (freeOperator != null)
            {
                _connections.Add(new Connection(freeOperator, client, this));
            }
            else
            {
                _notServicedClients.Add(new Waiting(client, this));
            }
        }

        public void Terminate(Client client)
        {
            var connection = _connections.FirstOrDefault(c => c.CheckConnection(client)); //delegate<T> (T c) { c.CheckConnection } - псевдокод

            if (connection != null)
            {
                connection.Disconnect();
            }
        }

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
