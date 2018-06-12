using System;
using System.IO;
using System.Reflection;
using System.Xml;

[assembly: log4net.Config.XmlConfigurator]
namespace CoinProcessor.Logger
{
    public class Logger
    {
        private log4net.ILog log;

        public Logger()
        {
            log = log4net.LogManager.GetLogger(typeof(Program));
        }
        
        public void Log(string message)
        {
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead("log4net.config"));

            var repo = log4net.LogManager.CreateRepository(
                Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);

            log.Info(message);
            //Lines removed for brevity
        }
    }
}
