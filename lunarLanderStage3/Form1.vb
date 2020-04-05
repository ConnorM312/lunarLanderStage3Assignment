'TODO LIST:
'           -implement XML documentation
'           -rename files correctly
'           -rename LABELS correctly
'           -fix data types
'       -->  CHECK game win state
'       -->  simplift fuel part, line 122
'       -->  terrain
'       -->  implement sorting algorithms
'creep the feathures...
'           -CUT STUFF OUT OF THE MAIN GAME LOOP
'           -establish source control
'           -uncopy matrix and framecounter

'SUVAT IS FOR CONSTANT ACCELERATION YOU IDIOT
Imports System.Drawing.Drawing2D

Public Class Form1
    Public Class keyInput
        Public w As Boolean = False
        Public a As Boolean = False
        Public s As Boolean = False
        Public d As Boolean = False
        Public space As Boolean = False
    End Class


    'default to 64
    Dim frameRate As Double = 64
    Dim frameCounter As Integer

    'unharcode terrainSlice, and delete
    Dim terrainSlice(2) As Point


    Dim terrainRendered As Integer = 64

    Public Class landerStatistics
        Public position As Vector
        Public angle As Double
        Public velocity As Vector
        Public acceleration As Vector
        Public gravity As Vector
        Public thrust As Vector
        Public thrustConst As Double
        Public fuel As Double
    End Class

    Public Structure Vector
        Public X As Single
        Public Y As Single
        'make it so that I can add and sub tract vectors, not multiply though
        Public Shared Operator +(ByVal v1 As Vector, ByVal v2 As Vector)
            Dim vsum As Vector
            vsum.X = v1.X + v2.X
            vsum.Y = v1.Y + v2.Y
            Return vsum
        End Operator

        Public Shared Operator -(ByVal v1 As Vector, ByVal v2 As Vector)
            Dim vsum As Vector
            vsum.X = v1.X - v2.X
            vsum.Y = v1.Y - v2.Y
            Return vsum
        End Operator

    End Structure

    Dim lStats As New landerStatistics
    Dim kPut As New keyInput

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
        stopWatch.Start()

        'define gravity etc...
        lStats.gravity.Y = 0.006
        lStats.gravity.X = 0

        lStats.thrust.X = 0
        lStats.thrust.Y = 0.017

        lStats.thrustConst = 0.017

        lStats.position.X = 100
        lStats.position.Y = 100


        lStats.velocity.X = 0
        lStats.velocity.Y = 0

        lStats.acceleration.X = 0
        lStats.acceleration.Y = 0

        lStats.angle = 270

        lStats.fuel = 2000

        'generate terrain into array of points?
        'can use:
        '       midpoint dispacement noise 
        '       https://en.wikipedia.org/wiki/Diamond-square_algorithm#Midpoint_displacement_algorithm
        Randomize()

        'Max recursion depth = 11, assuming that 1920 is the total side value of heightmap
        'I found that:
        'https://www.desmos.com/calculator/eie2mmuwzz
        'Add to report, visual basic does not support tail-recursion

        'hardcode:
        Dim sideSize As Integer = 2
        Dim random As System.Random = New System.Random()
        'functions
        Dim rTerrainMap = mapStarter(sideSize, random)
        recursiveTerrainAlgorithm(rTerrainMap, random, sideSize)


        '3x3 heightmap complete using the diamond-square algorithm!
        terrainSlice(0).Y = rTerrainMap(0, 1)
        terrainSlice(1).Y = rTerrainMap(1, 1)
        terrainSlice(2).Y = rTerrainMap(2, 1)
        'dodgy

        'idk if any of these are usefull, im using a 2d, x and y grid.
        'array of vectors
        Dim size As Integer = rTerrainMap.GetLength(0) - 1
        Dim randomNumber As System.Random = New System.Random()
        For i As Integer = 0 To size - 1 Step 1
            terrainSlice(i).X = i * 100
        Next
        'second hill:
        'Dim sizeTwo As Integer = 4
        ''Dim terrainSlice(size) As Vector
        'Dim randomNumberTwo As System.Random = New System.Random()
        'For i As Integer = 2 To sizeTwo - 1 Step 1
        '    terrainSlice(i).X = i * 100
        '    'terrainSlice(i).Y = randomNumber.Next(0, 650)
        'Next

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Invalidate()
    End Sub

    'render loop
    Private Sub mainGameLoop(ByVal sender As Object, ByVal e As PaintEventArgs) Handles MyBase.Paint
        Dim whitePen As New Pen(Color.White, 3)
        Dim flamePen As New Pen(Color.FromArgb(168, 5, 5), 3)

        If terrainRendered > 0 Then
            For i As Integer = 0 To terrainSlice.Length() - 2 Step 1
                e.Graphics.DrawLine(whitePen, terrainSlice(i), terrainSlice(i + 1))
                'Dim pointy As New Point(540, 540)
                'e.Graphics.DrawLine(whitePen, pointy, terrainSlice(i + 1))
                'Dim rectangley As New Rectangle(540, 540, 20, 20)
                'e.Graphics.DrawRectangle(flamePen, rectangley)
                'Console.WriteLine("drew line from: " & terrainSlice(i).X & "x" & terrainSlice(i).Y & " to: " & terrainSlice(i + 1).X & "x" & terrainSlice(i + 1).Y)
            Next
            'terrainRendered -= 1
        End If

        frameCounter += 1

        'alter the velocity to seem more speedy and display it
        Label1.Text = "VERTICAL VELOCITY: " & CInt(lStats.velocity.Y * 30)
        Label2.Text = "HORIZONTAL VELOCITY " & CInt(lStats.velocity.X * 30)

        'Console.WriteLine("angle:" & lStats.angle)

        'rotate lander based on user input
        If kPut.a = True And lStats.angle - 1 >= 180 Then
            lStats.angle -= 1
        ElseIf kPut.d = True And lStats.angle + 1 <= 360 Then
            lStats.angle += 1
        End If

        lStats.thrust.X = Math.Cos(Math.PI / 180 * (lStats.angle)) * lStats.thrustConst
        lStats.thrust.Y = Math.Sin(Math.PI / 180 * (lStats.angle)) * lStats.thrustConst

        'apply acceleration of thruster or gravity depending on user input
        If kPut.space = True And lStats.fuel > 0 Then
            lStats.angle = 270
            'double thrust for emergency escape
            lStats.acceleration = lStats.gravity + lStats.thrust + lStats.thrust
            If lStats.fuel - 4 > 0 Then
                lStats.fuel -= 4
            Else
                lStats.fuel = 0
            End If
            Label6.Text = "FUEL:  " & CInt(lStats.fuel)
        ElseIf kPut.w = True And lStats.fuel > 0 Then
            'total acceleration is the acceleration from gravity + the acceleration from thrusters
            lStats.acceleration = lStats.gravity + lStats.thrust
            'lStats.fuel = max(0, lStats.fuel - 0.25)
            If lStats.fuel - 0.25 > 0 Then
                lStats.fuel -= 0.25
            Else
                lStats.fuel = 0
            End If
            Label6.Text = "FUEL:  " & CInt(lStats.fuel)
        Else
            lStats.acceleration = lStats.gravity
        End If

        'for my ocd (not actually neccessary, can remove)
        'If lStats.angle >= 360 Then
        '    lStats.angle -= 360
        'ElseIf lStats.angle <= 0 Then
        '    lStats.angle += 360
        'End If

        lStats.velocity += lStats.acceleration
        lStats.position += lStats.velocity

        'Dim newPos As Point
        'Dim currentPos As New Point(lStats.position.X + 10, lStats.position.Y + 10)

        'newPos.X = Math.Cos(Math.PI / 180 * (lStats.angle)) * 50 + currentPos.X
        'newPos.Y = Math.Sin(Math.PI / 180 * lStats.angle) * 50 + currentPos.Y

        'e.Graphics.DrawLine(whitePen, currentPos, newPos)
        'e.Graphics.DrawRectangle(whitePen, newPos.X, newPos.Y, 5, 5)
        'e.Graphics.DrawRectangle(flamePen, lStats.position.X + 10, lStats.position.Y + 17, 2, 2)

        'terrain
        Dim bottomLeftPoint As New Point(0, (700 - 50))
        Dim bottomRightPoint As New Point(1920, (700 - 50))
        e.Graphics.DrawLine(whitePen, bottomLeftPoint, bottomRightPoint)

        Dim centerOfRotation As New Point(lStats.position.X + 10, lStats.position.Y + 17)
        'do the rotation matrix (currently only a demo)
        Dim myMatrix As New Matrix
        myMatrix.RotateAt(lStats.angle + 90, centerOfRotation)
        e.Graphics.Transform = myMatrix

        Dim landerLinesPosition As New Point(lStats.position.X, lStats.position.Y)
        drawLander(landerLinesPosition, whitePen, flamePen, e)

        checkWin()

        CheckFrameRate()
    End Sub

    Private Function mapStarter(sideSize As Integer, random As System.Random)
        Dim rTerrainMap(sideSize, sideSize)

        'corners, random max = 300
        rTerrainMap(0, 0) = random.Next(0, 300)
        rTerrainMap(sideSize, 0) = random.Next(0, 300)
        rTerrainMap(0, sideSize) = random.Next(0, 300)
        rTerrainMap(sideSize, sideSize) = random.Next(0, 300)

        Return rTerrainMap
    End Function
    Private Sub recursiveTerrainAlgorithm(rTerrainMap As Array, random As System.Random, sideSize As Integer)
        'middle of "diamond step" unhardcoded:
        'size must be 2^n + 1 and are zero indexed, therfore, 5 is 4 and can be /2 to get 2.
        Dim middle As New Point((sideSize) / 2, (sideSize) / 2)

        'sum middle, same for all sizes
        rTerrainMap(middle.X, middle.Y) = ((rTerrainMap(0, 0) +
                                          rTerrainMap(sideSize, 0) +
                                          rTerrainMap(0, sideSize) +
                                          rTerrainMap(sideSize, sideSize)) / 4) +
                                          random.Next(0, 100)
        'random amount less than corners
        'rTerrainMap(1, 1) = ((rTerrainMap(0, 0) + rTerrainMap(2, 0) + rTerrainMap(0, 2) + rTerrainMap(2, 2)) / 4) + random.Next(0, 100)

        Dim edgeCase As Boolean = True
        'square step: challenging, un hardcode
        'note: ONLY FOR EDGE CASES: the divide by 3, same for all edge cases
        If edgeCase = True Then
            rTerrainMap(middle.X, 0) = (rTerrainMap(0, 0) + rTerrainMap(middle.X, middle.Y) + rTerrainMap(0, sideSize) / 3) + random.Next(0, 100)
            rTerrainMap(0, middle.Y) = (rTerrainMap(0, 0) + rTerrainMap(middle.X, middle.Y) + rTerrainMap(0, sideSize) / 3) + random.Next(0, 100)
            rTerrainMap(sideSize, middle.Y) = (rTerrainMap(sideSize, 0) + rTerrainMap(middle.X, middle.Y) + rTerrainMap(sideSize, sideSize) / 3) + random.Next(0, 100)
            rTerrainMap(middle.X, sideSize) = (rTerrainMap(0, 0) + rTerrainMap(middle.X, middle.Y) + rTerrainMap(0, 2) / 3) + random.Next(0, 100)
        End If

        'check if finished, rudimentary
        Dim printer As String = ""
        For x As Integer = 0 To rTerrainMap.GetLength(0) - 1
            For y As Integer = 0 To rTerrainMap.GetLength(0) - 1
                printer += rTerrainMap(x, y) & ", "
            Next
        Next
        Console.WriteLine(printer)
    End Sub

    Private Sub drawLander(landerLinesPosition As Point, whitePen As Pen, flamePen As Pen, e As PaintEventArgs)
        'lander body
        Dim landerSize As New SizeF(20, 20)
        Dim landerRect As New RectangleF(landerLinesPosition, landerSize)
        e.Graphics.DrawEllipse(whitePen, landerRect)

        'legs
        Dim leftPoint As New Point(landerLinesPosition.X, landerLinesPosition.Y + 10)
        Dim rightPoint As New Point(landerLinesPosition.X + 20, landerLinesPosition.Y + 10)
        Dim leftLegPoint As New Point(leftPoint.X - 10, leftPoint.Y + 25)
        Dim rightLegPoint As New Point(rightPoint.X + 10, rightPoint.Y + 25)
        e.Graphics.DrawLine(whitePen, leftPoint, leftLegPoint)
        e.Graphics.DrawLine(whitePen, rightPoint, rightLegPoint)

        'feet
        Dim leftFoot As New Rectangle(leftLegPoint.X - 2, leftLegPoint.Y, 5, 5)
        Dim rightFoot As New Rectangle(rightLegPoint.X - 3, rightLegPoint.Y, 5, 5)
        e.Graphics.DrawRectangle(whitePen, leftFoot)
        e.Graphics.DrawRectangle(whitePen, rightFoot)

        'thruster
        'thruster cone start points
        Dim leftThrusterPointS As New Point(leftPoint.X + 5, leftPoint.Y + 9)
        Dim rightThrusterPointS As New Point(rightPoint.X - 5, rightPoint.Y + 9)
        'thruster cone finish points
        Dim leftThrusterPointE As New Point(leftThrusterPointS.X - 5, leftThrusterPointS.Y + 10)
        Dim rightThrusterPointE As New Point(rightThrusterPointS.X + 5, rightThrusterPointS.Y + 10)
        'draw the thruster cone
        e.Graphics.DrawLine(whitePen, leftThrusterPointS, leftThrusterPointE)
        e.Graphics.DrawLine(whitePen, rightThrusterPointS, rightThrusterPointE)
        'draw thruster arc
        'e.Graphics.DrawArc(whitePen, leftThrusterPointE, 20, 20)
        e.Graphics.DrawLine(whitePen, leftThrusterPointE, rightThrusterPointE)
        If kPut.space = True And lStats.fuel > 0 Then
            'draw the flame
            Dim leftFlameS As New Point(leftPoint.X + 4, leftPoint.Y + 20)
            Dim rightFlameS As New Point(rightPoint.X - 4, leftPoint.Y + 20)
            Dim endFlame As New Point(landerLinesPosition.X + 10, landerLinesPosition.Y + 90)

            e.Graphics.DrawLine(flamePen, leftFlameS, endFlame)
            e.Graphics.DrawLine(flamePen, rightFlameS, endFlame)
        ElseIf kPut.w = True And lStats.fuel > 0 Then
            'draw the flame
            Dim leftFlameS As New Point(leftPoint.X + 4, leftPoint.Y + 20)
            Dim rightFlameS As New Point(rightPoint.X - 4, leftPoint.Y + 20)
            Dim endFlame As New Point(landerLinesPosition.X + 10, landerLinesPosition.Y + 60)

            e.Graphics.DrawLine(flamePen, leftFlameS, endFlame)
            e.Graphics.DrawLine(flamePen, rightFlameS, endFlame)
        End If
    End Sub

    'EDIT SO NOT COPY FROM OLD CODE
    Dim stopWatch As New Stopwatch()
    Private Sub CheckFrameRate()
        frameRate = frameCounter / stopWatch.Elapsed.TotalSeconds
        Label5.Text = "TIME:  " & CInt(stopWatch.Elapsed.TotalSeconds)
        'displays framerate live, facilitating adjustment of timing parameters if the user wishes to troubleshoot
        'Console.WriteLine("The framerate is: " & frameRate & " Total frames are: " & frameCounter)
    End Sub

    Private Sub checkWin()
        If (lStats.position.Y + 40) >= 650 And lStats.velocity.Y * 30 < 5 And Math.Abs(lStats.velocity.X * 30) < 5 Then
            'successfull landing
            lStats.velocity.X = 0
            lStats.velocity.Y = 0
            'lStats.acceleration.X = 0
            lStats.acceleration.Y = 0
            lStats.gravity.Y = 0
            'lStats.thrust.Y = 0
            'lStats.thrust.X = 0
        ElseIf (lStats.position.Y + 40) >= 650 Then
            'failed landing
            Me.Close()
        End If
    End Sub

    'catch keyboard input
    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyData = Keys.W Then
            kPut.w = True
        ElseIf e.KeyData = Keys.A Then
            kPut.a = True
        ElseIf e.KeyData = Keys.S Then
            kPut.s = True
        ElseIf e.KeyData = Keys.D Then
            kPut.d = True
        ElseIf e.KeyData = Keys.Space Then
            kPut.space = True
        End If

    End Sub
    Private Sub Form1_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyData = Keys.W Then
            kPut.w = False
        ElseIf e.KeyData = Keys.A Then
            kPut.a = False
        ElseIf e.KeyData = Keys.S Then
            kPut.s = False
        ElseIf e.KeyData = Keys.D Then
            kPut.d = False
        ElseIf e.KeyData = Keys.Space Then
            kPut.space = False
        End If
    End Sub

End Class
