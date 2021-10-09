Namespace AutoUpdaterVBDotNET

  '/ <summary>
  '/     Interface for authentication
  '/ </summary>
  Public Interface IAuthentication

    '/ <summary>
    '/     Apply the authentication to webclient.
    '/ </summary>
    '/ <param name="webClient">WebClient for which you want to use this authentication method.</param>
    Sub Apply(ByRef webClient As MyWebClient)

  End Interface

End Namespace
