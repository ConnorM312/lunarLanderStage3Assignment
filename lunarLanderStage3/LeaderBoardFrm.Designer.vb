<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LeaderBoardFrm
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
        Me.TitleLbl = New System.Windows.Forms.Label()
        Me.SubtitleLbl = New System.Windows.Forms.Label()
        Me.HighScoresLstbx = New System.Windows.Forms.ListBox()
        Me.titleBtn = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'TitleLbl
        '
        Me.TitleLbl.AutoSize = True
        Me.TitleLbl.BackColor = System.Drawing.Color.Transparent
        Me.TitleLbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleLbl.ForeColor = System.Drawing.Color.White
        Me.TitleLbl.Location = New System.Drawing.Point(295, 9)
        Me.TitleLbl.Name = "TitleLbl"
        Me.TitleLbl.Size = New System.Drawing.Size(238, 33)
        Me.TitleLbl.TabIndex = 7
        Me.TitleLbl.Text = "LUNAR LANDER"
        '
        'SubtitleLbl
        '
        Me.SubtitleLbl.AutoSize = True
        Me.SubtitleLbl.BackColor = System.Drawing.Color.Transparent
        Me.SubtitleLbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SubtitleLbl.ForeColor = System.Drawing.Color.White
        Me.SubtitleLbl.Location = New System.Drawing.Point(340, 52)
        Me.SubtitleLbl.Name = "SubtitleLbl"
        Me.SubtitleLbl.Size = New System.Drawing.Size(129, 25)
        Me.SubtitleLbl.TabIndex = 8
        Me.SubtitleLbl.Text = "High Scores"
        '
        'HighScoresLstbx
        '
        Me.HighScoresLstbx.BackColor = System.Drawing.Color.Black
        Me.HighScoresLstbx.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HighScoresLstbx.ForeColor = System.Drawing.Color.White
        Me.HighScoresLstbx.FormattingEnabled = True
        Me.HighScoresLstbx.ItemHeight = 20
        Me.HighScoresLstbx.Location = New System.Drawing.Point(183, 80)
        Me.HighScoresLstbx.Name = "HighScoresLstbx"
        Me.HighScoresLstbx.Size = New System.Drawing.Size(447, 284)
        Me.HighScoresLstbx.TabIndex = 9
        '
        'titleBtn
        '
        Me.titleBtn.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.titleBtn.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.titleBtn.ForeColor = System.Drawing.Color.White
        Me.titleBtn.Location = New System.Drawing.Point(313, 401)
        Me.titleBtn.Name = "titleBtn"
        Me.titleBtn.Size = New System.Drawing.Size(174, 28)
        Me.titleBtn.TabIndex = 10
        Me.titleBtn.Text = "Title Screen"
        Me.titleBtn.UseVisualStyleBackColor = False
        '
        'LeaderBoardFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.titleBtn)
        Me.Controls.Add(Me.HighScoresLstbx)
        Me.Controls.Add(Me.SubtitleLbl)
        Me.Controls.Add(Me.TitleLbl)
        Me.Name = "LeaderBoardFrm"
        Me.Text = "LeaderBoardFrm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TitleLbl As Label
    Friend WithEvents SubtitleLbl As Label
    Friend WithEvents HighScoresLstbx As ListBox
    Friend WithEvents titleBtn As Button
End Class
