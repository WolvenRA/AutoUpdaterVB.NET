Namespace AutoUpdaterVBDotNET

  <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
  Partial Class UpdateForm
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UpdateForm))
    Me.pictureBoxIcon = New System.Windows.Forms.PictureBox()
    Me.labelReleaseNotes = New System.Windows.Forms.Label()
    Me.labelDescription = New System.Windows.Forms.Label()
    Me.labelUpdate = New System.Windows.Forms.Label()
    Me.webBrowser = New System.Windows.Forms.WebBrowser()
    Me.buttonUpdate = New System.Windows.Forms.Button()
    Me.buttonSkip = New System.Windows.Forms.Button()
    Me.buttonRemindLater = New System.Windows.Forms.Button()
    Me.ButtonCancel = New System.Windows.Forms.Button()
    CType(Me.pictureBoxIcon, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'pictureBoxIcon
    '
    Me.pictureBoxIcon.Image = Global.AutoUpdaterVB.NET.My.Resources.Resources.update
    resources.ApplyResources(Me.pictureBoxIcon, "pictureBoxIcon")
    Me.pictureBoxIcon.Name = "pictureBoxIcon"
    Me.pictureBoxIcon.TabStop = False
    '
    'labelReleaseNotes
    '
    resources.ApplyResources(Me.labelReleaseNotes, "labelReleaseNotes")
    Me.labelReleaseNotes.Name = "labelReleaseNotes"
    '
    'labelDescription
    '
    resources.ApplyResources(Me.labelDescription, "labelDescription")
    Me.labelDescription.Name = "labelDescription"
    '
    'labelUpdate
    '
    resources.ApplyResources(Me.labelUpdate, "labelUpdate")
    Me.labelUpdate.Name = "labelUpdate"
    '
    'webBrowser
    '
    resources.ApplyResources(Me.webBrowser, "webBrowser")
    Me.webBrowser.Name = "webBrowser"
    Me.webBrowser.ScriptErrorsSuppressed = True
    '
    'buttonUpdate
    '
    resources.ApplyResources(Me.buttonUpdate, "buttonUpdate")
    Me.buttonUpdate.Image = Global.AutoUpdaterVB.NET.My.Resources.Resources.download
    Me.buttonUpdate.Name = "buttonUpdate"
    Me.buttonUpdate.UseVisualStyleBackColor = True
    '
    'buttonSkip
    '
    resources.ApplyResources(Me.buttonSkip, "buttonSkip")
    Me.buttonSkip.DialogResult = System.Windows.Forms.DialogResult.Abort
    Me.buttonSkip.Image = Global.AutoUpdaterVB.NET.My.Resources.Resources.hand_point
    Me.buttonSkip.Name = "buttonSkip"
    Me.buttonSkip.UseVisualStyleBackColor = True
    '
    'buttonRemindLater
    '
    resources.ApplyResources(Me.buttonRemindLater, "buttonRemindLater")
    Me.buttonRemindLater.Image = Global.AutoUpdaterVB.NET.My.Resources.Resources.clock_go
    Me.buttonRemindLater.Name = "buttonRemindLater"
    Me.buttonRemindLater.UseVisualStyleBackColor = True
    '
    'ButtonCancel
    '
    resources.ApplyResources(Me.ButtonCancel, "ButtonCancel")
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.UseVisualStyleBackColor = True
    '
    'UpdateForm
    '
    resources.ApplyResources(Me, "$this")
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.ButtonCancel)
    Me.Controls.Add(Me.pictureBoxIcon)
    Me.Controls.Add(Me.labelReleaseNotes)
    Me.Controls.Add(Me.labelDescription)
    Me.Controls.Add(Me.labelUpdate)
    Me.Controls.Add(Me.webBrowser)
    Me.Controls.Add(Me.buttonUpdate)
    Me.Controls.Add(Me.buttonSkip)
    Me.Controls.Add(Me.buttonRemindLater)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "UpdateForm"
    CType(Me.pictureBoxIcon, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

End Sub
    Private WithEvents pictureBoxIcon As System.Windows.Forms.PictureBox
    Private WithEvents labelReleaseNotes As System.Windows.Forms.Label
    Private WithEvents labelDescription As System.Windows.Forms.Label
    Private WithEvents labelUpdate As System.Windows.Forms.Label
    Private WithEvents webBrowser As System.Windows.Forms.WebBrowser
    Private WithEvents buttonUpdate As System.Windows.Forms.Button
    Private WithEvents buttonSkip As System.Windows.Forms.Button
    Private WithEvents buttonRemindLater As System.Windows.Forms.Button
    Private WithEvents ButtonCancel As System.Windows.Forms.Button
  End Class

End Namespace
