using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using System.IO;

namespace ContinuousJobGracefulShutdown
{
    public static class Functions
    {
        [NoAutomaticTrigger]
        public static async Task LoopTestFunction(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    Console.WriteLine("LoopTestFunction in loop");
                    await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    Console.WriteLine("LoopTestFunction entering Shutdown wait");
                    //simulate some shutdown cleanup
                    await Task.Delay(TimeSpan.FromSeconds(5));
                }
            }

            Console.WriteLine("LoopTestFunction completed succesfully.");        
        }

        public static async Task TimerTestFunction([TimerTrigger("*/20 * * * * *", RunOnStartup = true)] TimerInfo info, CancellationToken token, TextWriter log)
        {
            Console.WriteLine("TimerTestFunction ran");
            log.Write("TimerTestFunction ran");
        }
    }
}
