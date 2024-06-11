using System;
using System.Data.Entity.Core.Mapping;
using System.Data.SQLite;
using System.Windows.Forms;
using static Course_Work4.Query;

namespace Course_Work4
{
    public partial class Form1 : Form
    {
        private readonly List<User> _list;
        private string? checkText;
        public Form1()
        {

            InitializeComponent();
            using (StreamReader reader = new("MenuStatus.txt"))
            {
                string line;
                line = reader.ReadLine()!;
                if (Convert.ToInt32(line) == 1)
                {

                    MessageBox.Show("������������ ������ �4.\r\n " +
                                    "����������������� � ������������� ����������������\r\n " +
                                    "�������� ������������� ��������������� ��������.\r\n " +
                                    "������� ������ 413, ����� ���� ����������. 2023 ���",
                                    "���������",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1,
                                    MessageBoxOptions.DefaultDesktopOnly);
                                    ShowMenu.Checked = true;
                }
            }
            _list = new List<User>();
            bsUsers.DataSource = _list;
            List<User>? list = ConnectionToSQLAndShowUsers();
            if (list != null && list.Count > 0)
            {
                _list.AddRange(list);
                bsUsers.ResetBindings(false);
            }
            dataGridView1.AutoGenerateColumns = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Visible == true && CheckUpdateData())
            {
                int row = dataGridView1.CurrentCell.RowIndex;
                int index = (int)dataGridView1.Rows[row].Cells[0].Value;
                _list.Clear();
                List<User>? list = UpdateAndShowUsers(comboBox1.Text,
                                                         textBox2.Text,
                                                         textBox3.Text,
                                                         textBox4.Text,
                                                         textBox5.Text,
                                                         index);
                if (list != null && list.Count > 0)
                {
                    _list.AddRange(list);
                    bsUsers.ResetBindings(false);
                }
            }


            _list.Clear();
            List<User>? list1 = ConnectionToSQLAndShowUsers();
            if (list1 != null && list1.Count > 0) 
            { 
                _list.AddRange(list1);
                bsUsers.ResetBindings(false);
            }   
        }
        private bool CheckUpdateData()
        {

            try
            {
                if (comboBox1.Text != "" && textBox4.Text != "")
                {
                    textBox2.Text = Convert.ToDouble(textBox2.Text).ToString();
                    textBox3.Text = Convert.ToDouble(textBox3.Text).ToString();
                    textBox5.Text = Convert.ToDouble(textBox5.Text).ToString();
                }
                else
                {
                    MessageBox.Show(this, "�������� ������ ������.\n" +
                     "�������� ������ � ��������� ����", "������!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

            }
            catch
            {
                MessageBox.Show(this, "�������� ������ ������.\n" +
                      "�������� ������ � ��������� ����", "������!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        



        private void button2_Click(object sender, EventArgs e)
        {
            Form form2 = new Form2();
            form2.Show();
            Visible = false;
        }
        public void FillDatabase()
        {
            Form2? form2 = Application.OpenForms.OfType<Form2>().FirstOrDefault(); // ����� ��� ���������� ���������� form2
            if (form2 != null)
            {
                int row = dataGridView1.CurrentCell.RowIndex;
                int index = (int)dataGridView1.Rows[row].Cells[0].Value;
                _list.Clear();
                 List<User>? list = AddAndShowUsers(form2.comboBox1.Text,
                                                    form2.textBox2.Text,
                                                    form2.textBox3.Text,
                                                    form2.textBox4.Text,
                                                    form2.textBox5.Text,
                                                    index);
                 if (list != null && list.Count > 0)
                 {
                     _list.AddRange(list);
                     bsUsers.ResetBindings(false);
                 }
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            int row = dataGridView1.CurrentCell.RowIndex;
            int index = (int)dataGridView1.Rows[row].Cells[0].Value;
            _list.Clear();
            DialogResult result = MessageBox.Show("�� ����� ������ ������� ������?",
                                                  "���������",
                                                  MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                List<User>? list = DeleteAndShowUsers(index);
                if (list != null && list.Count > 0)
                {
                    _list.AddRange(list);
                    bsUsers.ResetBindings(false);
                }
            }
            else 
            {
                List<User>? list = ConnectionToSQLAndShowUsers();
                if (list != null && list.Count > 0)
                {
                    _list.AddRange(list);
                    bsUsers.ResetBindings(false);//�������� ��������
                }
            }
        }
        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            textBox5.Visible = true;

            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            
            checkText = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            if (checkText == "������������� ������� �����")
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.SelectedIndex = 1;
            }
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();

        }

      

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName;
            using StreamWriter writer = new(filename, false);

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 1; j < dataGridView1.ColumnCount; j++)
                {
                    string? text = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    writer.WriteLine(text);
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
           
            string filename = openFileDialog1.FileName;
            using StreamReader reader = new(filename);
            try
            {
                string[] dataFromFile = File.ReadAllLines(filename);
                if (dataFromFile.Length % 5 != 0)
                    return;

                _list.Clear();
                List<User>? list = TruncateAllUsers();
                string[,] dataFromTheFile = new string[dataFromFile.Length / 5, dataGridView1.ColumnCount];
                int counter = 0;
                for (int i = 0; i < (dataFromFile.Length / 5); i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        dataFromTheFile[i, j] = dataFromFile[counter];
                        counter++;
                    }
                    list = TakeFromFileAndShowUsers(dataFromTheFile[i, 0],
                                                    dataFromTheFile[i, 1],
                                                    dataFromTheFile[i, 2],
                                                    dataFromTheFile[i, 3],
                                                    dataFromTheFile[i, 4]);


                }
                if (list != null && list.Count > 0)
                {
                    _list.AddRange(list);
                    bsUsers.ResetBindings(false);
                }
            }
            catch
            {
                MessageBox.Show(this, "��������� ������ ��� ������� ���������� ������ �� �����." +
                                    "�������������� ������ � ���������� �����", "������!",
                                     MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ��������������������������������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using StreamWriter writer = new("MenuStatus.txt", false);

            if (ShowMenu.Checked == true)
            {
                ShowMenu.Checked = false;
            }
            else
            {
                ShowMenu.Checked = true;
            }
            if (ShowMenu.Checked == true)
            {
                writer.WriteLine(1);
            }
            else
            {

                writer.WriteLine(0);
            }

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 44) // �����, ������� BackSpace � �������
            {
                e.Handled = true;
            }


        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 44) // �����, ������� BackSpace � �������
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // �����, ������� BackSpace
            {
                e.Handled = true;
            }
        }

       
    }
}