using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using WindowsFormsApp1.connection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static string sqlstr;

        private void MyUpdate()
        {
            Db.Conect();
            dataGridView2.DataSource = Db.StudentsList;
            dataGridView2.Columns[1].HeaderText = "Курс"; 
            dataGridView2.Columns[2].HeaderText = "Имя";
            dataGridView2.Columns[3].HeaderText = "Фамилия";
            dataGridView2.Columns[4].HeaderText = "Отчество";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MyUpdate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            MyUpdate();
            comboBox1.SelectedIndex = -1;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "0";
            }
            if (textBox5.Text == "")
            {
                textBox5.Text = "0";
            }
            if (textBox6.Text == "")
            {
                textBox6.Text = "0";
            }
            if (textBox7.Text == "")
            {
                textBox7.Text = "0";
            }

            if (radioButton1.Checked)
            {
                string sqlstr2 = "select * from student where surname ='" + textBox4.Text + "'";
                Db.Read(sqlstr2, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text);
            }
            if (radioButton2.Checked)
            {
                string sqlstr2 = "select * from student where name ='" + textBox5.Text + "'";
                Db.Read(sqlstr2, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text);
            }
            if (radioButton3.Checked)
            {
                string sqlstr2 = "select * from student where middle_name ='" + textBox6.Text + "'";
                Db.Read(sqlstr2, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text);
            }
            if (radioButton4.Checked)
            {
                string sqlstr2 = /*sqlstr*/"select * from student where cource =" + textBox7.Text ;
                Db.Read(sqlstr2, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text);
            }

            dataGridView2.DataSource = Db.StudentsList;
            dataGridView2.Columns[1].HeaderText = "Курс";
            dataGridView2.Columns[2].HeaderText = "Имя";
            dataGridView2.Columns[3].HeaderText = "Фамилия";
            dataGridView2.Columns[4].HeaderText = "Отчество";
        }
        private void btn_delete_Click(object sender, EventArgs e)
        {
            DataGridViewCell selectedCell = dataGridView2.SelectedCells[0];
            string selectedCellValue = selectedCell.Value.ToString();
            Db.Delete(selectedCellValue);
            MyUpdate();
        }
        private void btn_plus_Click(object sender, EventArgs e)
        {
            label4.Text = "";
            string firstname = "";
            string surname = "";
            string secondname = "";
            int id_group = 2;
            int course = 0;
            bool check1;
            bool check2;

            if (comboBox1.Text != "" && textBox3.Text != "" && textBox2.Text != "" && textBox1.Text != "")
            {
                check1 = true;
                check2 = true; 
            }
            else
            {
                label4.Text = "Не все поля заполнены!";
                check1 = false;
                check2 = false;
            }

            if(check2)
            {
                switch (comboBox1.Text) 
                {
                    case "1":
                        course = 1; break;
                    case "2":
                        course = 2; break;
                    case "3":
                        course = 3; break;
                    case "4":
                        course = 4; break;
                }

                firstname = textBox2.Text;
                surname = textBox3.Text;
                secondname = textBox1.Text;

                Db.Add(firstname, secondname, surname, course);
                label4.Text = "Запись успешно добавлена!";
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
    }
}