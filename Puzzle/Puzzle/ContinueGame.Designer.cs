namespace Puzzle
{
    partial class ContinueGame
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
            this.components = new System.ComponentModel.Container();
            this.time = new System.Windows.Forms.Label();
            this.view_pic = new System.Windows.Forms.Button();
            this.info = new System.Windows.Forms.Button();
            this.help = new System.Windows.Forms.Button();
            this.rating = new System.Windows.Forms.Button();
            this.exit = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.help_lab = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // time
            // 
            this.time.AutoSize = true;
            this.time.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.time.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.time.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.time.Location = new System.Drawing.Point(977, 489);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(96, 37);
            this.time.TabIndex = 17;
            this.time.Text = "00:00";
            // 
            // view_pic
            // 
            this.view_pic.BackColor = System.Drawing.Color.Snow;
            this.view_pic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.view_pic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.view_pic.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.view_pic.Location = new System.Drawing.Point(906, 399);
            this.view_pic.Name = "view_pic";
            this.view_pic.Size = new System.Drawing.Size(244, 70);
            this.view_pic.TabIndex = 16;
            this.view_pic.Text = "Посмотреть исходную картинку";
            this.view_pic.UseVisualStyleBackColor = false;
            this.view_pic.Click += new System.EventHandler(this.view_pic_Click);
            // 
            // info
            // 
            this.info.BackColor = System.Drawing.Color.Snow;
            this.info.Cursor = System.Windows.Forms.Cursors.Hand;
            this.info.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.info.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.info.Location = new System.Drawing.Point(906, 294);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(244, 68);
            this.info.TabIndex = 15;
            this.info.Text = "Справочная информация";
            this.info.UseVisualStyleBackColor = false;
            this.info.Click += new System.EventHandler(this.info_Click);
            // 
            // help
            // 
            this.help.BackColor = System.Drawing.Color.Snow;
            this.help.Cursor = System.Windows.Forms.Cursors.Hand;
            this.help.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.help.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.help.Location = new System.Drawing.Point(906, 164);
            this.help.Name = "help";
            this.help.Size = new System.Drawing.Size(244, 45);
            this.help.TabIndex = 14;
            this.help.Text = "Подсказка";
            this.help.UseVisualStyleBackColor = false;
            this.help.Click += new System.EventHandler(this.help_Click);
            // 
            // rating
            // 
            this.rating.BackColor = System.Drawing.Color.Snow;
            this.rating.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rating.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rating.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rating.Location = new System.Drawing.Point(197, 12);
            this.rating.Name = "rating";
            this.rating.Size = new System.Drawing.Size(136, 45);
            this.rating.TabIndex = 13;
            this.rating.Text = "Рейтинг";
            this.rating.UseVisualStyleBackColor = false;
            // 
            // exit
            // 
            this.exit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.exit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exit.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.exit.Location = new System.Drawing.Point(32, 12);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(136, 45);
            this.exit.TabIndex = 12;
            this.exit.Text = "Выйти";
            this.exit.UseVisualStyleBackColor = false;
            this.exit.Click += new System.EventHandler(this.exit_Click_1);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Snow;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(906, 82);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(244, 45);
            this.button1.TabIndex = 11;
            this.button1.Text = "Сохранить";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(33, 647);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1117, 144);
            this.flowLayoutPanel1.TabIndex = 10;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(33, 82);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(729, 444);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            // 
            // help_lab
            // 
            this.help_lab.AutoSize = true;
            this.help_lab.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.help_lab.Location = new System.Drawing.Point(911, 244);
            this.help_lab.Name = "help_lab";
            this.help_lab.Size = new System.Drawing.Size(225, 27);
            this.help_lab.TabIndex = 18;
            this.help_lab.Text = "Количество подсказок:";
            // 
            // ContinueGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1279, 818);
            this.Controls.Add(this.help_lab);
            this.Controls.Add(this.time);
            this.Controls.Add(this.view_pic);
            this.Controls.Add(this.info);
            this.Controls.Add(this.help);
            this.Controls.Add(this.rating);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "ContinueGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Мир пазлов";
            this.Load += new System.EventHandler(this.ContinueGame_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label time;
        private System.Windows.Forms.Button view_pic;
        private System.Windows.Forms.Button info;
        private System.Windows.Forms.Button help;
        private System.Windows.Forms.Button rating;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label help_lab;
    }
}