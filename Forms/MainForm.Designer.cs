using Updater.Controls;

namespace Updater.Forms
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblProgress = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnClose = new Updater.Controls.ImageButton();
            this.btnMinimize = new Updater.Controls.ImageButton();
            this.updatePage = new System.Windows.Forms.WebBrowser();
            this.lblFileProgress = new System.Windows.Forms.Label();
            this.lblTotalProgress = new System.Windows.Forms.Label();
            this.progressFile = new Updater.Controls.ProgressBar();
            this.progressTotal = new Updater.Controls.ProgressBar();
            this.btnSettings = new Updater.Controls.ImageButton();
            this.btnStart = new Updater.Controls.ImageButton();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblProgress
            // 
            this.lblProgress.BackColor = System.Drawing.Color.Transparent;
            this.lblProgress.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblProgress.ForeColor = System.Drawing.Color.White;
            this.lblProgress.Location = new System.Drawing.Point(657, 569);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(135, 22);
            this.lblProgress.TabIndex = 5;
            this.lblProgress.Text = "Checking for updates...";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.Controls.Add(this.btnClose);
            this.pnlHeader.Controls.Add(this.btnMinimize);
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(800, 31);
            this.pnlHeader.TabIndex = 6;
            // 
            // btnClose
            // 
            this.btnClose.ButtonImage = global::Updater.Images.btn_close;
            this.btnClose.ButtonImageClicked = global::Updater.Images.btn_close_clicked;
            this.btnClose.ButtonImageHover = global::Updater.Images.btn_close_hover;
            this.btnClose.Location = new System.Drawing.Point(774, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(21, 21);
            this.btnClose.TabIndex = 8;
            // 
            // btnMinimize
            // 
            this.btnMinimize.ButtonImage = global::Updater.Images.btn_minimise;
            this.btnMinimize.ButtonImageClicked = global::Updater.Images.btn_minimise_clicked;
            this.btnMinimize.ButtonImageHover = global::Updater.Images.btn_minimise_hover;
            this.btnMinimize.Location = new System.Drawing.Point(754, 5);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(21, 21);
            this.btnMinimize.TabIndex = 7;
            // 
            // updatePage
            // 
            this.updatePage.IsWebBrowserContextMenuEnabled = false;
            this.updatePage.Location = new System.Drawing.Point(338, 33);
            this.updatePage.MinimumSize = new System.Drawing.Size(20, 20);
            this.updatePage.Name = "updatePage";
            this.updatePage.ScriptErrorsSuppressed = true;
            this.updatePage.ScrollBarsEnabled = false;
            this.updatePage.Size = new System.Drawing.Size(457, 447);
            this.updatePage.TabIndex = 7;
            this.updatePage.TabStop = false;
            this.updatePage.Visible = false;
            this.updatePage.WebBrowserShortcutsEnabled = false;
            this.updatePage.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.updatePage_DocumentCompleted);
            this.updatePage.NewWindow += new System.ComponentModel.CancelEventHandler(this.updatePage_NewWindow);
            // 
            // lblFileProgress
            // 
            this.lblFileProgress.BackColor = System.Drawing.Color.Transparent;
            this.lblFileProgress.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblFileProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(44)))), ((int)(((byte)(29)))));
            this.lblFileProgress.Location = new System.Drawing.Point(337, 487);
            this.lblFileProgress.Name = "lblFileProgress";
            this.lblFileProgress.Size = new System.Drawing.Size(270, 18);
            this.lblFileProgress.TabIndex = 11;
            this.lblFileProgress.Text = "365kB / 1,565kB";
            this.lblFileProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalProgress
            // 
            this.lblTotalProgress.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalProgress.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTotalProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(44)))), ((int)(((byte)(29)))));
            this.lblTotalProgress.Location = new System.Drawing.Point(337, 526);
            this.lblTotalProgress.Name = "lblTotalProgress";
            this.lblTotalProgress.Size = new System.Drawing.Size(270, 18);
            this.lblTotalProgress.TabIndex = 12;
            this.lblTotalProgress.Text = "0 / 316";
            this.lblTotalProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressFile
            // 
            this.progressFile.BackColor = System.Drawing.Color.Black;
            this.progressFile.Location = new System.Drawing.Point(337, 505);
            this.progressFile.Name = "progressFile";
            this.progressFile.ProgressImage = null;
            this.progressFile.Size = new System.Drawing.Size(270, 18);
            this.progressFile.TabIndex = 1;
            // 
            // progressTotal
            // 
            this.progressTotal.BackColor = System.Drawing.Color.Black;
            this.progressTotal.Location = new System.Drawing.Point(337, 544);
            this.progressTotal.Name = "progressTotal";
            this.progressTotal.ProgressImage = null;
            this.progressTotal.Size = new System.Drawing.Size(270, 18);
            this.progressTotal.TabIndex = 0;
            // 
            // btnSettings
            // 
            this.btnSettings.ButtonImage = null;
            this.btnSettings.ButtonImageClicked = null;
            this.btnSettings.ButtonImageHover = null;
            this.btnSettings.Location = new System.Drawing.Point(0, 0);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(0, 0);
            this.btnSettings.TabIndex = 0;
            // 
            // btnStart
            // 
            this.btnStart.ButtonImage = global::Updater.Images.btn_start;
            this.btnStart.ButtonImageClicked = global::Updater.Images.btn_start_clicked;
            this.btnStart.ButtonImageHover = global::Updater.Images.btn_start_hover;
            this.btnStart.Location = new System.Drawing.Point(613, 492);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(179, 71);
            this.btnStart.TabIndex = 2;
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Updater.Images.background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.ControlBox = false;
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.lblTotalProgress);
            this.Controls.Add(this.lblFileProgress);
            this.Controls.Add(this.updatePage);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.progressFile);
            this.Controls.Add(this.progressTotal);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nin Online Updater";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ProgressBar progressFile;
        private ProgressBar progressTotal;
        private ImageButton btnStart;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Panel pnlHeader;
        private ImageButton btnClose;
        private ImageButton btnMinimize;
        private System.Windows.Forms.WebBrowser updatePage;
        private System.Windows.Forms.Label lblFileProgress;
        private System.Windows.Forms.Label lblTotalProgress;
        private ImageButton btnSettings;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
    }
}

