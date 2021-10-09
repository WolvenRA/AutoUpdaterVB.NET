Imports System
Imports System.Windows.Forms

Namespace AutoUpdaterVBDotNET

  Public Class RemindLaterForm

    Public Property RemindLaterFormat As RemindLaterFormat
    Public Property RemindLaterAt As Integer

    Private Sub RemindLaterFormLoad(ByVal sender As Object, ByVal e As EventArgs) _
    Handles Me.Load

      comboBoxRemindLater.SelectedIndex = 0
      radioButtonYes.Checked = True

    End Sub

    Private Sub ButtonOkClick(ByVal sender As Object, ByVal e As EventArgs) _
    Handles buttonOK.Click

      If radioButtonYes.Checked Then
        Select Case comboBoxRemindLater.SelectedIndex
'          Case 0
'            RemindLaterFormat = RemindLaterFormat.Minutes
'            RemindLaterAt = 1
          Case 0
            RemindLaterFormat = RemindLaterFormat.Minutes
            RemindLaterAt = 30
          Case 1
            RemindLaterFormat = RemindLaterFormat.Hours
            RemindLaterAt = 12
          Case 2
            RemindLaterFormat = RemindLaterFormat.Days
            RemindLaterAt = 1
          Case 3
            RemindLaterFormat = RemindLaterFormat.Days
            RemindLaterAt = 2
          Case 4
            RemindLaterFormat = RemindLaterFormat.Days
            RemindLaterAt = 4
          Case 5
            RemindLaterFormat = RemindLaterFormat.Days
            RemindLaterAt = 8
          Case 6
            RemindLaterFormat = RemindLaterFormat.Days
            RemindLaterAt = 10
        End Select

        DialogResult = DialogResult.OK

      Else
        DialogResult = DialogResult.Abort
      End If

    End Sub

    Private Sub ButtonCancelClick(ByVal sender As Object, ByVal e As EventArgs) _
    Handles ButtonCancel.Click

      DialogResult = DialogResult.Abort

    End Sub

    Private Sub RadioButtonYesCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)

      comboBoxRemindLater.Enabled = radioButtonYes.Checked

    End Sub

  End Class
End Namespace
