using MesaSuite.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Text;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MesaSuite
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static List<Updater.MCSyncVersion> updates = new List<Updater.MCSyncVersion>();
        private const string MUTEX_ID = "MesaSuiteFEA8712FFBC5450B958E5A88EB6160B3";
        private const string PipeName = "MesaSuiteB64CBD3D41794E02989558153B4995CD";
        private static Thread _mutexPipedServerThread;
        [STAThread]
        static void Main(string[] args)
        {
            StartupArguments.SetupArguments(args);
            if (StartupArguments.UpdaterProcessID != -1)
            {
                Console.WriteLine("Waiting for Updater to close...");
                while (Process.GetProcesses().Any(p => p.Id == StartupArguments.UpdaterProcessID))
                {
                    Thread.Sleep(50);
                }
            }

            if (!string.IsNullOrEmpty(StartupArguments.FolderToDelete) && Directory.Exists(StartupArguments.FolderToDelete))
            {
                Directory.Delete(StartupArguments.FolderToDelete, true);
            }

            Mutex mutex = new Mutex(true, MUTEX_ID, out bool isNewInstance);
            if (!isNewInstance)
            {
                if (StartupArguments.RunUri == null)
                {
                    StartupArguments.RunUri = new Uri("mesasuite://dashboard");
                }

                NotifyOtherInstance();
                return;
            }

            // Splash screen logic
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frmSplash splash = new frmSplash();
            splash.Show();
            Stopwatch shownTime = new Stopwatch();
            shownTime.Start();
            Application.DoEvents();

            Thread initThread = new Thread(new ThreadStart(InitializeApplication));
            initThread.Start();

            while (initThread.IsAlive)
            {
                Thread.Sleep(50);
                Application.DoEvents();
            }

            while (shownTime.ElapsedMilliseconds < 1000)
            {
                Thread.Sleep(50);
            }

            shownTime.Stop();
            splash.Close();

            if (updates == null)
            {
                MessageBox.Show("An error occurred while retrieving updates.  MesaSuite may not start without checking for updates.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (updates.Any())
            {
                frmUpdate update = new frmUpdate();
                update.UpdaterResults = updates;
                Application.Run(update);
                return;
            }

            if (!string.IsNullOrEmpty(StartupArguments.Run))
            {
                RunOther();
            }
            else
            {
                Application.Run(new frmMain());
            }
        }

        private static void NotifyOtherInstance()
        {
            using(NamedPipeClientStream clientStream = new NamedPipeClientStream(".", PipeName, PipeDirection.Out))
            {
                try
                {
                    clientStream.Connect(1000);
                    using(var writer = new StreamWriter(clientStream))
                    {
                        writer.WriteLine(StartupArguments.RunUri.ToString());
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Failed to notify other instance: {ex.Message}");
                }
            }
        }

        private static void RunOther()
        {
            switch(StartupArguments.Run)
            {
                case "mcsync":
                    MCSync.Program.Main(StartupArguments.GetArgsForApp("mcsync"));
                    break;
            }
        }

        private static void InitializeApplication()
        {
            Application.ApplicationExit += Application_ApplicationExit;
            ServicePointManager.DefaultConnectionLimit = 20; // Hopefully this'll be enough for a while
            Task<Updater.UpdaterResults> task = new Task<Updater.UpdaterResults>(Updater.Run);
            task.Start();

            InitCustomLabelFont();
            Authentication.Initialize();
            StartMutexPipedServerThread();

            while (task.Status == TaskStatus.Running)
            {
                Thread.Sleep(50);
            }

            if (task.Status == TaskStatus.Faulted)
            {
                updates = null;
                return;
            }

            if (task.Result.HasUpdates)
            {
                updates = task.Result.UpdatesAvailable;
            }
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            Authentication.Shutdown();
            _mutexPipedServerThread?.Abort(); 
        }

        private static void InitCustomLabelFont()
        {
            try
            {
                //PrivateFontCollection pfc = new PrivateFontCollection();
                //int fontLength = Properties.Resources.MC_FONT.Length;
                //byte[] fontdata = Properties.Resources.MC_FONT;
                //System.IntPtr data = Marshal.AllocCoTaskMem(fontLength);
                //Marshal.Copy(fontdata, 0, data, fontLength);
                //pfc.AddMemoryFont(data, fontLength);

                // Set font here
                //lblFontTest.Font = new System.Drawing.Font(pfc.Families[0], lblFontTest.Font.Size);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred:\r\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void StartMutexPipedServerThread()
        {
            _mutexPipedServerThread = new Thread(new ThreadStart(MutexPipedServer));
            _mutexPipedServerThread.IsBackground = true;
            _mutexPipedServerThread.Start();
        }

        private static void MutexPipedServer()
        {
            while (true)
            {
                using (NamedPipeServerStream namedPipeServer = new NamedPipeServerStream(PipeName, PipeDirection.In))
                {
                    namedPipeServer.WaitForConnection();

                    string uriMessage;
                    using (StreamReader reader = new StreamReader(namedPipeServer))
                    {
                        uriMessage = reader.ReadToEnd();
                    }

                    try
                    {
                        namedPipeServer.Disconnect();
                    }
                    catch { }

                    if (Uri.TryCreate(uriMessage, UriKind.Absolute, out Uri uri))
                    {
                        StartupArguments.RunUri = uri;

                        frmMain main = Application.OpenForms.OfType<frmMain>().FirstOrDefault();
                        if (main != null)
                        {
                            try
                            {
                                main.PerformRunURI(forceRun: true);
                            }
                            catch { }
                        }
                    }
                }
            }
        }
    }
}
