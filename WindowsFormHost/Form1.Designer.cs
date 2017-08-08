namespace WindowsFormHost
{
    partial class Form1
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
            this.Startsvc = new System.Windows.Forms.Button();
            this.Stopsvc = new System.Windows.Forms.Button();
            this.svcstatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Startsvc
            // 
            this.Startsvc.Location = new System.Drawing.Point(43, 86);
            this.Startsvc.Name = "Startsvc";
            this.Startsvc.Size = new System.Drawing.Size(85, 23);
            this.Startsvc.TabIndex = 0;
            this.Startsvc.Text = "Start WCF svc";
            this.Startsvc.UseVisualStyleBackColor = true;
            this.Startsvc.Click += new System.EventHandler(this.Startsvc_Click);
            // 
            // Stopsvc
            // 
            this.Stopsvc.Location = new System.Drawing.Point(352, 86);
            this.Stopsvc.Name = "Stopsvc";
            this.Stopsvc.Size = new System.Drawing.Size(88, 23);
            this.Stopsvc.TabIndex = 1;
            this.Stopsvc.Text = "Stop WCF svc";
            this.Stopsvc.UseVisualStyleBackColor = true;
            this.Stopsvc.Click += new System.EventHandler(this.Stopsvc_Click);
            // 
            // svcstatus
            // 
            this.svcstatus.AutoSize = true;
            this.svcstatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.svcstatus.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.svcstatus.ForeColor = System.Drawing.SystemColors.WindowText;
            this.svcstatus.Location = new System.Drawing.Point(150, 165);
            this.svcstatus.Name = "svcstatus";
            this.svcstatus.Size = new System.Drawing.Size(37, 15);
            this.svcstatus.TabIndex = 2;
            this.svcstatus.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 263);
            this.Controls.Add(this.svcstatus);
            this.Controls.Add(this.Stopsvc);
            this.Controls.Add(this.Startsvc);
            this.Name = "Form1";
            this.Text = "WindowsForm Hosted WCF Service";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Startsvc;
        private System.Windows.Forms.Button Stopsvc;
        private System.Windows.Forms.Label svcstatus;
    }
}

