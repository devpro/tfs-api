TFS API
==============================

[![Build Status](https://travis-ci.org/devpro/tfs-api.svg?branch=master)](https://travis-ci.org/devpro/tfs-api)

# Table of Contents
* General presentation
* Technical information
    * [General information](#chapter-2-1)
    * [Local installation](#chapter-2-2)
    * [Debug](#chapter-2-3)
* User guide
    * [API calls](#chapter-3-1)

# General presentation

This API is a lightweight ASP.NET Core application to have easy and quick access to TFS data.
The goal is to hide the complexity of TFS REST/SOAP calls that can be customized depending the configuration of the team project.
Targets: TFS 2015 (on premise) and TFS online.

# Technical informations

## General information <a id="chapter-2-1"></a>

TODO @bertrand

## Local installation <a id="chapter-2-2"></a>

1. Pre-requisites
    - Visual Studio 2015
    - Git client
    - .NET Framework 4.5.2

2. Get source code from Github
> https://github.com/devpro/tfs-api.git

3. TODO @bertrand

## Debug <a id="chapter-2-3"></a>

1. Open Visual Studio 2015

TODO @bertrand

## Publish

1. Prerequisites: install and download [DotNetCore.1.0.1-WindowsHosting.exe](https://go.microsoft.com/fwlink/?LinkID=827547)

2. Execute the following command line in the TfsApi folder

    * For a deployment on Windows Server
```bat
dotnet publish -f net452 -o "only_absolute_path\publish\DevProTfsApi" -c Release
```

    *  For a deployment on Linux with Docker
```bat
powershell .\dockerTask.ps1 -Build -Environment Release
docker run -i -p 5000:5000 -t tfsapi:latest
```

Known issues:

* Cannot run on Linux:

Explanation: Microsoft.VisualStudio.Services.WebApi.HttpMessageExtensions directlty used by Microsoft.VisualStudio.Services.WebApi.VssHttpClientBase

Details:

    info: Microsoft.AspNetCore.Hosting.Internal.WebHost[1]
          Request starting HTTP/1.1 GET http://localhost:5000/
    info: Microsoft.AspNetCore.Hosting.Internal.WebHost[2]
          Request finished in 157.6452ms 404
    info: Microsoft.AspNetCore.Hosting.Internal.WebHost[1]
          Request starting HTTP/1.1 GET http://localhost:5000/api/workitems?teamProjectName=BlueSkySeleniumCampaign
    dbug: Devpro.TfsApi.Lib.TfsClient[0]
          Constructor called
    fail: Microsoft.AspNetCore.Server.Kestrel[13]
          Connection id "0HKVUE7M7VM5M": An unhandled exception was thrown by the application.
    System.DllNotFoundException: ADVAPI32.DLL
       at (wrapper managed-to-native) Microsoft.VisualStudio.Services.WebApi.VssRequestTimerTrace:EventActivityIdControl (int,System.Guid&)
       at Microsoft.VisualStudio.Services.WebApi.VssRequestTimerTrace.TraceRequest (System.Net.Http.HttpRequestMessage message) [0x00023] in <2a79a60e778a4821aa836c494076ebd7>:0
       at Microsoft.VisualStudio.Services.WebApi.HttpMessageExtensions.Trace (System.Net.Http.HttpRequestMessage request) [0x0003b] in <2a79a60e778a4821aa836c494076ebd7>:0
       at Microsoft.VisualStudio.Services.WebApi.VssHttpClientBase+<SendAsync>d__79.MoveNext () [0x000dd] in <2a79a60e778a4821aa836c494076ebd7>:0
       --- End of stack trace from previous location where exception was thrown ---
       at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw () [0x0000c] in <dca3b561b8ad4f9fb10141d81b39ff45>:0
       at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess (System.Threading.Tasks.Task task) [0x0004e] in <dca3b561b8ad4f9fb10141d81b39ff45>:0
       at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification (System.Threading.Tasks.Task task) [0x0002e] in <dca3b561b8ad4f9fb10141d81b39ff45>:0
       at System.Runtime.CompilerServices.TaskAwaiter.ValidateEnd (System.Threading.Tasks.Task task) [0x0000b] in <dca3b561b8ad4f9fb10141d81b39ff45>:0
       at System.Runtime.CompilerServices.TaskAwaiter`1[TResult].GetResult () [0x00000] in <dca3b561b8ad4f9fb10141d81b39ff45>:0
       at Microsoft.VisualStudio.Services.WebApi.TaskExtensions.SyncResult[T] (System.Threading.Tasks.Task`1[TResult] task) [0x00007] in <2a79a60e778a4821aa836c494076ebd7>:0
       at Microsoft.VisualStudio.Services.Client.VssConnection.GetClient[T] () [0x00025] in <6414e73ad4fe482fb29dfd82c8e1ce78>:0
       at Devpro.TfsApi.Lib.TfsClient.OpenConnection () [0x00051] in <4c4bc6e45fc84ee59aa8a29eab13af77>:0
       at Devpro.TfsApi.Lib.TfsClient..ctor (Microsoft.Extensions.Logging.ILogger`1[TCategoryName] logger, Devpro.TfsApi.Lib.ITfsClientConfiguration clientConfiguration) [0x0002a] in <4c4bc6e45fc84ee59aa8a29eab13af77>:0
       at (wrapper managed-to-native) System.Reflection.MonoCMethod:InternalInvoke (System.Reflection.MonoCMethod,object,object[],System.Exception&)
       at System.Reflection.MonoCMethod.InternalInvoke (System.Object obj, System.Object[] parameters) [0x00002] in <dca3b561b8ad4f9fb10141d81b39ff45>:0

# User guide

## General information <a id="chapter-3-1"></a>

Subject | Url
------------ | -------------
Get work items | http://url:port/api/workitems?teamProjectName=myproject

TODO @bertrand
