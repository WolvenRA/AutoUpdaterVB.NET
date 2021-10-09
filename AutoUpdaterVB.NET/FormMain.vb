Imports System
Imports System.Globalization
Imports System.Management
Imports System.Reflection
Imports System.Windows.Forms

Namespace AutoUpdaterVBDotNET

  Public Class FormMain

    ' These are used by the Internet Connection test subr
    Private Declare Function InternetGetConnectedState Lib "wininet.dll" (ByRef lpdwFlags As Integer, ByVal dwReserved As Integer) As Integer
    Private Const InternetConnectionIsConfigured As Integer = &H40S

    Public Sub New()

      InitializeComponent()
      labelVersion.Text = "CurrentVersion is " & Assembly.GetEntryAssembly().GetName().Version.ToString
      labelUserID.Text = "User ID: " + System.Environment.UserName

    End Sub

    Private Sub FormMain_Load(ByVal sender As Object, ByVal e As EventArgs) _
    Handles Me.Load

      ' Uncomment below line to see russian version
'        AutoUpdater.CurrentCulture = CultureInfo.CreateSpecificCulture("ru");

      ' If you want to open download page when user click on download button uncomment below line.
'        AutoUpdater.OpenDownloadPage = False

      ' Don't want user to select remind later time in AutoUpdater notification window then uncomment 3 lines below so default remind later time will be set to 2 days.
'        AutoUpdater.LetUserSelectRemindLater = False
'        AutoUpdater.RemindLaterTimeSpan = RemindLaterFormat.Days
'        AutoUpdater.RemindLaterAt = 2

      ' Don't want to show Skip button then uncomment below line.
'        AutoUpdater.ShowSkipButton = False

      ' Want to handle update logic yourself then uncomment below line.
'        AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent

'        AutoUpdater.Start("http://Wolven.net/Uploads/WOUpdater/Version.xml")

    End Sub

    Private Sub buttonCheckForUpdate_Click(ByVal sender As Object, ByVal e As EventArgs) _
    Handles buttonCheckForUpdate.Click

      'Check if Internet connection is currently configured
      If InternetGetConnectedState(InternetConnectionIsConfigured, 0) = 0 Then
        MsgBox("internet isn't connected", MsgBoxStyle.Information, "information")

      ElseIf InternetGetConnectedState(InternetConnectionIsConfigured, 0) = 1 Then
'        MsgBox("internet is connected", MsgBoxStyle.Information, "information")
        AutoUpdater.Start("http://Wolven.net/Uploads/AutoUpdaterVB.Net/Version.xml")
      End If


    End Sub

  End Class

End Namespace
