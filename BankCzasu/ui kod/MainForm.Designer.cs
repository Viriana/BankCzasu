namespace BankCzasu
{
    partial class MainForm
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
            this.btnMyProfile = new System.Windows.Forms.Button();
            this.TestProfileForm = new System.Windows.Forms.Button();
            this.ChatButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnMyProfile
            // 
            this.btnMyProfile.Location = new System.Drawing.Point(12, 12);
            this.btnMyProfile.Name = "btnMyProfile";
            this.btnMyProfile.Size = new System.Drawing.Size(75, 23);
            this.btnMyProfile.TabIndex = 0;
            this.btnMyProfile.Text = "My profile";
            this.btnMyProfile.UseVisualStyleBackColor = true;
            this.btnMyProfile.Click += new System.EventHandler(this.btnMyProfile_Click);
            // 
            // TestProfileForm
            // 
            this.TestProfileForm.Location = new System.Drawing.Point(152, 12);
            this.TestProfileForm.Name = "TestProfileForm";
            this.TestProfileForm.Size = new System.Drawing.Size(161, 23);
            this.TestProfileForm.TabIndex = 1;
            this.TestProfileForm.Text = "TestProfileForm";
            this.TestProfileForm.UseVisualStyleBackColor = true;
            this.TestProfileForm.Click += new System.EventHandler(this.TestProfileForm_Click);
            // 
            // ChatButton
            // 
            this.ChatButton.Location = new System.Drawing.Point(400, 12);
            this.ChatButton.Name = "ChatButton";
            this.ChatButton.Size = new System.Drawing.Size(75, 23);
            this.ChatButton.TabIndex = 2;
            this.ChatButton.Text = "Chat";
            this.ChatButton.UseVisualStyleBackColor = true;
            this.ChatButton.Click += new System.EventHandler(this.ChatButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(131, 91);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(280, 173);
            this.button1.TabIndex = 3;
            this.button1.Text = "Search godlike teachers";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 377);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ChatButton);
            this.Controls.Add(this.TestProfileForm);
            this.Controls.Add(this.btnMyProfile);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnMyProfile;
        private System.Windows.Forms.Button TestProfileForm;
        private System.Windows.Forms.Button ChatButton;
        private System.Windows.Forms.Button button1;
    }
}