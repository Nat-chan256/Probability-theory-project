using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Генератор_вариантов
{
    using Word = Microsoft.Office.Interop.Word;

    public partial class Form1 : Form
    {
        private Form2 source_version_;
        TextBox way_for_versions_;
        TextBox way_for_answers_;
        NumericUpDown num_of_versions_;

        public Form1()
        {
            InitializeComponent();
            source_version_ = new Form2(this);
        }

        private void b_show_Click(object sender, EventArgs e)
        {
            this.Hide();
            source_version_.Show();
        }

        private void b_generate_Click(object sender, EventArgs e)
        {
            this.Height = this.Height * 2;
            Label[] labels = new Label[3];
            //Настраиваем надписи
            labels[0] = new Label();
            labels[0].Text = "Количество вариантов: ";
            labels[0].Location = new Point(20, this.Height/2 + 10);
            labels[0].Size = labels[0].PreferredSize;
            labels[0].AutoSize = true;

            labels[1] = new Label();
            labels[1].Text = "Папка для вариантов: ";
            labels[1].Location = new Point(20, labels[0].Location.Y + labels[0].Height * 3);
            labels[1].Size = labels[1].PreferredSize;
            labels[1].AutoSize = true;

            labels[2] = new Label();
            labels[2].Text = "Папка для ответов: ";
            labels[2].Location = new Point(20, labels[1].Location.Y + labels[1].Height * 3);
            labels[2].Size = labels[2].PreferredSize;
            labels[2].AutoSize = true;

            for (int i = 0; i < 3; ++i)
                this.Controls.Add(labels[i]);

            //Настраиваем компоненты из правой половины
            num_of_versions_ = new NumericUpDown();
            num_of_versions_.Value = 1;
            num_of_versions_.Maximum = 200;
            num_of_versions_.Minimum = 1;
            num_of_versions_.Location = new Point(this.Width / 2, labels[0].Location.Y);
            num_of_versions_.Width = num_of_versions_.Width * 4 / 3;
            this.Controls.Add(num_of_versions_);

            way_for_versions_ = new TextBox();
            way_for_versions_.Font = this.Font;
            way_for_versions_.Text = "Введите путь";
            way_for_versions_.ForeColor = Color.Gray;
            way_for_versions_.Width = num_of_versions_.Width;
            way_for_versions_.Location = new Point(this.Width / 2, labels[1].Location.Y);
            way_for_versions_.MouseClick += new MouseEventHandler(text_boxes_Click);
            way_for_versions_.Leave += new EventHandler(text_boxes_MouseLeave);
            way_for_versions_.TextChanged += new EventHandler(text_boxes_TextChanged);
            this.Controls.Add(way_for_versions_);

            way_for_answers_ = new TextBox();
            way_for_answers_.Font = this.Font;
            way_for_answers_.Text = "Введите путь";
            way_for_answers_.ForeColor = Color.Gray;
            way_for_answers_.Width = num_of_versions_.Width;
            way_for_answers_.Location = new Point(this.Width / 2, labels[2].Location.Y);
            way_for_answers_.MouseClick += new MouseEventHandler(text_boxes_Click);
            way_for_answers_.Leave += new EventHandler(text_boxes_MouseLeave);
            way_for_answers_.TextChanged += new EventHandler(text_boxes_TextChanged);
            this.Controls.Add(way_for_answers_);

            Button browse1 = new Button();
            browse1.Text = "Обзор";
            browse1.Location = new Point(way_for_versions_.Location.X + way_for_versions_.Width + 5, way_for_versions_.Location.Y);
            browse1.AutoSize = true;
            browse1.Click += new EventHandler(browse1_Click);
            this.Controls.Add(browse1);

            Button browse2 = new Button();
            browse2.Text = "Обзор";
            browse2.Location = new Point(way_for_versions_.Location.X + way_for_versions_.Width + 5, way_for_answers_.Location.Y);
            browse2.AutoSize = true;
            browse2.Click += new EventHandler(browse2_Click);
            this.Controls.Add(browse2);

            //Большущая кнопка "Сгенерировать"
            Button b_gen = new Button();
            b_gen.Text = "Сгенерировать";
            b_gen.Width = b_generate.Width;
            b_gen.Location = new Point(this.Width / 2 - b_gen.Width / 2, way_for_answers_.Location.Y + way_for_answers_.Height + 30);
            b_gen.Font = new Font(this.Font, FontStyle.Bold);
            b_gen.AutoSize = true;
            b_gen.Click += new EventHandler(b_gen_Click);
            this.Controls.Add(b_gen);

            //Делаем кнопку недоступной
            Button but = (Button)sender;
            but.Enabled = false;
        }

        //ТекстБоксы
        private void text_boxes_Click(object sender, EventArgs e)
        {
            TextBox text_box = (TextBox)sender;
            if (text_box.Text.Contains("Введите путь"))
                text_box.Text = "";
        }
        
        private void text_boxes_MouseLeave(object sender, EventArgs e)
        {
            TextBox text_box = (TextBox)sender;
            if (text_box.Text.Length == 0)
            {
                text_box.ForeColor = Color.Gray;
                text_box.Text = "Введите путь";
            }
        }

        private void text_boxes_TextChanged(object sender, EventArgs e)
        {
            TextBox text_box = (TextBox)sender;
            if (text_box.Text == "Введите путь") return;
            text_box.ForeColor = Color.Black;
        }

        //Кнопки
        private void browse1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder_browser_dialog = new FolderBrowserDialog();

            if (folder_browser_dialog.ShowDialog() == DialogResult.OK)
            {
                way_for_versions_.Text = folder_browser_dialog.SelectedPath;
            }
        }

        private void browse2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder_browser_dialog = new FolderBrowserDialog();

            if (folder_browser_dialog.ShowDialog() == DialogResult.OK)
            {
                way_for_answers_.Text = folder_browser_dialog.SelectedPath;
            }
        }

        private void b_gen_Click(object sender, EventArgs e)
        {

            //Создаем новый вордовский документ
            //Word.Application app = new Word.Application();
            //app.Visible = false;
            //Word.Document doc = app.Documents.Add();
            //doc.Paragraphs[1].Range.Text = this.text_box.Text;

            //for (int i = 1; i < doc.Paragraphs.Count; ++i)
            //{
            //    doc.Paragraphs[i].Range.Font.Name = "Times New Roman";
            //    doc.Paragraphs[i].Range.Font.Size = 14;
            //}

            //doc.SaveAs2(save_file_dialog.FileName);
            //doc.Close();
            //app.Quit();
        }

        private TestVersion GenerateTestVersion(int num_of_version)
        {
            //Создаем экземпляр класа TestVersion, который будет хранить текст сгенерированных заданий и решения к ним
            TestVersion testVersion = new TestVersion(num_of_version);
            testVersion.generateTasks();

            return testVersion;
        }

    }
}
