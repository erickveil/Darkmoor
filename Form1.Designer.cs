namespace Darkmoor
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
            this.bu_newYear = new System.Windows.Forms.Button();
            this.bu_save = new System.Windows.Forms.Button();
            this.bu_load = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bu_newYear
            // 
            this.bu_newYear.Location = new System.Drawing.Point(12, 21);
            this.bu_newYear.Name = "bu_newYear";
            this.bu_newYear.Size = new System.Drawing.Size(132, 23);
            this.bu_newYear.TabIndex = 0;
            this.bu_newYear.Text = "Advance Year";
            this.bu_newYear.UseVisualStyleBackColor = true;
            this.bu_newYear.Click += new System.EventHandler(this.bu_newYear_Click);
            // 
            // bu_save
            // 
            this.bu_save.Location = new System.Drawing.Point(12, 145);
            this.bu_save.Name = "bu_save";
            this.bu_save.Size = new System.Drawing.Size(75, 23);
            this.bu_save.TabIndex = 1;
            this.bu_save.Text = "Save";
            this.bu_save.UseVisualStyleBackColor = true;
            this.bu_save.Click += new System.EventHandler(this.bu_save_Click);
            // 
            // bu_load
            // 
            this.bu_load.Location = new System.Drawing.Point(115, 145);
            this.bu_load.Name = "bu_load";
            this.bu_load.Size = new System.Drawing.Size(75, 23);
            this.bu_load.TabIndex = 2;
            this.bu_load.Text = "Load";
            this.bu_load.UseVisualStyleBackColor = true;
            this.bu_load.Click += new System.EventHandler(this.bu_load_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 189);
            this.Controls.Add(this.bu_load);
            this.Controls.Add(this.bu_save);
            this.Controls.Add(this.bu_newYear);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bu_newYear;
        private System.Windows.Forms.Button bu_save;
        private System.Windows.Forms.Button bu_load;
    }
}

