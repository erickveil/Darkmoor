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
            this.nud_originX = new System.Windows.Forms.NumericUpDown();
            this.nud_originY = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.nud_tierWidth = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_startWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_startHeight)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_resizeWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_resizeHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_originX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_originY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_tierWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // bu_newYear
            // 
            this.bu_newYear.Location = new System.Drawing.Point(32, 50);
            this.bu_newYear.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.bu_newYear.Name = "bu_newYear";
            this.bu_newYear.Size = new System.Drawing.Size(352, 55);
            this.bu_newYear.TabIndex = 0;
            this.bu_newYear.Text = "Advance Year";
            this.bu_newYear.UseVisualStyleBackColor = true;
            this.bu_newYear.Click += new System.EventHandler(this.bu_newYear_Click);
            // 
            // bu_save
            // 
            this.bu_save.Location = new System.Drawing.Point(438, 416);
            this.bu_save.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.bu_save.Name = "bu_save";
            this.bu_save.Size = new System.Drawing.Size(200, 55);
            this.bu_save.TabIndex = 1;
            this.bu_save.Text = "Save";
            this.bu_save.UseVisualStyleBackColor = true;
            this.bu_save.Click += new System.EventHandler(this.bu_save_Click);
            // 
            // bu_load
            // 
            this.bu_load.Location = new System.Drawing.Point(438, 492);
            this.bu_load.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.bu_load.Name = "bu_load";
            this.bu_load.Size = new System.Drawing.Size(200, 55);
            this.bu_load.TabIndex = 2;
            this.bu_load.Text = "Load";
            this.bu_load.UseVisualStyleBackColor = true;
            this.bu_load.Click += new System.EventHandler(this.bu_load_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(32, 119);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.groupBox1.Size = new System.Drawing.Size(407, 452);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "New World";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.nud_startWidth, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.nud_startHeight, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.nud_originX, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.nud_originY, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.but_newWorld, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.nud_tierWidth, 1, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(16, 45);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(374, 383);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // nud_startWidth
            // 
            this.nud_startWidth.Location = new System.Drawing.Point(158, 7);
            this.nud_startWidth.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.nud_startWidth.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nud_startWidth.Name = "nud_startWidth";
            this.nud_startWidth.Size = new System.Drawing.Size(197, 38);
            this.nud_startWidth.TabIndex = 0;
            this.nud_startWidth.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // nud_startHeight
            // 
            this.nud_startHeight.Location = new System.Drawing.Point(158, 70);
            this.nud_startHeight.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.nud_startHeight.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nud_startHeight.Name = "nud_startHeight";
            this.nud_startHeight.Size = new System.Drawing.Size(197, 38);
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
            this.label1.Location = new System.Drawing.Point(8, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "Width";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 63);
            this.label2.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "Height";
            // 
            // but_newWorld
            // 
            this.but_newWorld.Location = new System.Drawing.Point(158, 322);
            this.but_newWorld.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.but_newWorld.Name = "but_newWorld";
            this.but_newWorld.Size = new System.Drawing.Size(197, 54);
            this.but_newWorld.TabIndex = 4;
            this.but_newWorld.Text = "New World";
            this.but_newWorld.UseVisualStyleBackColor = true;
            this.but_newWorld.Click += new System.EventHandler(this.but_newWorld_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Location = new System.Drawing.Point(438, 119);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.groupBox2.Size = new System.Drawing.Size(339, 277);
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(16, 43);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(307, 210);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 32);
            this.label3.TabIndex = 0;
            this.label3.Text = "Width";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 69);
            this.label4.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 32);
            this.label4.TabIndex = 1;
            this.label4.Text = "Height";
            // 
            // nud_resizeWidth
            // 
            this.nud_resizeWidth.Location = new System.Drawing.Point(126, 7);
            this.nud_resizeWidth.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.nud_resizeWidth.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nud_resizeWidth.Name = "nud_resizeWidth";
            this.nud_resizeWidth.Size = new System.Drawing.Size(173, 38);
            this.nud_resizeWidth.TabIndex = 2;
            this.nud_resizeWidth.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // nud_resizeHeight
            // 
            this.nud_resizeHeight.Location = new System.Drawing.Point(126, 76);
            this.nud_resizeHeight.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.nud_resizeHeight.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nud_resizeHeight.Name = "nud_resizeHeight";
            this.nud_resizeHeight.Size = new System.Drawing.Size(173, 38);
            this.nud_resizeHeight.TabIndex = 3;
            this.nud_resizeHeight.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // but_resizeWorld
            // 
            this.but_resizeWorld.Location = new System.Drawing.Point(126, 145);
            this.but_resizeWorld.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.but_resizeWorld.Name = "but_resizeWorld";
            this.but_resizeWorld.Size = new System.Drawing.Size(173, 55);
            this.but_resizeWorld.TabIndex = 4;
            this.but_resizeWorld.Text = "Resize";
            this.but_resizeWorld.UseVisualStyleBackColor = true;
            this.but_resizeWorld.Click += new System.EventHandler(this.but_resizeWorld_Click);
            // 
            // nud_originX
            // 
            this.nud_originX.Location = new System.Drawing.Point(153, 129);
            this.nud_originX.Name = "nud_originX";
            this.nud_originX.Size = new System.Drawing.Size(204, 38);
            this.nud_originX.TabIndex = 5;
            // 
            // nud_originY
            // 
            this.nud_originY.Location = new System.Drawing.Point(153, 192);
            this.nud_originY.Name = "nud_originY";
            this.nud_originY.Size = new System.Drawing.Size(204, 38);
            this.nud_originY.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 32);
            this.label5.TabIndex = 7;
            this.label5.Text = "Origin X";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 189);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 32);
            this.label6.TabIndex = 8;
            this.label6.Text = "Origin Y";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 252);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(144, 32);
            this.label7.TabIndex = 9;
            this.label7.Text = "Tier Width";
            // 
            // nud_tierWidth
            // 
            this.nud_tierWidth.Location = new System.Drawing.Point(153, 255);
            this.nud_tierWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_tierWidth.Name = "nud_tierWidth";
            this.nud_tierWidth.Size = new System.Drawing.Size(202, 38);
            this.nud_tierWidth.TabIndex = 10;
            this.nud_tierWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 607);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bu_load);
            this.Controls.Add(this.bu_save);
            this.Controls.Add(this.bu_newYear);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            ((System.ComponentModel.ISupportInitialize)(this.nud_originX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_originY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_tierWidth)).EndInit();
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
        private System.Windows.Forms.NumericUpDown nud_originX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nud_originY;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nud_tierWidth;
    }
}

