namespace Генератор_вариантов
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.text_box = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // text_box
            // 
            this.text_box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.text_box.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.text_box.Location = new System.Drawing.Point(0, 0);
            this.text_box.Name = "text_box";
            this.text_box.ReadOnly = true;
            this.text_box.Size = new System.Drawing.Size(423, 586);
            this.text_box.TabIndex = 0;
            this.text_box.Text = "";
            this.text_box.TextChanged += new System.EventHandler(this.text_box_TextChanged);
            // 
            // Form2
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(423, 586);
            this.Controls.Add(this.text_box);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox text_box;
    }
}