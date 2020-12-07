namespace Puzzle
{
    partial class UserChoosingPuzzle
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.select_img = new System.Windows.Forms.Button();
            this.back = new System.Windows.Forms.Button();
            this.play = new System.Windows.Forms.Button();
            this.info = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.AutoCompleteCustomSource.AddRange(new string[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.comboBox1.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.comboBox1.Location = new System.Drawing.Point(168, 190);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(518, 44);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.Text = "Выбрать уровень";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(322, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(235, 46);
            this.label1.TabIndex = 1;
            this.label1.Text = "Выбор пазла";
            // 
            // select_img
            // 
            this.select_img.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.select_img.Cursor = System.Windows.Forms.Cursors.Hand;
            this.select_img.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.select_img.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.select_img.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.select_img.Location = new System.Drawing.Point(168, 320);
            this.select_img.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.select_img.Name = "select_img";
            this.select_img.Size = new System.Drawing.Size(519, 55);
            this.select_img.TabIndex = 2;
            this.select_img.Text = "Выбрать картинку";
            this.select_img.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.select_img.UseVisualStyleBackColor = false;
            this.select_img.Click += new System.EventHandler(this.select_img_Click);
            // 
            // back
            // 
            this.back.BackColor = System.Drawing.Color.WhiteSmoke;
            this.back.Cursor = System.Windows.Forms.Cursors.Hand;
            this.back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.back.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.back.Location = new System.Drawing.Point(168, 540);
            this.back.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(144, 51);
            this.back.TabIndex = 3;
            this.back.Text = "Назад";
            this.back.UseVisualStyleBackColor = false;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // play
            // 
            this.play.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.play.Cursor = System.Windows.Forms.Cursors.Hand;
            this.play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.play.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.play.Location = new System.Drawing.Point(542, 454);
            this.play.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.play.Name = "play";
            this.play.Size = new System.Drawing.Size(144, 51);
            this.play.TabIndex = 4;
            this.play.Text = "Играть";
            this.play.UseVisualStyleBackColor = false;
            this.play.Click += new System.EventHandler(this.play_Click);
            // 
            // info
            // 
            this.info.BackColor = System.Drawing.Color.White;
            this.info.Cursor = System.Windows.Forms.Cursors.Hand;
            this.info.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.info.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.info.Location = new System.Drawing.Point(168, 454);
            this.info.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(299, 51);
            this.info.TabIndex = 5;
            this.info.Text = "Справочная информация";
            this.info.UseVisualStyleBackColor = false;
            // 
            // UserChoosingPuzzle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Ivory;
            this.ClientSize = new System.Drawing.Size(922, 606);
            this.Controls.Add(this.info);
            this.Controls.Add(this.play);
            this.Controls.Add(this.back);
            this.Controls.Add(this.select_img);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UserChoosingPuzzle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserChoosingPuzzle";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button select_img;
        private System.Windows.Forms.Button back;
        private System.Windows.Forms.Button play;
        private System.Windows.Forms.Button info;
    }
}