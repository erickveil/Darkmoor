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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nud_resizeWidth = new System.Windows.Forms.NumericUpDown();
            this.nud_resizeHeight = new System.Windows.Forms.NumericUpDown();
            this.but_resizeWorld = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_startWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_startHeight)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_resizeWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_resizeHeight)).BeginInit();
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
            this.groupBox1.Size = new System.Drawing.Size(142, 116);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "New World";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.50794F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.49206F));
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(126, 88);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // nud_startWidth
            // 
            this.nud_startWidth.Location = new System.Drawing.Point(49, 3);
            this.nud_startWidth.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nud_startWidth.Name = "nud_startWidth";
            this.nud_startWidth.Size = new System.Drawing.Size(74, 20);
            this.nud_startWidth.TabIndex = 0;
            this.nud_startWidth.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // nud_startHeight
            // 
            this.nud_startHeight.Location = new System.Drawing.Point(49, 27);
            this.nud_startHeight.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nud_startHeight.Name = "nud_startHeight";
            this.nud_startHeight.Size = new System.Drawing.Size(74, 20);
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
            this.but_newWorld.Location = new System.Drawing.Point(49, 51);
            this.but_newWorld.Name = "but_newWorld";
            this.but_newWorld.Size = new System.Drawing.Size(74, 31);
            this.but_newWorld.TabIndex = 4;
            this.but_newWorld.Text = "New World";
            this.but_newWorld.UseVisualStyleBackColor = true;
            this.but_newWorld.Click += new System.EventHandler(this.but_newWorld_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Location = new System.Drawing.Point(160, 50);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(127, 116);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Resize World";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.73874F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 61.26126F));
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.nud_resizeWidth, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.nud_resizeHeight, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.but_resizeWorld, 1, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 18);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(115, 88);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Width";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Height";
            // 
            // nud_resizeWidth
            // 
            this.nud_resizeWidth.Location = new System.Drawing.Point(47, 3);
            this.nud_resizeWidth.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nud_resizeWidth.Name = "nud_resizeWidth";
            this.nud_resizeWidth.Size = new System.Drawing.Size(65, 20);
            this.nud_resizeWidth.TabIndex = 2;
            this.nud_resizeWidth.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // nud_resizeHeight
            // 
            this.nud_resizeHeight.Location = new System.Drawing.Point(47, 32);
            this.nud_resizeHeight.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nud_resizeHeight.Name = "nud_resizeHeight";
            this.nud_resizeHeight.Size = new System.Drawing.Size(65, 20);
            this.nud_resizeHeight.TabIndex = 3;
            this.nud_resizeHeight.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // but_resizeWorld
            // 
            this.but_resizeWorld.Location = new System.Drawing.Point(47, 61);
            this.but_resizeWorld.Name = "but_resizeWorld";
            this.but_resizeWorld.Size = new System.Drawing.Size(65, 23);
            this.but_resizeWorld.TabIndex = 4;
            this.but_resizeWorld.Text = "Resize";
            this.but_resizeWorld.UseVisualStyleBackColor = true;
            this.but_resizeWorld.Click += new System.EventHandler(this.but_resizeWorld_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 209);
            this.Controls.Add(this.groupBox2);
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
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_resizeWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_resizeHeight)).EndInit();
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
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nud_resizeWidth;
        private System.Windows.Forms.NumericUpDown nud_resizeHeight;
        private System.Windows.Forms.Button but_resizeWorld;
    }
}

