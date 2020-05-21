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

        private TestVersion GenerateTasks(int num_of_version)
        {
            string[] tasks = new string[18];
            string[] solutions = new string[18];
            int[] int_params = new int[4];
            double[] double_params = new double[6];
            Random rand_generator = new Random();

            //Первое задание
            tasks[0] = num_of_version + " ВАРИАНТ";
            tasks[0] += "\n\n" + num_of_version + ".1. На завод привезли партию из ";
            int_params[0] = rand_generator.Next(40,81);
            int_params[0] -= int_params[0] % 10;
            tasks[0] += int_params[0] + " подшипников, в которою попали ";
            int_params[1] = rand_generator.Next(5, 5 + int_params[0] / 3);
            tasks[0] += int_params[1] + " бракованных. Определить вероятность того, что из ";
            int_params[2] = rand_generator.Next(2, 2 + int_params[1] / 3);
            tasks[0] += int_params[2] + " взятых наугад подшипников окажется: а)по крайней мере один годный, б) ";
            int_params[3] += rand_generator.Next(1, int_params[2] - 2);
            tasks[0] += int_params[3] + " годных и " + (int_params[2] - int_params[3]) + " бракованных.";
            solutions[0] = Solution1(int_params[0], int_params[1], int_params[2], int_params[3]);

            //Второе задание
            tasks[1] = "\n\n" + num_of_version + ".2. В урне ";
            int_params[0] = rand_generator.Next(4, 24);
            tasks[1] += int_params[0] + " белых и ";
            int_params[1] = rand_generator.Next(4, 24);
            tasks[1] += int_params[1] + " черных шаров. Вынимают сразу ";
            int_params[2] = rand_generator.Next(3, 8);
            tasks[1] += int_params[2] + " шара. Найти вероятность того, что среди них окажется ровно ";
            int_params[3] = rand_generator.Next(1, 3);
            tasks[1] += int_params[3] + " белых шара.";
            solutions[1] = Solution2(int_params[0], int_params[1], int_params[2], int_params[3]);

            //Третье задание
            tasks[2] = "\n\n" + num_of_version + ".3. В колоде ";
            int_params[0] = (rand_generator.Next(0,2) == 0) ? 36 : 52;
            tasks[2] += int_params[0] + " карт. Наугад вынимают ";
            int_params[1] = rand_generator.Next(1, 11);
            tasks[2] += int_params[1] + "карт. Найти вероятность того, что среди них окажется хотя бы один туз.";
            solutions[2] = Solution3(int_params[0], int_params[1]);

            //Четвертое задание
            tasks[3] = "\n\n" + num_of_version + ".4. Вероятности появления каждого из двух независимых событий А и В равны ";
            double_params[0] = Math.Round(rand_generator.NextDouble(), 2);
            tasks[3] += double_params[0] + " и " + (1 - double_params[0]) + " соответственно. Найти вероятность появления только одного из них. ";
            solutions[3] = Solution4(double_params[0], 1-double_params[0]);

            //Пятое задание
            tasks[4] = "\n\n" + num_of_version + ".5.  Узел содержит ";
            int_params[0] = rand_generator.Next(2,7);
            tasks[4] += int_params[0] + "  независимо работающих деталей. Вероятности отказа деталей соответственно равны p1 = ";
            double_params[0] = rand_generator.Next(1,11) * 0.01;
            tasks[4] += double_params[0] + ", p";
            for (int i = 1; i <= int_params[0]; ++i)
            {
                double_params[i] = rand_generator.Next(1, 11) * 0.01;
                tasks[4] += (i + 1) + " = " + double_params[i];
                if (i < int_params[0]) tasks[4] += ", p";
            }
            tasks[4] += ". Найти вероятность отказа узла, если для этого достаточно, чтобы отказала хотя бы одна деталь.";
            solutions[4] = Solution5(int_params[0], double_params);

            //Шестое задание
            tasks[5] = "\n\n" + num_of_version + ".6.  Радист трижды вызывает корреспондента. Вероятность того, что будет принят первый вызов, равна ";
            double_params[0] = rand_generator.Next(1,6) * 0.1;
            tasks[5] += double_params[0] + ", второй - ";
            double_params[1] = rand_generator.Next(1,6) * 0.1;
            tasks[5] += double_params[1] + ", третий - ";
            double_params[2] = rand_generator.Next(1,6) * 0.1;
            tasks[5] += double_params[2] + ". События, состоящие в том, что данный вызов будет услышан, независимы. Найти вероятность того, "
                + "что корреспондент услышит вызов.";
            solutions[5] = Solution6(double_params[0], double_params[1], double_params[2]);

            //Седьмое задание
            tasks[6] = "\n\n" + num_of_version + ".7.  Два автомата производят детали, поступающие в сборочный цех. " +
                "Вероятность получения брака на первом автомате ";
            double_params[0] = rand_generator.Next(1,11) * 0.01;
            tasks[6] += double_params[0] + ", на втором - ";
            double_params[1] = rand_generator.Next(1,11) * 0.01;
            tasks[6] += double_params[1] + " Производительность второго автомата вдвое больше производительности первого. Найти вероятность того, "
                + "что наудачу взятая деталь будет бракованная.";
            solutions[6] = Solution7(double_params[0], double_params[1]);

            //Восьмое задание
            tasks[7] = "\n\n" + num_of_version + ".8.  Для сигнализации о пожаре установлены два независимо работающих сигнализатора. Вероятность того, "
                + "что при пожаре сигнализатор сработает, равна ";
            double_params[0] = rand_generator.Next(84, 99) * 0.01;
            tasks[7] += double_params[0] + " для первого сигнализатора и ";
            double_params[1] = rand_generator.Next(75, 99) * 0.01;
            tasks[7] += double_params[1] + " для второго. Найти вероятность того, что при пожаре сработает только один сигнализатор.";
            solutions[7] = Solution8(double_params[0], double_params[1]);

            //Девятое задание
            tasks[8] = "\n\n" + num_of_version + ".9. В больницу поступает в среднем ";
            int_params[0] = rand_generator.Next(1,6) * 10;
            tasks[8] += int_params[0] + "% больных с заболеванием А, ";
            int_params[1] = rand_generator.Next(1,5) * 10;
            tasks[8] += int_params[0] + "% с заболеванием В, ";
            int_params[2] = 100 - int_params[0] - int_params[1];
            tasks[8] += int_params[2] + "% с заболеванием С.  Вероятность полного выздоровления для каждого заболевания соответственно равны ";
            double_params[0] = rand_generator.Next(5, 9) * 0.1;
            double_params[1] = rand_generator.Next(5, 9) * 0.1;
            double_params[2] = rand_generator.Next(5, 9) * 0.1;
            tasks[8] += double_params[0] + "; " + double_params[1] + "; " + +double_params[2] + "Больной был выписан из больницы здоровым. " +
                "Найти вероятность того, что он страдал заболеванием А. ";
            solutions[8] = Solution9(int_params[0], int_params[1], int_params[2],
                double_params[0], double_params[1], double_params[2]);

            //Десятое задание
            tasks[9] = "\n\n" + num_of_version + ".10. В семье ";
            int_params[0] = rand_generator.Next(4, 10);
            tasks[9] += int_params[0] + " детей. Найти вероятность того, что среди них ";
            int_params[1] = rand_generator.Next(1, int_params[0]);
            tasks[9] += int_params[1] + " девочки. Вероятность рождения девочки равна ";
            double_params[0] = rand_generator.Next(20,60) * 0.01;
            tasks[9] += double_params[0] + ".";
            solutions[9] = Solution10(int_params[0], int_params[1], double_params[0]);

            //Одиннадцатое задание
            tasks[10] = "\n\n" + num_of_version + ".11. Случайная величина ξ имеет распределения вероятностей, представленное таблицей:"
                + "\nξ     | 0,1 | 0,2  | 0,3  | 0,4  | 0,5 |" + "\nР(х) | ";
            double_params[4] = 1;
            for (int i = 0; i < 4; ++i)
            {
                double_params[i] = rand_generator.Next(1,26);
                double_params[i] -= double_params[i] % 5;
                double_params[i] *= 0.01;
                tasks[10] += double_params[i] + " | ";
                double_params[4] -= double_params[i];
            }
            tasks[10] += double_params[4] + " | " + "\nПостроить многоугольник распределения и найти функцию распределения F(x). ";
            solutions[10] = Solution11(double_params);

            //Двенадцатое задание
            tasks[11] = "\n\n" + num_of_version + ".12. Найти М(ξ), D(ξ), σ(ξ) случайной величины ξ примера 11.";
            solutions[11] = Solution12(double_params);

            //Тринадцатое задание
            tasks[12] = "\n\n" + num_of_version + ".13. Задана плотность распределения непрерывной случайной величины:"
                + "\n φ(х) = Ax^";
            int_params[0] = rand_generator.Next(-2, 3);
            tasks[12] += int_params[0] + ", ∀x ∈ (";
            int_params[1] = rand_generator.Next(0, 4);
            int_params[2] = rand_generator.Next(int_params[1], int_params[1] + 4);
            tasks[12] += int_params[1] + ";" + int_params[2] + "]\n φ(х) = 0, ∀x ∉ (" + int_params[1] + 
                ";" + int_params[2] + "]. \nНайти А и функцию распределения F(x).";
            solutions[12] = Solution13(int_params[0], int_params[1], int_params[2]);

            //Четырнадцатое задание
            tasks[13] = "\n\n" + num_of_version + ".14.  ξ - непрерывная случайная величина примера 13. Найти М(ξ), D(ξ), σ(ξ) ";
            solutions[13] = Solution14(int_params[0], int_params[1], int_params[2]);

            //Пятнадцатое задание
            tasks[14] = "\n\n" + num_of_version + ".15.  Вероятность наступления события А в каждом опыте равна ";
            double_params[0] = rand_generator.Next(1, 91)*0.01;
            tasks[14] += double_params[0] + ". Найти вероятность того, что событие А в ";
            int_params[0] = rand_generator.Next(200, 3200);
            int_params[0] -= int_params[0] % 100;
            int_params[1] = rand_generator.Next(100, 100 + (int)(0.4 * int_params[0]));
            tasks[14] += int_params[0] + " опытах произойдет " + int_params[1] + " раз.";
            solutions[14] = Solution15(double_params[0], int_params[0], int_params[1]);

            //Шестнадцатое задание
            tasks[15] = "\n\n" + num_of_version + ".16. ξ - нормально распределенная случайная величина с параметрами а = ";
            double_params[0] = rand_generator.Next(5, 51) * 0.1;
            tasks[15] += double_params[0] + "; σ = ";
            double_params[1] = rand_generator.Next(2, 6) * 0.1;
            tasks[15] += double_params[1] + ". Найти Р(|ξ-";
            double_params[2] = rand_generator.Next(3, 7) * 0.5;
            tasks[15] += double_params[2] + "| < ";
            double_params[3] = rand_generator.Next(1, 6) * 0.1;
            tasks[15] += double_params[3] + ").";
            solutions[15] = Solution16(double_params[0], double_params[1], double_params[2], double_params[3]);

            //Семнадцатое задание
            tasks[16] = "\n\n" + num_of_version + ".17. Вероятность появления события в каждом из ";
            int_params[0] = rand_generator.Next(4, 41) * 25;
            tasks[16] += int_params[0] + " независимых испытаний постоянна и равна Р = ";
            double_params[0] = rand_generator.Next(7, 9) * 0.1;
            tasks[16] += double_params[0] + ". Найти вероятность того, что событие появится не более ";
            int_params[1] = rand_generator.Next(int_params[0] / 2, 3*int_params[0]/4);
            tasks[16] += int_params[1] + " раз.";
            solutions[16] = Solution17(int_params[0], double_params[0], int_params[1]);

            //Восемнадцатое задание
            tasks[17] = "\n\n" + num_of_version + ".18. Дана таблица распределения вероятностей двумерной случайной величины (ξ,η)"
                + "\nξ \\ η |  -1 |  0  | 1\n0      | ";
            int zero_generated = 0; //Флаг, указывающий, был ли сгененрирован ноль (ноль нужно сгененрировать не более одного раза)
            double_params[0] = rand_generator.Next(0, 4) * 0.1;
            if (double_params[0] == 0) zero_generated = 1;

            double_params[1] = rand_generator.Next(zero_generated, 4) * 0.1;
            if (double_params[1] == 0) zero_generated = 1;
            int max_value = ((int)(10 * (1 - double_params[0] - double_params[1])) < 3) ? (int)(10 * (1 - double_params[0] - double_params[1])) + 1 : 4;
            
            double_params[2] = rand_generator.Next(zero_generated, max_value) * 0.1;
            if (double_params[2] == 0) zero_generated = 1;
            max_value = ((int)(10 * (1 - double_params[0] - double_params[1] - double_params[2])) < 3) ? 
                (int)(10 * (1 - double_params[0] - double_params[1] - double_params[2])) + 1 : 4;
            
            double_params[3] = rand_generator.Next(zero_generated, max_value) * 0.1;
            if (double_params[3] == 0) zero_generated = 1;

            max_value = ((int)(10 * (1 - double_params[0] - double_params[1] - double_params[2] - double_params[3])) < 3) ?
               (int)(10 * (1 - double_params[0] - double_params[1] - double_params[2] - double_params[3])) + 1 : 4;
            double_params[4] = rand_generator.Next(zero_generated, max_value) * 0.1;
            if (double_params[4] == 0) zero_generated = 1;

            max_value = 10 - (int)double_params[0] * 10 - (int)double_params[1] * 10 - (int)double_params[2] * 10
                - (int)double_params[3] * 10 - (int)double_params[4] * 10 + 1; 
            double_params[5] = rand_generator.Next(zero_generated, max_value) * 0.1;

            tasks[17] += double_params[0] + "| " + double_params[1] + " | " + double_params[2] + "\n1      |  "
                + double_params[3] + "| " + double_params[4] + " | " + double_params[5] + "\nНайти М(ξ), М(η), М(ξη), D(ξ), D(η), D(ξη).";
            solutions[17] = Solution18(double_params);

            return new TestVersion(tasks, solutions);
        }

        //Решения заданий
        private string Solution1(int num_of_bearings, int_params[1], int_params[2], int_params[3])
    }
}
