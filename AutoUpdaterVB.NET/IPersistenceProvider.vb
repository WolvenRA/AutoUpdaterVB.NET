Imports System

Namespace AutoUpdaterVBDotNET

  '/ <summary>
  '/ Provides a mechanism for storing AutoUpdater state between sessions.
  '/ </summary>
  Public Interface IPersistenceProvider

    '/ <summary>
    '/ Reads the flag indicating whether a specific version should be skipped or not.
    '/ </summary>
    '/ <returns>Returns a version to skip. If skip value is false or not present then it will return null.</returns>
    Function GetSkippedVersion() As Version

    '/ <summary>
    '/ Reads the value containing the date and time at which the user must be given again the possibility to upgrade the application.
    '/ </summary>
    '/ <returns>Returns a DateTime value at which the user must be given again the possibility to upgrade the application. If remind later value is not present then it will return null.</returns>
    Function GetRemindLater() As DateTime?

    '/ <summary>
    '/ Sets the values indicating the specific version that must be ignored by AutoUpdater.
    '/ </summary>
    '/ <param name="version">Version code for the specific version that must be ignored. Set it to null if you don't want to skip any version.</param>
    Sub SetSkippedVersion(ByVal version As Version)

    '/ <summary>
    '/ Sets the date and time at which the user must be given again the possibility to upgrade the application.
    '/ </summary>
    '/ <param name="remindLaterAt">Date and time at which the user must be given again the possibility to upgrade the application.</param>
    Sub SetRemindLater(ByVal remindLaterAt As DateTime?)

  End Interface

End Namespace
