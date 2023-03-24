namespace Project
{
    partial class WebInstallerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebInstallerForm));
            this.lblState = new System.Windows.Forms.Label();
            this.teDest = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnFinish = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pbUninstall = new System.Windows.Forms.PictureBox();
            this.pbRepair = new System.Windows.Forms.PictureBox();
            this.exitPb = new System.Windows.Forms.PictureBox();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.btnInstall = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbUninstall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRepair)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitPb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblState
            // 
            this.lblState.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblState.Location = new System.Drawing.Point(55, 69);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(414, 72);
            this.lblState.TabIndex = 6;
            this.lblState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // teDest
            // 
            this.teDest.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teDest.Location = new System.Drawing.Point(55, 144);
            this.teDest.Name = "teDest";
            this.teDest.ReadOnly = true;
            this.teDest.Size = new System.Drawing.Size(314, 27);
            this.teDest.TabIndex = 8;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBrowse.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(375, 144);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(94, 27);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(55, 192);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(414, 23);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 10;
            this.progressBar.Visible = false;
            // 
            // btnFinish
            // 
            this.btnFinish.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(10)))), ((int)(((byte)(210)))));
            this.btnFinish.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFinish.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFinish.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnFinish.ForeColor = System.Drawing.Color.White;
            this.btnFinish.Location = new System.Drawing.Point(169, 228);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(186, 53);
            this.btnFinish.TabIndex = 0;
            this.btnFinish.Text = "Нээх";
            this.btnFinish.UseVisualStyleBackColor = false;
            this.btnFinish.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(361, 228);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(108, 34);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Цуцлах";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            // 
            // pbUninstall
            // 
            this.pbUninstall.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbUninstall.Image = global::Project.Properties.Resources.delete__1_;
            this.pbUninstall.Location = new System.Drawing.Point(15, 81);
            this.pbUninstall.Name = "pbUninstall";
            this.pbUninstall.Size = new System.Drawing.Size(34, 38);
            this.pbUninstall.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbUninstall.TabIndex = 17;
            this.pbUninstall.TabStop = false;
            this.pbUninstall.Visible = false;
            // 
            // pbRepair
            // 
            this.pbRepair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbRepair.Image = global::Project.Properties.Resources.hammer;
            this.pbRepair.Location = new System.Drawing.Point(15, 12);
            this.pbRepair.Name = "pbRepair";
            this.pbRepair.Size = new System.Drawing.Size(34, 37);
            this.pbRepair.TabIndex = 14;
            this.pbRepair.TabStop = false;
            this.pbRepair.Visible = false;
            // 
            // exitPb
            // 
            this.exitPb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exitPb.Image = ((System.Drawing.Image)(resources.GetObject("exitPb.Image")));
            this.exitPb.Location = new System.Drawing.Point(487, 1);
            this.exitPb.Name = "exitPb";
            this.exitPb.Size = new System.Drawing.Size(34, 31);
            this.exitPb.TabIndex = 13;
            this.exitPb.TabStop = false;
            // 
            // pbLogo
            // 
            this.pbLogo.Image = global::Project.Properties.Resources.masu_1;
            this.pbLogo.Location = new System.Drawing.Point(55, 1);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(414, 65);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLogo.TabIndex = 12;
            this.pbLogo.TabStop = false;
            // 
            // btnInstall
            // 
            this.btnInstall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(10)))), ((int)(((byte)(210)))));
            this.btnInstall.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInstall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInstall.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnInstall.ForeColor = System.Drawing.Color.White;
            this.btnInstall.Image = global::Project.Properties.Resources.download__2_;
            this.btnInstall.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInstall.Location = new System.Drawing.Point(169, 228);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(186, 53);
            this.btnInstall.TabIndex = 1;
            this.btnInstall.Text = "Суулгах";
            this.btnInstall.UseVisualStyleBackColor = false;
            // 
            // WebInstallerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(523, 293);
            this.ControlBox = false;
            this.Controls.Add(this.pbUninstall);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.pbRepair);
            this.Controls.Add(this.exitPb);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.teDest);
            this.Controls.Add(this.btnInstall);
            this.Controls.Add(this.lblState);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(539, 309);
            this.MinimumSize = new System.Drawing.Size(539, 309);
            this.Name = "WebInstallerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pbUninstall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbRepair)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exitPb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.TextBox teDest;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.PictureBox exitPb;
        private System.Windows.Forms.PictureBox pbRepair;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.PictureBox pbUninstall;
    }
}