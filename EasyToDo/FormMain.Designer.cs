
namespace EasyToDo
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.panelSettings = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxDataStorage = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkAutostart = new EasyToDo.Controls.EasyCheckBox();
            this.checkConfirmDeletion = new EasyToDo.Controls.EasyCheckBox();
            this.panelSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSettings
            // 
            this.panelSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSettings.AutoScroll = true;
            this.panelSettings.Controls.Add(this.checkAutostart);
            this.panelSettings.Controls.Add(this.checkConfirmDeletion);
            this.panelSettings.Controls.Add(this.panel1);
            this.panelSettings.Controls.Add(this.buttonSearch);
            this.panelSettings.Controls.Add(this.label4);
            this.panelSettings.Controls.Add(this.textBoxDataStorage);
            this.panelSettings.Controls.Add(this.label1);
            this.panelSettings.Controls.Add(this.label3);
            this.panelSettings.Controls.Add(this.label2);
            this.panelSettings.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.panelSettings.Location = new System.Drawing.Point(9, 31);
            this.panelSettings.Margin = new System.Windows.Forms.Padding(0);
            this.panelSettings.Name = "panelSettings";
            this.panelSettings.Size = new System.Drawing.Size(365, 524);
            this.panelSettings.TabIndex = 0;
            this.panelSettings.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Location = new System.Drawing.Point(3, 133);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(356, 1);
            this.panel1.TabIndex = 9;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.buttonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonSearch.Location = new System.Drawing.Point(332, 96);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(27, 23);
            this.buttonSearch.TabIndex = 8;
            this.buttonSearch.Text = "...";
            this.buttonSearch.UseVisualStyleBackColor = false;
            this.buttonSearch.Click += new System.EventHandler(this.SelectFolderForDataStorage);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label4.Location = new System.Drawing.Point(8, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Storage path for data:";
            // 
            // textBoxDataStorage
            // 
            this.textBoxDataStorage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDataStorage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.textBoxDataStorage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxDataStorage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxDataStorage.ForeColor = System.Drawing.Color.Gainsboro;
            this.textBoxDataStorage.Location = new System.Drawing.Point(10, 96);
            this.textBoxDataStorage.Name = "textBoxDataStorage";
            this.textBoxDataStorage.Size = new System.Drawing.Size(317, 23);
            this.textBoxDataStorage.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(1, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "Settings";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(-1, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "License";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label2.Location = new System.Drawing.Point(8, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(352, 339);
            this.label2.TabIndex = 1;
            this.label2.Text = resources.GetString("label2.Text");
            // 
            // checkAutostart
            // 
            this.checkAutostart.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.checkAutostart.Location = new System.Drawing.Point(11, 56);
            this.checkAutostart.Name = "checkAutostart";
            this.checkAutostart.Size = new System.Drawing.Size(199, 17);
            this.checkAutostart.TabIndex = 11;
            this.checkAutostart.Text = "Launch EasyToDo with Windows.";
            this.checkAutostart.UseVisualStyleBackColor = true;
            this.checkAutostart.CheckedChanged += new System.EventHandler(this.CheckStartupChanged);
            // 
            // checkConfirmDeletion
            // 
            this.checkConfirmDeletion.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.checkConfirmDeletion.Location = new System.Drawing.Point(11, 34);
            this.checkConfirmDeletion.Name = "checkConfirmDeletion";
            this.checkConfirmDeletion.Size = new System.Drawing.Size(171, 17);
            this.checkConfirmDeletion.TabIndex = 10;
            this.checkConfirmDeletion.Text = "Confirm deletion of entries.";
            this.checkConfirmDeletion.UseVisualStyleBackColor = true;
            this.checkConfirmDeletion.CheckedChanged += new System.EventHandler(this.CheckConfirmDeletionChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(379, 574);
            this.Controls.Add(this.panelSettings);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.panelSettings.ResumeLayout(false);
            this.panelSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSettings;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxDataStorage;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Panel panel1;
        private Controls.EasyCheckBox checkConfirmDeletion;
        private Controls.EasyCheckBox checkAutostart;
    }
}

