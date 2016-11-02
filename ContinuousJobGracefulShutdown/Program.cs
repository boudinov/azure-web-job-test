using System;
using System.Reflection;
using Microsoft.Azure.WebJobs;
using System.Threading.Tasks;

namespace ContinuousJobGracefulShutdown
{
    class Program
    {
        static void Main(string[] args)
        {
            //Environment.SetEnvironmentVariable("WEBJOBS_SHUTDOWN_FILE", "d:\\shutdown.test");

            var config = new JobHostConfiguration();
            config.UseTimers();

            JobHost host = new JobHost(config);

            MethodInfo methodInfo = typeof(Functions).GetMethod("LoopTestFunction");
            var task = host.CallAsync(methodInfo);

            host.RunAndBlock();

            try
            {
                task.Wait();
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("While wating task: " + ex.Message);
                ex.Handle(e => e is TaskCanceledException);
            }
        }
    }
}
