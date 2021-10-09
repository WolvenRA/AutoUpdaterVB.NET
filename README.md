# AutoUpdaterVB.NET

AutoUpdaterVB.Net is a package that allows .Net developers to EASILY add auto update functionality to their software. 

This is basically a VB version of AutoUpdater.NET which is written in C#. The C# version is on github at ravibpatel/AutoUpdater.NET.
This VB version was written with Visual Studio 2017 and requires .net framework 4.0 or higher.
If you want to upgrade it to a later .net version, then I assume that you know how to do that.

Although the coding style isn't the way I would have done it, I didn't feel like rewriting the application from scratch.
I did add\modify some things compared to the C# version to eliminate problems I ran into during testing.
I also eliminated some things I didn't feel were important and didn't want to mess with, 
  such as; Having a ParseUpdateInfoEvent handler that uses JSON to retrieve\process a Version.json file instead of using a Version.xml file, 
    and implementing other CultureInfo files.  If you want that functionality, you can copy the various .resx files 
    from the C# version and then do any conversion work necessary to convert them to VB.net.	

There is both an AutoUpdaterVB.NET.dll and an AutoUpdaterVB.NET.exe file in the release download.  You can add either one
to your application and reference it (Add it to the References of your app).  They both work exactly the same so use 
whichever one you like.

	
	
## How the application works.

AutoUpdaterVB.NET downloads a XML file containing update information from your server. It uses this XML file to get the information about the latest version of the software. If the latest version of the software is greater than the current version of the software installed on User's PC then AutoUpdaterVB.NET shows an update dialog to the user. If user presses the update button to update the software then it downloads the update file (Installer) from the URL provided in the XML file and executes the installer file it just downloaded. It is the job of the installer after this point to carry out the update. If you provide a .zip file URL instead of an installer file, then AutoUpdaterVB.NET will extract the contents of .zip file to the application directory.
It also has a number of properties that you can set to alter how the application works.  These are described below.


### XML file

AutoUpdaterVB.NET uses a XML file (such as Version.xml) located on a server to get the release information about the latest version of the software. You need to create a XML file like the one below and then you need to upload it to your server.

````xml
<?xml version="1.0" encoding="UTF-8"?>
<item>
    <version>2.0.0.0</version>
    <url>http://Yourwebsite.xxx/Updates/AutoUpdaterVB.NET.zip</url>
    <changelog>https://github.com/WolvenRA/AutoUpdaterVB.NET/releases</changelog>
    <mandatory>false</mandatory>
</item>
````

There are two things you HAVE to provide in XML file you see above.

* version (Required) : You need to provide the latest version of the application between version tags. Version should be in X.X.X.X format.
* url (Required): You need to provide the URL of the latest version installer file or zip file between url tags. AutoUpdaterVB.NET downloads the file provided here and installs it when user presses the Update button.
* changelog (Optional): You can provide the URL of the change log of your application between changelog tags. If you don't provide the URL of the changelog then the update dialog won't show the change log.
* mandatory (Optional): You can set this to true if you don't want the user to skip this version. This will ignore Remind Later and Skip options and hide both the Skip and Remind Later buttons on the update dialog.
  * mode (Attribute, Optional): You can provide a mode attribute for the mandatory element to change the behaviour of the mandatory flag. If you provide "1" as the mode attribute value then it will hide the Close button on the update dialog. If you provide "2" as the mode attribute value then it will skip the update dialog and go directly to downloading and updating the application automatically.

   ````xml
   <mandatory mode="2">true</mandatory>
   ````

  * minVersion (Attribute, Optional): You can also prvoide a minVersion attribute for the mandatory element. When you provide it, the Mandatory option will be triggered only if the installed version of the app is less than the mininum version you specified here.

   ````xml
   <mandatory minVersion="1.2.0.0">true</mandatory>
   ````

* args (Optional): You can provide command line arguments for the Installer between this tag. You can include %path% with your command line arguments, it will be replaced by path of the directory where the currently executing application resides.
* checksum (Optional): You can also provide a checksum for the update file between this tag. If you do this AutoUpdaterVB.NET will compare the checksum of the downloaded file before executing the update process to check the integrity of the file. You can provide an algorithm attribute for the checksum tag to specify which algorithm should be used to generate the checksum of the downloaded file. Currently, MD5, SHA1, SHA256, SHA384, and SHA512 are supported.

````xml
<checksum algorithm="MD5">Update file Checksum</checksum>
````

### You only HAVE to add a couple of lines to your application to implement AutoUpdaterVB.NET.

After you're done creating and uploading the XML file, It is very easy to add the auto update functionality to your application. First you need to add following line at the top of your form.

````vb.net
Imports AutoUpdaterVB.NET
````

Now you just need to add the following line to your main form constructor or in the Form_Load event or some subroutine called during the opening of your application. You can add this line anywhere you like. If you don't like to check for updates when the application starts then you can create a Check for Update button and add this line to a ButtonUpdate.Click event.

````vb.net
AutoUpdaterVB.NET.AutoUpdaterVBDotNET.Start("http://Yourwebsite.xxx/Updates/Version.xml")
````

The Start method of the AutoUpdater class takes the URL of the XML file you uploaded to the server as a parameter.

AutoUpdater.Start should be called from UI thread.

If you want to modify some of the properties before starting the AutoUpdater, use the lines below.  
There are also a number of other properties described later that you could set before calling the Start function.

' If you want to open download page when user click on download button uncomment below line.
'  AutoUpdater.OpenDownloadPage = False

' Don't want user to select remind later time in AutoUpdater notification window then uncomment 3 lines below so default remind later time will be set to 2 days.
'  AutoUpdater.LetUserSelectRemindLater = False
'  AutoUpdater.RemindLaterTimeSpan = RemindLaterFormat.Days
'  AutoUpdater.RemindLaterAt = 2

' Don't want to show Skip button then uncomment below line.
'  AutoUpdater.ShowSkipButton = False

  AutoUpdaterVB.NET.AutoUpdaterVBDotNET.Start("http://Yourwebsite.xxx/Updates/Version.xml")


### Current version detection

AutoUpdaterVB.NET uses the Assembly version to determine the current version of the application. You can update it by opening the Properties of the project and then on the "Application" tab click the "Assmebley Information" button and then set the version and any other pertinent information.

The Version specified in the XML file should be higher than Assembly version to trigger the update.

If you want to provide your own Assembly then you can do it by providing a second argument to the Start method as shown below.

````vb.net
AutoUpdaterVB.NET.AutoUpdaterVBDotNET.Start("http://Yourwebsite.xxx/Updates/Version.xml", myAssembly)
````

## Configuration Options

### Provide installed version manually

If you don't want AutoUpdaterVB.NET to determine the installed version from the currently executing assembly then you can provide your own version by assigning it to InstalledVersion field as shown below.

````vb.net
AutoUpdater.InstalledVersion = new Version("1.2")
````

### Download the Update file and XML using FTP

If you like to use a ftp XML URL to check for updates or download the update file then you can provide you FTP credentials in an alternative Start method as shown below.

````vb.net
AutoUpdaterVB.NET.AutoUpdaterVBDotNET.Start("ftp://Yourwebsite.xxx/Updates/Version.xml", new NetworkCredential("FtpUserName", "FtpPassword"))
````

If you are using a FTP download URL in the XML file then the credentials provided here will be used to authenticate the request.

### Check for updates synchronously

If you want to check for updates synchronously then set Synchronous to true before starting the update as shown below.

````vb.net
AutoUpdater.Synchronous = True
````

### Disable the Skip Button

If you don't want to show the Skip button on the Update form then just add the following line.

````vb.net
AutoUpdater.ShowSkipButton = False
````

### Disable the Remind Later Button

If you don't want to show the Remind Later button on Update form then just add the following line.

````vb.net
AutoUpdater.ShowRemindLaterButton = False
````

### Ignore previous Remind Later or Skip settings

If you want to ignore previously set Remind Later and Skip settings then you can set the Mandatory property to true. It will also hide the Skip and Remind Later buttons. If you set Mandatory to True in code then the value of Mandatory in your XML file will be ignored.

````vb.net
AutoUpdater.Mandatory = True
````

### Forced updates

You can enable forced updates by setting the Mandatory property to True and setting the UpdateMode to a value of `Mode.Forced` or `Mode.ForcedDownload`. The `Mode.Forced` option will hide the Remind Later, Skip and Close buttons on the update dialog. The `Mode.ForcedDownload` option will skip the standard update dialog and start downloading and updating the application without user interaction. The `Mode.ForceDownload` option will also ignore the value of the OpenDownloadPage flag.

````vb.net
AutoUpdater.Mandatory = True
AutoUpdater.UpdateMode = Mode.Forced
````

### Basic Authentication

You can provide Basic Authentication for the XML, Update file and Change Log as shown in below code.

````vb.net
BasicAuthentication basicAuthentication = new BasicAuthentication("myUserName", "myPassword");
AutoUpdater.BasicAuthXML = AutoUpdater.BasicAuthDownload = AutoUpdater.BasicAuthChangeLog = basicAuthentication
````

### Set User-Agent for the http web requests

Set the User-Agent string to be used for HTTP web requests so you can differentiate them in your web server request logs.

````vb.net
AutoUpdater.HttpUserAgent = "AutoUpdater";
````

### Enable Error Reporting

You can turn on error reporting by adding the code below. If you do this AutoUpdaterVB.NET will show error messages, if there is no update available or if it can't access the XML file on the web server.

````vb.net
AutoUpdater.ReportErrors = True
````

### Run update process without Administrator privileges

If your application doesn't need administrator privileges to replace the old version then you can set RunUpdateAsAdmin to false, although I wouldn't recomend it.

````vb.net
AutoUpdater.RunUpdateAsAdmin = False
````

### Open Download Page

If you DON'T want to download the latest version of the application and instead you just want to open the URL between url tags of your XML file then you need to add the following line.  I wouldn't recommend this either.
This kind of scenario is useful if you want to show some information to users before they download the latest version of the application.

````vb.net
AutoUpdater.OpenDownloadPage = True
````

### Remind Later

If you don't want users to select the Remind Later time when they press the Remind Later button of the update dialog then you need to add following lines.
In the example below, when the user presses the Remind Later button of the update dialog, it will set the RemindLater time to 2 days.
The way the RemindLater is actually implemented is when the application calls the AutoUpdaterVB.NET.AutoUpdaterVBDotNET.Start function, AutoUpdater will retrieve the 
ReminderLater date\time stored in the registry and compare that to the current date\time.  If the current date\time is less than the ReminderLater date\time 
then AutoUpdater will not display the update dialog or download and install the new version.  
Importantly, it DOESN'T actually create a Reminder messaage that will pop up at the ReminderLater date\time.

````vb.net
AutoUpdater.LetUserSelectRemindLater = False
AutoUpdater.RemindLaterTimeSpan = RemindLaterFormat.Days
AutoUpdater.RemindLaterAt = 2
````


### Proxy Server

If your XML and Update file can only be used from a certain Proxy Server then you can use following settings to tell AutoUpdaterVB.NET to use that proxy. Currently, if your Changelog URL is also restricted to a Proxy server then you should omit the changelog tag from the XML file because it is not supported when using a Proxy Server.

````vb.net
Dim MyProxy = New WebProxy("ProxyIP:ProxyPort", True) _
    With {.Credentials = New NetworkCredential("ProxyUserName", "ProxyPassword")}

AutoUpdater.Proxy = proxy;
````

### Specify where to download the update file

You can specify where you want to download the update file to by assigning a DownloadPath field as shown below. It will be used for the ZipExtractor too.

````vb.net
AutoUpdater.DownloadPath = Environment.CurrentDirectory
````

### Specify where to extract zip file containing the updated files

If you are using a zip file as an update file then you can set the "InstallationPath" to the path where your app is installed. This is only necessary when your installation directory differs from your executable path.

````vb.net
dim currentDirectory = new DirectoryInfo(Environment.CurrentDirectory)
If (currentDirectory.Parent != Nothing) Then
  AutoUpdater.InstallationPath = currentDirectory.Parent.FullName
EndIf
````

### Change the storage method of the Remind Later and Skip options.

You can change how AutoUpdaterVB.NET saves the Remind Later and Skip values by assigning the PersistenceProvider. If you don't provide a PersistenceProvider then it will save the values in the Windows registry.
You can create your own PersistenceProvider by implementing the IPersistenceProvider interface which you can find in the release source code .zip file.

## Check updates frequently

You can call the Start method inside a Timer to check for updates frequently.

### WinForms

````vb.net
Imports System.Timers

Dim Timer As New System.Timers.Timer(2 * 60 * 1000)

AddHandler Timer.Elapsed, New ElapsedEventHandler(AddressOf TimerElapsed)

Timer.Start()

Sub TimerElapsed(ByVal sender As Object, ByVal e As ElapsedEventArgs)
  AutoUpdaterVB.NET.AutoUpdaterVBDotNET.Start("http://Yourwebsite.xxx/Updates/Version.xml")
End Sub
````

## Handling the Application Exit logic manually.

If you want to handle the Application Exit logic yourself then you can use an ApplicationExit Event like below. This is very useful if you like to do something before closing the application.

````vb.net
Private Sub Aplication_FormClosing(ByVal sender As Object, _
ByVal e As FormClosingEventArgs) _
Handles Me.FormClosing

  Me.Text = @"Closing application..."
  Thread.Sleep(5000)
  Application.Exit()

End Sub
````
