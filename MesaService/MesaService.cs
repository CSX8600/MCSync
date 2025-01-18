using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MesaService.ServiceTasks;

namespace MesaService
{
    public partial class MesaService : ServiceBase
    {
        private CancellationTokenSource _cancellationTokenSource;
        Task threadTask;
        public MesaService()
        {
            InitializeComponent();
        }

#if DEBUG
        public void Start(string[] args)
        {
            OnStart(args);
        }
#endif

        protected override void OnStart(string[] args)
        {
            EventLogEntry("Starting MesaService...");
            _cancellationTokenSource = new CancellationTokenSource();
            threadTask = Task.Run(MesaServiceThread, _cancellationTokenSource.Token);
        }

        protected override void OnStop()
        {
            _cancellationTokenSource.Cancel();
        }

        private void MesaServiceThread()
        {
            try
            {
                // Discover all service tasks
                List<IServiceTask> serviceTasks = new List<IServiceTask>();
                Type serviceTaskType = typeof(IServiceTask);
                foreach (Type type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes().Where(t => t != serviceTaskType && serviceTaskType.IsAssignableFrom(t))))
                {
                    IServiceTask serviceTask = (IServiceTask)Activator.CreateInstance(type);
                    serviceTasks.Add(serviceTask);
                }

                while (true)
                {
                    foreach (IServiceTask serviceTask in serviceTasks)
                    {
                        try
                        {
                            if (serviceTask.NextRunTime > DateTime.Now)
                            {
                                continue;
                            }

                            EventLogEntry($"Running service task {serviceTask.Name}");
                            if (!serviceTask.Run())
                            {
                                throw new Exception($"{serviceTask.Name} did not complete successfully");
                            }
                        }
                        catch (Exception ex)
                        {
                            EventLogEntry($"Caught an exception while executing task {serviceTask.Name}:\r\n{ex.ToString()}");
                        }

                        if (_cancellationTokenSource.IsCancellationRequested)
                        {
                            break;
                        }
                    }


                    if (_cancellationTokenSource.IsCancellationRequested)
                    {
                        break;
                    }

                    _cancellationTokenSource.Token.WaitHandle.WaitOne(60_000);

                    if (_cancellationTokenSource.IsCancellationRequested)
                    {
                        break;
                    }
                }
            }
            catch(Exception ex)
            {
                EventLogEntry("An unexpected exception occurred - service stopped:\r\n" + ex.ToString());
            }
        }

        public void WaitForFinish()
        {
            threadTask.Wait();
        }

        private void EventLogEntry(string entry)
        {
#if DEBUG
            Console.WriteLine(entry);
#else
            try
            {
                string eventSource = Program.InternalEdition ? "MesaServiceInternalEdition" : "MesaService";
                if (!EventLog.SourceExists(eventSource))
                {
                    EventLog.CreateEventSource(eventSource, "Application");
                }
                EventLog.WriteEntry(eventSource, entry, EventLogEntryType.Information);
            }
            catch
            {
                Console.WriteLine("WARNING: Could not write to Event Log: " + entry);
            }
#endif
        }
    }
}
