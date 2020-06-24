<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TestingFrm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.testOutputLstbx = New System.Windows.Forms.ListBox()
        Me.testBtn = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'testOutputLstbx
        '
        Me.testOutputLstbx.FormattingEnabled = True
        Me.testOutputLstbx.Location = New System.Drawing.Point(43, 74)
        Me.testOutputLstbx.Name = "testOutputLstbx"
        Me.testOutputLstbx.Size = New System.Drawing.Size(683, 316)
        Me.testOutputLstbx.TabIndex = 0
        '
        'testBtn
        '
        Me.testBtn.Location = New System.Drawing.Point(269, 394)
        Me.testBtn.Name = "testBtn"
        Me.testBtn.Size = New System.Drawing.Size(53, 32)
        Me.testBtn.TabIndex = 1
        Me.testBtn.Text = "Test"
        Me.testBtn.UseVisualStyleBackColor = True
        '
        'TestingFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.testBtn)
        Me.Controls.Add(Me.testOutputLstbx)
        Me.Name = "TestingFrm"
        Me.Text = "TestingFrm"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents testOutputLstbx As ListBox
    Friend WithEvents testBtn As Button
End Class
