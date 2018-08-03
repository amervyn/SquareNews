using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SquareNews.Lib.Aggregation;
using NLog;

namespace SquareNews.Processor
{
    partial class SquareNewsProcessor : ServiceBase
    {
        private Thread _worker;
        private volatile bool _run;
        private Logger _logger = LogManager.GetCurrentClassLogger();
        public SquareNewsProcessor()
        {
            InitializeComponent();
            //StartIt();
        }

        private void StartIt()
        {
            _logger.Info("Starting Service");
            _worker = new Thread(DoWork);
            _run = true;
            _worker.Start();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.

            _logger.Info("Starting Service");
            _worker = new Thread(DoWork);
            _run = true;
            _worker.Start();            
        }

        private void DoWork()
        {
            PublicApiCaller api = new PublicApiCaller();

            var delay = Convert.ToInt32(ConfigurationManager.AppSettings["QueryDelay"]); //minutes

            _logger.Info("Thread delay set to: " + delay + " mins");

            while (_run)
            {
                Task.Run(async () => await api.CallPublicService());                               

                Thread.Sleep(new TimeSpan(0, delay, 0));
            }
            
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.

            _logger.Info("Stopping Service");

            _run = false;
            if (_worker.IsAlive)
                _worker.Abort();
        }
    }
}
