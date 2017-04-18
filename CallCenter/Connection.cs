using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace CallCenter
{
    public class Connection
    {
        private readonly Operator _operator;
        private readonly Client _client;
        private readonly SupportService _supportService;

        private readonly DispatcherTimer _sessionTimer;

        public Connection(Operator oper, Client client, SupportService supportService)
        {
            _operator = oper;
            _client = client;
            _supportService = supportService;

            _client.CurrentOperatorId = _operator.OperatorId;
            _operator.CurrentClientId = _client.ClientId;
            _operator.Status = false;
            _client.Status = ClientStatus.Connected;

            _sessionTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _sessionTimer.Tick += SessionTimerTick;
            _sessionTimer.Start();
        }

        private void SessionTimerTick(object sender, EventArgs e)
        {
            _operator.SessionTime = _operator.SessionTime.Add(TimeSpan.FromSeconds(1));
            _client.SessionTime = _operator.SessionTime;

            if (_operator.SessionTime >= TimeSpan.FromMinutes(5))
            {
                Disconnect();
            }
        }

        public void Disconnect()
        {
            _sessionTimer.Stop();
            _operator.CurrentClientId = null;
            _client.CurrentOperatorId = null;
            _operator.SessionTime = new TimeSpan();
            _client.SessionTime = new TimeSpan();
            _client.Status = ClientStatus.None;

            _operator.Salary += 35;
            _operator.Status = true;
            _supportService.RemoveConnection(this);
        }

        public bool CheckConnection(Client client)
        {
            return _client == client;
        }
    }
}
