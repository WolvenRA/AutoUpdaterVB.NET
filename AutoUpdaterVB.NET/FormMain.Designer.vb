Namespace AutoUpdaterVBDotNET

  <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
  Partial Class FormMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
      Try
        If disposing AndAlso components IsNot Nothing Then
          components.Dispose()
        End If
      Finally
        MyBase.Dispose(disposing)
      End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
      Me.labelUserID = New System.Windows.Forms.Label()
      Me.labelVersion = New System.Windows.Forms.Label()
      Me.buttonCheckForUpdate = New System.Windows.Forms.Button()
      Me.SuspendLayout()
      '
      'labelUserID
      '
      Me.labelUserID.AutoSize = True
      Me.labelUserID.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, CType(0, Byte))
      Me.labelUserID.Location = New System.Drawing.Point(11, 32)
      Me.labelUserID.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
      Me.labelUserID.Name = "labelUserID"
      Me.labelUserID.Size = New System.Drawing.Size(62, 16)
      Me.labelUserID.TabIndex = 5
      Me.labelUserID.Text = "User ID: "
      '
      'labelVersion
      '
      Me.labelVersion.AutoSize = True
      Me.labelVersion.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, CType(0, Byte))
      Me.labelVersion.Location = New System.Drawing.Point(11, 10)
      Me.labelVersion.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
      Me.labelVersion.Name = "labelVersion"
      Me.labelVersion.Size = New System.Drawing.Size(162, 16)
      Me.labelVersion.TabIndex = 4
      Me.labelVersion.Text = "Current version : 1.0.0.0"
      '
      'buttonCheckForUpdate
      '
      Me.buttonCheckForUpdate.Font = New System.Drawing.Font("Arial", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, CType(0, Byte))
      Me.buttonCheckForUpdate.Location = New System.Drawing.Point(11, 53)
      Me.buttonCheckForUpdate.Margin = New System.Windows.Forms.Padding(2)
      Me.buttonCheckForUpdate.Name = "buttonCheckForUpdate"
      Me.buttonCheckForUpdate.Size = New System.Drawing.Size(162, 31)
      Me.buttonCheckForUpdate.TabIndex = 3
      Me.buttonCheckForUpdate.Text = "Check for update"
      Me.buttonCheckForUpdate.UseVisualStyleBackColor = True
      '
      'FormMain
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.ClientSize = New System.Drawing.Size(184, 94)
      Me.Controls.Add(Me.labelUserID)
      Me.Controls.Add(Me.labelVersion)
      Me.Controls.Add(Me.buttonCheckForUpdate)
      Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
      Me.MaximizeBox = False
      Me.MinimizeBox = False
      Me.Name = "FormMain"
      Me.Text = "AutoUpdaterVBDotNet"
      Me.ResumeLayout(False)
      Me.PerformLayout()

    End Sub
    Private WithEvents labelUserID As System.Windows.Forms.Label
    Private WithEvents labelVersion As System.Windows.Forms.Label
    Private WithEvents buttonCheckForUpdate As System.Windows.Forms.Button
  End Class
End Namespace
