Imports System
Imports System.Diagnostics
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Reflection
Imports System.Windows.Forms
Imports Microsoft.Win32

Namespace AutoUpdaterVBDotNET

  Public Class UpdateForm

    Private ReadOnly _args As UpdateInfoEventArgs

    Private Property HideReleaseNotes As Boolean


    Public Sub New(ByVal args As UpdateInfoEventArgs)

      InitializeComponent()

      _args = args

      UseLatestIE()

      buttonSkip.Visible = AutoUpdater.ShowSkipButton
      buttonRemindLater.Visible = AutoUpdater.ShowRemindLaterButton

      Dim resources = New System.ComponentModel.ComponentResourceManager(GetType(UpdateForm))

      Me.Text = AutoUpdater.AppTitle & " version: " & _args.CurrentVersion.ToString & " is available!"

      labelUpdate.Text = "A new version of " & AutoUpdater.AppTitle & " is available!. "
      '      labelUpdate.Text = String.Format(resources.GetString("labelUpdate.Text", CultureInfo.CurrentCulture), AutoUpdater.AppTitle)

      labelDescription.Text = AutoUpdater.AppTitle & " " & _args.CurrentVersion.ToString & " is now available.  You have version " _
                            & _args.InstalledVersion.ToString & " installed.  Would you like to update now?"

      '      labelDescription.Text = String.Format(resources.GetString("labelDescription.Text", CultureInfo.CurrentCulture), AutoUpdater.AppTitle, _args.CurrentVersion, _args.InstalledVersion)

      If AutoUpdater.Mandatory AndAlso AutoUpdater.UpdateMode = Mode.Forced Then
        ControlBox = False
      End If

      UseLatestIE()

      If String.IsNullOrEmpty(_args.ChangelogURL) Then
        HideReleaseNotes = True

        Dim reduceHeight = labelReleaseNotes.Height + webBrowser.Height
        labelReleaseNotes.Hide()
        webBrowser.Hide()
        Height -= reduceHeight
      End If

    End Sub

    Private Sub UseLatestIE()

      Dim ieValue As Integer = 0

      Select Case WebBrowser.Version.Major
        Case 11
          ieValue = 11001
        Case 10
          ieValue = 10001
        Case 9
          ieValue = 9999
        Case 8
          ieValue = 8888
        Case 7
          ieValue = 7000
      End Select

      If ieValue <> 0 Then
        Try
          Using registryKey As RegistryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", True)
            If registryKey Is Nothing Then
              registryKey.SetValue(Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName), ieValue, RegistryValueKind.DWord)
            End If
          End Using

        Catch __unusedException1__ As Exception
        End Try
      End If

    End Sub

    Private Sub UpdateFormLoad(ByVal sender As Object, ByVal e As EventArgs) _
    Handles Me.Load

'      If String.IsNullOrEmpty(_args.ChangelogURL) Then
'        Dim reduceHeight = labelReleaseNotes.Height + webBrowser.Height

'      Else
        If AutoUpdater.BasicAuthChangeLog IsNot Nothing Then
          webBrowser.Navigate(_args.ChangelogURL, "", Nothing, "Authorization: " & AutoUpdater.BasicAuthChangeLog.ToString)
        Else
          webBrowser.Navigate(_args.ChangelogURL)
        End If
'      End If

      Dim labelSize = New Size(Width - 110, 0)
      labelDescription.MaximumSize = labelUpdate.MaximumSize

    End Sub

    Private Sub ButtonUpdateClick(ByVal sender As Object, ByVal e As EventArgs) _
    Handles buttonUpdate.Click

      If AutoUpdater.OpenDownloadPage Then
        Dim processStartInfo = New ProcessStartInfo(_args.DownloadURL)
        Process.Start(processStartInfo)
        DialogResult = DialogResult.OK

      Else
        If AutoUpdater.DownloadUpdate(_args) Then
          DialogResult = DialogResult.OK
        End If
      End If

    End Sub

    Private Sub ButtonSkipClick(ByVal sender As Object, ByVal e As EventArgs) _
    Handles buttonSkip.Click

      AutoUpdater.PersistenceProvider.SetSkippedVersion(New Version(_args.CurrentVersion))

    End Sub

    Private Sub ButtonRemindLaterClick(ByVal sender As Object, ByVal e As EventArgs) _
    Handles buttonRemindLater.Click

      Dim remindLaterDateTime As DateTime = DateTime.Now

      If AutoUpdater.LetUserSelectRemindLater Then

        Using remindLaterForm = New RemindLaterForm()
          Dim dialogResult = remindLaterForm.ShowDialog()

          If dialogResult.Equals(dialogResult.OK) Then
            AutoUpdater.RemindLaterTimeSpan = remindLaterForm.RemindLaterFormat
            AutoUpdater.RemindLaterAt = remindLaterForm.RemindLaterAt

          ElseIf dialogResult.Equals(dialogResult.Abort) Then
            ButtonUpdateClick(sender, e)
            Return

          Else
            Return
          End If
        End Using
      End If

      AutoUpdater.PersistenceProvider.SetSkippedVersion(Nothing)

      If AutoUpdater.RemindLaterAt <> 0 Then
        Select Case AutoUpdater.RemindLaterTimeSpan
          Case RemindLaterFormat.Days
            remindLaterDateTime = DateTime.Now + TimeSpan.FromDays(AutoUpdater.RemindLaterAt)
          Case RemindLaterFormat.Hours
            remindLaterDateTime = DateTime.Now + TimeSpan.FromHours(AutoUpdater.RemindLaterAt)
          Case RemindLaterFormat.Minutes
            remindLaterDateTime = DateTime.Now + TimeSpan.FromMinutes(AutoUpdater.RemindLaterAt)
        End Select

        AutoUpdater.PersistenceProvider.SetRemindLater(remindLaterDateTime)
        AutoUpdater.SetTimer(remindLaterDateTime)

      Else
        Running = False
        DialogResult = 0
        Return
      End If

      DialogResult = DialogResult.Cancel

    End Sub

    Private Sub ButtonCancelClick(ByVal sender As Object, ByVal e As EventArgs) _
    Handles ButtonCancel.Click

      DialogResult = DialogResult.Cancel
      Me.Close()

    End Sub

    Private Sub UpdateForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) _
    Handles Me.FormClosing

      If AutoUpdater.Mandatory AndAlso AutoUpdater.UpdateMode = Mode.Forced Then
        e.Cancel = ModifierKeys = Keys.Alt OrElse ModifierKeys = Keys.F4
      End If

    End Sub

    Private Sub UpdateForm_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) _
    Handles Me.FormClosed

      AutoUpdater.Running = False

    End Sub

  End Class

End Namespace
