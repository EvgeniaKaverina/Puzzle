namespace Puzzle
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.enter = new System.Windows.Forms.Button();
            this.password_enter = new System.Windows.Forms.TextBox();
            this.login_enter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.registration = new System.Windows.Forms.Button();
            this.password_reg = new System.Windows.Forms.TextBox();
            this.login_reg = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button_admin = new System.Windows.Forms.Button();
            this.password_admin = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(122, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(508, 499);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.enter);
            this.tabPage1.Controls.Add(this.password_enter);
            this.tabPage1.Controls.Add(this.login_enter);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(500, 470);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Вход";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // enter
            // 
            this.enter.Location = new System.Drawing.Point(180, 313);
            this.enter.Name = "enter";
            this.enter.Size = new System.Drawing.Size(131, 40);
            this.enter.TabIndex = 4;
            this.enter.Text = "Войти";
            this.enter.UseVisualStyleBackColor = true;
            this.enter.Click += new System.EventHandler(this.enter_Click);
            // 
            // password_enter
            // 
            this.password_enter.Location = new System.Drawing.Point(192, 197);
            this.password_enter.Name = "password_enter";
            this.password_enter.Size = new System.Drawing.Size(264, 22);
            this.password_enter.TabIndex = 3;
            // 
            // login_enter
            // 
            this.login_enter.Location = new System.Drawing.Point(192, 131);
            this.login_enter.Name = "login_enter";
            this.login_enter.Size = new System.Drawing.Size(264, 22);
            this.login_enter.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(32, 197);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Введите пароль:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(32, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Введите логин:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.registration);
            this.tabPage2.Controls.Add(this.password_reg);
            this.tabPage2.Controls.Add(this.login_reg);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(500, 470);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Регистрация";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // registration
            // 
            this.registration.Location = new System.Drawing.Point(181, 327);
            this.registration.Name = "registration";
            this.registration.Size = new System.Drawing.Size(167, 42);
            this.registration.TabIndex = 4;
            this.registration.Text = "Зарегистрироваться";
            this.registration.UseVisualStyleBackColor = true;
            this.registration.Click += new System.EventHandler(this.registration_Click);
            // 
            // password_reg
            // 
            this.password_reg.Location = new System.Drawing.Point(191, 230);
            this.password_reg.Name = "password_reg";
            this.password_reg.Size = new System.Drawing.Size(264, 22);
            this.password_reg.TabIndex = 3;
            // 
            // login_reg
            // 
            this.login_reg.Location = new System.Drawing.Point(191, 143);
            this.login_reg.Name = "login_reg";
            this.login_reg.Size = new System.Drawing.Size(264, 22);
            this.login_reg.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(46, 232);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 19);
            this.label4.TabIndex = 1;
            this.label4.Text = "Введите пароль:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(46, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "Введите логин:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button_admin);
            this.tabPage3.Controls.Add(this.password_admin);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(500, 470);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Администратор";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button_admin
            // 
            this.button_admin.Location = new System.Drawing.Point(142, 278);
            this.button_admin.Name = "button_admin";
            this.button_admin.Size = new System.Drawing.Size(205, 42);
            this.button_admin.TabIndex = 2;
            this.button_admin.Text = "Войти как администратор";
            this.button_admin.UseVisualStyleBackColor = true;
            this.button_admin.Click += new System.EventHandler(this.button_admin_Click);
            // 
            // password_admin
            // 
            this.password_admin.Location = new System.Drawing.Point(201, 172);
            this.password_admin.Name = "password_admin";
            this.password_admin.Size = new System.Drawing.Size(264, 22);
            this.password_admin.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(48, 172);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 19);
            this.label5.TabIndex = 0;
            this.label5.Text = "Введите пароль:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 524);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Авторизация";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox password_enter;
        private System.Windows.Forms.TextBox login_enter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button enter;
        private System.Windows.Forms.Button registration;
        private System.Windows.Forms.TextBox password_reg;
        private System.Windows.Forms.TextBox login_reg;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_admin;
        private System.Windows.Forms.TextBox password_admin;
        private System.Windows.Forms.Label label5;
    }
}

