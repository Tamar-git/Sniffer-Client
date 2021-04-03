namespace SnifferClient
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.linkLabelInfo = new System.Windows.Forms.LinkLabel();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.Label();
            this.SignInButton = new System.Windows.Forms.Button();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxAnswer = new System.Windows.Forms.TextBox();
            this.comboBoxQuestions = new System.Windows.Forms.ComboBox();
            this.labelQuestion = new System.Windows.Forms.Label();
            this.labelEmail = new System.Windows.Forms.Label();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.linkLabelForgotPass = new System.Windows.Forms.LinkLabel();
            this.textBoxCaptcha = new System.Windows.Forms.TextBox();
            this.labelInfo = new System.Windows.Forms.Label();
            this.labelAnswer = new System.Windows.Forms.Label();
            this.refreshToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxRefresh = new System.Windows.Forms.PictureBox();
            this.pictureBoxCaptcha = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCaptcha)).BeginInit();
            this.SuspendLayout();
            // 
            // linkLabelInfo
            // 
            this.linkLabelInfo.AutoSize = true;
            this.linkLabelInfo.Font = new System.Drawing.Font("Narkisim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelInfo.Location = new System.Drawing.Point(154, 9);
            this.linkLabelInfo.Name = "linkLabelInfo";
            this.linkLabelInfo.Size = new System.Drawing.Size(81, 14);
            this.linkLabelInfo.TabIndex = 12;
            this.linkLabelInfo.TabStop = true;
            this.linkLabelInfo.Text = "Sign up here!";
            this.linkLabelInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelInfo_LinkClicked);
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Font = new System.Drawing.Font("Narkisim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.PasswordLabel.Location = new System.Drawing.Point(62, 136);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(56, 14);
            this.PasswordLabel.TabIndex = 11;
            this.PasswordLabel.Text = "Password";
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Font = new System.Drawing.Font("Narkisim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameLabel.Location = new System.Drawing.Point(62, 77);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(40, 14);
            this.NameLabel.TabIndex = 10;
            this.NameLabel.Text = "Name";
            // 
            // SignInButton
            // 
            this.SignInButton.Location = new System.Drawing.Point(185, 127);
            this.SignInButton.Name = "SignInButton";
            this.SignInButton.Size = new System.Drawing.Size(75, 23);
            this.SignInButton.TabIndex = 6;
            this.SignInButton.Text = "Sign In";
            this.SignInButton.UseVisualStyleBackColor = true;
            this.SignInButton.Click += new System.EventHandler(this.SignInButton_Click);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textBoxPassword.Location = new System.Drawing.Point(61, 153);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(100, 21);
            this.textBoxPassword.TabIndex = 1;
            // 
            // textBoxName
            // 
            this.textBoxName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textBoxName.Location = new System.Drawing.Point(61, 94);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(100, 21);
            this.textBoxName.TabIndex = 0;
            // 
            // textBoxAnswer
            // 
            this.textBoxAnswer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textBoxAnswer.Location = new System.Drawing.Point(62, 320);
            this.textBoxAnswer.Name = "textBoxAnswer";
            this.textBoxAnswer.Size = new System.Drawing.Size(212, 21);
            this.textBoxAnswer.TabIndex = 5;
            this.textBoxAnswer.Visible = false;
            // 
            // comboBoxQuestions
            // 
            this.comboBoxQuestions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxQuestions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.comboBoxQuestions.FormattingEnabled = true;
            this.comboBoxQuestions.Items.AddRange(new object[] {
            "What was your mothers maiden name?",
            "What was the name of your first pet?",
            "What elementry school did you go to?",
            "Where did you grow up in?"});
            this.comboBoxQuestions.Location = new System.Drawing.Point(61, 272);
            this.comboBoxQuestions.Name = "comboBoxQuestions";
            this.comboBoxQuestions.Size = new System.Drawing.Size(213, 23);
            this.comboBoxQuestions.TabIndex = 4;
            this.comboBoxQuestions.Visible = false;
            // 
            // labelQuestion
            // 
            this.labelQuestion.AutoSize = true;
            this.labelQuestion.Font = new System.Drawing.Font("Narkisim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelQuestion.Location = new System.Drawing.Point(62, 252);
            this.labelQuestion.Name = "labelQuestion";
            this.labelQuestion.Size = new System.Drawing.Size(139, 14);
            this.labelQuestion.TabIndex = 15;
            this.labelQuestion.Text = "Please choose a question";
            this.labelQuestion.Visible = false;
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Font = new System.Drawing.Font("Narkisim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelEmail.Location = new System.Drawing.Point(62, 191);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(37, 14);
            this.labelEmail.TabIndex = 17;
            this.labelEmail.Text = "Email";
            this.labelEmail.Visible = false;
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textBoxEmail.Location = new System.Drawing.Point(61, 208);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(195, 21);
            this.textBoxEmail.TabIndex = 3;
            this.textBoxEmail.Visible = false;
            // 
            // linkLabelForgotPass
            // 
            this.linkLabelForgotPass.AutoSize = true;
            this.linkLabelForgotPass.Font = new System.Drawing.Font("Narkisim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.linkLabelForgotPass.Location = new System.Drawing.Point(12, 34);
            this.linkLabelForgotPass.Name = "linkLabelForgotPass";
            this.linkLabelForgotPass.Size = new System.Drawing.Size(131, 14);
            this.linkLabelForgotPass.TabIndex = 19;
            this.linkLabelForgotPass.TabStop = true;
            this.linkLabelForgotPass.Text = "Forgot your password?";
            this.linkLabelForgotPass.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelForgotPass_LinkClicked);
            // 
            // textBoxCaptcha
            // 
            this.textBoxCaptcha.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textBoxCaptcha.Location = new System.Drawing.Point(61, 272);
            this.textBoxCaptcha.Name = "textBoxCaptcha";
            this.textBoxCaptcha.Size = new System.Drawing.Size(100, 21);
            this.textBoxCaptcha.TabIndex = 2;
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Font = new System.Drawing.Font("Narkisim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelInfo.Location = new System.Drawing.Point(12, 9);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(136, 14);
            this.labelInfo.TabIndex = 23;
            this.labelInfo.Text = "Don\'t have an account?";
            // 
            // labelAnswer
            // 
            this.labelAnswer.AutoSize = true;
            this.labelAnswer.Font = new System.Drawing.Font("Narkisim", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.labelAnswer.Location = new System.Drawing.Point(62, 303);
            this.labelAnswer.Name = "labelAnswer";
            this.labelAnswer.Size = new System.Drawing.Size(75, 14);
            this.labelAnswer.TabIndex = 24;
            this.labelAnswer.Text = "Your answer";
            this.labelAnswer.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SnifferClient.Properties.Resources.CAPCKET_logo_red_cut_removebg_preview;
            this.pictureBox1.Location = new System.Drawing.Point(174, 41);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 25;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBoxRefresh
            // 
            this.pictureBoxRefresh.Image = global::SnifferClient.Properties.Resources.png_clipart_cycle_arrow_logo_illustration_silhouette_angle_monochrome_graphy_fish_dolphin_refresh_angle_marine_mammal;
            this.pictureBoxRefresh.Location = new System.Drawing.Point(2, 186);
            this.pictureBoxRefresh.Name = "pictureBoxRefresh";
            this.pictureBoxRefresh.Size = new System.Drawing.Size(57, 48);
            this.pictureBoxRefresh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxRefresh.TabIndex = 22;
            this.pictureBoxRefresh.TabStop = false;
            this.refreshToolTip.SetToolTip(this.pictureBoxRefresh, "change captcha pattern");
            this.pictureBoxRefresh.Click += new System.EventHandler(this.pictureBoxRefresh_Click);
            // 
            // pictureBoxCaptcha
            // 
            this.pictureBoxCaptcha.Location = new System.Drawing.Point(61, 186);
            this.pictureBoxCaptcha.Name = "pictureBoxCaptcha";
            this.pictureBoxCaptcha.Size = new System.Drawing.Size(180, 80);
            this.pictureBoxCaptcha.TabIndex = 20;
            this.pictureBoxCaptcha.TabStop = false;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OldLace;
            this.ClientSize = new System.Drawing.Size(286, 352);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelAnswer);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.pictureBoxRefresh);
            this.Controls.Add(this.textBoxCaptcha);
            this.Controls.Add(this.pictureBoxCaptcha);
            this.Controls.Add(this.linkLabelForgotPass);
            this.Controls.Add(this.textBoxEmail);
            this.Controls.Add(this.labelEmail);
            this.Controls.Add(this.labelQuestion);
            this.Controls.Add(this.comboBoxQuestions);
            this.Controls.Add(this.textBoxAnswer);
            this.Controls.Add(this.linkLabelInfo);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.SignInButton);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginForm";
            this.Text = "CAPCKET login";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCaptcha)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabelInfo;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Button SignInButton;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxAnswer;
        private System.Windows.Forms.ComboBox comboBoxQuestions;
        private System.Windows.Forms.Label labelQuestion;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.LinkLabel linkLabelForgotPass;
        private System.Windows.Forms.PictureBox pictureBoxCaptcha;
        private System.Windows.Forms.TextBox textBoxCaptcha;
        private System.Windows.Forms.PictureBox pictureBoxRefresh;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Label labelAnswer;
        private System.Windows.Forms.ToolTip refreshToolTip;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

