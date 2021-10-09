Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Net.Mime
Imports System.Security.Cryptography
Imports System.Text
Imports System.Windows.Forms
Imports AutoUpdaterVB.NET.My.Resources

Namespace AutoUpdaterVBDotNET

  Public Class DownloadUpdateDialog

    Private ReadOnly _args As UpdateInfoEventArgs

    Private _tempFile, WrkString1, WrkString2, WrkString3, WrkString4 As String
    Private _webClient As MyWebClient
    Private _startedAt As DateTime

    Public Sub New(ByVal args As UpdateInfoEventArgs)

      InitializeComponent()

      _args = args

      If AutoUpdater.Mandatory AndAlso AutoUpdater.UpdateMode = Mode.ForcedDownload Then
        ControlBox = False
      End If
    End Sub

    Private Sub DownloadUpdateDialogLoad(sender As Object, e As EventArgs) _
    Handles Me.Load

      Dim Wrk_Uri = New Uri(_args.DownloadURL)

      If Wrk_Uri.Scheme.Equals(Uri.UriSchemeFtp) Then
        _webClient = New MyWebClient With {
          .Credentials = AutoUpdater.FtpCredentials}

      Else
        _webClient = AutoUpdater.GetWebClient(Wrk_Uri, AutoUpdater.BasicAuthDownload)

        If Wrk_Uri.Scheme.Equals(Uri.UriSchemeHttp) OrElse Wrk_Uri.Scheme.Equals(Uri.UriSchemeHttps) Then
          If AutoUpdater.BasicAuthDownload IsNot Nothing Then
            _webClient.Headers(HttpRequestHeader.Authorization) = AutoUpdater.BasicAuthDownload.ToString()
          End If

          _webClient.Headers(HttpRequestHeader.UserAgent) = AutoUpdater.GetUserAgent()
        End If
      End If

      If String.IsNullOrEmpty(AutoUpdater.DownloadPath) Then
        _tempFile = Path.GetTempFileName()

      Else
        _tempFile = Path.Combine(AutoUpdater.DownloadPath, "" & Guid.NewGuid().ToString() & ".tmp")

        If Not Directory.Exists(AutoUpdater.DownloadPath) Then
          Directory.CreateDirectory(AutoUpdater.DownloadPath)
        End If
      End If


      AddHandler _webClient.DownloadProgressChanged, AddressOf OnDownloadProgressChanged
      AddHandler _webClient.DownloadFileCompleted, AddressOf WebClientOnDownloadFileCompleted

      _webClient.DownloadFileAsync(Wrk_Uri, _tempFile)

    End Sub

    Private Sub OnDownloadProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs)

      If _startedAt = Nothing Then
        _startedAt = DateTime.Now

      Else
        Dim timeSpan = DateTime.Now - _startedAt
        Dim totalSeconds As Long = CLng(timeSpan.TotalSeconds)

        If totalSeconds > 0 Then
          Dim bytesPerSecond = e.BytesReceived / totalSeconds
          labelInformation.Text = String.Format(My.Resources.DownloadSpeedMessage, BytesToString(bytesPerSecond))
        End If
      End If

      labelSize.Text = "" & BytesToString(e.BytesReceived) & "/" & BytesToString(e.TotalBytesToReceive) & ""
      progressBar.Value = e.ProgressPercentage

    End Sub

    Private Sub WebClientOnDownloadFileCompleted(ByVal sender As Object, ByVal asyncCompletedEventArgs As AsyncCompletedEventArgs)

      If asyncCompletedEventArgs.Cancelled Then
        Return
      End If

      Try
        If asyncCompletedEventArgs.[Error] IsNot Nothing Then
          Throw asyncCompletedEventArgs.[Error]
        End If

        If _args.CheckSum IsNot Nothing Then
          CompareChecksum(_tempFile, _args.CheckSum)
        End If

        Dim fileName As String = ""
        Dim contentDisposition As String = If(_webClient.ResponseHeaders("Content-Disposition"), String.Empty)

        If String.IsNullOrEmpty(contentDisposition) Then
          fileName = Path.GetFileName(_webClient.ResponseUri.LocalPath)
        Else
          fileName = TryToFindFileName(contentDisposition, "filename=")

          If String.IsNullOrEmpty(fileName) Then
            fileName = TryToFindFileName(contentDisposition, "filename*=UTF-8''")
          End If
        End If

        Dim tempPath = Path.Combine(If(String.IsNullOrEmpty(AutoUpdater.DownloadPath), Path.GetTempPath(), AutoUpdater.DownloadPath), fileName)

        If File.Exists(tempPath) Then
          File.Delete(tempPath)
        End If

        File.Move(_tempFile, tempPath)

        Dim installerArgs As String = Nothing

        If Not String.IsNullOrEmpty(_args.InstallerArgs) Then
          installerArgs = _args.InstallerArgs.Replace("%path%", Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName))
        End If

        Dim processStartInfo = New ProcessStartInfo With {
          .FileName = tempPath,
          .UseShellExecute = True,
          .Arguments = If(installerArgs, String.Empty)}
'          .Arguments = installerArgs}

        Dim extension = Path.GetExtension(tempPath)

        If extension.Equals(".zip", StringComparison.OrdinalIgnoreCase) Then
          Dim installerPath As String = Path.Combine(Path.GetDirectoryName(tempPath), "ZipExtractor.exe")

          Try
            File.WriteAllBytes(installerPath, My.Resources.ZipExtractor)
          Catch e As Exception
            MessageBox.Show(e.Message, e.[GetType]().ToString(), MessageBoxButtons.OK, MessageBoxIcon.[Error])
            _webClient = Nothing
            Close()
            Return
          End Try

          Dim executablePath As String = Process.GetCurrentProcess().MainModule.FileName
          Dim extractionPath As String = Path.GetDirectoryName(executablePath)

          If Not String.IsNullOrEmpty(AutoUpdater.InstallationPath) _
          AndAlso Directory.Exists(AutoUpdater.InstallationPath) Then
            extractionPath = AutoUpdater.InstallationPath
          End If

          WrkString1 = "" & tempPath & ""
          WrkString2 = "" & extractionPath & ""
          WrkString3 = "" & executablePath & ""
          WrkString3 = WrkString3.Replace(".vshost", "")


          WrkString4 = """" & WrkString1 & """" & " """ & WrkString2 & """" & " """ & WrkString3 & """"

          'Dim arguments As StringBuilder = New StringBuilder(WrkString3)
          Dim arguments As StringBuilder = New StringBuilder(WrkString4)

          Dim args As String() = Environment.GetCommandLineArgs()

          For i As Integer = 1 To args.Length - 1
            If i.Equals(1) Then
              arguments.Append(" """)
            End If

            arguments.Append(args(i))
            arguments.Append(If(i.Equals(args.Length - 1), """", " "))
          Next

          processStartInfo = New ProcessStartInfo With {
            .FileName = installerPath,
            .UseShellExecute = True,
            .Arguments = arguments.ToString()}

        ElseIf extension.Equals(".msi", StringComparison.OrdinalIgnoreCase) Then
          processStartInfo = New ProcessStartInfo With {
              .FileName = "msiexec",
            .Arguments = "/i """ & tempPath & """"}

          If Not String.IsNullOrEmpty(installerArgs) Then
            processStartInfo.Arguments += " " & installerArgs
          End If
        End If

        If AutoUpdater.RunUpdateAsAdmin Then
          processStartInfo.Verb = "runas"
        End If

        Try
          Process.Start(processStartInfo)

        Catch exception As Win32Exception
          If exception.NativeErrorCode = 1223 Then
            _webClient = Nothing
          Else
            Throw
          End If
        End Try

      Catch e As Exception
        MessageBox.Show(e.Message, e.[GetType]().ToString(), MessageBoxButtons.OK, MessageBoxIcon.[Error])
        _webClient = Nothing

      Finally
        DialogResult = If(_webClient Is Nothing, DialogResult.Cancel, DialogResult.OK)
'        FormClosing = AddressOf DownloadUpdateDialog_FormClosing
        Me.Close()

      End Try
    End Sub

    Private Shared Function BytesToString(ByVal byteCount As Long) As String

      Dim suf As String() = {"B", "KB", "MB", "GB", "TB", "PB", "EB"}

      If byteCount = 0 Then
        Return "0" & suf(0)
      End If

      Dim bytes As Long = Math.Abs(byteCount)
      Dim place As Integer = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)))
      Dim num As Double = Math.Round(bytes / Math.Pow(1024, place), 1)

      Return "" & (Math.Sign(byteCount) * num).ToString(CultureInfo.InvariantCulture) & suf(place) & ""

    End Function

    Private Shared Function TryToFindFileName(ByVal contentDisposition As String, ByVal lookForFileName As String) As String

      Dim fileName As String = String.Empty

      If Not String.IsNullOrEmpty(contentDisposition) Then
        Dim index = contentDisposition.IndexOf(lookForFileName, StringComparison.CurrentCultureIgnoreCase)
        If index >= 0 Then
          fileName = contentDisposition.Substring(index + lookForFileName.Length)
        End If

        If fileName.StartsWith("""") Then
          Dim file = fileName.Substring(1, fileName.Length - 1)
          Dim i = file.IndexOf("""", StringComparison.CurrentCultureIgnoreCase)

          If i <> -1 Then
            fileName = file.Substring(0, i)
          End If
        End If
      End If

      Return fileName

    End Function

    Private Shared Sub CompareChecksum(ByVal fileName As String, ByVal prm_checksum As CheckSum)

      Using Wrk_HashAlgorithm = HashAlgorithm.Create(prm_checksum.HashingAlgorithm)
        Using stream = File.OpenRead(fileName)

          If Wrk_HashAlgorithm IsNot Nothing Then
            Dim hash = Wrk_HashAlgorithm.ComputeHash(stream)
            Dim fileChecksum = BitConverter.ToString(hash).Replace("-", String.Empty).ToLowerInvariant()

            If fileChecksum = prm_checksum.ToString.ToLower() Then
              Return
            End If

            Throw New Exception(My.Resources.FileIntegrityCheckFailedMessage)

          Else
            Throw New Exception(My.Resources.HashAlgorithmNotSupportedMessage)
          End If

'          Return False
        End Using
      End Using

    End Sub

    Private Sub DownloadUpdateDialog_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) _
    Handles Me.FormClosing

      If AutoUpdater.Mandatory AndAlso AutoUpdater.UpdateMode = Mode.ForcedDownload Then

        If ModifierKeys = Keys.Alt OrElse ModifierKeys = Keys.F4 Then
          e.Cancel = True
          Return
        End If
      End If

      If _webClient.IsBusy Then
        _webClient.CancelAsync()
        DialogResult = DialogResult.Cancel
      End If

    End Sub

  End Class

End Namespace
