
namespace Cliente
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
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.listBoxMessages = new System.Windows.Forms.ListBox();
            this.labelState = new System.Windows.Forms.Label();
            this.textBoxState = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.Location = new System.Drawing.Point(12, 406);
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.Size = new System.Drawing.Size(658, 31);
            this.textBoxMessage.TabIndex = 0;
            this.textBoxMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxMessage_KeyPress);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(676, 404);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 34);
            this.button1.TabIndex = 1;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // listBoxMessages
            // 
            this.listBoxMessages.FormattingEnabled = true;
            this.listBoxMessages.ItemHeight = 25;
            this.listBoxMessages.Location = new System.Drawing.Point(12, 63);
            this.listBoxMessages.Name = "listBoxMessages";
            this.listBoxMessages.Size = new System.Drawing.Size(776, 329);
            this.listBoxMessages.TabIndex = 2;
            // 
            // labelState
            // 
            this.labelState.AutoSize = true;
            this.labelState.Location = new System.Drawing.Point(573, 15);
            this.labelState.Name = "labelState";
            this.labelState.Size = new System.Drawing.Size(51, 25);
            this.labelState.TabIndex = 4;
            this.labelState.Text = "State";
            // 
            // textBoxState
            // 
            this.textBoxState.Enabled = false;
            this.textBoxState.Location = new System.Drawing.Point(638, 12);
            this.textBoxState.Name = "textBoxState";
            this.textBoxState.ReadOnly = true;
            this.textBoxState.Size = new System.Drawing.Size(150, 31);
            this.textBoxState.TabIndex = 3;
            this.textBoxState.Text = "Disconnected";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelState);
            this.Controls.Add(this.textBoxState);
            this.Controls.Add(this.listBoxMessages);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxMessage);
            this.Name = "Form1";
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxMessage;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBoxMessages;
        private System.Windows.Forms.Label labelState;
        private System.Windows.Forms.TextBox textBoxState;
    }
}

