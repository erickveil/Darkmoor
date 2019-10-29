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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.nud_startWidth = new System.Windows.Forms.NumericUpDown();
            this.nud_startHeight = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.but_newWorld = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_startWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_startHeight)).BeginInit();
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
            this.bu_save.Location = new System.Drawing.Point(12, 172);
            this.bu_save.Name = "bu_save";
            this.bu_save.Size = new System.Drawing.Size(75, 23);
            this.bu_save.TabIndex = 1;
            this.bu_save.Text = "Save";
            this.bu_save.UseVisualStyleBackColor = true;
            this.bu_save.Click += new System.EventHandler(this.bu_save_Click);
            // 
            // bu_load
            // 
            this.bu_load.Location = new System.Drawing.Point(93, 172);
            this.bu_load.Name = "bu_load";
            this.bu_load.Size = new System.Drawing.Size(75, 23);
            this.bu_load.TabIndex = 2;
            this.bu_load.Text = "Load";
            this.bu_load.UseVisualStyleBackColor = true;
            this.bu_load.Click += new System.EventHandler(this.bu_load_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(206, 116);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "New World";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.52083F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.47917F));
            this.tableLayoutPanel1.Controls.Add(this.nud_startWidth, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.nud_startHeight, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.but_newWorld, 1, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(192, 88);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // nud_startWidth
            // 
            this.nud_startWidth.Location = new System.Drawing.Point(99, 3);
            this.nud_startWidth.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nud_startWidth.Name = "nud_startWidth";
            this.nud_startWidth.Size = new System.Drawing.Size(90, 20);
            this.nud_startWidth.TabIndex = 0;
            this.nud_startWidth.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // nud_startHeight
            // 
            this.nud_startHeight.Location = new System.Drawing.Point(99, 27);
            this.nud_startHeight.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nud_startHeight.Name = "nud_startHeight";
            this.nud_startHeight.Size = new System.Drawing.Size(90, 20);
            this.nud_startHeight.TabIndex = 1;
            this.nud_startHeight.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Width";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Height";
            // 
            // but_newWorld
            // 
            this.but_newWorld.Location = new System.Drawing.Point(99, 51);
            this.but_newWorld.Name = "but_newWorld";
            this.but_newWorld.Size = new System.Drawing.Size(75, 31);
            this.but_newWorld.TabIndex = 4;
            this.but_newWorld.Text = "New World";
            this.but_newWorld.UseVisualStyleBackColor = true;
            this.but_newWorld.Click += new System.EventHandler(this.but_newWorld_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(229, 202);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bu_load);
            this.Controls.Add(this.bu_save);
            this.Controls.Add(this.bu_newYear);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_startWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_startHeight)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bu_newYear;
        private System.Windows.Forms.Button bu_save;
        private System.Windows.Forms.Button bu_load;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.NumericUpDown nud_startWidth;
        private System.Windows.Forms.NumericUpDown nud_startHeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button but_newWorld;
    }
}

