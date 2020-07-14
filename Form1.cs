using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using WaitWnd;

using Font = System.Drawing.Font;
using Point = System.Drawing.Point;

namespace Генератор_вариантов
{
    using Word = Microsoft.Office.Interop.Word;

    public enum FileContent { Tasks, Answers }; //Варианты содержимого файла

    public partial class Form1 : Form
    {
        private Form2 source_version_;
        private TextBox way_for_versions_;
        private TextBox way_for_answers_;
        private NumericUpDown num_of_versions_;
        private delegate void _workWithWordDelegate(string path, string text, decimal versionNum, FileContent fileContnet);
        private Microsoft.Office.Interop.Word.Application _app;
        private WaitWndFun _waitWindow;
        private bool _applyToAll;
        private DialogResult _usersConfirmFormResult = DialogResult.None; 

        public Form1()
        {
            InitializeComponent();
            source_version_ = new Form2(this);

            //Открываем ворд на фоне
            _app = new Microsoft.Office.Interop.Word.Application();
            _app.Visible = false;

            _waitWindow = new WaitWndFun();

            //Настраиваем расположение окна
            Screen screen = Screen.FromControl(this);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(screen.WorkingArea.Width/2 - this.Width/2, screen.WorkingArea.Height/2 - this.Height);
        }

//------------------------------------------------------Текстбоксы--------------------------------------------------------------
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


//------------------------------------------------------Кнопки------------------------------------------------------------------
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

        //Большущая кнопка "Сгенерировать"
        private void b_gen_Click(object sender, EventArgs e)
        {
            if (!PathsAreCorrect()) return; //Проверка введенных путей

            //Генерируем тексты вариантов и ответы к ним
            List<TestVersion> testVersions = GenerateTestVersions(num_of_versions_.Value);


            //Сохраняем тексты вариантов
            _waitWindow.Show();
            foreach (TestVersion version in testVersions)
            {
                if (_usersConfirmFormResult == DialogResult.Cancel && _applyToAll == true) return; //Если пользователь отменил
                                                                                                   //сохранение всех файлов
                                                                                                   //выходим
                _workWithWordDelegate d = new _workWithWordDelegate(SaveToWordFile);
                IAsyncResult result = d.BeginInvoke(way_for_versions_.Text, version.VersionText, version.VersionNum, 
                    FileContent.Tasks, null, null);
                d.EndInvoke(result);
            }
           
            //Сохраняем ответы
            foreach (TestVersion version in testVersions)
            {
                if (_usersConfirmFormResult == DialogResult.Cancel && _applyToAll == true) return; //Если пользователь отменил
                                                                                                   //сохранение всех файлов
                                                                                                   //выходим
                _workWithWordDelegate d = new _workWithWordDelegate(SaveToWordFile);
                IAsyncResult result = d.BeginInvoke(way_for_versions_.Text, version.AnswersText, version.VersionNum, 
                    FileContent.Answers, null, null);
                d.EndInvoke(result);
            }
            _waitWindow.Close();

            _usersConfirmFormResult = DialogResult.None;
            _applyToAll = false;
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
            labels[0].Location = new Point(20, this.Height / 2 + 10);
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



        private List<TestVersion> GenerateTestVersions(decimal numOfVersions)
        {
            List<TestVersion> resultList = new List<TestVersion>();
            for (decimal i = 1; i <= numOfVersions; ++i)
            {
                TestVersion version = GenerateTestVersion(i);
                resultList.Add(version);
            }
            return resultList;
        }

        private TestVersion GenerateTestVersion(decimal num_of_version)
        {
            //Создаем экземпляр класа TestVersion, который будет хранить текст сгенерированных заданий и решения к ним
            TestVersion testVersion = new TestVersion(num_of_version);
            testVersion.generateTasks();

            return testVersion;
        }

        private void SaveToWordFile(string path, string text, decimal numOfVersion, FileContent fileContent)
        {
            //Создаем новый вордовский документ
            Word.Document doc = _app.Documents.Add();
            doc.Paragraphs[1].Range.Text = text;

            for (int i = 1; i <= doc.Paragraphs.Count; ++i)
            {
                doc.Paragraphs[i].Range.Font.Name = "Times New Roman";
                doc.Paragraphs[i].Range.Font.Size = 14;
            }

            //Генерируем название документа в зависимости от его содержимого (ответы или варианты)
            string title;
            if (fileContent == FileContent.Answers)
            {
                title = path + @"\Вариант " + numOfVersion + " ответы.docx";
            }
            else
            {
                title = path + @"\Вариант " + numOfVersion + ".docx";
            }

            if (File.Exists(title) && _applyToAll == false) //Если файл с таким именем уже существует
            {
                UsersConfirmForms usersConfirm = new UsersConfirmForms(numOfVersion, fileContent); //Открываем окно, в котором 
                                                                                                   //спрашиваем пользователя,
                                                                                                   //что делать
                _applyToAll = usersConfirm.ApplyToAll;
                _usersConfirmFormResult = usersConfirm.ShowDialog();

                if (_usersConfirmFormResult == DialogResult.Cancel)
                {
                    doc.Close();
                    return;
                }
            }

            if (_usersConfirmFormResult == DialogResult.No) //Если пользователь решил сохранить оба документа
            {
                string finalTitle = setTitle(title); //Настраиваем название файла в зависимости от того, существуют ли файлы с таким 
                                                     //же названием
                finalTitle = finalTitle.Remove(finalTitle.Length - 5, 5); //Убираем расширение .docx из названия файла
                doc.SaveAs2(finalTitle);
            }
            else
                doc.SaveAs2(title);

            doc.Close();
        }

        //Проверка, существуют ли введенные пути
        private bool PathsAreCorrect()
        {
            if (!Directory.Exists(way_for_versions_.Text))
            {
                MessageBox.Show("Путь для вариантов не найден.");
                return false;
            }

            if (!Directory.Exists(way_for_answers_.Text))
            {
                MessageBox.Show("Путь для ответов не найден.");
                return false;
            }
            return true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Закрываем ворд
            _app.Quit();
        }

        //Настройка названия в зависимости от того, существуют ли файлы с таким же названием
        private string setTitle(string primaryTitle)
        {
            //Возвращаем первоначальное название, если файла с таким именем не существует
            if (!File.Exists(primaryTitle)) return primaryTitle;

            string title = primaryTitle;
            int counter = 0; //Номер файла
            while (File.Exists(title)) //Если файл с таким названием уже существует
            {
                counter++; //Увеличиваем номер файлa
                if (!title.Contains('('))
                {
                    title = title.Remove(title.Length - 5, 5);
                    title += $" ({counter}).docx";
                }
                else
                {
                    string[] titleFragmentally = title.Split(new char[] { '(' }); //Отделяем название файла от его номера
                    title = $"{titleFragmentally[0]} ({counter}).docx";
                }
            }

            return title;
        }
    }
}
