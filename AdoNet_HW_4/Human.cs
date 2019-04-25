using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet_HW_4
{
    public class Human
    {
        public string Name { get; set; }
        public string Telephon_number { get; set; }

        public Human()
        {
            Name = "";
            Telephon_number = "";           
        }
        public Human(string Telephon_number, string Name)
        {
            this.Name = Name;
            this.Telephon_number = Telephon_number;            
        }
    }
}
