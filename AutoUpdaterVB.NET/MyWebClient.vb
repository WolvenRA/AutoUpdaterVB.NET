Imports System
Imports System.Net

Namespace AutoUpdaterVBDotNET

  '/ <inheritdoc />
  Public Class MyWebClient
    Inherits WebClient

    '/ <summary>
    '/     Response Uri after any redirects.
    '/ </summary>
    Public ResponseUri As Uri

    '/ <inheritdoc />
    Protected Overrides Function GetWebResponse(ByVal request As WebRequest, ByVal result As IAsyncResult) As WebResponse
      Dim webResponse As WebResponse = MyBase.GetWebResponse(request, result)
      ResponseUri = webResponse.ResponseUri
      Return webResponse
    End Function

  End Class

End Namespace
