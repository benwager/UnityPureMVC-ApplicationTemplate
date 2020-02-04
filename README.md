# UnityPureMVC-ApplicationTemplate

A template repository to get started with a new UnityPureMVC application.

UnityPureMVC uses a slightly modified version of the MultiCore PureMVC C# port:

- https://puremvc.org/

- https://github.com/PureMVC/puremvc-csharp-multicore-framework/wiki

The main differences are that it removes some multi threaded features not compatible with Unity, and adds support for easily passing notifications between cores.

It is highly recommended to read through the puremvc documentation to familiarize yourself with those concepts before attempting to use this template project.


# Usage

UnityPureMVC is split in to 3 areas:

### Application

This is where the main application logic resides. In this template we use the ApplicationMediator to create an instance of our ApplicationView prefab. In some circumstances, you might also attach a ViewComponent ( a Unity MonoBehaviour ) to a prefab created in a Mediator, and use the Mediator to mediate between the View component and the rest of the application. 

In this template, the ApplicationMediator also sends a Notification to `REQUEST_LOAD_APPLICATION_DATA`. This is picked up by a registered command, `UnityPureMVC.Application.Controller.Commands.Request.RequestLoadApplicationDataCommand` which in turn sends a request to the DataLoader module. `UnityPureMVC.Application.Controller.Commands.Result.ApplicationDataLoadedCommand` is registered here, and receives the processed data from the DataLoader module. 

Although you may register Commands, Proxies and Mediators almost anywhere (see `RequestLoadApplicationDataCommand.cs`), it is preferred to register them within the Prepare Commands where possible, in `UnityPureMVC.Application.Controller.Commands.Prepare`

Once the data is received, the ApplicationDataLoaded command stores it, and it is now accessible anywhere in the application via a PureMVC Proxy ( `UnityPureMVC.Application.Model.Proxies.ApplicationDataProxy` ). Finally, the ApplicationDataLoadedCommand sends an APPLICATION_READY notification, which any interested mediator can listen to, and take appropritate action. 

All Mediators should extend ApplicationBaseMediator. This base mediator is an abstract class which already registers interest in the `APPLICATION_READY` notification, as well as `APPLICATION_STATE_CHANGED` and `APPLICATION_DATA_RELOADED`. It also conveniently handles initializing a view component when a viewPrefabPath is set, and stores references to the applicationDataProxy and applicationStateProxy.

### Core 

This is the main PureMVC core. It is a git submodule, which contains the modified PureMVC library and some other necessities. 

### Modules

Modules are specific feature sets which are generic enough to exist indepently from the Application area. 

Although they may sometimes make references to the `UnityPureMVC.Core` namespace, they should never directly reference any other Module or the `UnityPureMVC.Application` namespace. Data should be passed to and from Modules via the PureMVC notification system. This is to ensure that Modules are reusable between different applications.

In this example we make use of the DataLoader module. Which handles loading a JSON file from a given location and returns it in the form of a PureMVC VO.

# Installation

## Step 1

Select the 'Use This Template' button above to create a new repository using this as a template.

## Step 2

Add the following submodules.

`git submodule add https://github.com/benwager/UnityPureMVC-Core Assets/Source/UnityPureMVC/Core/`

`git submodule add https://github.com/benwager/UnityPureMVC-Modules-DataLoader Assets/Source/UnityPureMVC/Modules/DataLoader/`

## Step 3

Open project in Unity, open the Main scene check the Application GameObject in inspector. There is probably a Missing Reference. This should be pointing to...

- DataLoaderModule.cs


## Step 4 

Hit Play.
