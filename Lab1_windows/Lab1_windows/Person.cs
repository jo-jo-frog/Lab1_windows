using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_windows
{
    internal class Person
    {
        public int CardNumber;
        public string Name;
        public DateTime Birthday;
        public Person(int num, string name, DateTime day)
        {
            CardNumber = num;
            Name = name;
            Birthday = day;
        }

        public int calcAge(DateTime date)
        {
            int age;
            age = DateTime.Now.Year - date.Year;
            if (DateTime.Now.Month < date.Month || (DateTime.Now.Day < date.Day && DateTime.Now.Month == date.Month))
                age--;
            return age;
        }
    };
}
