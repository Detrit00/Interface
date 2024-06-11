
namespace Course_Work4
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 44) // цифры, клавиша BackSpace и запятая
            {
    
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8 && number != 44) // цифры, клавиша BackSpace и запятая
            {
                e.Handled = true;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) // цифры, клавиша BackSpace
            {
                e.Handled = true;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                if (comboBox1.Text != "" && textBox4.Text != "")
                {
                    textBox2.Text = Convert.ToDouble(textBox2.Text).ToString();
                    textBox3.Text = Convert.ToDouble(textBox3.Text).ToString();
                    textBox5.Text = Convert.ToDouble(textBox5.Text).ToString();
                    Form1? form1 = Application.OpenForms.OfType<Form1>().FirstOrDefault(); // Поиск уже созданного экземпляра form2
                    if (form1 != null)
                    {
                        form1.FillDatabase();
                        form1.Visible = true;
                    }
                    
                    Close();
                }
                else
                {
                    MessageBox.Show(this, "Неверный формат данных.\n" +
                     "Измените данные и повторите ввод", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch
            {
                MessageBox.Show(this, "Неверный формат данных.\n" +
                      "Измените данные и повторите ввод", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1? form1 = Application.OpenForms.OfType<Form1>().FirstOrDefault(); // Поиск уже созданного экземпляра form2
            if (form1 != null)
            {
                form1.Visible = true;
            }
            Close();
        }
    }
}
