Imports System
Imports System.Globalization
Imports Microsoft.Win32

Namespace AutoUpdaterVBDotNET

  '/ <summary>
  '/ Provides a mechanism for storing AutoUpdater state between sessions based on storing data on the Windows Registry.
  '/ </summary>
  Public Class RegistryPersistenceProvider
    Implements IPersistenceProvider

    '/ <summary>
    '/ Gets/sets the path for the Windows Registry key that will contain the data.
    '/ </summary>
    Private Property RegistryLocation As String

    Private Const RemindLaterValueName As String = "RemindLaterAt"
    Private Const SkippedVersionValueName As String = "SkippedVersion"

    '/ <summary>
    '/ Initializes a new instance of the RegistryPersistenceProvider class indicating the path for the Windows registry key to use for storing the data.
    '/ </summary>
    '/ <param name="registryLocation"></param>
    Public Sub New(ByVal prm_registryLocation As String)
      RegistryLocation = prm_registryLocation
    End Sub

    '/ <inheritdoc />
    Public Function GetSkippedVersion() As Version Implements IPersistenceProvider.GetSkippedVersion

      Try
        Using updateKey As RegistryKey = Registry.CurrentUser.OpenSubKey(RegistryLocation)
          Dim skippedVersionValue As Object = updateKey.GetValue(SkippedVersionValueName)

          If skippedVersionValue IsNot Nothing Then
            Return New Version(skippedVersionValue.ToString())
          End If
        End Using

      Catch __unusedException1__ As Exception
      End Try

      Return Nothing

    End Function

    '/ <inheritdoc />
    Public Function GetRemindLater() As DateTime? Implements IPersistenceProvider.GetRemindLater

      Try
        Using updateKey As RegistryKey = Registry.CurrentUser.OpenSubKey(RegistryLocation)
          Dim remindLaterValue As Object = updateKey.GetValue(RemindLaterValueName)

          If remindLaterValue IsNot Nothing Then
            Return Convert.ToDateTime(remindLaterValue.ToString(), CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat)
          End If
        End Using

      Catch __unusedException1__ As Exception
      End Try

      Return Nothing

    End Function

    '/ <inheritdoc />
    Public Sub SetSkippedVersion(ByVal version As Version) Implements IPersistenceProvider.SetSkippedVersion

      Using autoUpdaterKey As RegistryKey = Registry.CurrentUser.CreateSubKey(RegistryLocation)
        autoUpdaterKey.SetValue(SkippedVersionValueName, If(version IsNot Nothing, version.ToString(), String.Empty))
      End Using

    End Sub

    '/ <inheritdoc />
    Public Sub SetRemindLater(ByVal remindLaterAt As DateTime?) Implements IPersistenceProvider.SetRemindLater

      Using autoUpdaterKey As RegistryKey = Registry.CurrentUser.CreateSubKey(RegistryLocation)
        autoUpdaterKey.SetValue(RemindLaterValueName, If(remindLaterAt IsNot Nothing, remindLaterAt.Value.ToString(CultureInfo.CreateSpecificCulture("en-US").DateTimeFormat), String.Empty))
      End Using

    End Sub

  End Class
End Namespace
