using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab1_windows
{
    public partial class Form2 : Form
    {
        public event Action<int, string, DateTime> OnDataSubmitted;

        private bool new_rec;
        private bool access;

        public Form2(bool a)
        {
            InitializeComponent();
            this.KeyPreview = true;
            new_rec = a;
            this.KeyDown += Form2_KeyDown;
            timer1 = new Timer(); // Создаем экземпляр Timer
            timer1.Start();
            timer1.Interval = 100;
            timer1.Tick += timer1_Tick;
        }

        int dx, dy;

        private void timer1_Tick(object sender, EventArgs e)
        {
            create.Left += dx;
            create.Top += dy;

            // Проверка границ формы
            if (create.Left < 0 || create.Left > this.Width - create.Width)
            {
                dx = -dx; // изменить направление движения по оси x
            }
            if (create.Top < 0 || create.Top > this.Height - create.Height)
            {
                dy = -dy; // изменить направление движения по оси y
            }
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (!new_rec)
            {
                if (e.Control && e.Shift && e.KeyCode == Keys.L)
                {
                    // открываем немодальное окно "выбор логина"
                    Form3 form3 = new Form3();

                    // Подписываемся на событие
                    form3.OnDataSubmitted_1 += Form3_OnDataSubmitted_1;

                    // Показываем форму как модальное окно
                    form3.ShowDialog();

                    // Закрываем форму вручную, если это нужно
                    form3.Dispose();
                    //loginForm.ShowDialog();
                }
            }
        }
        private void Form3_OnDataSubmitted_1(bool admin)
        {
            access = admin;
            MyForm_FlagChanged(this, EventArgs.Empty);
        }

        private void MyForm_FlagChanged(object sender, EventArgs e)
        {
            DateDateTimePicker.Enabled = access;
            CardTextBox.Enabled = access;
        }
        public void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }
        public void DateDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (new_rec)
                DateDateTimePicker.Enabled = true;
            else
            {
                if (access)
                    DateDateTimePicker.Enabled = true;
                else
                    DateDateTimePicker.Enabled = false;
            }
        }
        public void CardTextBox_TextChanged(object sender, EventArgs e)
        {
            if (new_rec)
                CardTextBox.Enabled = true;
            else
            {
                if (access)
                    CardTextBox.Enabled = true;
                else
                    CardTextBox.Enabled = false;
            }

            if (CardTextBox.Text.Length > 5)
            {
                CardTextBox.Text = CardTextBox.Text.Substring(0, 5);
                CardTextBox.SelectionStart = CardTextBox.Text.Length;
            }

            foreach (char c in CardTextBox.Text)
            {
                if (!char.IsDigit(c))
                {
                    CardTextBox.Text = CardTextBox.Text.Replace(c.ToString(), "");
                    CardTextBox.SelectionStart = CardTextBox.Text.Length;
                }
            }
        }
        private void create_Click(object sender, EventArgs e)
        {
            string name = NameTextBox.Text;
            int card = int.Parse(CardTextBox.Text);  // Пример: для карты, можно добавить проверки
            DateTime day = DateDateTimePicker.Value; // Пример: для даты

            if (CardTextBox.Text.Length < 5)
            {
                MessageBox.Show("Вы ввели слишком мало символов");
            }
            else
            {
                if (access)
                {
                    Random rand = new Random();

                    dx = rand.Next(-50, 60); // изменение координаты x
                    dy = rand.Next(-50, 60);
                }
                // Вызываем событие для передачи данных
                OnDataSubmitted?.Invoke(card, name, day);

                // Закрываем форму после отправки данных
                this.Close();
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

    }
}
