using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.IO.File;
using Extensions = Updater.Core.Extensions;
using Helpers = Updater.Core.Helpers;

namespace Updater.Forms
{
    public partial class MainForm : Form
    {
        #region Field Region

        private byte[] _checksums;

        private bool _formMoving;
        private Point _startPoint = new Point(0, 0);

        private int _totalDownloads;
        private int _completedDownloads;

        private readonly Queue<string> _downloadQueue = new Queue<string>();

        #endregion

        #region Constructor Region

        public MainForm()
        {
            InitializeComponent();

            updatePage.Url = new Uri(Helpers.ChangeLogAddress);

            progressFile.BackgroundImage = Images.bar_background;
            progressFile.ProgressImage = Images.progress_yellow;

            progressTotal.BackgroundImage = Images.bar_background;
            progressTotal.ProgressImage = Images.progress_blue;

            lblFileProgress.Visible = false;
            lblTotalProgress.Visible = false;

            btnStart.Visible = false;

            btnStart.Click += BtnStart_Click;
            btnMinimize.Click += BtnMinimize_Click;
            btnClose.Click += BtnClose_Click;

            pnlHeader.MouseDown += PnlHeader_MouseDown;
            pnlHeader.MouseMove += PnlHeader_MouseMove;
            pnlHeader.MouseUp += PnlHeader_MouseUp;
        }

        #endregion

        #region Method Region
        private void UpdateSelf()
        {
            const string existingUpdater = "Launcher.exe";
            const string replacementUpdater = "updater_latest.exe";
            var appName = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);

            // We're running the replacement updater - delete the old app
            if (appName.Equals(replacementUpdater))
            {
                if (Exists(existingUpdater))
                {
                    Extensions.ForceDelete(existingUpdater);
                }

                Copy(replacementUpdater, existingUpdater);

                var p = new ProcessStartInfo(existingUpdater)
                { UseShellExecute = true };

                Process.Start(p);
                Application.Exit();
                return;
            }

            // Delete any replacement updaters we have lying around.
            if (Exists(replacementUpdater))
            {
                Extensions.ForceDelete(replacementUpdater);
            }

            // Next download the replacement again and check if the hashes are out of sync.
            using (var wc = new WebClient())
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Ssl3;
                wc.DownloadFile(new Uri(Helpers.UpdaterAddress), replacementUpdater);                      

                var remoteHash = SHA256.GetChecksum(replacementUpdater);
                var localHash = SHA256.GetChecksum(existingUpdater);
                 
                if (!remoteHash.Equals(localHash))
                {
                    if (!Exists(replacementUpdater)) return;

                    var p = new ProcessStartInfo(replacementUpdater)
                    {
                        UseShellExecute = true
                    };

                    Process.Start(p);
                    Application.Exit();
                    return;
                }

                // Files matched - delete downloaded file
                if (Exists(replacementUpdater))
                {
                    Extensions.ForceDelete(replacementUpdater);
                }
            }
        }

        private void UpdateProgress(string progress)
        {
            lblProgress.Text = progress;
        }

        private void GetServerChecksums()
        {
            using (var wc = new WebClient())
            {
                wc.DownloadDataCompleted += WebClient_DownloadDataChecksumCompleted;
                wc.DownloadDataAsync(new Uri(Helpers.ChecksumAddress));
            }
        }

        private void CheckFiles(object sender, DoWorkEventArgs e)
        {
            var files = e.Argument as XElement;

            if (files == null) return;

            var totalFiles = files.Elements().Count();
            _totalDownloads = totalFiles;

            foreach (var file in files.Elements())
            {
                var remotePath = file.Attribute("name").Value;
                var remoteHash = file.Attribute("hash").Value;

                var fileRequiresUpdate = false;
                var localPath = Path.Combine(Helpers.LocalPath, remotePath);

                if (Exists(localPath))
                {
                    var localHash = SHA256.GetChecksum(localPath);

                    if (!Equals(localHash, remoteHash))
                    {
                        fileRequiresUpdate = true;
                    }
                }

                else
                {
                    fileRequiresUpdate = true;
                }

                if (fileRequiresUpdate)
                {
                    // If path doesn't exist, create it
                    if (localPath.Equals(""))
                    {
                        _totalDownloads -= 1;
                        continue;
                    }


                    var directoryName = Path.GetDirectoryName(localPath);

                    if (directoryName == null || directoryName.Equals(""))
                    {
                        _totalDownloads -= 1;
                        continue;
                    }


                    // Create any missing directories that need to be made.
                    if (!Directory.Exists(directoryName))
                    {
                        Directory.CreateDirectory(directoryName);
                    }


                    // Skip any locked files.
                    if (Exists(localPath))
                    {
                        if (Extensions.FileLocked(localPath))
                        {
                            _totalDownloads -= 1;
                            continue;
                        }
                    }


                    // Delete the old file
                    if (Exists(localPath))
                    {
                        Delete(localPath);
                    }


                    if (!remotePath.Equals(""))
                    {
                        _downloadQueue.Enqueue(remotePath);
                    }
                }

                else
                {
                    _totalDownloads -= 1;
                }

            }

            progressTotal.Maximum = _totalDownloads;
        }

        private void UpdateComplete()
        {
            UpdateProgress(_totalDownloads == 0 ? Helpers.UpdateNotFound : Helpers.UpdateCompleted);

            progressFile.Value = progressFile.Maximum;
            progressTotal.Value = progressTotal.Maximum;

            lblFileProgress.Visible = false;
            lblTotalProgress.Visible = false;

            btnStart.Visible = true;

            var checksumsPath = Path.Combine(Helpers.LocalPath, "checksums.xml");

            // Save the checksums
            File.WriteAllBytes(checksumsPath, _checksums);
        }

        private void UpdateFail()
        {
            UpdateProgress(Helpers.UnknownError);

            progressFile.Value = progressFile.Maximum;
            progressTotal.Value = progressTotal.Maximum;

            lblFileProgress.Visible = false;
            lblTotalProgress.Visible = false;

            btnStart.Visible = true;
        }

        private void ConnectionFail()
        {
            UpdateProgress(Helpers.ConnectionFailed);

            progressFile.Value = progressFile.Maximum;
            progressTotal.Value = progressTotal.Maximum;

            lblFileProgress.Visible = false;
            lblTotalProgress.Visible = false;

            btnStart.Visible = true;
        }

        private void DownloadFile()
        {
            // Get the path and dequeue the download
            if (_downloadQueue.Count > 0)
            {
                var fileAddress = _downloadQueue.Dequeue();

                // Get the name of the file
                var lastIndex = fileAddress.LastIndexOf("\\", StringComparison.Ordinal);
                var fileName = fileAddress.Substring(lastIndex + 1);

                var fileUri = new Uri(Path.Combine(Helpers.DirectoryAddress, fileAddress));
                var localPath = Path.Combine(Helpers.LocalPath, fileAddress);

                using (var wc = new WebClient())
                {
                    UpdateProgress($"Downloading {fileName}");

                    wc.DownloadProgressChanged += WebClient_DownloadProgressChanged;
                    wc.DownloadFileCompleted += WebClient_DownloadFileCompleted;
                    wc.DownloadFileAsync(fileUri, localPath);
                }
            }

            else
            {
                UpdateComplete();
            }
        }

        #endregion

        #region Event Handler Region

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            var totalSize = (int)(e.TotalBytesToReceive / 1024);
            var downloadedSize = (int)(e.BytesReceived / 1024);

            if (totalSize == 0)
            {
                totalSize = 1;
                downloadedSize = 1;
            }

            progressFile.Maximum = totalSize;
            progressFile.Value = downloadedSize;

            var progressText = $"{downloadedSize} KB / {totalSize} KB";
            lblFileProgress.Text = progressText;

            if (!lblFileProgress.Visible)
                lblFileProgress.Visible = true;
        }

        private void WebClient_DownloadDataChecksumCompleted(object sender, DownloadDataCompletedEventArgs downloadDataCompletedEventArgs)
        {
            _checksums = downloadDataCompletedEventArgs.Result;

            if (_checksums == null)
            {
                UpdateFail();
                return;
            }

            var checksumsPath = Path.Combine(Helpers.LocalPath, "checksums.xml");

            if (Exists(checksumsPath))
            {
                if (_checksums.SequenceEqual(File.ReadAllBytes(checksumsPath)))
                {
                    // They match no update needed
                    UpdateComplete();
                    return;
                }
            }

            var worker = new BackgroundWorker();
            var files = XElement.Load(new MemoryStream(_checksums));

            worker.DoWork += CheckFiles; 
            worker.RunWorkerAsync(files);
            worker.RunWorkerCompleted += CheckUpdate;
        }

        private void WebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        { 
            _completedDownloads += 1;
            progressTotal.Value = _completedDownloads;

            var progressText = $"{_completedDownloads} / {_totalDownloads}" + " Files";
            lblTotalProgress.Text = progressText;

            if (!lblProgress.Visible)
            {
                lblProgress.Visible = true;
            }

            if (!lblTotalProgress.Visible)
            {
                lblTotalProgress.Visible = true;
            }

            if (_totalDownloads == _completedDownloads)
            { 
                UpdateComplete();
            }
            else
            {
                progressFile.Value = 0;
                DownloadFile();
            }            
        }

        private void CheckUpdate(object sender, AsyncCompletedEventArgs e)
        {
            if (_totalDownloads > 0)
            {
                progressTotal.Maximum = _totalDownloads;
                DownloadFile();
            }
            else
            {
                UpdateComplete();
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            var clientPath = Path.Combine(Helpers.LocalPath, "Client.exe");

            if (Exists(clientPath))
            {
                try
                {
                    var p = new ProcessStartInfo(clientPath)
                    {UseShellExecute = true};

                    Process.Start(p);
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            else
            {
                MessageBox.Show(Helpers.ClientNotFound);
            }

            Application.Exit();
        }

        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            _startPoint = new Point(e.X, e.Y);
            _formMoving = true;
        }

        private void PnlHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_formMoving) return;

            var p1 = new Point(e.X, e.Y);
            var p2 = PointToScreen(p1);
            var p3 = new Point(p2.X - _startPoint.X, p2.Y - _startPoint.Y);
            Location = p3;
        }

        private void PnlHeader_MouseUp(object sender, MouseEventArgs e)
        {
            _formMoving = false;
        }

        private void updatePage_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (Extensions.Ping())
            {
                updatePage.Visible = true;
            }   
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            if (Extensions.Connect())
            {
#if !DEBUG
                UpdateSelf();
#endif

                GetServerChecksums();
            }

            else
            {
                ConnectionFail();
            }
        }    

        #endregion

        private void updatePage_NewWindow(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Process.Start(updatePage.StatusText.ToString());
        }
    }
}
