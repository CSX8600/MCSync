using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MesaService
{
    static class Program
    {
        public const bool InternalEdition = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            MesaService mainService = new MesaService();
#if DEBUG
            Console.WriteLine("Starting Service...");
            mainService.Start(args);
            Console.WriteLine("Press any key to stop service...");
            Console.ReadKey();
            Console.WriteLine("Stopping Service...");
            mainService.Stop();
            mainService.WaitForFinish();
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                mainService
            };
            ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}
