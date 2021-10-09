Imports System
Imports System.Xml.Serialization

Namespace AutoUpdaterVBDotNET

  '/ <summary>
  '/     Object of this class gives you all the details about the update useful in handling the update logic yourself.
  '/ </summary>
  <XmlRoot("item")>
  Public Class UpdateInfoEventArgs
    Inherits EventArgs

    Private _changelogURL As String
    Private _downloadURL As String

    '/ <inheritdoc />
    Public Sub New()
      Mandatory = New Mandatory()
    End Sub

    '/ <summary>
    '/     If new update is available then returns true otherwise false.
    '/ </summary>
    Public Property IsUpdateAvailable As Boolean

    '/ <summary>
    '/     If there is an error while checking for update then this property won't be null.
    '/ </summary>
    <XmlIgnore()>
    Public Property [Error] As Exception

    '/ <summary>
    '/     Download URL of the update file.
    '/ </summary>
    <XmlElement("url")>
    Public Property DownloadURL As String

      Get
        Return GetURL(AutoUpdater.BaseUri, _downloadURL)
      End Get
      Set(ByVal value As String)
        _downloadURL = value
      End Set

    End Property

    '/ <summary>
    '/     URL of the webpage specifying changes in the new update.
    '/ </summary>
    <XmlElement("changelog")>
    Public Property ChangelogURL As String

      Get
        Return GetURL(AutoUpdater.BaseUri, _changelogURL)
      End Get
      Set(ByVal value As String)
        _changelogURL = value
      End Set

    End Property

    '/ <summary>
    '/     Returns newest version of the application available to download.
    '/ </summary>
    <XmlElement("version")>
    Public Property CurrentVersion As String

    '/ <summary>
    '/     Returns version of the application currently installed on the user's PC.
    '/ </summary>
    Public Property InstalledVersion As Version

    '/ <summary>
    '/     Shows if the update is required or optional.
    '/ </summary>
    <XmlElement("mandatory")>
    Public Property Mandatory As Mandatory

    '/ <summary>
    '/     Command line arguments used by Installer.
    '/ </summary>
    <XmlElement("args")>
    Public Property InstallerArgs As String

    '/ <summary>
    '/     Checksum of the update file.
    '/ </summary>
    <XmlElement("checksum")>
    Public Property CheckSum As CheckSum

    Friend Shared Function GetURL(ByVal baseUri As Uri, ByVal url As String) As String

      If Not String.IsNullOrEmpty(url) AndAlso Uri.IsWellFormedUriString(url, UriKind.Relative) Then
        Dim uri As Uri = New Uri(baseUri, url)

        If uri.IsAbsoluteUri Then
          url = uri.AbsoluteUri
        End If
      End If

      Return url

    End Function

  End Class

  '/ <summary>
  '/     Mandatory class to fetch the XML values related to Mandatory field.
  '/ </summary>
  Public Class Mandatory

    '/ <summary>
    '/     Value of the Mandatory field.
    '/ </summary>
    <XmlText()>
    Public Property Value As Boolean

    '/ <summary>
    '/     If this is set and 'Value' property is set to true then it will trigger the mandatory update only when current installed version is less than value of this property.
    '/ </summary>
    <XmlAttribute("minVersion")>
    Public Property MinimumVersion As String

    '/ <summary>
    '/     Mode that should be used for this update.
    '/ </summary>
    <XmlAttribute("mode")>
    Public Property UpdateMode As Mode

  End Class

  '/ <summary>
  '/     Checksum class to fetch the XML values for checksum.
  '/ </summary>
  Public Class CheckSum

    '/ <summary>
    '/     Hash of the file.
    '/ </summary>
    <XmlText()>
    Public Property Value As String

    '/ <summary>
    '/     Hash algorithm that generated the hash.
    '/ </summary>
    <XmlAttribute("algorithm")>
    Public Property HashingAlgorithm As String

  End Class

End Namespace
