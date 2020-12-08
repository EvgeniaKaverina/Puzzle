namespace Puzzle
{
    partial class Game
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.exit = new System.Windows.Forms.Button();
            this.rating = new System.Windows.Forms.Button();
            this.help = new System.Windows.Forms.Button();
            this.info = new System.Windows.Forms.Button();
            this.view_pic = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.time = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(33, 82);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(729, 444);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(33, 563);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(807, 228);
            this.flowLayoutPanel1.TabIndex = 1;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(984, 82);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 36);
            this.button1.TabIndex = 2;
            this.button1.Text = "Сохранить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // exit
            // 
            this.exit.Location = new System.Drawing.Point(33, 26);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(136, 36);
            this.exit.TabIndex = 3;
            this.exit.Text = "Выйти";
            this.exit.UseVisualStyleBackColor = true;
            // 
            // rating
            // 
            this.rating.Location = new System.Drawing.Point(198, 26);
            this.rating.Name = "rating";
            this.rating.Size = new System.Drawing.Size(136, 36);
            this.rating.TabIndex = 4;
            this.rating.Text = "Рейтинг";
            this.rating.UseVisualStyleBackColor = true;
            // 
            // help
            // 
            this.help.Location = new System.Drawing.Point(984, 142);
            this.help.Name = "help";
            this.help.Size = new System.Drawing.Size(136, 36);
            this.help.TabIndex = 5;
            this.help.Text = "Подсказка";
            this.help.UseVisualStyleBackColor = true;
            // 
            // info
            // 
            this.info.Location = new System.Drawing.Point(949, 202);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(192, 36);
            this.info.TabIndex = 6;
            this.info.Text = "Справочная информация";
            this.info.UseVisualStyleBackColor = true;
            // 
            // view_pic
            // 
            this.view_pic.Location = new System.Drawing.Point(922, 261);
            this.view_pic.Name = "view_pic";
            this.view_pic.Size = new System.Drawing.Size(244, 36);
            this.view_pic.TabIndex = 7;
            this.view_pic.Text = "Посмотреть исходную картинку";
            this.view_pic.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // time
            // 
            this.time.AutoSize = true;
            this.time.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.time.Location = new System.Drawing.Point(1000, 322);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(96, 38);
            this.time.TabIndex = 8;
            this.time.Text = "00:00";
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 803);
            this.Controls.Add(this.time);
            this.Controls.Add(this.view_pic);
            this.Controls.Add(this.info);
            this.Controls.Add(this.help);
            this.Controls.Add(this.rating);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Game";
            this.Text = "Game";
            this.Load += new System.EventHandler(this.Game_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.Button rating;
        private System.Windows.Forms.Button help;
        private System.Windows.Forms.Button info;
        private System.Windows.Forms.Button view_pic;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label time;
    }
}