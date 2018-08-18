namespace KinectCourse
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
            this.lblGreeting = new System.Windows.Forms.Label();
            this.btnShowGreeting = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblGreeting
            // 
            this.lblGreeting.AutoSize = true;
            this.lblGreeting.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblGreeting.ForeColor = System.Drawing.Color.Red;
            this.lblGreeting.Location = new System.Drawing.Point(12, 81);
            this.lblGreeting.Name = "lblGreeting";
            this.lblGreeting.Size = new System.Drawing.Size(261, 46);
            this.lblGreeting.TabIndex = 0;
            this.lblGreeting.Text = "Hello World!!!";
            // 
            // btnShowGreeting
            // 
            this.btnShowGreeting.Location = new System.Drawing.Point(59, 168);
            this.btnShowGreeting.Name = "btnShowGreeting";
            this.btnShowGreeting.Size = new System.Drawing.Size(139, 42);
            this.btnShowGreeting.TabIndex = 1;
            this.btnShowGreeting.Text = "Show greetings";
            this.btnShowGreeting.UseVisualStyleBackColor = true;
            this.btnShowGreeting.Click += new System.EventHandler(this.btnShowGreeting_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnShowGreeting);
            this.Controls.Add(this.lblGreeting);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGreeting;
        private System.Windows.Forms.Button btnShowGreeting;
    }
}

