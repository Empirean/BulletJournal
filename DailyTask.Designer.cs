namespace BulletJournal
{
    partial class DailyTask
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
            this.lbl_startdate = new System.Windows.Forms.Label();
            this.txt_taskDate = new System.Windows.Forms.TextBox();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_description = new System.Windows.Forms.TextBox();
            this.list_taskList = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_taskType = new System.Windows.Forms.ComboBox();
            this.chk_important = new System.Windows.Forms.CheckBox();
            this.btn_add = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_edit = new System.Windows.Forms.Button();
            this.btn_delete = new System.Windows.Forms.Button();
            this.txt_enddate = new System.Windows.Forms.TextBox();
            this.lbl_enddate = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_startdate
            // 
            this.lbl_startdate.AutoSize = true;
            this.lbl_startdate.Location = new System.Drawing.Point(19, 44);
            this.lbl_startdate.Name = "lbl_startdate";
            this.lbl_startdate.Size = new System.Drawing.Size(77, 17);
            this.lbl_startdate.TabIndex = 0;
            this.lbl_startdate.Text = "Task Date:";
            // 
            // txt_taskDate
            // 
            this.txt_taskDate.Enabled = false;
            this.txt_taskDate.Location = new System.Drawing.Point(113, 44);
            this.txt_taskDate.Name = "txt_taskDate";
            this.txt_taskDate.Size = new System.Drawing.Size(304, 22);
            this.txt_taskDate.TabIndex = 1;
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(113, 127);
            this.monthCalendar1.MaxSelectionCount = 1;
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 2;
            this.monthCalendar1.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 352);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Description:";
            // 
            // txt_description
            // 
            this.txt_description.Location = new System.Drawing.Point(113, 352);
            this.txt_description.Name = "txt_description";
            this.txt_description.Size = new System.Drawing.Size(304, 22);
            this.txt_description.TabIndex = 4;
            // 
            // list_taskList
            // 
            this.list_taskList.FormattingEnabled = true;
            this.list_taskList.ItemHeight = 16;
            this.list_taskList.Location = new System.Drawing.Point(437, 37);
            this.list_taskList.Name = "list_taskList";
            this.list_taskList.Size = new System.Drawing.Size(364, 372);
            this.list_taskList.TabIndex = 5;
            this.list_taskList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.list_taskList_MouseUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 394);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Task Type:";
            // 
            // cmb_taskType
            // 
            this.cmb_taskType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_taskType.Items.AddRange(new object[] {
            "Task",
            "Event",
            "Notes",
            "Closed"});
            this.cmb_taskType.Location = new System.Drawing.Point(113, 391);
            this.cmb_taskType.Name = "cmb_taskType";
            this.cmb_taskType.Size = new System.Drawing.Size(304, 24);
            this.cmb_taskType.TabIndex = 7;
            // 
            // chk_important
            // 
            this.chk_important.AutoSize = true;
            this.chk_important.Location = new System.Drawing.Point(113, 437);
            this.chk_important.Name = "chk_important";
            this.chk_important.Size = new System.Drawing.Size(89, 21);
            this.chk_important.TabIndex = 9;
            this.chk_important.Text = "Important";
            this.chk_important.UseVisualStyleBackColor = true;
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(726, 469);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(75, 42);
            this.btn_add.TabIndex = 10;
            this.btn_add.Text = "Add";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(813, 28);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(123, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(726, 421);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(75, 42);
            this.btn_clear.TabIndex = 12;
            this.btn_clear.Text = "Clear";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_edit
            // 
            this.btn_edit.Location = new System.Drawing.Point(645, 469);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(75, 42);
            this.btn_edit.TabIndex = 13;
            this.btn_edit.Text = "Edit";
            this.btn_edit.UseVisualStyleBackColor = true;
            this.btn_edit.Click += new System.EventHandler(this.btn_edit_Click);
            // 
            // btn_delete
            // 
            this.btn_delete.Location = new System.Drawing.Point(564, 469);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(75, 42);
            this.btn_delete.TabIndex = 14;
            this.btn_delete.Text = "Delete";
            this.btn_delete.UseVisualStyleBackColor = true;
            this.btn_delete.Click += new System.EventHandler(this.btn_delete_Click);
            // 
            // txt_enddate
            // 
            this.txt_enddate.Enabled = false;
            this.txt_enddate.Location = new System.Drawing.Point(113, 84);
            this.txt_enddate.Name = "txt_enddate";
            this.txt_enddate.Size = new System.Drawing.Size(304, 22);
            this.txt_enddate.TabIndex = 16;
            this.txt_enddate.Visible = false;
            // 
            // lbl_enddate
            // 
            this.lbl_enddate.AutoSize = true;
            this.lbl_enddate.Location = new System.Drawing.Point(19, 84);
            this.lbl_enddate.Name = "lbl_enddate";
            this.lbl_enddate.Size = new System.Drawing.Size(71, 17);
            this.lbl_enddate.TabIndex = 15;
            this.lbl_enddate.Text = "End Date:";
            this.lbl_enddate.Visible = false;
            // 
            // DailyTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 532);
            this.Controls.Add(this.txt_enddate);
            this.Controls.Add(this.lbl_enddate);
            this.Controls.Add(this.btn_delete);
            this.Controls.Add(this.btn_edit);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.chk_important);
            this.Controls.Add(this.cmb_taskType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.list_taskList);
            this.Controls.Add(this.txt_description);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.txt_taskDate);
            this.Controls.Add(this.lbl_startdate);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DailyTask";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "<••> New Daily Task";
            this.Load += new System.EventHandler(this.AddDailyTask_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_startdate;
        private System.Windows.Forms.TextBox txt_taskDate;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_description;
        private System.Windows.Forms.ListBox list_taskList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_taskType;
        private System.Windows.Forms.CheckBox chk_important;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Button btn_edit;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.TextBox txt_enddate;
        private System.Windows.Forms.Label lbl_enddate;
    }
}