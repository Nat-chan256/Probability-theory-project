namespace Генератор_вариантов
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.b_show = new System.Windows.Forms.Button();
            this.b_generate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // b_show
            // 
            this.b_show.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.b_show.Location = new System.Drawing.Point(90, 12);
            this.b_show.Name = "b_show";
            this.b_show.Size = new System.Drawing.Size(360, 53);
            this.b_show.TabIndex = 0;
            this.b_show.Text = "Показать текст исходного варианта";
            this.b_show.UseVisualStyleBackColor = true;
            this.b_show.Click += new System.EventHandler(this.b_show_Click);
            // 
            // b_generate
            // 
            this.b_generate.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.b_generate.Location = new System.Drawing.Point(90, 127);
            this.b_generate.Name = "b_generate";
            this.b_generate.Size = new System.Drawing.Size(360, 53);
            this.b_generate.TabIndex = 1;
            this.b_generate.Text = "Сгенерировать новые варианты";
            this.b_generate.UseVisualStyleBackColor = true;
            this.b_generate.Click += new System.EventHandler(this.b_generate_Click);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(520, 192);
            this.Controls.Add(this.b_generate);
            this.Controls.Add(this.b_show);
            this.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Вас приветствует генератор вариантов";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button b_show;
        private System.Windows.Forms.Button b_generate;
    }
}

