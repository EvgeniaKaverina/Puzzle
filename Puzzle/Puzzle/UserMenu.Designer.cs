namespace Puzzle
{
    partial class UserMenu
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
            this.buttonNewGame = new System.Windows.Forms.Button();
            this.buttonAboutGame = new System.Windows.Forms.Button();
            this.buttonAboutCreators = new System.Windows.Forms.Button();
            this.buttonRating = new System.Windows.Forms.Button();
            this.buttonContinue = new System.Windows.Forms.Button();
            this.back_user = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonNewGame
            // 
            this.buttonNewGame.Location = new System.Drawing.Point(274, 38);
            this.buttonNewGame.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonNewGame.Name = "buttonNewGame";
            this.buttonNewGame.Size = new System.Drawing.Size(134, 42);
            this.buttonNewGame.TabIndex = 0;
            this.buttonNewGame.Text = "Начать новую игру";
            this.buttonNewGame.UseVisualStyleBackColor = true;
            this.buttonNewGame.Click += new System.EventHandler(this.buttonNewGame_Click);
            // 
            // buttonAboutGame
            // 
            this.buttonAboutGame.Location = new System.Drawing.Point(274, 334);
            this.buttonAboutGame.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonAboutGame.Name = "buttonAboutGame";
            this.buttonAboutGame.Size = new System.Drawing.Size(134, 37);
            this.buttonAboutGame.TabIndex = 1;
            this.buttonAboutGame.Text = "Об игре";
            this.buttonAboutGame.UseVisualStyleBackColor = true;
            this.buttonAboutGame.Click += new System.EventHandler(this.buttonAboutGame_Click);
            // 
            // buttonAboutCreators
            // 
            this.buttonAboutCreators.Location = new System.Drawing.Point(274, 253);
            this.buttonAboutCreators.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonAboutCreators.Name = "buttonAboutCreators";
            this.buttonAboutCreators.Size = new System.Drawing.Size(134, 37);
            this.buttonAboutCreators.TabIndex = 2;
            this.buttonAboutCreators.Text = "О разработчиках";
            this.buttonAboutCreators.UseVisualStyleBackColor = true;
            this.buttonAboutCreators.Click += new System.EventHandler(this.buttonAboutCreators_Click);
            // 
            // buttonRating
            // 
            this.buttonRating.Location = new System.Drawing.Point(274, 177);
            this.buttonRating.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonRating.Name = "buttonRating";
            this.buttonRating.Size = new System.Drawing.Size(134, 37);
            this.buttonRating.TabIndex = 3;
            this.buttonRating.Text = "Рейтинг";
            this.buttonRating.UseVisualStyleBackColor = true;
            this.buttonRating.Click += new System.EventHandler(this.buttonRating_Click);
            // 
            // buttonContinue
            // 
            this.buttonContinue.Location = new System.Drawing.Point(274, 109);
            this.buttonContinue.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonContinue.Name = "buttonContinue";
            this.buttonContinue.Size = new System.Drawing.Size(134, 37);
            this.buttonContinue.TabIndex = 4;
            this.buttonContinue.Text = "Продолжить";
            this.buttonContinue.UseVisualStyleBackColor = true;
            this.buttonContinue.Click += new System.EventHandler(this.buttonContinue_Click);
            // 
            // back_user
            // 
            this.back_user.Location = new System.Drawing.Point(274, 404);
            this.back_user.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.back_user.Name = "back_user";
            this.back_user.Size = new System.Drawing.Size(134, 37);
            this.back_user.TabIndex = 5;
            this.back_user.Text = "Выйти";
            this.back_user.UseVisualStyleBackColor = true;
            this.back_user.Click += new System.EventHandler(this.back_user_Click);
            // 
            // UserMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 473);
            this.Controls.Add(this.back_user);
            this.Controls.Add(this.buttonContinue);
            this.Controls.Add(this.buttonRating);
            this.Controls.Add(this.buttonAboutCreators);
            this.Controls.Add(this.buttonAboutGame);
            this.Controls.Add(this.buttonNewGame);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "UserMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserMenu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonNewGame;
        private System.Windows.Forms.Button buttonAboutGame;
        private System.Windows.Forms.Button buttonAboutCreators;
        private System.Windows.Forms.Button buttonRating;
        private System.Windows.Forms.Button buttonContinue;
        private System.Windows.Forms.Button back_user;
    }
}