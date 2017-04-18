using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace CallCenter
{
    public class Waiting
    {
        private readonly Client _client;
        private readonly SupportService _supportService;
        private readonly DispatcherTimer _timer;

        public Waiting(Client client, SupportService supportService)
        {
            _client = client;
            _supportService = supportService;

            client.Status = ClientStatus.Wating;

            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += FindOperator;
            _timer.Start();

        }

        private void FindOperator(object sender, EventArgs e)
        {
            _client.SessionTime = _client.SessionTime.Add(TimeSpan.FromSeconds(1));

            var freeOperator = _supportService.Operators.FirstOrDefault(o => o.Status); //Linq

            if (_client.SessionTime >= TimeSpan.FromSeconds(60))
            {
                _timer.Stop();
                _client.SessionTime = new TimeSpan();
                _supportService.RemoveWaiting(this);
                
                foreach (var oper in _supportService.Operators)
                {
                    oper.Salary = (int) (oper.Salary * 0.99);
                }

                _client.Status = ClientStatus.Missed;
            }

            if (freeOperator != null)
            {
                _timer.Stop();
                _client.SessionTime = new TimeSpan();
                _supportService.RemoveWaiting(this);
                _supportService.AddConnection(new Connection(freeOperator, _client, _supportService));
            }
        }
    }
}
