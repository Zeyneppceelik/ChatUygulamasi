namespace ChatClientGUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            txtUsername = new TextBox();
            btnConnect = new Button();
            txtChatBox = new TextBox();
            txtMessageBox = new TextBox();
            btnSend = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 25);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(111, 25);
            label1.TabIndex = 0;
            label1.Text = "Kullanıcı Adı:";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(178, 22);
            txtUsername.Margin = new Padding(4, 5, 4, 5);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(228, 31);
            txtUsername.TabIndex = 1;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(470, 15);
            btnConnect.Margin = new Padding(4, 5, 4, 5);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(107, 38);
            btnConnect.TabIndex = 2;
            btnConnect.Text = "Bağlan";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // txtChatBox
            // 
            txtChatBox.Location = new Point(17, 68);
            txtChatBox.Margin = new Padding(4, 5, 4, 5);
            txtChatBox.Multiline = true;
            txtChatBox.Name = "txtChatBox";
            txtChatBox.ReadOnly = true;
            txtChatBox.ScrollBars = ScrollBars.Vertical;
            txtChatBox.Size = new Size(627, 446);
            txtChatBox.TabIndex = 3;
            // 
            // txtMessageBox
            // 
            txtMessageBox.Location = new Point(13, 162);
            txtMessageBox.Margin = new Padding(4, 5, 4, 5);
            txtMessageBox.Name = "txtMessageBox";
            txtMessageBox.Size = new Size(511, 31);
            txtMessageBox.TabIndex = 4;
            txtMessageBox.KeyDown += txtMessageBox_KeyDown;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(543, 155);
            btnSend.Margin = new Padding(4, 5, 4, 5);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(107, 38);
            btnSend.TabIndex = 5;
            btnSend.Text = "Gönder";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(663, 585);
            Controls.Add(btnSend);
            Controls.Add(txtMessageBox);
            Controls.Add(txtChatBox);
            Controls.Add(btnConnect);
            Controls.Add(txtUsername);
            Controls.Add(label1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "Form1";
            Text = "Chat Uygulaması";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtChatBox;
        private System.Windows.Forms.TextBox txtMessageBox;
        private System.Windows.Forms.Button btnSend;
    }
}

