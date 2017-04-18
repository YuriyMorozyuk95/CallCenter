using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CallCenter
{
    class ClientFactory
    {
        private readonly List<PersonName> _names;

        public ClientFactory()
        {
            var xmlSerializer = new XmlSerializer(typeof(List<PersonName>));

            using (var fileStream = new FileStream(string.Format("{0}\\PersonNames.xml", Directory.GetCurrentDirectory()),
                FileMode.Open))
            {
                _names = (List<PersonName>)xmlSerializer.Deserialize(fileStream);
            }
        }

        public List<Client> GetClients(int count)
        {
            var clients = new List<Client>();
            var rand = new Random(DateTime.Now.Millisecond - 100); //генерує відносно якого часу

            for (int i = 0; i < count; i++)
            {
                var rndPersonName = _names[rand.Next(0, _names.Count())];

                clients.Add(new Client
                {
                    Name = rndPersonName.Name,
                    ClientId = rand.Next(int.MaxValue),
                    SessionTime = new TimeSpan()
                });
            }

            return clients;
        }
    }
}
