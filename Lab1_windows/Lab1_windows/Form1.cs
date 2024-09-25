using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1_windows
{
    public partial class Form1 : Form
    { 
        List<Person> personList = new List<Person>();
        public Form1()
        {
            InitializeComponent();
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        { 

        }
        private void Add_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(true);

            // Подписываемся на событие
            form2.OnDataSubmitted += Form2_OnDataSubmitted;

            // Показываем форму как модальное окно
            form2.ShowDialog();

            // Закрываем форму вручную, если это нужно
            form2.Dispose();
        }

        private void Form2_OnDataSubmitted(int card, string name, DateTime day)
        {
            Person newPerson = new Person(card, name, day);

            personList.Add(newPerson);

            // Очищаем listBox1
            listBox1.Items.Clear();

            // Добавляем новые элементы в listBox1
            foreach (Person person in personList)
            {
                string personString = $"{person.Name} - {person.calcAge(person.Birthday)}";
                listBox1.Items.Add(personString);
            }
        }

        private void modify_Click(object sender, EventArgs e)
        {

            // Получаем выбранный элемент в ListBox
            int selectedIndex = listBox1.SelectedIndex;

            //ListBox listBox = sender as ListBox;
            if (selectedIndex != -1)
            {
                // Создаем форму для изменения данных
                Form2 form2 = new Form2(false);
                form2.NameTextBox.Text = personList[selectedIndex].Name;
                form2.CardTextBox.Text = personList[selectedIndex].CardNumber.ToString();
                form2.DateDateTimePicker.Value = personList[selectedIndex].Birthday;

                // Подписываемся на событие
                form2.OnDataSubmitted += (card, name, day) =>
                {
                    // Изменяем данные выбранного объекта Person
                    personList[selectedIndex].CardNumber = card;
                    personList[selectedIndex].Name = name;
                    personList[selectedIndex].Birthday = day;

                    // Обновляем выбранный элемент в listBox1
                    string personString = $"{name} - {personList[selectedIndex].calcAge(personList[selectedIndex].Birthday)}";
                    listBox1.Items[selectedIndex] = personString;
                };

                // Показываем форму как модальное окно
                form2.ShowDialog();

                // Закрываем форму вручную, если это нужно
                form2.Dispose();
            }
        }

        private void Del_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Удалить запись?", "НИТ", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                int selectedIndex = listBox1.SelectedIndex;

                if (selectedIndex != -1)
                {
                    // Удаляем выбранный объект Person из списка
                    personList.RemoveAt(selectedIndex);

                    // Удаляем элемент из списка
                    listBox1.Items.RemoveAt(selectedIndex);
                }
            }
        }
    }
}