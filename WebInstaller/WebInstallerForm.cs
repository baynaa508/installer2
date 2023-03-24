using IWshRuntimeLibrary;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace Project
{
    public partial class WebInstallerForm : Form
    {
        private string destinationFolder = string.Empty;
        private string destinationPath = string.Empty;
        private string shortcutPath = string.Empty;
        private const string url = "https://app.masu.mn/files/Masu.zip";
        private int extractionProgressValue;
        private bool isCanceled;

        public WebInstallerForm()
        {
            InitializeComponent();
            AdminRelauncher();
            Init();
        }
        private void Init()
        {
            this.Load += (s, e) =>
            {
                teDest.Text = "C:\\Masu";

                if (isInstalled())
                {
                    lblState.Text = "Masu энэ компьютерд аль хэдийн суулгасан байна!";
                    btnInstall.Visible = btnBrowse.Enabled = false;
                    btnFinish.Visible = pbRepair.Visible = pbUninstall.Visible = true;
                }
            };

            pbUninstall.MouseDown += (s, e) => pbUninstall.BackColor = Color.Gray;
            pbUninstall.MouseUp += (s, e) => pbUninstall.BackColor = Color.White;
            pbRepair.MouseDown += (s, e) => pbRepair.BackColor = Color.Gray;
            pbRepair.MouseUp += (s, e) => pbRepair.BackColor = Color.White;
            pbUninstall.Click += (s, e) => Uninstall();

            pbRepair.Click += (s, e) =>
            {
                if (MessageBox.Show("Дахин суулгах уу?", "MASU", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Registry.LocalMachine.DeleteSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\MASU");
                    Registry.ClassesRoot.DeleteSubKeyTree(@"masu");
                    Application.Restart();
                    Environment.Exit(0);
                }
            };

            exitPb.Click += (s, e) => Application.Exit();

            btnFinish.Click += (s, e) =>
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\MASU");
                if (key == null && key.GetValue("InstallLocation") == null)
                    return;
                Run(key.GetValue("InstallLocation").ToString());
                Application.Exit();
            };

            btnBrowse.Click += (s, e) =>
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.Description = "MASU";

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    destinationFolder = fbd.SelectedPath;
                    teDest.Text = destinationFolder.Trim() + "\\Masu";
                }
            };

            btnInstall.Click += (s, e) => Download();

        }
        private bool isInstalled()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\MASU");
            return key != null && key.GetValue("InstallLocation") != null;
        }
        private void ExtractFilesWithProgress(string archivePath, string outputDirectory)
        {
            try
            {
                using (var archive = ZipFile.OpenRead(archivePath))
                {
                    var buffer = new byte[4096];
                    var totalBytesWritten = 0L;
                    var lastReportedProgress = -1;
                    var totalBytesToWrite = archive.Entries.Sum(e => e.Length);

                    foreach (var entry in archive.Entries)
                    {
                        if (entry.Length == 0)
                        {
                            continue;
                        }

                        var entryPath = Path.Combine(outputDirectory, entry.FullName);
                        Directory.CreateDirectory(Path.GetDirectoryName(entryPath));

                        using (var entryStream = entry.Open())
                        using (var outputStream = System.IO.File.Create(entryPath))
                        {
                            int bytesRead;
                            do
                            {
                                bytesRead = entryStream.Read(buffer, 0, buffer.Length);
                                outputStream.Write(buffer, 0, bytesRead);
                                totalBytesWritten += bytesRead;

                                extractionProgressValue = (int)Math.Floor((double)totalBytesWritten / totalBytesToWrite * 100);
                                if (extractionProgressValue != lastReportedProgress)
                                {
                                    progressBar.Value = extractionProgressValue;
                                    progressBar.Refresh();
                                    lblState.Text = $"Суулгаж байна... {progressBar.Value}  %";
                                    lastReportedProgress = extractionProgressValue;
                                    Application.DoEvents();
                                }
                            } while (bytesRead > 0);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        private bool IsRunAsAdmin()
        {
            try
            {
                WindowsIdentity id = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(id);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (Exception)
            {
                return false;
            }
        }
        private void AdminRelauncher()
        {
            if (!IsRunAsAdmin())
            {
                ProcessStartInfo proc = new ProcessStartInfo();
                proc.UseShellExecute = true;
                proc.WorkingDirectory = Environment.CurrentDirectory;
                proc.FileName = Assembly.GetEntryAssembly().Location.Replace(".dll", ".exe");

                proc.Verb = "runas";

                try
                {
                    Process.Start(proc);
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("This program must be run as an administrator! \n\n" + ex.ToString());
                }
            }
        }
        private bool CheckInternet()
        {
            try
            {
                Ping myPing = new Ping();
                String host = "google.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }
        }
        protected virtual bool IsFileLocked(FileInfo file)
        {
            try
            {
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                return true;
            }

            return false;
        }
        private bool CheckForVcredit()
        {
            string regKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\VisualStudio\12.0\VC\Runtimes\x86";
            object value = Registry.GetValue(regKey, "Installed", null);
            return value != null && (int)value == 1;
        }
        private bool CheckForDotNetFrameWork()
        {
            string regKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full";
            object value = Registry.GetValue(regKey, "Release", null);
            return value != null && (int)value >= 379893;
        }
        private void CreateShortcut(Environment.SpecialFolder folder)
        {
            string pathTo = Environment.GetFolderPath(folder);
            shortcutPath = Path.Combine(pathTo, "MASU.lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
            shortcut.TargetPath = destinationPath;
            shortcut.WorkingDirectory = destinationFolder;
            shortcut.Save();
        }
        private void Download()
        {
            if (!CheckInternet())
            {
                MessageBox.Show("Интернет холболт алга байна.", "Анхаар!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Cursor.Current = Cursors.WaitCursor;
            btnInstall.Visible = false;
            progressBar.Visible = true;
            exitPb.Visible = false;
            btnBrowse.Enabled = false;
            destinationFolder = Path.Combine(teDest.Text.Trim(), "");
            destinationPath = Path.Combine(destinationFolder, "MASU.exe");

            DirectorySecurity securityRules = new DirectorySecurity();
            securityRules.AddAccessRule(new FileSystemAccessRule("Users", FileSystemRights.FullControl, AccessControlType.Allow));

            DirectoryInfo di = Directory.CreateDirectory(destinationFolder, securityRules);

            var dir = new DirectoryInfo(di.FullName);
            if (dir.Exists)
                dir.Delete(true);

            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadProgressChanged += (s, e) =>
                    {
                        if (isCanceled) return;
                        btnCancel.Visible = true;
                        progressBar.Value = e.ProgressPercentage;
                        lblState.Text = $"Татаж байна... {progressBar.Value}  %";
                        Application.DoEvents();
                    };
                    btnCancel.Click += (s, e) =>
                    {
                        isCanceled = true;
                        client.CancelAsync();
                        exitPb.Visible = true;
                        lblState.Text = "Цуцлагдлаа.";
                        Application.DoEvents();
                    };
                    client.DownloadFileCompleted += (s, e) =>
                    {
                        if (isCanceled) return;
                        btnCancel.Enabled = false;
                        progressBar.Value = 0;
                        progressBar.Refresh();
                        Application.DoEvents();

                        Thread.Sleep(1000);

                        ExtractFilesWithProgress(destinationFolder + "\\MASU.zip", destinationFolder);

                        if (!IsFileLocked(new FileInfo(destinationFolder + "\\MASU.zip")) && !client.IsBusy)
                        {

                            CreateShortcut(Environment.SpecialFolder.Desktop);
                            CreateShortcut(Environment.SpecialFolder.StartMenu);

                            if (!CheckForDotNetFrameWork())
                            {

                            }
                            else if (!CheckForVcredit())
                            {

                            }

                            if (extractionProgressValue >= 100)
                            {
                                btnCancel.Visible = false;
                                btnFinish.Visible = true;
                                exitPb.Visible = true;
                                lblState.Text = "Амжилттай суулаа!";
                                if (System.IO.File.Exists(destinationFolder + "\\MASU.zip"))
                                    System.IO.File.Delete(destinationFolder + "\\MASU.zip");
                                CreateUrlScheme();
                                RegisterProgram();
                                CreateUninstaller();
                            }
                        }
                        else
                        {
                            lblState.Text = "Aлдаа!\n Дахин оролдоно уу.";
                            exitPb.Visible = true;
                            btnInstall.Visible = true;
                            return;
                        }
                    };

                    Directory.CreateDirectory(destinationFolder);
                    client.DownloadFileAsync(new Uri(url), destinationFolder + "\\MASU.zip");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Алдаа!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnInstall.Visible = true;
                exitPb.Visible = true;
            }
            finally { Cursor.Current = Cursors.Default; }
        }
        private void Run(string path)
        {
            if (string.IsNullOrEmpty(path))
                return;
            ProcessStartInfo info = new ProcessStartInfo(path + @"\MASU.exe");
            info.Domain = path;
            info.WorkingDirectory = path;
            info.Verb = "runas";
            try
            {
                Process.Start(info);
            }
            catch
            {

            }
        }
        private void CreateUrlScheme()
        {
            string appName = "Masu";
            string appScheme = "MASU";
            RegistryKey key = Registry.ClassesRoot.CreateSubKey(appScheme);
            key.SetValue("", appName);
            key.SetValue("URL Protocol", "URL:masu");
            RegistryKey shellKey = key.CreateSubKey(@"shell\open\command");
            string appPath = destinationFolder + @"\MASU.exe";
            shellKey.SetValue("", "\"" + appPath + "\" \"%1\"");
            key.Close();
        }
        private void CreateUninstaller()
        {
            string fileName = string.Concat(Process.GetCurrentProcess().ProcessName, ".exe");
            string filePath = Path.Combine(Environment.CurrentDirectory, fileName);
            System.IO.File.Copy(filePath, Path.Combine(destinationFolder, fileName));
        }
        private void Uninstall()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\MASU");
            string path = key.GetValue("InstallLocation").ToString();
            var dir = new DirectoryInfo(path);
            if (dir.Exists)
                dir.Delete(true);
            Registry.LocalMachine.DeleteSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\MASU");
            Registry.ClassesRoot.DeleteSubKeyTree(@"masu");
            MessageBox.Show("Устагдлаа!","MASU POS LLC",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            Application.Restart();
            Environment.Exit(0);
        }
        private void RegisterProgram()
        {
            string uninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            string fileName = "MASU";
            string version = "1.1.0";
            using (RegistryKey key = Registry.LocalMachine.CreateSubKey($"{uninstallKey}\\{fileName}"))
            {
                key.SetValue("DisplayName", fileName);
                key.SetValue("DisplayVersion", version);
                key.SetValue("UninstallString", destinationFolder + @"\MASUInstaller.exe");
                key.SetValue("InstallDate", DateTime.Now.ToShortDateString());
                key.SetValue("Publisher", "MasuPos LLC");
                key.SetValue("InstallLocation", destinationFolder);
            }
        }
    }
}
