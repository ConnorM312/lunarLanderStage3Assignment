<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TitleScreenFrm
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
        Me.PlayBtn = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.TitleLbl = New System.Windows.Forms.Label()
        Me.MyNameLbl = New System.Windows.Forms.Label()
        Me.LeaderBoardBtn = New System.Windows.Forms.Button()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PlayBtn
        '
        Me.PlayBtn.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.PlayBtn.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PlayBtn.ForeColor = System.Drawing.Color.White
        Me.PlayBtn.Location = New System.Drawing.Point(359, 388)
        Me.PlayBtn.Name = "PlayBtn"
        Me.PlayBtn.Size = New System.Drawing.Size(115, 27)
        Me.PlayBtn.TabIndex = 0
        Me.PlayBtn.Text = "Play"
        Me.PlayBtn.UseVisualStyleBackColor = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.lunarLanderStage3.My.Resources.Resources.landerWallpaper
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox1.Location = New System.Drawing.Point(191, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(450, 291)
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'TitleLbl
        '
        Me.TitleLbl.AutoSize = True
        Me.TitleLbl.BackColor = System.Drawing.Color.Transparent
        Me.TitleLbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleLbl.ForeColor = System.Drawing.Color.White
        Me.TitleLbl.Location = New System.Drawing.Point(297, 317)
        Me.TitleLbl.Name = "TitleLbl"
        Me.TitleLbl.Size = New System.Drawing.Size(238, 33)
        Me.TitleLbl.TabIndex = 2
        Me.TitleLbl.Text = "LUNAR LANDER"
        '
        'MyNameLbl
        '
        Me.MyNameLbl.AutoSize = True
        Me.MyNameLbl.BackColor = System.Drawing.Color.Transparent
        Me.MyNameLbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MyNameLbl.ForeColor = System.Drawing.Color.White
        Me.MyNameLbl.Location = New System.Drawing.Point(343, 365)
        Me.MyNameLbl.Name = "MyNameLbl"
        Me.MyNameLbl.Size = New System.Drawing.Size(147, 20)
        Me.MyNameLbl.TabIndex = 3
        Me.MyNameLbl.Text = "By Connor McHugh"
        '
        'LeaderBoardBtn
        '
        Me.LeaderBoardBtn.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.LeaderBoardBtn.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LeaderBoardBtn.ForeColor = System.Drawing.Color.White
        Me.LeaderBoardBtn.Location = New System.Drawing.Point(359, 421)
        Me.LeaderBoardBtn.Name = "LeaderBoardBtn"
        Me.LeaderBoardBtn.Size = New System.Drawing.Size(115, 27)
        Me.LeaderBoardBtn.TabIndex = 4
        Me.LeaderBoardBtn.Text = "High Scores"
        Me.LeaderBoardBtn.UseVisualStyleBackColor = False
        '
        'TitleScreenFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.LeaderBoardBtn)
        Me.Controls.Add(Me.MyNameLbl)
        Me.Controls.Add(Me.TitleLbl)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.PlayBtn)
        Me.MaximumSize = New System.Drawing.Size(816, 489)
        Me.MinimumSize = New System.Drawing.Size(816, 489)
        Me.Name = "TitleScreenFrm"
        Me.Text = "Lunar Lander Launcher"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PlayBtn As Button
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents TitleLbl As Label
    Friend WithEvents MyNameLbl As Label
    Friend WithEvents LeaderBoardBtn As Button
End Class
