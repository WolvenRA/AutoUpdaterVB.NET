Namespace AutoUpdaterVBDotNET

  <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
  Partial Class RemindLaterForm
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RemindLaterForm))
    Me.tableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
    Me.ButtonCancel = New System.Windows.Forms.Button()
    Me.pictureBoxIcon = New System.Windows.Forms.PictureBox()
    Me.labelTitle = New System.Windows.Forms.Label()
    Me.radioButtonNo = New System.Windows.Forms.RadioButton()
    Me.comboBoxRemindLater = New System.Windows.Forms.ComboBox()
    Me.radioButtonYes = New System.Windows.Forms.RadioButton()
    Me.labelDescription = New System.Windows.Forms.Label()
    Me.buttonOK = New System.Windows.Forms.Button()
    Me.tableLayoutPanel.SuspendLayout()
    CType(Me.pictureBoxIcon, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'tableLayoutPanel
    '
    resources.ApplyResources(Me.tableLayoutPanel, "tableLayoutPanel")
    Me.tableLayoutPanel.Controls.Add(Me.ButtonCancel, 0, 4)
    Me.tableLayoutPanel.Controls.Add(Me.pictureBoxIcon, 0, 0)
    Me.tableLayoutPanel.Controls.Add(Me.labelTitle, 1, 0)
    Me.tableLayoutPanel.Controls.Add(Me.radioButtonNo, 1, 3)
    Me.tableLayoutPanel.Controls.Add(Me.comboBoxRemindLater, 2, 2)
    Me.tableLayoutPanel.Controls.Add(Me.radioButtonYes, 1, 2)
    Me.tableLayoutPanel.Controls.Add(Me.labelDescription, 1, 1)
    Me.tableLayoutPanel.Controls.Add(Me.buttonOK, 2, 4)
    Me.tableLayoutPanel.Name = "tableLayoutPanel"
    '
    'ButtonCancel
    '
    Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Abort
    resources.ApplyResources(Me.ButtonCancel, "ButtonCancel")
    Me.ButtonCancel.Name = "ButtonCancel"
    Me.ButtonCancel.UseVisualStyleBackColor = True
    '
    'pictureBoxIcon
    '
    resources.ApplyResources(Me.pictureBoxIcon, "pictureBoxIcon")
    Me.pictureBoxIcon.Image = Global.AutoUpdaterVB.NET.My.Resources.Resources.clock_go_32
    Me.pictureBoxIcon.Name = "pictureBoxIcon"
    Me.tableLayoutPanel.SetRowSpan(Me.pictureBoxIcon, 2)
    Me.pictureBoxIcon.TabStop = False
    '
    'labelTitle
    '
    resources.ApplyResources(Me.labelTitle, "labelTitle")
    Me.tableLayoutPanel.SetColumnSpan(Me.labelTitle, 2)
    Me.labelTitle.Name = "labelTitle"
    '
    'radioButtonNo
    '
    resources.ApplyResources(Me.radioButtonNo, "radioButtonNo")
    Me.tableLayoutPanel.SetColumnSpan(Me.radioButtonNo, 2)
    Me.radioButtonNo.Name = "radioButtonNo"
    Me.radioButtonNo.UseVisualStyleBackColor = True
    '
    'comboBoxRemindLater
    '
    resources.ApplyResources(Me.comboBoxRemindLater, "comboBoxRemindLater")
    Me.comboBoxRemindLater.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.comboBoxRemindLater.FormattingEnabled = True
    Me.comboBoxRemindLater.Items.AddRange(New Object() {resources.GetString("comboBoxRemindLater.Items"), resources.GetString("comboBoxRemindLater.Items1"), resources.GetString("comboBoxRemindLater.Items2"), resources.GetString("comboBoxRemindLater.Items3"), resources.GetString("comboBoxRemindLater.Items4"), resources.GetString("comboBoxRemindLater.Items5"), resources.GetString("comboBoxRemindLater.Items6")})
    Me.comboBoxRemindLater.Name = "comboBoxRemindLater"
    '
    'radioButtonYes
    '
    resources.ApplyResources(Me.radioButtonYes, "radioButtonYes")
    Me.radioButtonYes.Checked = True
    Me.radioButtonYes.Name = "radioButtonYes"
    Me.radioButtonYes.TabStop = True
    Me.radioButtonYes.UseVisualStyleBackColor = True
    '
    'labelDescription
    '
    resources.ApplyResources(Me.labelDescription, "labelDescription")
    Me.tableLayoutPanel.SetColumnSpan(Me.labelDescription, 2)
    Me.labelDescription.Name = "labelDescription"
    '
    'buttonOK
    '
    Me.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK
    resources.ApplyResources(Me.buttonOK, "buttonOK")
    Me.buttonOK.Image = Global.AutoUpdaterVB.NET.My.Resources.Resources.clock_go
    Me.buttonOK.Name = "buttonOK"
    Me.buttonOK.UseVisualStyleBackColor = True
    '
    'RemindLaterForm
    '
    resources.ApplyResources(Me, "$this")
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.ButtonCancel
    Me.Controls.Add(Me.tableLayoutPanel)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "RemindLaterForm"
    Me.ShowIcon = False
    Me.ShowInTaskbar = False
    Me.tableLayoutPanel.ResumeLayout(False)
    Me.tableLayoutPanel.PerformLayout()
    CType(Me.pictureBoxIcon, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

End Sub
    Private WithEvents tableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Private WithEvents pictureBoxIcon As System.Windows.Forms.PictureBox
    Private WithEvents labelTitle As System.Windows.Forms.Label
    Private WithEvents radioButtonNo As System.Windows.Forms.RadioButton
    Private WithEvents comboBoxRemindLater As System.Windows.Forms.ComboBox
    Private WithEvents radioButtonYes As System.Windows.Forms.RadioButton
    Private WithEvents labelDescription As System.Windows.Forms.Label
    Private WithEvents buttonOK As System.Windows.Forms.Button
    Private WithEvents ButtonCancel As System.Windows.Forms.Button
  End Class
End Namespace
