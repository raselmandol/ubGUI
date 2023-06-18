# UB GUI (Under Development)

This application is under development and maintained by me. If you find any vulnerblity or have any recommendation please create a new issue or pr or directly reach me here [raselm@duck.com](raselm@duck.com) or visit [https://raselmandol.com](https://raselmandol.com) for other contact details. 

Ubuntu Based theme application for Windows.

![Ubuntu GUI on Windows](https://raw.githubusercontent.com/raselmandol/ubGUI/main/Images/1.png)

I am delighted to present UB GUI, a desktop application(made with C#(.NET)) that I made to give Windows a look of Ubuntu. I made this application in my free time (can call in my ToTo Lab). Created with unwavering dedication during precious moments of free time, UB GUI is a tribute to the unconditional love and unwavering support of two incredible individuals: My Parents. When I will get free time again, i will make a better version of this one. Hope you all will enjoy this one.


### Features

#### Full Screen(No Minimize Option)
When this application is opened, it will be in Maximized mode, occupying the entire screen. To access other minimized applications, a button has been included to restore them, while the application itself will remain in full-screen mode. Only the taskbar will be visible, unhidden.

![Ubuntu GUI on Windows](https://raw.githubusercontent.com/raselmandol/ubGUI/main/Images/3.png)
![Ubuntu GUI on Windows](https://raw.githubusercontent.com/raselmandol/ubGUI/main/Images/4.png)

#### Default Notification
While this application is opened in full-screen mode, the Default Notification System of Windows will continue to function. Notifications will remain visible at all times.

## Dev and contributing

Built with [C#(.NET)](https://dotnet.microsoft.com/en-us/)

Dev and compile works on Linux and Windows. You will need [dotnet Framework](https://dotnet.microsoft.com/en-us/download/dotnet-framework) to run this app.

Any contribution or suggestion is accepted. Feel free to create any report for issues or app crashes. You can also use the report link provided in the application menu to create a crash report or contact me through the link provided.
Pull requests are accepted.

### Developing
1. `editing mainForm`
1. `adding otherForms as required.`

## Running the App

### Executables

You can build your own executables from the latest source by running one of the commands below. Executables(with ConfuserX) can be found [here](https://github.com/raselmandol/ubGUI/tree/main/Release).

### Compiling from source
To import and compile the project in Visual Studio, you can follow these steps:

Clone the repository: Start by cloning the GitHub repository to your local machine. You can use a Git client or the command line to do this. Navigate to your desired directory and run the following command:

 `$ git clone https://github.com/raselmandol/ubGUI`

Open the project in Visual Studio: Launch Visual Studio and select "Open a project or solution" from the startup screen. Navigate to the directory where you cloned the repository and select the project file (typically with a .csproj extension). Click "Open" to load the project into Visual Studio.

Restore NuGet packages: The project relies on some external NuGet packages, you'll need to restore them. In Visual Studio, right-click on the project in the Solution Explorer and select "Restore NuGet Packages." This will download and install the required packages defined in the project's dependencies.

Build the project: Once the packages are restored, you can build the project. Go to the "Build" menu and select "Build Solution" (or press Ctrl + Shift + B). This will compile the source code and generate the executable or library files. You can find the executable on Debug folder.


For building with installer project, find the documentation [here](https://learn.microsoft.com/en-us/visualstudio/deployment/installer-projects-net-core?view=vs-2022).


## Authors

- [@raselmandol](https://www.github.com/raselmandol)

