using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CallCenter
{
    public class PersonName
    {
        public string Name { get; set; }
        public Sex Sex { get; set; }
    }

    public enum Sex : byte
    {
        Male, Female
    }
}
