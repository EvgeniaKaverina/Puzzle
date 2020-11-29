namespace Puzzle
{
    partial class CreatePuzzle
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.select_pict = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.back = new System.Windows.Forms.Button();
            this.create_puzzle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(275, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(239, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "Создание пазла";
            // 
            // comboBox1
            // 
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox1.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.comboBox1.Location = new System.Drawing.Point(149, 123);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(485, 39);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.Text = "Выбрать уровень";
            // 
            // select_pict
            // 
            this.select_pict.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.select_pict.Cursor = System.Windows.Forms.Cursors.Hand;
            this.select_pict.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.select_pict.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.select_pict.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.select_pict.Location = new System.Drawing.Point(149, 205);
            this.select_pict.Name = "select_pict";
            this.select_pict.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.select_pict.Size = new System.Drawing.Size(485, 48);
            this.select_pict.TabIndex = 2;
            this.select_pict.Text = "Выбрать картинку";
            this.select_pict.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.select_pict.UseVisualStyleBackColor = false;
            this.select_pict.Click += new System.EventHandler(this.select_pict_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox2.Font = new System.Drawing.Font("Times New Roman", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Лента",
            "Поле"});
            this.comboBox2.Location = new System.Drawing.Point(149, 298);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(485, 39);
            this.comboBox2.TabIndex = 3;
            this.comboBox2.Text = "Выбрать расположение фрагментов";
            // 
            // back
            // 
            this.back.BackColor = System.Drawing.Color.WhiteSmoke;
            this.back.Cursor = System.Windows.Forms.Cursors.Hand;
            this.back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.back.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.back.Location = new System.Drawing.Point(149, 386);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(166, 39);
            this.back.TabIndex = 5;
            this.back.Text = "Назад";
            this.back.UseVisualStyleBackColor = false;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // create_puzzle
            // 
            this.create_puzzle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.create_puzzle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.create_puzzle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.create_puzzle.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.create_puzzle.Location = new System.Drawing.Point(468, 386);
            this.create_puzzle.Name = "create_puzzle";
            this.create_puzzle.Size = new System.Drawing.Size(166, 39);
            this.create_puzzle.TabIndex = 6;
            this.create_puzzle.Text = "Создать";
            this.create_puzzle.UseVisualStyleBackColor = false;
            this.create_puzzle.Click += new System.EventHandler(this.create_puzzle_Click);
            // 
            // CreatePuzzle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Ivory;
            this.ClientSize = new System.Drawing.Size(820, 485);
            this.Controls.Add(this.create_puzzle);
            this.Controls.Add(this.back);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.select_pict);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "CreatePuzzle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Создание пазла";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button select_pict;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button back;
        private System.Windows.Forms.Button create_puzzle;
    }
}