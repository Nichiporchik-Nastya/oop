using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Bank2lab
{
    public partial class Form2 : Form
    {
        Bank tempBank = new Bank();
        Bank searchResBank = new Bank();
        
        public Form2()
        {
            Program.f2 = this; // теперь f2 будет ссылкой на форму Form2
            InitializeComponent();

            //заполнение comboBox
            string[] depositTypes = { "бессрочный", "долгосрочный", "краткосрочный", "отзывной", "безотзывной" };
            foreach (string type in depositTypes)
            {
                comboBox1.Items.Add(type);
            }
        }

        private void button2_Click(object sender, EventArgs e)//найти
        {
            Program.f2.listBox5.Items.Clear();//очистить перед выводом
            Search();   
        }
        public void Search()
        {
            string s = "";//строка для проверки
            bool f = false;//флаг
            searchResBank.owners.Clear();
            tempBank.owners.Clear();
            listBox5.Items.Clear();

            MatchCollection matches;
            Regex regex = new Regex($@"(\w*){1}(\w*)", RegexOptions.IgnoreCase);

            

            int i = 0;//индекс в цикле
            foreach (Owner ow in Program.f1.bank.owners)
            {
                s = ow.ToString();
                if (Program.f2.textBox1.Text != "")//фио
                {
                    regex = new Regex($@"(\w*){Program.f2.textBox1.Text}(\w*)", RegexOptions.IgnoreCase);
                    s += ow.name + ow.surName + ow.patronymic;
                }
                if (Program.f2.textBox2.Text != "")//баланс
                {
                    regex = new Regex($@"(\w*){Program.f2.textBox2.Text}(\w*)", RegexOptions.IgnoreCase);
                    s = ow.score.balance.ToString();
                }
                if (Program.f2.textBox3.Text != "")//номер счёта
                {
                    regex = new Regex($@"(\w*){Program.f2.textBox3.Text}(\w*)", RegexOptions.IgnoreCase);
                    s = ow.score.scoreNumber.ToString();
                }
                if (Program.f2.comboBox1.Text != "")
                {
                    regex = new Regex($@"(\w*){Program.f2.comboBox1.Text}(\w*)", RegexOptions.IgnoreCase);
                    s = ow.score.typeOfDiposit;
                }

                matches = regex.Matches(s);
                if (matches.Count > 0)
                {
                    f = true;
                    searchResBank.owners.Add(ow);

                    if (!(radioButton1.Checked && radioButton2.Checked))//сортировка не указана
                    {
                        Program.f1.PrintRes(searchResBank, Program.f2.listBox5, i);
                    }
                }
                s = "";
                i++;
            }

            if (radioButton1.Checked || radioButton2.Checked)//сортировка указана
            {
                tempBank.owners.Clear();
                IEnumerable<Owner> ordered = null;

                if (radioButton1.Checked)//по типу вклада
                    ordered = searchResBank.owners.OrderBy(p => p.score.typeOfDiposit);
                else if (radioButton2.Checked)//по дате открытия счёта
                    ordered = searchResBank.owners.OrderBy(p => p.score.openingDate);

                foreach (Owner own in ordered)
                {
                    tempBank.owners.Add(own);
                }
                //searchResBank = tempBank;
                listBox5.Items.Clear();
                Program.f1.PrintRes(tempBank, Program.f2.listBox5, -1);
            }

            if (f == false)
            {
                Program.f2.listBox5.Items.Add("Совпадений не найдено");
            }
            if (Program.f2.textBox1.Text == "" && Program.f2.textBox2.Text == "" && Program.f2.textBox3.Text == "" && Program.f2.comboBox1.Text == "")
            {
                MessageBox.Show("Заполните хотя бы 1 поле!");
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)//номер и баланс
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры и клавиша BackSpace
            {
                e.Handled = true;
            } 
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)//фио тип вклада
        {
            char mchar = e.KeyChar;
            if (!Char.IsLetter(mchar) && mchar != 8) // буквы и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)//сохранить в отдельный xml
        {
            bool trySer = true;
            try
            {
                System.IO.File.Delete("D:/файлы рабочего стола/второй сем/Banklab3/searchRes.xml");//удалить, если есть
                XmlSerializeWrapper.Serialize(tempBank, "D:/файлы рабочего стола/второй сем/Banklab3/searchRes.xml");
                trySer = true;
                if (tempBank.owners.Count == 0)
                {
                    trySer = false;
                }
            }
            catch (Exception) { trySer = false; }

            if (trySer)
            {
                MessageBox.Show("Информация сохранена в файл searchRes.xml в текущей папке");
            }
            else
            {
                MessageBox.Show("Ошибка сериализации");
                return;
            }
        }


    }
}
