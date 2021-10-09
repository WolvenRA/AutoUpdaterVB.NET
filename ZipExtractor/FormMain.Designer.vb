Namespace ZipExtractor

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
      Me.labelInformation = New System.Windows.Forms.Label()
      Me.pictureBoxIcon = New System.Windows.Forms.PictureBox()
      Me.progressBar = New System.Windows.Forms.ProgressBar()
      CType(Me.pictureBoxIcon, System.ComponentModel.ISupportInitialize).BeginInit()
      Me.SuspendLayout()
      '
      'labelInformation
      '
      Me.labelInformation.AutoSize = True
      Me.labelInformation.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.labelInformation.Location = New System.Drawing.Point(94, 16)
      Me.labelInformation.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
      Me.labelInformation.Name = "labelInformation"
      Me.labelInformation.Size = New System.Drawing.Size(85, 17)
      Me.labelInformation.TabIndex = 6
      Me.labelInformation.Text = "Extracting..."
      '
      'pictureBoxIcon
      '
      Me.pictureBoxIcon.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
              Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
      Me.pictureBoxIcon.Image = Global.ZipExtractor.My.Resources.Resources.ZipExtractor
      Me.pictureBoxIcon.Location = New System.Drawing.Point(13, 13)
      Me.pictureBoxIcon.Margin = New System.Windows.Forms.Padding(5)
      Me.pictureBoxIcon.Name = "pictureBoxIcon"
      Me.pictureBoxIcon.Size = New System.Drawing.Size(72, 72)
      Me.pictureBoxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
      Me.pictureBoxIcon.TabIndex = 5
      Me.pictureBoxIcon.TabStop = False
      '
      'progressBar
      '
      Me.progressBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
              Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
      Me.progressBar.Location = New System.Drawing.Point(96, 47)
      Me.progressBar.Margin = New System.Windows.Forms.Padding(5)
      Me.progressBar.Name = "progressBar"
      Me.progressBar.Size = New System.Drawing.Size(396, 35)
      Me.progressBar.TabIndex = 4
      '
      'FormMain
      '
      Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 17.0!)
      Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
      Me.AutoSize = True
      Me.ClientSize = New System.Drawing.Size(504, 99)
      Me.Controls.Add(Me.labelInformation)
      Me.Controls.Add(Me.pictureBoxIcon)
      Me.Controls.Add(Me.progressBar)
      Me.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
      Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
      Me.Margin = New System.Windows.Forms.Padding(4)
      Me.MaximizeBox = False
      Me.MinimizeBox = False
      Me.Name = "FormMain"
      Me.ShowIcon = False
      Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
      Me.Text = "Installing update..."
      CType(Me.pictureBoxIcon, System.ComponentModel.ISupportInitialize).EndInit()
      Me.ResumeLayout(False)
      Me.PerformLayout()

    End Sub
    Private WithEvents labelInformation As System.Windows.Forms.Label
    Private WithEvents pictureBoxIcon As System.Windows.Forms.PictureBox
    Private WithEvents progressBar As System.Windows.Forms.ProgressBar
  End Class

End Namespace
