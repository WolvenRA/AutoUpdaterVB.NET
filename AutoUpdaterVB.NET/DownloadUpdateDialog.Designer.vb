Namespace AutoUpdaterVBDotNET

  <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
  Partial Class DownloadUpdateDialog
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
      Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DownloadUpdateDialog))
      Me.labelSize = New System.Windows.Forms.Label()
      Me.labelInformation = New System.Windows.Forms.Label()
      Me.progressBar = New System.Windows.Forms.ProgressBar()
      Me.pictureBoxIcon = New System.Windows.Forms.PictureBox()
      CType(Me.pictureBoxIcon, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.SuspendLayout()
      '
      'labelSize
      '
      resources.ApplyResources(Me.labelSize, "labelSize")
      Me.labelSize.Name = "labelSize"
      '
      'labelInformation
      '
      resources.ApplyResources(Me.labelInformation, "labelInformation")
      Me.labelInformation.Name = "labelInformation"
      '
      'progressBar
      '
      resources.ApplyResources(Me.progressBar, "progressBar")
      Me.progressBar.Name = "progressBar"
      '
      'pictureBoxIcon
      '
      Me.pictureBoxIcon.Image = Global.AutoUpdaterVB.NET.My.Resources.Resources.download_32
      resources.ApplyResources(Me.pictureBoxIcon, "pictureBoxIcon")
      Me.pictureBoxIcon.Name = "pictureBoxIcon"
      Me.pictureBoxIcon.TabStop = False
      '
      'DownloadUpdateDialog
      '
      resources.ApplyResources(Me, "$this")
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.Controls.Add(Me.labelSize)
      Me.Controls.Add(Me.labelInformation)
      Me.Controls.Add(Me.progressBar)
      Me.Controls.Add(Me.pictureBoxIcon)
      Me.MaximizeBox = False
      Me.MinimizeBox = False
      Me.Name = "DownloadUpdateDialog"
      CType(Me.pictureBoxIcon, System.ComponentModel.ISupportInitialize).EndInit()
      Me.ResumeLayout(False)
      Me.PerformLayout()

    End Sub
    Private WithEvents labelSize As System.Windows.Forms.Label
    Private WithEvents labelInformation As System.Windows.Forms.Label
    Private WithEvents progressBar As System.Windows.Forms.ProgressBar
    Private WithEvents pictureBoxIcon As System.Windows.Forms.PictureBox

  End Class

End Namespace
