namespace BulletJournal
{
    partial class AddFutureLog
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
            this.btn_add = new System.Windows.Forms.Button();
            this.chk_important = new System.Windows.Forms.CheckBox();
            this.cmb_taskType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.list_taskList = new System.Windows.Forms.ListBox();
            this.txt_description = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmb_taskMonth = new System.Windows.Forms.ComboBox();
            this.cmb_taskYear = new System.Windows.Forms.ComboBox();
            this.btn_delete = new System.Windows.Forms.Button();
            this.btn_edit = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(722, 461);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(75, 42);
            this.btn_add.TabIndex = 20;
            this.btn_add.Text = "Add";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // chk_important
            // 
            this.chk_important.AutoSize = true;
            this.chk_important.Location = new System.Drawing.Point(109, 200);
            this.chk_important.Name = "chk_important";
            this.chk_important.Size = new System.Drawing.Size(89, 21);
            this.chk_important.TabIndex = 19;
            this.chk_important.Text = "Important";
            this.chk_important.UseVisualStyleBackColor = true;
            // 
            // cmb_taskType
            // 
            this.cmb_taskType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_taskType.Items.AddRange(new object[] {
            "Task",
            "Event",
            "Notes",
            "Closed"});
            this.cmb_taskType.Location = new System.Drawing.Point(109, 154);
            this.cmb_taskType.Name = "cmb_taskType";
            this.cmb_taskType.Size = new System.Drawing.Size(304, 24);
            this.cmb_taskType.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 17);
            this.label3.TabIndex = 17;
            this.label3.Text = "Task Type:";
            // 
            // list_taskList
            // 
            this.list_taskList.FormattingEnabled = true;
            this.list_taskList.ItemHeight = 16;
            this.list_taskList.Location = new System.Drawing.Point(433, 29);
            this.list_taskList.Name = "list_taskList";
            this.list_taskList.Size = new System.Drawing.Size(364, 372);
            this.list_taskList.TabIndex = 16;
            this.list_taskList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.list_taskList_MouseUp);
            // 
            // txt_description
            // 
            this.txt_description.Location = new System.Drawing.Point(109, 115);
            this.txt_description.Name = "txt_description";
            this.txt_description.Size = new System.Drawing.Size(304, 22);
            this.txt_description.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "Description:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "Task Month:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 17);
            this.label4.TabIndex = 21;
            this.label4.Text = "Task Year:";
            // 
            // cmb_taskMonth
            // 
            this.cmb_taskMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_taskMonth.Location = new System.Drawing.Point(109, 36);
            this.cmb_taskMonth.Name = "cmb_taskMonth";
            this.cmb_taskMonth.Size = new System.Drawing.Size(304, 24);
            this.cmb_taskMonth.TabIndex = 23;
            // 
            // cmb_taskYear
            // 
            this.cmb_taskYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_taskYear.Location = new System.Drawing.Point(109, 75);
            this.cmb_taskYear.Name = "cmb_taskYear";
            this.cmb_taskYear.Size = new System.Drawing.Size(304, 24);
            this.cmb_taskYear.TabIndex = 24;
            // 
            // btn_delete
            // 
            this.btn_delete.Location = new System.Drawing.Point(560, 461);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(75, 42);
            this.btn_delete.TabIndex = 27;
            this.btn_delete.Text = "Delete";
            this.btn_delete.UseVisualStyleBackColor = true;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // btn_edit
            // 
            this.btn_edit.Location = new System.Drawing.Point(641, 461);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(75, 42);
            this.btn_edit.TabIndex = 26;
            this.btn_edit.Text = "Edit";
            this.btn_edit.UseVisualStyleBackColor = true;
            this.btn_edit.Click += new System.EventHandler(this.btn_edit_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(722, 413);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(75, 42);
            this.btn_clear.TabIndex = 25;
            this.btn_clear.Text = "Clear";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(813, 30);
            this.menuStrip1.TabIndex = 28;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 26);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(123, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // AddFutureLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 532);
            this.Controls.Add(this.btn_delete);
            this.Controls.Add(this.btn_edit);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.cmb_taskYear);
            this.Controls.Add(this.cmb_taskMonth);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.chk_important);
            this.Controls.Add(this.cmb_taskType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.list_taskList);
            this.Controls.Add(this.txt_description);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AddFutureLog";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "<••> New Future Log";
            this.Load += new System.EventHandler(this.AddFutureLog_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.CheckBox chk_important;
        private System.Windows.Forms.ComboBox cmb_taskType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox list_taskList;
        private System.Windows.Forms.TextBox txt_description;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmb_taskMonth;
        private System.Windows.Forms.ComboBox cmb_taskYear;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.Button btn_edit;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
    }
}