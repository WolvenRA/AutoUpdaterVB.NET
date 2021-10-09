Imports System

Namespace AutoUpdaterVBDotNET

  '/ <summary>
  '/     An object of this class contains the AppCast file received from server.
  '/ </summary>
  Public Class ParseUpdateInfoEventArgs
    Inherits EventArgs

    '/ <summary>
    '/     Remote data received from the AppCast file.
    '/ </summary>
    Public Property RemoteData As String

    '/ <summary>
    '/      Set this object with values received from the AppCast file.
    '/ </summary>
    Public Property UpdateInfo As UpdateInfoEventArgs


    '/ <summary>
    '/     An object containing the AppCast file received from server.
    '/ </summary>
    '/ <param name="remoteData">A string containing remote data received from the AppCast file.</param>
    Public Sub New(ByVal prm_RemoteData As String)

      RemoteData = prm_RemoteData

    End Sub

  End Class

End Namespace
