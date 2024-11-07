using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zd2_forrat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        PhoneBook pb = new PhoneBook();
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox1.Text == null)
            {
                MessageBox.Show("Имя не введено");
            }
            else
            {
                int index = pb.SearchContact(textBox1.Text);

                if (index != -1)
                {
                    MessageBox.Show($"Найден контакт: {pb.GetAllContacts()[index].GetInfo()}");
                    listBox1.SelectedIndex = index;
                    listBox1.TopIndex = index;
                }
                else
                    MessageBox.Show("Контакт не найден");
            }
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel2.Visible = true;
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = true;
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel4.Visible = false;
            panel3.Visible = true;
        }

        private void поискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel1.Visible = true;
        }

        private void вывестиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            UpdateList();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PhoneBookLoader.Save(pb, "contacts.csv");
            UpdateList();
            MessageBox.Show("Данные сохранены");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PhoneBookLoader.Load(pb, "contacts.csv");
        }

        private void выходИзПрограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Вы действительно хотите выйти из программы", "Подтверждение", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            int index = -1;
            bool check = true;
            string name = textBox5.Text;
            string number = textBox4.Text;
            if (!(name == "" || number == ""))
            {
                // Проверка на наличие цифр в имени
                foreach (char c in name)
                {
                    if (char.IsNumber(c))
                    {
                        MessageBox.Show("В имени не могут быть цифры!");
                        check = false;
                        break;
                    }
                }


                // Проверка на наличие букв в номере
                foreach (char c in number)
                {
                    if (char.IsLetter(c))
                    {
                        MessageBox.Show("В номере только цифры!");
                        check = false;
                        break;
                    }
                }

                // Если все проверки пройдены, обновляем контакт
                if (check)
                {
                    index = listBox1.SelectedIndex;
                    MessageBox.Show(pb.ChangeContact(name, number, index));
                    UpdateList();
                }
            }
            else
            {
                MessageBox.Show("Обнаружены пустые поля");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            bool check = true;
            string name = textBox2.Text;
            string number = textBox3.Text;
            if (!(name == "" || number == ""))
            {
                foreach (char c in name)
                {
                    if (char.IsNumber(c))
                    {
                        MessageBox.Show("В имени не могут быть цифры!");
                        check = false;
                        break;
                    }
                }


                foreach (char c in number)
                {
                    if (char.IsLetter(c))
                    {
                        MessageBox.Show("В номере только цифры!");
                        check = false;
                        break;
                    }
                    if (!IsValidPhoneNumber(number))
                    {
                        MessageBox.Show("Номер телефона введён некорректно. Пожалуйста, введите в формате (333)333-33-33.");
                        check = false;
                        break;
                    }
                }

                if (check)
                {
                    MessageBox.Show(pb.AddContact(name, number));
                    UpdateList();
                }

            }
            else
                MessageBox.Show("Обнаружены пустые поля");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox7.Text = listBox1.SelectedItem.ToString();
            string[] parts = listBox1.SelectedItem.ToString().Split(';');
            textBox5.Text = parts[0];
            textBox4.Text = parts[1];
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox7.Text))
            {
                MessageBox.Show("Контакт не выбран. Пожалуйста, введите имя контакта для удаления.");
            }
            else
            {
                // Проверка на существование контакта в ListBox
                if (listBox1.Items.Contains(textBox7.Text))
                {
                    pb.DeleteContact(textBox7.Text); // Удаление контакта из источника данных
                    UpdateList();                     // Обновление списка
                    MessageBox.Show("Контакт удалён.");
                }
                else
                {
                    MessageBox.Show("Контакт не найден. Проверьте введённое имя.");
                }
            }

        }

        //обнавляет элементы в листбокс
        public void UpdateList()
        {
            listBox1.Items.Clear();
            foreach (var contact in pb.GetAllContacts())
            {
                listBox1.Items.Add($"{contact.Name}; {contact.Phone}");
            }
        }

        private bool IsValidPhoneNumber(string number)
        {
            // Используем регулярное выражение для проверки формата номера телефона
            string pattern = @"^\(\d{3}\)\d{3}-\d{2}-\d{2}$";
            return System.Text.RegularExpressions.Regex.IsMatch(number, pattern);
        }
    }
}
