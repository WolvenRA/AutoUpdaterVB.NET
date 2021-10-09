Imports System
Imports System.Net

Namespace AutoUpdaterVBDotNET

  '/ <summary>
  '/     Provides credentials for Network Authentication.
  '/ </summary>
  Public Class NetworkAuthentication
    Implements IAuthentication

    Private Property Username As String
    Private Property Password As String

    '/ <summary>
    '/     Initializes credentials for Network Authentication.
    '/ </summary>
    '/ <param name="username">Username to use for Network Authentication</param>
    '/ <param name="password">Password to use for Network Authentication</param>
    Public Sub New(prm_username As String, prm_password As String)
      Username = prm_username
      Password = prm_password
    End Sub

    '/ <inheritdoc />
    Public Sub Apply(ByRef webClient As MyWebClient) Implements IAuthentication.Apply
      webClient.Credentials = New NetworkCredential(Username, Password)
    End Sub

  End Class

End Namespace
