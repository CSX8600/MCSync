using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Updater
{
    public class Uninstaller
    {
        public event EventHandler<int> NumberOfTasks;
        public event EventHandler<string> TaskExecuting;
        public event EventHandler<string> NonTaskExecuting;
        public event EventHandler UninstallSucceeded;
        public event EventHandler UninstallFailed;

        public InstallationConfiguration InstallationConfiguration { get; set; }

        private List<string> _errors = new List<string>();
        public IReadOnlyCollection<string> Errors => _errors;

        public async void Uninstall()
        {
            NonTaskExecuting?.Invoke(this, "Fetching latest version information...");
            string version = await GetLatestVersion();
            if (string.IsNullOrEmpty(version))
            {
                UninstallFailed?.Invoke(this, new EventArgs());
                return;
            }

            List<string> fileList = await GetFileList(version);
            if (fileList == null || fileList.Count == 0)
            {
                UninstallFailed?.Invoke(this, new EventArgs());
                return;
            }

            NumberOfTasks?.Invoke(this, fileList.Count + 2); // +2 for registry and icons
            await DeleteFiles(fileList);
            await RemoveRegistryValues();
            await RemoveIcons();

            if (_errors.Any())
            {
                UninstallFailed?.Invoke(this, new EventArgs());
            }
            else
            {
                UninstallSucceeded?.Invoke(this, new EventArgs());
            }
        }

        private async Task<string> GetLatestVersion()
        {
            try
            {
                string apiPrefix = Program.InternalEdition ? "internalapi" : "api";
                HttpWebRequest webRequest = WebRequest.CreateHttp($"http://{apiPrefix}.mesabrook.com/mcsync/Version/GetLatest");
                //HttpWebRequest webRequest = WebRequest.CreateHttp("http://localhost:23895/Version/GetLatest");
                webRequest.Method = WebRequestMethods.Http.Get;
                HttpWebResponse response = (HttpWebResponse)await webRequest.GetResponseAsync();
                string version;
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    version = reader.ReadLine().Replace("\"", "");
                }

                if (string.IsNullOrEmpty(version))
                {
                    _errors.Add("Unable to determine latest version, which means can't determine files to remove.");
                    return null;
                }
                return version;
            }
            catch (Exception ex)
            {
                _errors.Add("An error occurred fetching latest version:\r\n" + ex.ToString());
                return null;
            }
        }

        private async Task<List<string>> GetFileList(string version)
        {
            NonTaskExecuting?.Invoke(this, "Fetching file list...");
            List<string> files = new List<string>();
            try
            {
                string updateFolder = Program.InternalEdition ? "MesaSuiteInternal" : "MCSyncNew";
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create($"ftp://www.clussmanproductions.com/support/{updateFolder}/updates/" + version);
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                ftpRequest.Credentials = new NetworkCredential("Reporting", "NetLogon");
                FtpWebResponse response = (FtpWebResponse)await ftpRequest.GetResponseAsync();

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    while(!reader.EndOfStream)
                    {
                        files.Add(reader.ReadLine());
                    }
                }

                if (files.Contains("Updater.exe"))
                {
                    files.Remove("Updater.exe");
                }

                return files;
            }
            catch (Exception ex)
            {
                _errors.Add("An error occurred fetching file list:\r\n" + ex.ToString());
                return new List<string>();
            }
        }

        private async Task DeleteFiles(List<string> fileList)
        {
            await Task.Run(() =>
            {
                foreach (string fileName in fileList)
                {
                    try
                    {
                        TaskExecuting?.Invoke(this, $"Deleting {fileName}");
                        File.Delete(Path.Combine(InstallationConfiguration.InstallDirectory, fileName));
                    }
                    catch(Exception ex)
                    {
                        _errors.Add($"Failed to delete file {fileName}\r\n:{ex.ToString()}");
                    }
                }
            });
        }

        private async Task RemoveRegistryValues()
        {
            await Task.Run(() =>
            {
                TaskExecuting?.Invoke(this, "Removing registry values...");
                try
                {
                    RegistryKey uninstallKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", true);
                    if (uninstallKey == null)
                    {
                        return;
                    }

                    string subKey = Program.InternalEdition ? "MesaSuiteInternalEdition" : "MesaSuite";
                    uninstallKey.DeleteSubKeyTree(subKey);
                    uninstallKey.Close();
                }
                catch(Exception ex)
                {
                    _errors.Add("MesaSuite was successfully removed from the system, however, the registry could not be updated.  This means that MesaSuite will remain in your Add/Remove Programs window even though it does not exist.");
                }

                string protocolSubKey = Program.InternalEdition ? "mesasuiteie" : "mesasuite";
                try
                {
                    RegistryKey uninstallKey = Registry.ClassesRoot;
                    uninstallKey.DeleteSubKeyTree(protocolSubKey);
                    uninstallKey.Close();
                }
                catch (Exception ex)
                {
                    _errors.Add($"MesaSuite was successfully removed from the system, however, the registry could not be updated.  This means that a protocol handler ({protocolSubKey}://) for MesaSuite will still exist even though MesaSuite does not.");
                }
            });
        }

        private async Task RemoveIcons()
        {
            await Task.Run(() =>
            {
                TaskExecuting?.Invoke(this, "Removing shortcuts");
                try
                {
                    string shortcutName = Program.InternalEdition ? "MesaSuite (Internal Edition).lnk" : "MesaSuite.lnk";
                    if (InstallationConfiguration.MakeDesktopIcon)
                    {
                        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                        if (File.Exists(Path.Combine(desktopPath, shortcutName)))
                        {
                            File.Delete(Path.Combine(desktopPath, shortcutName));
                        }
                    }

                    if (InstallationConfiguration.MakeStartMenuIcon)
                    {
                        string startMenuPath = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
                        if (File.Exists(Path.Combine(startMenuPath, "Programs", shortcutName)))
                        {
                            File.Delete(Path.Combine(startMenuPath, "Programs", shortcutName));
                        }
                    }
                }
                catch(Exception ex)
                {
                    _errors.Add("MesaSuite was successfully removed, however some shortcuts were unable to be deleted:\r\n" + ex.ToString());
                }
            });
        }
    }
}
