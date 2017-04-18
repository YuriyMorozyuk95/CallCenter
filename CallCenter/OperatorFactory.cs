using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CallCenter
{
    class OperatorFactory
    {
        private readonly List<PersonName> _names;

        public OperatorFactory()
        {
            var xmlSerializer = new XmlSerializer(typeof(List<PersonName>));

            using (var fileStream = new FileStream(string.Format("{0}\\PersonNames.xml", Directory.GetCurrentDirectory()),
                FileMode.Open))
            {
                _names = (List<PersonName>)xmlSerializer.Deserialize(fileStream);
            }
        }

        public List<Operator> GetOperators(int count)
        {
            var operators = new List<Operator>();
            var rand = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < count; i++)
            {
                var rndPersonName = _names[rand.Next(0, _names.Count)];

                operators.Add(new Operator
                {
                    Name = rndPersonName.Name,
                    OperatorId = rand.Next(int.MaxValue),
                    OperatorSex = rndPersonName.Sex,
                    Salary = 0,
                    SessionTime = new TimeSpan(),
                    Status = true
                });
            }

            return operators;
        }
    }
}
