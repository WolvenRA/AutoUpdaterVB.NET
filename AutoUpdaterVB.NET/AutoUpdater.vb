Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Net.Cache
Imports System.Reflection
Imports System.Threading
Imports System.Windows.Forms
Imports System.Xml
Imports System.Xml.Serialization
Imports AutoUpdaterVB.NET.My.Resources

Namespace AutoUpdaterVBDotNET

  '/ <summary>
  '/   Enum representing the remind later time span.
  '/ </summary>
  Public Enum RemindLaterFormat

    '/ <summary>
    '/   Represents the time span in minutes.
    '/ </summary>
    Minutes

    '/ <summary>
    '/   Represents the time span in hours.
    '/ </summary>
    Hours

    '/ <summary>
    '/   Represents the time span in days.
    '/ </summary>
    Days

  End Enum

  '/ <summary>
  '/     Enum representing the effect of Mandatory flag.
  '/ </summary>
  Public Enum Mode

    '/ <summary>
    '/ In this mode, it ignores Remind Later and Skip values set previously and hide both buttons.
    '/ </summary>
    <XmlEnum("0")>
    Normal

    '/ <summary>
    '/ In this mode, it won't show close button in addition to Normal mode behaviour.
    '/ </summary>
    <XmlEnum("1")>
    Forced

    '/ <summary>
    '/ In this mode, it will start downloading and applying update without showing standarad update dialog in addition to Forced mode behaviour.
    '/ </summary>
    <XmlEnum("2")>
    ForcedDownload

  End Enum


  '/ <summary>
  '/   Main class that lets you auto update applications by setting some static fields and executing its Start method.
  '/ </summary>
  Public Module AutoUpdater

    Private _remindLaterTimer As System.Timers.Timer
    Private _isWinFormsApplication As Boolean

    Friend BaseUri As Uri
    Friend Running As Boolean
    Friend ReturnCode As UInteger

    '/ <summary>
    '     You can set this field to your current version if you don't want to determine the version from the assembly.
    '/ </summary>
    Public InstalledVersion As Version

    '/ <summary>
    '/     Set it to folder path where you want to download the update file. If not provided then it defaults to Temp folder.
    '/ </summary>
    Public DownloadPath As String

    '/ <summary>
    '/     If you are using a zip file as an update file then you can set this value to path where your app is installed. This is only necessary when your installation directory differs from your executable path.
    '/ </summary>
    Public InstallationPath As String

    '/ <summary>
    '/     Set the Application Title shown in Update dialog. Although AutoUpdater.NET will get it automatically, you can set this property if you like to give custom Title.
    '/ </summary>
    Public AppTitle As String

    '/ <summary>
    '/   URL of the xml file that contains information about latest version of the application.
    '/ </summary>
    Public AppCastURL As String

    '/ <summary>
    '/ Login/password/domain for FTP-request
    '/ </summary>
    Public FtpCredentials As NetworkCredential

    '/ <summary>
    '/   Opens the download url in default browser if true. Very usefull if you have portable application.
    '/ </summary>
    Public OpenDownloadPage As Boolean

    '/ <summary>
    '/     Set Basic Authentication credentials required to download the file.
    '/ </summary>
'    Public BasicAuthDownload As BasicAuthentication
    Public BasicAuthDownload As IAuthentication

    '/ <summary>
    '/     Set Basic Authentication credentials required to download the XML file.
    '/ </summary>
'    Public BasicAuthXML As BasicAuthentication
    Public BasicAuthXML As IAuthentication

    '/ <summary>
    '/     Set Basic Authentication credentials to navigate to the change log URL. 
    '/ </summary>
'    Public BasicAuthChangeLog As BasicAuthentication
    Public BasicAuthChangeLog As IAuthentication

    '/ <summary>
    '/     Set the User-Agent string to be used for HTTP web requests.
    '/ </summary>
    Public HttpUserAgent As String

    '/ <summary>
    '/   Sets the current culture of the auto update notification window. Set this value if your application supports
    '/   functionalty to change the languge of the application.
    '/ </summary>
'    Public CurrentCulture As CultureInfo

    '/ <summary>
    '/   If this is true users can see the skip button.
    '/ </summary>
    Public ShowSkipButton As Boolean = False

    '/ <summary>
    '/     If this is true users can see the Remind Later button.
    '/ </summary>
    Public ShowRemindLaterButton As Boolean = True

    '/ <summary>
    '/   If this is true users see dialog where they can set remind later interval otherwise it will take the interval from
    '/   RemindLaterAt and RemindLaterTimeSpan fields.
    '/ </summary>
    Public LetUserSelectRemindLater As Boolean = True

    '/ <summary>
    '/   Remind Later interval after user should be reminded of update.
    '/ </summary>
    Public RemindLaterAt As Integer = 2

    '/ <summary>
    '/   Set if RemindLaterAt interval should be in Minutes, Hours or Days.
    '/ </summary>
    Public RemindLaterTimeSpan As RemindLaterFormat = RemindLaterFormat.Days

    '/<summary>
    '/     AutoUpdater.NET will report errors if this is true.
    '/ </summary>
    Public ReportErrors As Boolean = False

    '/ <summary>
    '/     Set this to false if your application doesn't need administrator privileges to replace the old version.
    '/ </summary>
    Public RunUpdateAsAdmin As Boolean = True

    '/ <summary>
    '/     Set this to true if you want to run update check synchronously.
    '/ </summary>
    Public Synchronous As Boolean = False

    '/<summary>
    '/     Set this to true if you want to ignore previously assigned Remind Later and Skip settings. It will also hide Remind Later and Skip buttons.
    '/ </summary>
    Public Mandatory As Boolean
'    Public Mandatory As Boolean = True

    '/ <summary>
    '/     Set this to any of the available modes to change behaviour of the Mandatory flag.
    '/ </summary>
    Public UpdateMode As Mode
'    Public UpdateMode As Mode = Mode.ForcedDownload

    '/ <summary>
    '/     Set Proxy server to use for all the web requests in AutoUpdater.NET.
    '/ </summary>
    Public Proxy As IWebProxy

    '/ <summary>
    '/ Set this to an instance implementing the IPersistenceProvider interface for using a data storage method different from the default Windows Registry based one.
    '/ </summary>
    Public PersistenceProvider As IPersistenceProvider

    '/ <summary>
    '/   A delegate type for hooking up update notifications.
    '/ </summary>
    Public Delegate Sub ApplicationExitEventHandler()

    '/ <summary>
    '/     An event that developers can use to exit the application gracefully.
    '/ </summary>
    Public Event ApplicationExitEvent As ApplicationExitEventHandler

    '/ <summary>
    '/   A delegate type for hooking up update notifications.
    '/ </summary>
    '/ <param name="args">An object containing all the parameters recieved from AppCast XML file. If there is an error while looking for the XML file then this object will be null.</param>
    Public Delegate Sub CheckForUpdateEventHandler(ByVal args As UpdateInfoEventArgs)

    '/ <summary>
    '/   An event that clients can use to be notified whenever the update is checked.
    '/ </summary>
    Public Event CheckForUpdateEvent As CheckForUpdateEventHandler

    '/ <summary>
    '/   A delegate type for hooking up parsing logic.
    '/ </summary>
    '/ <param name="args">An object containing all the parameters recieved from AppCast XML file. If there is an error while looking for the XML file then this object will be null.</param>
    Public Delegate Sub ParseUpdateInfoHandler(ByVal args As ParseUpdateInfoEventArgs)

    '/ <summary>
    '/   An event that clients can use to be notified whenever the AppCast file needs parsing.
    '/ </summary>
    Public Event ParseUpdateInfoEvent As ParseUpdateInfoHandler
    Public UpdateFormSize As Size? = Nothing


    '/ <summary>
    '/   Start checking for new version of application and display dialog to the user if update is available.
    '/ <param name="myAssembly">Assembly to use for version checking.</param>
    '/ </summary>
    Sub Start(Optional ByVal myAssembly As Assembly = Nothing)

      Start(AppCastURL, myAssembly)

    End Sub

    '/ <summary>
    '/   Start checking for new version of application and display dialog to the user if update is available.
    '/ </summary>
    '/ <param name="appCast">URL of the xml file that contains information about latest version of the application.</param>
    '/ <param name="myAssembly">Assembly to use for version checking.</param>
    Sub Start(ByVal appCast As String, _FtpCredentials As NetworkCredential, Optional ByVal myAssembly As Assembly = Nothing)

      FtpCredentials = _FtpCredentials
      Start(appCast, myAssembly)

    End Sub

    '/ <summary>
    '/   Start checking for new version of application and display dialog to the user if update is available.
    '/ </summary>
    '/ <param name="appCast">URL of the xml file that contains information about latest version of the application.</param>
    '/ <param name="myAssembly">Assembly to use for version checking.</param>
    Sub Start(ByVal appCast As String, Optional ByVal myAssembly As Assembly = Nothing)

      Try
        ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol Or CType(192, SecurityProtocolType) Or CType(768, SecurityProtocolType) Or CType(3072, SecurityProtocolType)
      Catch __unusedNotSupportedException1__ As NotSupportedException
      End Try

      If Mandatory AndAlso _remindLaterTimer IsNot Nothing Then
        _remindLaterTimer.[Stop]()
        _remindLaterTimer.Close()
        _remindLaterTimer = Nothing
      End If

      If Not Running AndAlso _remindLaterTimer Is Nothing Then
        Running = True
        AppCastURL = appCast
        _isWinFormsApplication = Application.MessageLoop

        If Not _isWinFormsApplication Then
          Application.EnableVisualStyles()
        End If


        Dim assembly As Assembly = If(myAssembly, Assembly.GetEntryAssembly())

        If Synchronous Then
          Try
            Dim result = CheckUpdate(assembly)

            ReturnCode = 0

            Running = StartUpdate(result)

            If ReturnCode <> 0 Then
              Running = False
            End If

          Catch exception As Exception
            ShowError(exception)
          End Try

        Else
          Using backgroundWorker = New BackgroundWorker()
            AddHandler backgroundWorker.DoWork, _
            Sub(sender, args)
              Dim mainAssembly As Assembly = TryCast(args.Argument, Assembly)
              args.Result = CheckUpdate(mainAssembly)
            End Sub

            AddHandler backgroundWorker.RunWorkerCompleted, _
            Sub(sender, args)
              If args.[Error] IsNot Nothing Then
                ShowError(args.[Error])

              Else
                If Not args.Cancelled Then
                  If StartUpdate(args.Result) Then
                    Return
                  End If
                End If
              End If

              Running = False
            End Sub

            backgroundWorker.RunWorkerAsync(assembly)
          End Using
        End If
      End If

    End Sub

    Private Function CheckUpdate(ByVal mainAssembly As Assembly) As Object

      Dim companyAttribute = CType(GetAttribute(mainAssembly, GetType(AssemblyCompanyAttribute)), AssemblyCompanyAttribute)
      Dim appCompany As String = If(companyAttribute IsNot Nothing, companyAttribute.Company, "")

      If String.IsNullOrEmpty(AppTitle) Then
        Dim titleAttribute = CType(GetAttribute(mainAssembly, GetType(AssemblyTitleAttribute)), AssemblyTitleAttribute)
        AppTitle = If(titleAttribute IsNot Nothing, titleAttribute.Title, mainAssembly.GetName().Name)
      End If

      Dim wrk_RegistryLocation As String = If(Not String.IsNullOrEmpty(appCompany), _
      "Software\" & appCompany & "\" & AppTitle & "\AutoUpdater", "Software\" & AppTitle & "\" & "AutoUpdater")
'      Dim wrk_RegistryLocation As String = If(Not String.IsNullOrEmpty(appCompany), $"Software\{appCompany}\{AppTitle}\AutoUpdater", $"Software\{AppTitle}\AutoUpdater")

      PersistenceProvider = New RegistryPersistenceProvider(wrk_RegistryLocation)

      BaseUri = New Uri(AppCastURL)
      Dim args As UpdateInfoEventArgs

      Using client As MyWebClient = GetWebClient(BaseUri, BasicAuthXML)
        Dim xml As String = client.DownloadString(BaseUri)

        If ParseUpdateInfoEventEvent Is Nothing Then
          Dim xmlSerializer As XmlSerializer = New XmlSerializer(GetType(UpdateInfoEventArgs))
          Dim xmlTextReader As XmlTextReader = New XmlTextReader(New StringReader(xml)) With {
               .XmlResolver = Nothing}

          args = CType(xmlSerializer.Deserialize(xmlTextReader), UpdateInfoEventArgs)

        Else
          Dim parseArgs As ParseUpdateInfoEventArgs = New ParseUpdateInfoEventArgs(xml)
          RaiseEvent ParseUpdateInfoEvent(parseArgs)
          args = parseArgs.UpdateInfo
        End If
      End Using

'      If String.IsNullOrEmpty(args?.CurrentVersion) OrElse String.IsNullOrEmpty(args.DownloadURL) Then
      If String.IsNullOrEmpty(args.CurrentVersion.ToString) OrElse String.IsNullOrEmpty(args.DownloadURL) Then
        Throw New MissingFieldException()
      End If

      args.InstalledVersion = If(InstalledVersion IsNot Nothing, InstalledVersion, mainAssembly.GetName().Version)
      args.IsUpdateAvailable = args.CurrentVersion.ToString > args.InstalledVersion.ToString
'      args.IsUpdateAvailable = args.CurrentVersion > args.InstalledVersion

      If Not Mandatory Then
        If args.Mandatory.MinimumVersion = Nothing Then
'        If String.IsNullOrEmpty(args.Mandatory.MinimumVersion.ToString) Then
          Mandatory = args.Mandatory.Value
          UpdateMode = args.Mandatory.UpdateMode

        Else
          If Not args.Mandatory.MinimumVersion = Nothing Then
            If args.InstalledVersion < New Version(args.Mandatory.MinimumVersion) Then
              Mandatory = args.Mandatory.Value
              UpdateMode = args.Mandatory.UpdateMode
            End If
          End If
        End If
      End If

      If Mandatory Then
        ShowRemindLaterButton = False
        ShowSkipButton = False

      Else
        Dim skippedVersion = PersistenceProvider.GetSkippedVersion()

        If skippedVersion IsNot Nothing Then
          Dim currentVersion = New Version(args.CurrentVersion)
          If currentVersion <= skippedVersion Then
            Return Nothing
          End If

          If currentVersion > skippedVersion Then
            PersistenceProvider.SetSkippedVersion(Nothing)
          End If
        End If

        Dim remindLaterAt = PersistenceProvider.GetRemindLater()

        If remindLaterAt IsNot Nothing Then
          Dim compareResult As Integer = DateTime.Compare(DateTime.Now, remindLaterAt.Value)

          If compareResult < 0 Then
            Return remindLaterAt.Value
          End If
        End If
      End If

      Return args

    End Function

    Private Function StartUpdate(ByVal result As Object) As Boolean

      Dim args = New UpdateInfoEventArgs()

      If result.[GetType]() = GetType(UpdateInfoEventArgs) Then
        args = TryCast(result, UpdateInfoEventArgs)

        If CheckForUpdateEventEvent IsNot Nothing Then
          RaiseEvent CheckForUpdateEvent(args)

        Else
          If args.IsUpdateAvailable Then
            If Mandatory AndAlso UpdateMode = Mode.ForcedDownload Then
              DownloadUpdate(args)
              [Exit]()
            Else

              If Thread.CurrentThread.GetApartmentState().Equals(ApartmentState.STA) Then
                ShowUpdateForm(args, ReturnCode)

              Else
                Dim thread As Thread = New Thread(New ThreadStart _
                (Sub()
                  ShowUpdateForm(args, ReturnCode)
                End Sub))

                thread.CurrentCulture = CultureInfo.CurrentCulture
                thread.SetApartmentState(ApartmentState.STA)
                thread.Start()
                thread.Join()
              End If
            End If

            Return True
          End If

          If ReportErrors Then
            MessageBox.Show(My.Resources.UpdateUnavailableMessage, My.Resources.UpdateUnavailableCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
          End If
        End If
      End If

      Return False
    End Function

    Private Sub ShowError(ByVal exception As Exception)

      If CheckForUpdateEventEvent IsNot Nothing Then
        RaiseEvent CheckForUpdateEvent(New UpdateInfoEventArgs With {
          .[Error] = exception})
      Else

        If ReportErrors Then
          If TypeOf exception Is WebException Then
            MessageBox.Show(My.Resources.UpdateCheckFailedMessage, My.Resources.UpdateCheckFailedCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
          Else
            MessageBox.Show(exception.Message, exception.[GetType]().ToString(), MessageBoxButtons.OK, MessageBoxIcon.[Error])
          End If
        End If
      End If

    End Sub

    Private Function GetAttribute(ByVal assembly As Assembly, ByVal attributeType As Type) As Attribute

      Dim attributes As Object() = assembly.GetCustomAttributes(attributeType, False)

      If attributes.Length = 0 Then
        Return Nothing
      End If

      Return CType(attributes(0), Attribute)

    End Function

    Friend Function GetUserAgent() As String

      Return If(String.IsNullOrEmpty(HttpUserAgent), "AutoUpdaterVB.NET", HttpUserAgent)

    End Function

    Friend Sub SetTimer(ByVal remindLater As DateTime)

      Dim timeSpan As TimeSpan = remindLater - DateTime.Now

      If timeSpan.Milliseconds > 0 Then
        Dim context = SynchronizationContext.Current
          _remindLaterTimer = New System.Timers.Timer With {
          .Interval = CInt(timeSpan.TotalMilliseconds),
          .AutoReset = False}

        AddHandler _remindLaterTimer.Elapsed,
        Sub()
          _remindLaterTimer = Nothing

          If context IsNot Nothing Then
            Try
              context.Send(Sub(__) Start(), Nothing)
            Catch __unusedInvalidAsynchronousStateException1__ As InvalidAsynchronousStateException
              Start()
            End Try

          Else
            Start()
          End If
        End Sub

        _remindLaterTimer.Start()
      End If

    End Sub

    '/ <summary>
    '/     Opens the Download window that download the update and execute the installer when download completes.
    '/ </summary>
    Public Function DownloadUpdate(ByVal args As UpdateInfoEventArgs) As Boolean

      Using downloadDialog = New DownloadUpdateDialog(args)
        Try
          Return downloadDialog.ShowDialog().Equals(DialogResult.OK)
        Catch __unusedTargetInvocationException1__ As TargetInvocationException
        End Try
      End Using

      Return False
    End Function

    Sub ShowUpdateForm(ByVal args As UpdateInfoEventArgs, ByRef prm_ReturnCode As UInteger)

      prm_ReturnCode = 0

      Using updateForm = New UpdateForm(args)

        If UpdateFormSize.HasValue Then
          updateForm.Size = UpdateFormSize.Value
        End If

        If updateForm.ShowDialog().Equals(DialogResult.OK) Then
          [Exit]()

        Else
        ' The user closed the form
'          [Exit]()
          prm_ReturnCode = 1
        End If
      End Using

    End Sub

    Friend Function GetWebClient(ByVal uri As Uri, ByVal basicAuthentication As IAuthentication) As MyWebClient

      Dim webClient = New MyWebClient With {
        .CachePolicy = New RequestCachePolicy(RequestCacheLevel.NoCacheNoStore)}

      If Proxy IsNot Nothing Then
        webClient.Proxy = Proxy
      End If

      If uri.Scheme.Equals(uri.UriSchemeFtp) Then
        webClient.Credentials = FtpCredentials

      Else
        If basicAuthentication IsNot Nothing Then
          webClient.Headers(HttpRequestHeader.Authorization) = basicAuthentication.ToString()
        End If

        webClient.Headers(HttpRequestHeader.UserAgent) = HttpUserAgent
      End If

      Return webClient

    End Function

    Private Sub [Exit]()

      Dim currentProcess = Process.GetCurrentProcess()

      For Each process In System.Diagnostics.Process.GetProcessesByName(currentProcess.ProcessName)
        Dim processPath As String

        Try
          processPath = process.MainModule.FileName
        Catch __unusedWin32Exception1__ As Win32Exception
          Continue For
        End Try

        If process.Id <> currentProcess.Id AndAlso currentProcess.MainModule.FileName = processPath Then
          If process.CloseMainWindow() Then
            process.WaitForExit(CInt(TimeSpan.FromSeconds(10).TotalMilliseconds))
          End If

          If Not process.HasExited Then
            process.Kill()
          End If
        End If
      Next

      If ApplicationExitEventEvent IsNot Nothing Then
        RaiseEvent ApplicationExitEvent()

      Else
        If _isWinFormsApplication Then
          Application.Exit()
'          methodInvoker.Invoke()
'          Dim methodInvoker As MethodInvoker = Application.Exit()
'          methodInvoker.Invoke()

        Else
          Environment.Exit(0)
        End If
      End If

    End Sub

  End Module
End Namespace
