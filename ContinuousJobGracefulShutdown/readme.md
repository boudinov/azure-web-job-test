This sample demonstrates how to perform **graceful shutdown** in your job functions. For more information on graceful shutdown in WebJobs see the following links:

* http://blog.amitapple.com/post/2014/05/webjobs-graceful-shutdown
* https://azure.microsoft.com/en-us/documentation/articles/websites-dotnet-webjobs-sdk-storage-queues-how-to/#graceful
* https://github.com/projectkudu/kudu/wiki/Web-jobs

In this sample, we demonstrate increasing the **graceful shutdown timeout** from the default of 5 seconds.

* In the **settings.job** file, we set the timeout to **60 seconds**
* On startup we invoke our test job. The job simulates a long running task. It monitors the `CancellationToken` for shutdown
* You can test this by deploying the sample to Azure, and stopping your job in the portal UI (right click on your job in the UI and choose "Stop"
* You can also test this locally by setting an environment variable **WEBJOBS_SHUTDOWN_FILE** to full path name of a local shutdown file the JobHost should monitor. If you create a file at that location, it will trigger graceful shutdown (this is the same way it works in Azure)

As you can see from the job output below, the function receives the cancellation notification and is given time to clean up

* [12/31/2015 17:15:08 > 951460: SYS INFO] Status changed to Stopping
* [12/31/2015 17:15:09 > 951460: INFO] Function has been cancelled. Performing cleanup ...
* [12/31/2015 17:15:39 > 951460: INFO] Function was cancelled and has terminated gracefully.
* [12/31/2015 17:15:39 > 951460: INFO] Function completed succesfully.
* [12/31/2015 17:15:39 > 951460: INFO] Job host stopped
* [12/31/2015 17:15:39 > 951460: SYS INFO] Status changed to Success
* [12/31/2015 17:15:39 > 951460: SYS INFO] Status changed to Stopped
