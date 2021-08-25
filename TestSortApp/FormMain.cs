using System;
using System.Text;
using System.Windows.Forms;
using TestSortApp.Library;
// ReSharper disable LocalizableElement

namespace TestSortApp
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// Множество букв, которыми можно задать последовательность цветов
        /// </summary>
        private const string ValidStr = "кКзЗсС";

        /// <summary>
        /// Конструктор формы
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;
            comboBox3.SelectedIndex = 2;
        }

        /// <summary>
        /// Проверка неупорядоченной строки, введенной пользователем, на наличие недопустимых символов
        /// Присвоение TextBox новой строки, из которой удалены недопустимые символы
        /// </summary>
        /// <param name="notSortedStr">Ссылка на неупорядоченную строку</param>
        /// <returns>Строка, из которой удалены недопустимые символы или исходная строка</returns>
        private bool ValidateString(ref string notSortedStr)
        {
            DialogResult messageResult = DialogResult.None;

            foreach (var s in notSortedStr)
            {
                if (!ValidStr.Contains(s))
                {
                    messageResult = MessageBox.Show(
                        "Строка содержит недопустимые символы, отличные от К, З, С\nУдалить недопустимые символы из строки?",
                        "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (messageResult == DialogResult.Yes)
                        break;
                    else return false;
                }
            }

            if (messageResult == DialogResult.Yes)
            {
                StringBuilder newStr = new StringBuilder();
                foreach (var s in notSortedStr)
                {
                    if (ValidStr.Contains(s))
                        newStr.Append(s);
                }

                notSortedStr = newStr.ToString();
                textBoxNotSortedString.Text = notSortedStr;
            }

            return true;
        }

        #region Обработчики событий

        private void buttonStartSort_Click(object sender, EventArgs e)
        {
            var notSortedStr = textBoxNotSortedString.Text;

            if (!ValidateString(ref notSortedStr))
                return;

            var colorList = new ColorItemList(notSortedStr);

            var ruleCode = comboBox1.SelectedIndex * 100 +
                           comboBox2.SelectedIndex * 10 +
                           comboBox3.SelectedIndex;

            if (!Enum.IsDefined(typeof(ColorOrder), ruleCode))
            {
                MessageBox.Show(
                    $"Правило сортировки {comboBox1.SelectedItem} < {comboBox2.SelectedItem} < {comboBox3.SelectedItem}\nсформировано неверно: значения цветов должны различаться",
                    @"Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            colorList.SortColorList((ColorOrder)ruleCode);
            var sortedStr = colorList.ToString();
            textBoxSortedString.Text = sortedStr;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    comboBox2.SelectedIndex = 1;
                    comboBox3.SelectedIndex = 2;
                    break;
                case 1:
                    comboBox2.SelectedIndex = 0;
                    comboBox3.SelectedIndex = 2;
                    break;
                case 2:
                    comboBox2.SelectedIndex = 0;
                    comboBox3.SelectedIndex = 1;
                    break;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    if (comboBox1.SelectedIndex == 1) 
                        comboBox3.SelectedIndex = 2;
                    if (comboBox1.SelectedIndex == 2) 
                        comboBox3.SelectedIndex = 1;
                    break;
                case 1:
                    if (comboBox1.SelectedIndex == 0) 
                        comboBox3.SelectedIndex = 2;
                    if (comboBox1.SelectedIndex == 2) 
                        comboBox3.SelectedIndex = 0;
                    break;
                case 2:
                    if (comboBox1.SelectedIndex == 0) 
                        comboBox3.SelectedIndex = 1;
                    if (comboBox1.SelectedIndex == 1) 
                        comboBox3.SelectedIndex = 0;
                    break;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxSortedString.Text = string.Empty;
            textBoxNotSortedString.Text = string.Empty;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBoxNotSortedString_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b')
            {
                return;
            }

            if (!ValidStr.Contains(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #endregion
    }
}
