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
using SquareNews.Lib.Aggregation;

namespace SquareNews.Processor
{
    partial class SquareNewsProcessor : ServiceBase
    {
        private Thread _worker;
        private volatile bool _run;
        public SquareNewsProcessor()
        {
            InitializeComponent();
            //StartIt();
        }

        private void StartIt()
        {
            _worker = new Thread(DoWork);
            _run = true;
            _worker.Start();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            _worker = new Thread(DoWork);
            _run = true;
            _worker.Start();            
        }

        private void DoWork()
        {
            PublicApiCaller api = new PublicApiCaller();
            
            while(_run)
            {
                Task.Run(async () => await api.CallPublicService());

                Thread.Sleep(new TimeSpan(0, 15, 0));
            }
            
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            _run = false;
            if (_worker.IsAlive)
                _worker.Abort();
        }
    }
}
