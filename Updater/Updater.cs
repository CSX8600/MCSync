using IWshRuntimeLibrary;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Updater
{
    public class Updater
    {
        public InstallationConfiguration InstallationConfiguration { get; set; }

        public event EventHandler<int> NumberOfTasks;
        public event EventHandler<string> NonTaskExecuting;
        public event EventHandler<string> TaskExecuting;
        public event EventHandler UpdateFailed;
        public event EventHandler UpdateSucceeded;

        private List<string> _errors = new List<string>();

        public IReadOnlyCollection<string> Errors => _errors;

        public async Task BeginUpdate()
        {
            await Task.Run(() => PerformUpdate());
        }

        private void PerformUpdate()
        {
            if (StartupArguments.MCSyncProcessID != -1)
            {
                NonTaskExecuting?.Invoke(this, "Waiting for MCSync to close...");
                while (Process.GetProcesses().Any(p => p.Id == StartupArguments.MCSyncProcessID))
                {
                    Thread.Sleep(50);
                }
            }

            if (!DownloadFiles())
            {
                return;
            }

            if (!UpdateRegistry())
            {
                return;
            }

            AddIcons();

            UpdateMinecraftDirectory();

            UpdateSucceeded?.Invoke(this, new EventArgs());
        }

        private bool DownloadFiles()
        {
            if (string.IsNullOrEmpty(StartupArguments.VersionToDownload))
            {
                _errors.Add("Invalid version specified");
                UpdateFailed?.Invoke(this, new EventArgs());
                return false;
            }

            NonTaskExecuting?.Invoke(this, "Discovering download manifest");
            NetworkCredential ftpCredentials = new NetworkCredential("Reporting", "NetLogon");
            string updateFolder = Program.InternalEdition ? "MesaSuiteInternal" : "MCSyncNew";
            FtpWebRequest webRequest = (FtpWebRequest)WebRequest.Create($"ftp://www.clussmanproductions.com/support/{updateFolder}/updates/{StartupArguments.VersionToDownload}/manifest");
            webRequest.Method = WebRequestMethods.Ftp.DownloadFile;
            webRequest.Credentials = ftpCredentials;

            string manifest;
            try
            {
                using (Stream responseStream = webRequest.GetResponse().GetResponseStream())
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    manifest = reader.ReadToEnd();
                }
            }
            catch (WebException)
            {
                _errors.Add("No download manifest could be found. Was an invalid version specified?");
                UpdateFailed?.Invoke(this, new EventArgs());
                return false;
            }

            HashSet<string> directories = new HashSet<string>();
            using (StringReader reader = new StringReader(manifest))
            {
                string file;

                int counter = 0;
                while ((file = reader.ReadLine()) != null)
                {
                    counter++;
                    if (file.Contains("\\"))
                    {
                        directories.Add(file.Substring(0, file.LastIndexOf("\\")));
                    }
                }

                NumberOfTasks?.Invoke(this, counter + 3); // Adding 3 - one for registry, one for icons, one for MC directory
            }

            NonTaskExecuting?.Invoke(this, "Creating directories");
            foreach(string directory in directories)
            {
                Directory.CreateDirectory(Path.Combine(InstallationConfiguration.InstallDirectory, directory));
            }

            using (StringReader reader = new StringReader(manifest))
            {
                string file;
                while ((file = reader.ReadLine()) != null)
                {
                    TaskExecuting?.Invoke(this, "Downloading " + file);

                    webRequest = (FtpWebRequest)WebRequest.Create($"ftp://www.clussmanproductions.com/support/{updateFolder}/updates/{StartupArguments.VersionToDownload}/{file}");
                    webRequest.Credentials = ftpCredentials;
                    webRequest.Method = WebRequestMethods.Ftp.DownloadFile;

                    if (file.Equals("Updater.exe", StringComparison.OrdinalIgnoreCase))
                    {
                        try
                        {
                            using (FileStream tempStream = System.IO.File.OpenWrite(Path.Combine(InstallationConfiguration.InstallDirectory, file))) { }
                        }
                        catch(IOException)
                        {
                            continue;
                        }
                    }

                    try
                    {
                        using (Stream responseStream = webRequest.GetResponse().GetResponseStream())
                        using (Stream fileStream = System.IO.File.Create(Path.Combine(InstallationConfiguration.InstallDirectory, file)))
                        {
                            responseStream.CopyTo(fileStream);
                        }
                    }
                    catch
                    {
                        _errors.Add("Failed to write " + file);
                    }
                }
            }

            if (_errors.Any())
            {
                UpdateFailed?.Invoke(this, new EventArgs());
                return false;
            }

            return true;
        }

        private bool UpdateRegistry()
        {
            TaskExecuting?.Invoke(this, "Registering MesaSuite...");
            try
            {
                string subKey = Program.InternalEdition ? "MesaSuiteInternalEdition" : "MesaSuite";
                RegistryKey mesasuiteKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\" + subKey, true);
                mesasuiteKey.SetValue("DisplayName", "MesaSuite" + (Program.InternalEdition ? " (Internal Edition)" : ""));
                mesasuiteKey.SetValue("ApplicationVersion", StartupArguments.VersionToDownload);
                mesasuiteKey.SetValue("Publisher", "Clussman Productions");
                mesasuiteKey.SetValue("DisplayIcon", Path.Combine(InstallationConfiguration.InstallDirectory, "MesaSuite.exe"));
                mesasuiteKey.SetValue("DisplayVersion", StartupArguments.VersionToDownload);
                mesasuiteKey.SetValue("URLInfoAbout", "https://www.mesabrook.com/mcsync/index.html");
                mesasuiteKey.SetValue("Contact", "cnwaj@hotmail.com");
                mesasuiteKey.SetValue("InstallDate", DateTime.Now.ToString("yyyyMMdd"));
                mesasuiteKey.SetValue("UninstallString", Path.Combine(InstallationConfiguration.InstallDirectory, "Updater.exe") + " -uninstallquiet");
                mesasuiteKey.SetValue("DesktopIcon", InstallationConfiguration.MakeDesktopIcon);
                mesasuiteKey.SetValue("StartMenuIcon", InstallationConfiguration.MakeStartMenuIcon);
                mesasuiteKey.Close();

                subKey = Program.InternalEdition ? "mesasuiteie" : "mesasuite";
                mesasuiteKey = Registry.ClassesRoot.CreateSubKey(subKey);
                mesasuiteKey.SetValue("", "MesaSuite Protocol");
                mesasuiteKey.SetValue("URL Protocol", "");
                mesasuiteKey.Close();

                mesasuiteKey = Registry.ClassesRoot.CreateSubKey(subKey + @"\shell\open\command");
                mesasuiteKey.SetValue("", Path.Combine(InstallationConfiguration.InstallDirectory, "MesaSuite.exe") + " \"%1\"");
                mesasuiteKey.Close();
            }
            catch(Exception ex)
            {
                _errors.Add("Failed to register MesaSuite in system registry:\r\n" + ex.ToString());
                UpdateFailed?.Invoke(this, new EventArgs());
                return false;
            }

            return true;
        }

        private bool AddIcons()
        {
            TaskExecuting?.Invoke(this, "Creating icons...");
            if (InstallationConfiguration.MakeDesktopIcon)
            {
                WshShell shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "MesaSuite" + (Program.InternalEdition ? " (Internal Edition)" : "") + ".lnk"));
                shortcut.Description = "Launches MesaSuite" + (Program.InternalEdition ? " Internal Edition" : "");
                shortcut.IconLocation = Path.Combine(InstallationConfiguration.InstallDirectory, "icon.ico");
                shortcut.TargetPath = Path.Combine(InstallationConfiguration.InstallDirectory, "MesaSuite.exe");
                shortcut.WorkingDirectory = InstallationConfiguration.InstallDirectory;
                shortcut.Save();
            }

            if (InstallationConfiguration.MakeStartMenuIcon)
            {
                WshShell shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu), "Programs", "MesaSuite" + (Program.InternalEdition ? " (Internal Edition)" : "") + ".lnk"));
                shortcut.Description = "Launches MesaSuite" + (Program.InternalEdition ? " Internal Edition" : "");
                shortcut.IconLocation = Path.Combine(InstallationConfiguration.InstallDirectory, "icon.ico");
                shortcut.TargetPath = Path.Combine(InstallationConfiguration.InstallDirectory, "MesaSuite.exe");
                shortcut.WorkingDirectory = InstallationConfiguration.InstallDirectory;
                shortcut.Save();
            }

            return true;
        }

        private bool UpdateMinecraftDirectory()
        {
            TaskExecuting?.Invoke(this, "Updating Minecraft directory...");

            if (string.IsNullOrEmpty(InstallationConfiguration.MinecraftDirectory))
            {
                return true;
            }

            try
            {
                if (!System.IO.File.Exists(Path.Combine(InstallationConfiguration.InstallDirectory, "userpreferences.json")))
                {
                    using (FileStream stream = System.IO.File.Create(Path.Combine(InstallationConfiguration.InstallDirectory, "userpreferences.json"))) { }
                }

                string fileData = System.IO.File.ReadAllText(Path.Combine(InstallationConfiguration.InstallDirectory, "userpreferences.json"));
                if (string.IsNullOrEmpty(fileData))
                {
                    fileData = "{}";
                }

                JObject rootObject = JObject.Parse(fileData);
                if (!rootObject.ContainsKey("sections"))
                {
                    rootObject.Add("sections", new JObject());
                }

                JObject sections = rootObject.Property("sections").Value as JObject;
                if (!sections.ContainsKey("mcsync"))
                {
                    sections.Add("mcsync", new JObject());
                }

                JObject mcsync = sections.Property("mcsync").Value as JObject;
                JProperty minecraftDirectory;
                if (!mcsync.ContainsKey("minecraftDirectory"))
                {
                    minecraftDirectory = new JProperty("minecraftDirectory", "");
                    mcsync.Add(minecraftDirectory);
                }
                else
                {
                    minecraftDirectory = mcsync.Property("minecraftDirectory");
                }
                minecraftDirectory.Value = InstallationConfiguration.MinecraftDirectory;

                System.IO.File.WriteAllText(Path.Combine(InstallationConfiguration.InstallDirectory, "userpreferences.json"), rootObject.ToString());
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
