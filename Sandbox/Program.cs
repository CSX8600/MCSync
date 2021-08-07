﻿using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.Loader;
using ClussPro.ObjectBasedFramework.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebModels.hMailServer.dbo;
using WebModels.security;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = DataObjectFactory.Create<User>();
            //Schema.Deploy();

            LoaderController loader = new LoaderController();
            loader.Initialize();
            loader.Process();

            Console.WriteLine("Done!");
            Console.ReadKey();
        }
    }
}