Imports System
Imports System.Net
Imports System.Text

Namespace AutoUpdaterVBDotNET

  Public Class BasicAuthentication
    Implements IAuthentication

    Private UserNm As String
    Private PassWrd As String

    Public Property Username As String
      Get
        Return UserNm
      End Get
      Set(value As String)
        UserNm = Username
      End Set
    End Property

    Public Property Password As String
      Get
        Return PassWrd
      End Get
      Set(value As String)
        PassWrd = Password
      End Set
    End Property

    Public Sub New(ByVal prm_username As String, ByVal prm_password As String)

      UserNm = prm_username
      PassWrd = prm_password

    End Sub

    Public Overrides Function ToString() As String

      Dim token = System.Convert.ToBase64String(Encoding.ASCII.GetBytes("" & Username & ":" & Password & ""))
      Return "Basic " & token & ""

    End Function

    Public Sub Apply(ByRef webClient As MyWebClient) Implements IAuthentication.Apply

      webClient.Headers(HttpRequestHeader.Authorization) = ToString()

    End Sub

  End Class

End Namespace
