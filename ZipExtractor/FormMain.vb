Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.IO
Imports System.IO.Compression
Imports System.Text
Imports System.Windows.Forms
Imports ZipExtractor.My.Resources

Namespace ZipExtractor

  Public Class FormMain

    Private Started As Boolean = False
    Private _backgroundWorker As BackgroundWorker
    Private TotProgressPercentage As UInteger

    ReadOnly _logBuilder As StringBuilder = New StringBuilder()

    Private Sub FormMain_Load(ByVal sender As Object, ByVal e As EventArgs) _
    Handles Me.Load

      Try
        If Started = False Then
          Begin()
        End If

      Catch exception As Win32Exception
        If exception.NativeErrorCode <> 1223 Then Throw
      End Try
    End Sub

    Private Sub Begin()

      Started = True

      TotProgressPercentage = 0

      _logBuilder.AppendLine(DateTime.Now.ToString("F"))
      _logBuilder.AppendLine()
      _logBuilder.AppendLine("ZipExtractor started with following command line arguments.")

      Dim args As String() = Environment.GetCommandLineArgs()
      labelInformation.Text = "Args = " & args.Length.ToString()

'      For Each arg In args
'        MessageBox.Show(arg)
'      Next

      For index = 0 To args.Length - 1
        Dim arg = args(index)
        _logBuilder.AppendLine("[" & index & "] " & arg)
      Next

      _logBuilder.AppendLine()

      If args.Length >= 4 Then

        _backgroundWorker = New BackgroundWorker With {
          .WorkerReportsProgress = True,
          .WorkerSupportsCancellation = True}

        AddHandler _backgroundWorker.DoWork, _
          Sub(o, eventArgs)

            For Each Wrk_Process In Process.GetProcesses()
              Try
                If Wrk_Process.MainModule.FileName.Equals(args(2)) Then
                  _logBuilder.AppendLine("Waiting for application Wrk_Process to Exit...")
                  labelInformation.Text = "Waiting for application to Exit..."
                  _backgroundWorker.ReportProgress(0, "Waiting for application to Exit...")
                  Wrk_Process.WaitForExit()
                End If

              Catch exception As Exception
                Debug.WriteLine(exception.Message)
              End Try
            Next

            _logBuilder.AppendLine("BackgroundWorker started successfully.")
'            labelInformation.Text = "BackgroundWorker started successfully."

            Dim DirPath = System.IO.Path.GetDirectoryName(args(3))
            Dim zip As ZipStorer = ZipStorer.Open(args(1), FileAccess.Read)
            Dim dir As List(Of ZipStorer.ZipFileEntry) = zip.ReadCentralDir()

            _logBuilder.AppendLine("Found total of " & dir.Count & "files and folders inside the zip file.")
            '            labelInformation.Text = "Found total of " & dir.Count & "files and folders inside the zip file."

            For index = 0 To dir.Count - 1
              If _backgroundWorker.CancellationPending Then
                eventArgs.Cancel = True
                zip.Close()
                Return
              End If

              Dim entry As ZipStorer.ZipFileEntry = dir(index)
              zip.ExtractFile(entry, System.IO.Path.Combine(DirPath, entry.FilenameInZip))

              Dim currentFile As String = String.Format(My.Resources.CurrentFileExtracting, entry.FilenameInZip)
              Dim progress As Integer = (index + 1) * 100 / dir.Count

              _backgroundWorker.ReportProgress(progress, currentFile)
              _logBuilder.AppendLine(currentFile & " [" & progress & "%]")
'              labelInformation.Text = currentFile & " [" & progress & "%]"
            Next

            zip.Close()
          End Sub

        AddHandler _backgroundWorker.ProgressChanged, Sub(o, eventArgs)
          progressBar.Value = eventArgs.ProgressPercentage
          labelInformation.Text = eventArgs.UserState.ToString()
        End Sub

        AddHandler _backgroundWorker.RunWorkerCompleted,  Sub(o, eventArgs)

          Try
            If eventArgs.[Error] IsNot Nothing Then
              Throw eventArgs.[Error]
            End If

            If Not eventArgs.Cancelled Then
              labelInformation.Text = "Finished"

              Try
                Dim processStartInfo As ProcessStartInfo = New ProcessStartInfo(args(3))

                If args.Length > 4 Then
                  processStartInfo.Arguments = args(4)
                End If

                Process.Start(processStartInfo)

                _logBuilder.AppendLine("Successfully launched the updated application.")
                labelInformation.Text = "Successfully launched the updated application."

              Catch exception As Win32Exception

                If exception.NativeErrorCode <> 1223 Then
                  Throw
                End If
              End Try
            End If

          Catch exception As Exception
            _logBuilder.AppendLine()
            _logBuilder.AppendLine(exception.ToString())

            MessageBox.Show(exception.Message, exception.[GetType]().ToString(), MessageBoxButtons.OK, _
                            MessageBoxIcon.[Error])

          Finally
            _logBuilder.AppendLine()
            Application.Exit()
          End Try
        End Sub

        _backgroundWorker.RunWorkerAsync()
      End If

    End Sub

    Private Sub FormMain_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) _
    Handles Me.FormClosing

      '      Do While TotProgressPercentage < 100
      '      TotProgressPercentage += 1
      '      progressBar.Value = TotProgressPercentage
      '      Loop

      If _backgroundWorker IsNot Nothing Then
        _backgroundWorker.CancelAsync()
      End If

      _logBuilder.AppendLine()

      File.AppendAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ZipExtractor.log"), _logBuilder.ToString())

    End Sub

  End Class

End Namespace
