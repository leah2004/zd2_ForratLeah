using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zd2_forrat
{
    internal class Contact
    {
        public string Name { get; set; }
        public string Phone { get; set; }

        //Конструктор
        public Contact(string name, string phone)
        {
            Name = name;
            Phone = phone;
        }

        //Получение информации о контакте
        public string GetInfo()
        {
            return $"{Name};{Phone}";
        }

    }
}
