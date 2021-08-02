using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Servidor
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.textBoxState = new System.Windows.Forms.TextBox();
            this.labelState = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxState
            // 
            this.textBoxState.Enabled = false;
            this.textBoxState.Location = new System.Drawing.Point(77, 12);
            this.textBoxState.Name = "textBoxState";
            this.textBoxState.ReadOnly = true;
            this.textBoxState.Size = new System.Drawing.Size(150, 31);
            this.textBoxState.TabIndex = 0;
            this.textBoxState.Text = "Disconnected";
            // 
            // labelState
            // 
            this.labelState.AutoSize = true;
            this.labelState.Location = new System.Drawing.Point(12, 15);
            this.labelState.Name = "labelState";
            this.labelState.Size = new System.Drawing.Size(51, 25);
            this.labelState.TabIndex = 1;
            this.labelState.Text = "State";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelState);
            this.Controls.Add(this.textBoxState);
            this.Name = "Form1";
            this.Text = "Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox textBoxState;
        private System.Windows.Forms.Label labelState;
    }
}

