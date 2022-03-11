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
    public partial class Form1 : Form
    {
        public Bank bank;
        Bank tempBank = new Bank();
        public DateTime currentDate = DateTime.Today;
        string currentAction = "Ничего не происходит";
        int currentOwner = 0; //индекс выбранного владельца в массиве
        public Form1()
        {
            Program.f1 = this; // теперь f1 будет ссылкой на форму Form1
            InitializeComponent();

            bank = new Bank();
            bank.owners.Clear();
            //добавление кнопок в меню
            ToolStripButton searchBtn = new ToolStripButton();
            searchBtn.Text = "Поиск";
            // устанавливаем обработчик нажатия
            searchBtn.Click += SearchBtn_Click;
            toolStrip1.Items.Add(searchBtn);

            ToolStripButton sortBtn = new ToolStripButton();
            sortBtn.Text = "Сортировка";
            sortBtn.Click += SortBtn_Click;
            toolStrip1.Items.Add(sortBtn);

            ToolStripButton clearBtn = new ToolStripButton();
            clearBtn.Text = "Очистить";
            clearBtn.Click += ClearBtn_Click;
            toolStrip1.Items.Add(clearBtn);

            ToolStripButton deleteBtn = new ToolStripButton();
            deleteBtn.Text = "Удалить текущий";
            deleteBtn.Click += DeleteBtn_Click;
            toolStrip1.Items.Add(deleteBtn);

            ToolStripButton nextBtn = new ToolStripButton();
            nextBtn.Text = "Вперёд";
            nextBtn.Click += NextBtn_Click;
            toolStrip1.Items.Add(nextBtn);

            ToolStripButton prevBtn = new ToolStripButton();
            prevBtn.Text = "Назад";
            prevBtn.Click += PrevBtn_Click;
            toolStrip1.Items.Add(prevBtn);

            //значения строки состояния
            toolStripStatusLabel1.Text = "Всего объектов: " + bank.owners.Count();
            toolStripStatusLabel2.Text = "Текущее действие: " + currentAction;
            toolStripStatusLabel3.Text = currentDate.ToShortDateString();
            toolStripStatusLabel4.Text = DateTime.Now.ToShortTimeString();

            //заполнение comboBox
            string[] depositTypes = { "бессрочный", "долгосрочный", "краткосрочный", "отзывной", "безотзывной" };
            foreach (string type in depositTypes)
            {
                comboBox1.Items.Add(type);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
                toolStrip1.Show();//показать панель
        }

        public void AddingOwner()
        {
            try
            {
                if (textBox3.Text.Equals("") || textBox4.Text.Equals("") || textBox5.Text.Equals("") || maskedTextBox1.Text.Equals(""))
                {
                    MessageBox.Show("Введите всю информацию о владельце");
                    return;
                }
                if (!maskedTextBox1.MaskFull)
                {
                    MessageBox.Show("Номер паспорта вводится с латинскими буквами и 7 цифрами");
                    return;
                }
                if (numericUpDown1.Value.Equals(0) || trackBar1.Equals(0)|| comboBox1.Text.Equals(""))
                {
                    MessageBox.Show("Введите всю информацию о счёте");
                    return;
                }

                Score currentScore = new Score
                {
                    scoreNumber = numericUpDown1.Value,
                    typeOfDiposit = comboBox1.Text,
                    balance = trackBar1.Value,

                    openingDate = dateTimePicker1.Value,
                    banking = checkBox1.Checked,
                    cmc = checkBox2.Checked
                };
                Owner currentOwner = new Owner(currentScore)
                {
                    surName = textBox3.Text,
                    name = textBox4.Text,
                    patronymic = textBox5.Text,

                    bDay = dateTimePicker2.Value,
                    passportNumber = maskedTextBox1.Text
                };

                bank.owners.Add(currentOwner);

                currentAction = "Производится сохранение информации";
                CurrentAction();
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный тип вводимых данных");
            }
        }
        public void CurrentAction()
        {
            toolStripStatusLabel2.Text = "Текущее действие: " + currentAction;
        }
        public void PrintRes(Bank desrResult, ListBox lb, int index)
        {
           
            string strForRes;

            void PrintItem(Owner owner)//печать одного
            {
                strForRes = "Владелец " + ":" + " " + owner.surName + " " + owner.name + " " + owner.patronymic;
                lb.Items.Add(strForRes);
                strForRes = "";
                strForRes = "имеет счёт с номером" + " " + owner.score.scoreNumber + ", открытый " + owner.score.openingDate.ToShortDateString();
                lb.Items.Add(strForRes);
                strForRes = "";
                strForRes = "тип вклада: " + " " + owner.score.typeOfDiposit + ", баланс: " + owner.score.balance;
                lb.Items.Add(strForRes);
                strForRes = "\nдополнительно:";
                lb.Items.Add(strForRes);
                strForRes = "";
                strForRes = "дата рождения: " + owner.bDay.ToShortDateString() + ", паспорт:" + " " + owner.passportNumber;
                lb.Items.Add(strForRes);
                strForRes = "";

                if (owner.score.banking)
                {
                    strForRes += "\nподключен интернет-банкинг";
                    lb.Items.Add(strForRes);
                    strForRes = "";
                }
                if (owner.score.cmc)
                {
                    strForRes += "\nподключено смс оповещение";
                    lb.Items.Add(strForRes);
                    strForRes = "";
                }

                lb.Items.Add(strForRes);
            }

            if (index == -1)//для всех
            {
                foreach (Owner owner in desrResult.owners)
                {
                    PrintItem(owner);
                }
            }

            if (index != -1)//для одного выбранного
            {
                PrintItem(bank.owners[index]);
            }
        }
        void SearchBtn_Click(object sender, EventArgs e)
        {
            currentAction = "Производится поиск";
            CurrentAction();

            Form2 frm2 = new Form2(); //где Form2 - название вашей формы
            frm2.Show();
        }
        void OpenBtn_Click(object sender, EventArgs e)
        {
            toolStrip1.Show();//показать панель
        }
        void SortBtn_Click(object sender, EventArgs e)
        {
            currentAction = "Производится сортировка";
            CurrentAction();

            tempBank.owners.Clear();
            if (bank.owners.Count == 0)
            {
                MessageBox.Show("А сортировать то нечего! Введите данные");
            }
            else
            {
                IEnumerable<Owner> ordered = null;
                ordered = bank.owners.OrderBy(p => p.score.openingDate);


                foreach (Owner own in ordered)
                {
                    tempBank.owners.Add(own);
                }

                listBox1.Items.Clear();
                Program.f1.PrintRes(tempBank, listBox1, -1);
            }
        }
        void ClearBtn_Click(object sender, EventArgs e)
        {
            currentAction = "Производится очистка";
            CurrentAction();

            ClearGroupBoxes();//очистка содержимого формы
            listBox1.Items.Clear(); //очистить вывод
        }
        void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (currentOwner < 0)
            {
                currentOwner = bank.owners.Count();
            }
            if (currentOwner > bank.owners.Count())
            {
                currentOwner = 0;
            }
            DialogResult dialogResult = MessageBox.Show("Удалить текущий (последний созданный) элемент?", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (bank.owners.Count() != 0)
                {
                    bank.owners.Remove(bank.owners[currentOwner]); //очистить последнего из памяти
                    currentAction = "Производится удаление текущего объекта";
                    CurrentAction();

                    ClearGroupBoxes();//очистка содержимого формы
                }
                else MessageBox.Show("Элементов для удаления нет");
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }
        void NextBtn_Click(object sender, EventArgs e)
        {
            currentAction = "Производится переход к следующему элементу";
            CurrentAction();
            currentOwner++;
        }
        void PrevBtn_Click(object sender, EventArgs e)
        {
            currentAction = "Производится переход к предыдущему элементу";
            CurrentAction();
            currentOwner--;
        }
        public void ClearGroupBoxes()
        {
            TextBox[] textBoxes = { textBox3, textBox4, textBox5 };
            foreach(TextBox tb in textBoxes)
            {
                tb.Clear();
            }
            numericUpDown1.Value = 0;
            trackBar1.Value = 0;
            label8.Text = "";
            currentDate = DateTime.Today;
            dateTimePicker1.Value = currentDate;
            dateTimePicker2.Value = currentDate;
            comboBox1.Text = "";
            maskedTextBox1.Text = "";
            checkBox1.Checked = false;
            checkBox2.Checked = false;

        }

        private void button6_Click(object sender, EventArgs e)//сохранить изменения владелец
        {
            AddingOwner();
            //переписать значение
            toolStripStatusLabel1.Text = "Всего объектов: " + bank.owners.Count();
        }

        private void button1_Click(object sender, EventArgs e)//добавить нового владельца
        {
            ClearGroupBoxes();

            currentAction = "Производится добавление нового владельца";
            CurrentAction();
        }

        private void button2_Click(object sender, EventArgs e)//сохранить информацию в xml
        {
            bool trySer = true;
            try
            {
                System.IO.File.Delete("D:/файлы рабочего стола/второй сем/Banklab3/owners.xml");//удалить, если есть
                XmlSerializeWrapper.Serialize(bank, "D:/файлы рабочего стола/второй сем/Banklab3/owners.xml");
                trySer = true;
                if (bank.owners.Count == 0)
                {
                    trySer = false;
                }
            }
            catch (Exception) { trySer = false; }

            if (trySer)
            {
                MessageBox.Show("Информация сохранена в файл owners.xml в текущей папке");
            }
            else
            {
                MessageBox.Show("Ошибка сериализации");
                return;
            }
        }
        private void button3_Click(object sender, EventArgs e)//прочитать информацию из xml
        {
            Bank desrResult;

           try
           {
                desrResult = XmlSerializeWrapper.Deserialize<Bank>("D:/файлы рабочего стола/второй сем/Banklab3/owners.xml");
                desrResult.owners.RemoveAt(0);
                //if (desrResult.owners.Count() == 0) throw new Exception();

                listBox1.Items.Clear();//очистить, если что-то было
                PrintRes(desrResult, listBox1, -1);//вывод результата в первое окно

                currentAction = "Производится чтение из xml";
                CurrentAction();

                bank = desrResult;
                //переписать значение
                toolStripStatusLabel1.Text = "Всего объектов: " + bank.owners.Count();
            }
            catch (Exception) 
            {
                MessageBox.Show("Ошибка десериализации");
                return;
            }
        }

        private void textBox3_KeyPress_1(object sender, KeyPressEventArgs e)//фамилия, имя, отчество
        {
            char mchar = e.KeyChar;
            if (!Char.IsLetter(mchar) && mchar != 8) // буквы и клавиша BackSpace
            {
                e.Handled = true;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)//баланс
        {
            label8.Text = trackBar1.Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)//очистить всё
        {
            ClearGroupBoxes();//очистка содержимого формы

            listBox1.Items.Clear(); //очистить вывод
            if (bank.owners.Count != 0) 
            { 
                bank.owners.Clear();//очистить память
             }
            else
            {
                MessageBox.Show("Нет элементов для удаления");
            }

            currentAction = "Производится удаление информации";
            CurrentAction();
        } 

        private void toolStripMenuItem1_Click(object sender, EventArgs e)//меню - поиск
        {
            Form2 frm2 = new Form2(); //где Form2 - название вашей формы
            frm2.Show();

            currentAction = "Производится поиск";
            CurrentAction();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)//закрыть панель
        {
            toolStrip1.Hide();//скрыть панель

            //добавление кнопки
            Button openBtn = new Button();
            openBtn.BackgroundImage = Image.FromFile("D:/файлы рабочего стола/второй сем/Banklab3/icon_open.bmp");
            openBtn.Width = 75;
            openBtn.Height = 25;
            //координаты расположения контрола на форме
            openBtn.Location = new Point(0, 40);
            openBtn.Click += OpenBtn_Click;
            this.Controls.Add(openBtn);
        }

        private void типуВкладаToolStripMenuItem_Click(object sender, EventArgs e)//сортировать по - типу вклада
        {
            tempBank.owners.Clear();
            if (bank.owners.Count == 0)
            {
                MessageBox.Show("А сортировать то нечего! Введите данные");
            }
            else
            {
                IEnumerable<Owner> ordered = null;
                    ordered = bank.owners.OrderBy(p => p.score.typeOfDiposit);
                

                foreach (Owner own in ordered)
                {
                    tempBank.owners.Add(own);
                }

                listBox1.Items.Clear();
                Program.f1.PrintRes(tempBank, listBox1, -1);
            }
        }

        private void датеОткрытияСчётаToolStripMenuItem_Click(object sender, EventArgs e)//сортировать по - дате открытия счёта
        {
            tempBank.owners.Clear();
            if (bank.owners.Count == 0)
            {
                MessageBox.Show("А сортировать то нечего! Введите данные");
            }
            else
            {
                IEnumerable<Owner> ordered = null;
                ordered = bank.owners.OrderBy(p => p.score.openingDate);


                foreach (Owner own in ordered)
                {
                    tempBank.owners.Add(own);
                }

                listBox1.Items.Clear();
                Program.f1.PrintRes(tempBank, listBox1, -1);
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)//меню - сохранить
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
