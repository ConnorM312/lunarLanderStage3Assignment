<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GameFrm
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
        Me.VerticalLbl = New System.Windows.Forms.Label()
        Me.HorizontalLbl = New System.Windows.Forms.Label()
        Me.AltitudeLbl = New System.Windows.Forms.Label()
        Me.ScoreLbl = New System.Windows.Forms.Label()
        Me.TimeLbl = New System.Windows.Forms.Label()
        Me.FuelLbl = New System.Windows.Forms.Label()
        Me.LevelLbl = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Interval = 40
        '
        'VerticalLbl
        '
        Me.VerticalLbl.AutoSize = True
        Me.VerticalLbl.Font = New System.Drawing.Font("Courier New", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.VerticalLbl.ForeColor = System.Drawing.Color.White
        Me.VerticalLbl.Location = New System.Drawing.Point(1299, 71)
        Me.VerticalLbl.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.VerticalLbl.Name = "VerticalLbl"
        Me.VerticalLbl.Size = New System.Drawing.Size(250, 22)
        Me.VerticalLbl.TabIndex = 0
        Me.VerticalLbl.Text = "VERTICAL VELOCITY: 0"
        '
        'HorizontalLbl
        '
        Me.HorizontalLbl.AutoSize = True
        Me.HorizontalLbl.Font = New System.Drawing.Font("Courier New", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HorizontalLbl.ForeColor = System.Drawing.Color.White
        Me.HorizontalLbl.Location = New System.Drawing.Point(1299, 38)
        Me.HorizontalLbl.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.HorizontalLbl.Name = "HorizontalLbl"
        Me.HorizontalLbl.Size = New System.Drawing.Size(274, 22)
        Me.HorizontalLbl.TabIndex = 1
        Me.HorizontalLbl.Text = "HORIZONTAL VELOCITY: 0"
        '
        'AltitudeLbl
        '
        Me.AltitudeLbl.AutoSize = True
        Me.AltitudeLbl.Font = New System.Drawing.Font("Courier New", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AltitudeLbl.ForeColor = System.Drawing.Color.White
        Me.AltitudeLbl.Location = New System.Drawing.Point(1299, 6)
        Me.AltitudeLbl.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.AltitudeLbl.Name = "AltitudeLbl"
        Me.AltitudeLbl.Size = New System.Drawing.Size(142, 22)
        Me.AltitudeLbl.TabIndex = 2
        Me.AltitudeLbl.Text = "ALTITUDE: 0"
        '
        'ScoreLbl
        '
        Me.ScoreLbl.AutoSize = True
        Me.ScoreLbl.Font = New System.Drawing.Font("Courier New", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ScoreLbl.ForeColor = System.Drawing.Color.White
        Me.ScoreLbl.Location = New System.Drawing.Point(8, 6)
        Me.ScoreLbl.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.ScoreLbl.Name = "ScoreLbl"
        Me.ScoreLbl.Size = New System.Drawing.Size(106, 22)
        Me.ScoreLbl.TabIndex = 5
        Me.ScoreLbl.Text = "SCORE: 0"
        '
        'TimeLbl
        '
        Me.TimeLbl.AutoSize = True
        Me.TimeLbl.Font = New System.Drawing.Font("Courier New", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TimeLbl.ForeColor = System.Drawing.Color.White
        Me.TimeLbl.Location = New System.Drawing.Point(8, 38)
        Me.TimeLbl.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.TimeLbl.Name = "TimeLbl"
        Me.TimeLbl.Size = New System.Drawing.Size(106, 22)
        Me.TimeLbl.TabIndex = 4
        Me.TimeLbl.Text = "TIME:  0"
        '
        'FuelLbl
        '
        Me.FuelLbl.AutoSize = True
        Me.FuelLbl.Font = New System.Drawing.Font("Courier New", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FuelLbl.ForeColor = System.Drawing.Color.White
        Me.FuelLbl.Location = New System.Drawing.Point(8, 71)
        Me.FuelLbl.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.FuelLbl.Name = "FuelLbl"
        Me.FuelLbl.Size = New System.Drawing.Size(130, 22)
        Me.FuelLbl.TabIndex = 3
        Me.FuelLbl.Text = "FUEL:  500"
        '
        'LevelLbl
        '
        Me.LevelLbl.AutoSize = True
        Me.LevelLbl.Font = New System.Drawing.Font("Courier New", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LevelLbl.ForeColor = System.Drawing.Color.White
        Me.LevelLbl.Location = New System.Drawing.Point(714, 830)
        Me.LevelLbl.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.LevelLbl.Name = "LevelLbl"
        Me.LevelLbl.Size = New System.Drawing.Size(106, 22)
        Me.LevelLbl.TabIndex = 6
        Me.LevelLbl.Text = "LEVEL: 1"
        '
        'GameFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(1584, 861)
        Me.Controls.Add(Me.LevelLbl)
        Me.Controls.Add(Me.ScoreLbl)
        Me.Controls.Add(Me.TimeLbl)
        Me.Controls.Add(Me.FuelLbl)
        Me.Controls.Add(Me.AltitudeLbl)
        Me.Controls.Add(Me.HorizontalLbl)
        Me.Controls.Add(Me.VerticalLbl)
        Me.DoubleBuffered = True
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximumSize = New System.Drawing.Size(1600, 900)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(1600, 900)
        Me.Name = "GameFrm"
        Me.Text = "Lunar Lander"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Timer1 As Timer
    Friend WithEvents VerticalLbl As Label
    Friend WithEvents HorizontalLbl As Label
    Friend WithEvents AltitudeLbl As Label
    Friend WithEvents ScoreLbl As Label
    Friend WithEvents TimeLbl As Label
    Friend WithEvents FuelLbl As Label
    Friend WithEvents LevelLbl As Label
End Class
