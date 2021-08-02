using System;
using System.Collections.Generic;
using System.Text;

namespace ComunicationMessages
{
    public class Response
    {
        public DateTime Time { get; set; }
        public int Size { get; set; }
        public char SecondChar { get; set; }
        public bool ContainsCapitals { get; set; }
        public int AmountOfCapital { get; set; }
        public bool ContainsNumbers { get; set; }
        public int[] AllNumbersInAscendingOrder { get; set; }
    }
}
