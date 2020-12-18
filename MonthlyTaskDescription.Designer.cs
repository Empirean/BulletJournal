
namespace BulletJournal
{
    partial class MonthlyTaskDescription
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonthlyTaskDescription));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmb_taskType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chk_taskIsImportant = new System.Windows.Forms.CheckBox();
            this.txt_currentTaskDescription = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(442, 28);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // cmb_taskType
            // 
            this.cmb_taskType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_taskType.Items.AddRange(new object[] {
            "Task",
            "Event",
            "Notes"});
            this.cmb_taskType.Location = new System.Drawing.Point(106, 164);
            this.cmb_taskType.Name = "cmb_taskType";
            this.cmb_taskType.Size = new System.Drawing.Size(304, 24);
            this.cmb_taskType.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 17);
            this.label3.TabIndex = 19;
            this.label3.Text = "Task Type:";
            // 
            // chk_taskIsImportant
            // 
            this.chk_taskIsImportant.AutoSize = true;
            this.chk_taskIsImportant.Location = new System.Drawing.Point(106, 204);
            this.chk_taskIsImportant.Name = "chk_taskIsImportant";
            this.chk_taskIsImportant.Size = new System.Drawing.Size(89, 21);
            this.chk_taskIsImportant.TabIndex = 17;
            this.chk_taskIsImportant.Text = "Important";
            this.chk_taskIsImportant.UseVisualStyleBackColor = true;
            // 
            // txt_currentTaskDescription
            // 
            this.txt_currentTaskDescription.Location = new System.Drawing.Point(106, 55);
            this.txt_currentTaskDescription.Name = "txt_currentTaskDescription";
            this.txt_currentTaskDescription.Size = new System.Drawing.Size(304, 93);
            this.txt_currentTaskDescription.TabIndex = 16;
            this.txt_currentTaskDescription.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "Description:";
            // 
            // MonthlyTaskDescription
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 242);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.cmb_taskType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chk_taskIsImportant);
            this.Controls.Add(this.txt_currentTaskDescription);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MonthlyTaskDescription";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Monthly Task";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ComboBox cmb_taskType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chk_taskIsImportant;
        private System.Windows.Forms.RichTextBox txt_currentTaskDescription;
        private System.Windows.Forms.Label label1;
    }
}