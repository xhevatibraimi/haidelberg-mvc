using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Thread = System.Threading.Thread;

namespace LoggingDemo
{
    class Program
    {
        const int seconds = 5_000;
        static NLog.Logger _log = null;

        static void Main(string[] args)
        {
            ConfigureLogging();

            while (true)
            {
                _log.Info("Job Started");
                var sw = new Stopwatch();
                sw.Start();
                RunJob();
                sw.Stop();
                _log.Info($"Job Finished in {sw.ElapsedMilliseconds} ms");
                _log.Info($"Waiting for {seconds} seconds...");
                Thread.Sleep(seconds);
            }
        }

        private static void ConfigureLogging()
        {
            var loggerConfig = new NLog.Config.LoggingConfiguration();

            var fileTarget = new NLog.Targets.FileTarget("file")
            {
                FileName = "demo.log",
                Layout = "${longdate}|${level:uppercase=true}|${logger}|${threadid}|${message}|${exception:format=tostring}"
            };
            var consoleTarget = new NLog.Targets.ConsoleTarget("console")
            {
                Layout = "${longdate}|${level:uppercase=true}|${logger}|${threadid}|${message}|${exception:format=tostring}"
            };

            loggerConfig.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, fileTarget);
            loggerConfig.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, consoleTarget);
            
            NLog.LogManager.Configuration = loggerConfig;
            _log = NLog.LogManager.GetCurrentClassLogger();
        }

        static void RunJob()
        {
            _log.Info("downloading data");
            var data = DownloadJson();
            _log.Info("logging data");
            LogData(data);
        }

        private static void LogData(string data)
        {
            var todos = JsonConvert.DeserializeObject<List<Todo>>(data);
            _log.Info($"There are {todos.Count} todos");
        }

        private static string DownloadJson()
        {
            var sw = new Stopwatch();
            var httpClient = new HttpClient();
            sw.Start();
            var responseTask = httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/todos");
            var content = responseTask.Result;
            sw.Stop();
            _log.Info($"downloaded data in {sw.ElapsedMilliseconds} ms");
            return content;
        }
    }
}
