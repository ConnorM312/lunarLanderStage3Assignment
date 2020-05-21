<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HighScoreFrm
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
        Me.ScoreLabel = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ScorePlaceLbl = New System.Windows.Forms.Label()
        Me.inputTxtbx = New System.Windows.Forms.TextBox()
        Me.EnterPlaceLbl = New System.Windows.Forms.Label()
        Me.EnterBtn = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ScoreLabel
        '
        Me.ScoreLabel.AutoSize = True
        Me.ScoreLabel.BackColor = System.Drawing.Color.Transparent
        Me.ScoreLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ScoreLabel.ForeColor = System.Drawing.Color.White
        Me.ScoreLabel.Location = New System.Drawing.Point(217, 131)
        Me.ScoreLabel.Name = "ScoreLabel"
        Me.ScoreLabel.Size = New System.Drawing.Size(24, 25)
        Me.ScoreLabel.TabIndex = 0
        Me.ScoreLabel.Text = "0"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = Global.lunarLanderStage3.My.Resources.Resources.astronaut
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox1.Location = New System.Drawing.Point(397, -123)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(391, 690)
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'ScorePlaceLbl
        '
        Me.ScorePlaceLbl.AutoSize = True
        Me.ScorePlaceLbl.BackColor = System.Drawing.Color.Transparent
        Me.ScorePlaceLbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ScorePlaceLbl.ForeColor = System.Drawing.Color.White
        Me.ScorePlaceLbl.Location = New System.Drawing.Point(12, 131)
        Me.ScorePlaceLbl.Name = "ScorePlaceLbl"
        Me.ScorePlaceLbl.Size = New System.Drawing.Size(120, 25)
        Me.ScorePlaceLbl.TabIndex = 2
        Me.ScorePlaceLbl.Text = "Your Score"
        '
        'inputTxtbx
        '
        Me.inputTxtbx.Location = New System.Drawing.Point(222, 185)
        Me.inputTxtbx.Name = "inputTxtbx"
        Me.inputTxtbx.Size = New System.Drawing.Size(169, 20)
        Me.inputTxtbx.TabIndex = 3
        '
        'EnterPlaceLbl
        '
        Me.EnterPlaceLbl.AutoSize = True
        Me.EnterPlaceLbl.BackColor = System.Drawing.Color.Transparent
        Me.EnterPlaceLbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EnterPlaceLbl.ForeColor = System.Drawing.Color.White
        Me.EnterPlaceLbl.Location = New System.Drawing.Point(12, 180)
        Me.EnterPlaceLbl.Name = "EnterPlaceLbl"
        Me.EnterPlaceLbl.Size = New System.Drawing.Size(183, 25)
        Me.EnterPlaceLbl.TabIndex = 4
        Me.EnterPlaceLbl.Text = "Enter Your Name:"
        '
        'EnterBtn
        '
        Me.EnterBtn.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.EnterBtn.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EnterBtn.ForeColor = System.Drawing.Color.White
        Me.EnterBtn.Location = New System.Drawing.Point(114, 244)
        Me.EnterBtn.Name = "EnterBtn"
        Me.EnterBtn.Size = New System.Drawing.Size(174, 28)
        Me.EnterBtn.TabIndex = 5
        Me.EnterBtn.Text = "Add To Leaderboard"
        Me.EnterBtn.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(11, 32)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(238, 33)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "LUNAR LANDER"
        '
        'HighScoreFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.EnterBtn)
        Me.Controls.Add(Me.EnterPlaceLbl)
        Me.Controls.Add(Me.inputTxtbx)
        Me.Controls.Add(Me.ScorePlaceLbl)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.ScoreLabel)
        Me.MaximumSize = New System.Drawing.Size(816, 489)
        Me.MinimumSize = New System.Drawing.Size(816, 489)
        Me.Name = "HighScoreFrm"
        Me.Text = "Enter your HighScore"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ScoreLabel As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents ScorePlaceLbl As Label
    Friend WithEvents inputTxtbx As TextBox
    Friend WithEvents EnterPlaceLbl As Label
    Friend WithEvents EnterBtn As Button
    Friend WithEvents Label3 As Label
End Class
