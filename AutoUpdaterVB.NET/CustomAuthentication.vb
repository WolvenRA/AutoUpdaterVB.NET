Imports System.Net

Namespace AutoUpdaterVBDotNET

  '/ <summary>
  '/     Provides Custom Authentication header for web request.
  '/ </summary>
  Public Class CustomAuthentication
    Implements IAuthentication

    Private Wrk_HttpRequestHeaderAuthorizationValue As String

    Friend Property HttpRequestHeaderAuthorizationValue As String
      Get
        Return Wrk_HttpRequestHeaderAuthorizationValue
      End Get
      Set(value As String)
        Wrk_HttpRequestHeaderAuthorizationValue = value
      End Set
    End Property

    '/ <summary>
    '/     Initializes authorization header value for Custom Authentication
    '/ </summary>
    '/ <param name="httpRequestHeaderAuthorizationValue">Value to use as http request header authorization value</param>
    Public Sub New(ByVal prm_httpRequestHeaderAuthorizationValue As String)

      Wrk_HttpRequestHeaderAuthorizationValue = prm_httpRequestHeaderAuthorizationValue

    End Sub

    '/ <inheritdoc />
    Public Overrides Function ToString() As String

      Return HttpRequestHeaderAuthorizationValue

    End Function

    '/ <inheritdoc />
    Public Sub Apply(ByRef webClient As MyWebClient) Implements IAuthentication.Apply

      webClient.Headers(HttpRequestHeader.Authorization) = ToString()

    End Sub

  End Class
End Namespace
