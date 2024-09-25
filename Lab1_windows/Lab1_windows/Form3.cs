using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab1_windows
{
    public partial class Form3 : Form
    {
        public event Action<bool> OnDataSubmitted_1;
        public Form3()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        bool admin;

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) // админ
        {
            if (comboBox1.Text == "admin")
            {
                admin = true;
                textBox1.Enabled = true;
            }
            else
                textBox1.Enabled = false;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            OnDataSubmitted_1?.Invoke(admin);
            this.Close();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // The password character is an asterisk.
            textBox1.PasswordChar = '*';
            // The control will allow no more than 14 characters.
            textBox1.MaxLength = 14;
        }
    }
}
