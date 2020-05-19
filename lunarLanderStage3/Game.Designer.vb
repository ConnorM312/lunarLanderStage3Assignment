<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Game
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
        Me.components = New System.ComponentModel.Container()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.VerticalLabel = New System.Windows.Forms.Label()
        Me.HorizontalLabel = New System.Windows.Forms.Label()
        Me.AltitudeLabel = New System.Windows.Forms.Label()
        Me.ScoreLabel = New System.Windows.Forms.Label()
        Me.TimeLabel = New System.Windows.Forms.Label()
        Me.FuelLabel = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Interval = 1
        '
        'VerticalLabel
        '
        Me.VerticalLabel.AutoSize = True
        Me.VerticalLabel.Font = New System.Drawing.Font("Courier New", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.VerticalLabel.ForeColor = System.Drawing.Color.White
        Me.VerticalLabel.Location = New System.Drawing.Point(1299, 71)
        Me.VerticalLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.VerticalLabel.Name = "VerticalLabel"
        Me.VerticalLabel.Size = New System.Drawing.Size(250, 22)
        Me.VerticalLabel.TabIndex = 0
        Me.VerticalLabel.Text = "VERTICAL VELOCITY: 0"
        '
        'HorizontalLabel
        '
        Me.HorizontalLabel.AutoSize = True
        Me.HorizontalLabel.Font = New System.Drawing.Font("Courier New", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HorizontalLabel.ForeColor = System.Drawing.Color.White
        Me.HorizontalLabel.Location = New System.Drawing.Point(1299, 38)
        Me.HorizontalLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.HorizontalLabel.Name = "HorizontalLabel"
        Me.HorizontalLabel.Size = New System.Drawing.Size(274, 22)
        Me.HorizontalLabel.TabIndex = 1
        Me.HorizontalLabel.Text = "HORIZONTAL VELOCITY: 0"
        '
        'AltitudeLabel
        '
        Me.AltitudeLabel.AutoSize = True
        Me.AltitudeLabel.Font = New System.Drawing.Font("Courier New", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AltitudeLabel.ForeColor = System.Drawing.Color.White
        Me.AltitudeLabel.Location = New System.Drawing.Point(1299, 6)
        Me.AltitudeLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.AltitudeLabel.Name = "AltitudeLabel"
        Me.AltitudeLabel.Size = New System.Drawing.Size(142, 22)
        Me.AltitudeLabel.TabIndex = 2
        Me.AltitudeLabel.Text = "ALTITUDE: 0"
        '
        'ScoreLabel
        '
        Me.ScoreLabel.AutoSize = True
        Me.ScoreLabel.Font = New System.Drawing.Font("Courier New", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ScoreLabel.ForeColor = System.Drawing.Color.White
        Me.ScoreLabel.Location = New System.Drawing.Point(8, 6)
        Me.ScoreLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.ScoreLabel.Name = "ScoreLabel"
        Me.ScoreLabel.Size = New System.Drawing.Size(106, 22)
        Me.ScoreLabel.TabIndex = 5
        Me.ScoreLabel.Text = "SCORE: 0"
        '
        'TimeLabel
        '
        Me.TimeLabel.AutoSize = True
        Me.TimeLabel.Font = New System.Drawing.Font("Courier New", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TimeLabel.ForeColor = System.Drawing.Color.White
        Me.TimeLabel.Location = New System.Drawing.Point(8, 38)
        Me.TimeLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.TimeLabel.Name = "TimeLabel"
        Me.TimeLabel.Size = New System.Drawing.Size(106, 22)
        Me.TimeLabel.TabIndex = 4
        Me.TimeLabel.Text = "TIME:  0"
        '
        'FuelLabel
        '
        Me.FuelLabel.AutoSize = True
        Me.FuelLabel.Font = New System.Drawing.Font("Courier New", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FuelLabel.ForeColor = System.Drawing.Color.White
        Me.FuelLabel.Location = New System.Drawing.Point(8, 71)
        Me.FuelLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.FuelLabel.Name = "FuelLabel"
        Me.FuelLabel.Size = New System.Drawing.Size(130, 22)
        Me.FuelLabel.TabIndex = 3
        Me.FuelLabel.Text = "FUEL:  500"
        '
        'Game
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(1584, 861)
        Me.Controls.Add(Me.ScoreLabel)
        Me.Controls.Add(Me.TimeLabel)
        Me.Controls.Add(Me.FuelLabel)
        Me.Controls.Add(Me.AltitudeLabel)
        Me.Controls.Add(Me.HorizontalLabel)
        Me.Controls.Add(Me.VerticalLabel)
        Me.DoubleBuffered = True
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximumSize = New System.Drawing.Size(1600, 900)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(1600, 900)
        Me.Name = "Game"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Timer1 As Timer
    Friend WithEvents VerticalLabel As Label
    Friend WithEvents HorizontalLabel As Label
    Friend WithEvents AltitudeLabel As Label
    Friend WithEvents ScoreLabel As Label
    Friend WithEvents TimeLabel As Label
    Friend WithEvents FuelLabel As Label
End Class
