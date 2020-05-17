'TODO LIST:
'           -implement XML documentation
'           -rename files correctly
'           -rename LABELS correctly
'           -fix data types
'       -->  CHECK game win state
'       -->  terrain
'       -->  implement sorting algorithms
'creep the feathures...
'           -CUT STUFF OUT OF THE MAIN GAME LOOP
'           -establish source control
'           -uncopy matrix and framecounter


'   -add things to model


'Model, view, controller
'Model:
'    Describes all data, e.g. position of lander, terrainmap etc.
'View:
'   How things are displayed. UI, drawing to screen, drawing text etc.
'Controller:
'   Bridge between, manipulates the model.
Imports System.Drawing.Drawing2D

Public Class Game

    'default to 64
    Dim frameRate As Double = 64
    Dim frameCounter As Integer

    Dim terrainSlice(0) As Point


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

        Randomize()

        'generate terrain into array of points?
        'can use:
        '       midpoint dispacement noise 
        '       https://en.wikipedia.org/wiki/Diamond-square_algorithm#Midpoint_displacement_algorithm

        'Max recursion depth = 11, assuming that 1920 is the total side value of heightmap
        'I found that:
        'https://www.desmos.com/calculator/eie2mmuwzz
        'Add to report, visual basic does not support tail-recursion, therefore, infinite recursion is not possible, because the stack will be blown.

        'the length of the side of the square (0-4), maintaining 2^n + 1 form, however arrays are from 0 to value, not 1...
        Dim n As Integer = 6
        Dim sideSize As Integer = 2 ^ n
        Dim random As System.Random = New System.Random()
        Dim rTerrainMap As TerrainMap = terrainStarter(sideSize, random)
        ReDim terrainSlice(sideSize)

        sideSize /= 2
        'Note: middleIndex is the diamond in the diamond step, and therefore, the square step values are surrounding it (the centre), in a clockwise fashion
        Dim middleIndex As New Point(sideSize, sideSize)


        'debugging
        Dim maxY As Integer = 50
        Dim minY As Integer = Me.Height - 50

        'Note: sideSize is NOW the distance from the middleIndex to the edge of the square which is being constructed.
        recursiveTerrainAlgorithm(rTerrainMap, random, middleIndex, sideSize)


        'converts the 3d heightmap to 2d:
        'also adds the flat sections
        For x As Integer = 0 To terrainSlice.GetLength(0) - 1
            Dim y As Integer = terrainSlice.GetLength(0) / 2
            'Regulate y magnitude, to keep it on the screen


            'diagnostics and parameter setting
            If rTerrainMap.GetValue(x, y) < maxY Then
                maxY = rTerrainMap.GetValue(x, y)
            ElseIf rTerrainMap.GetValue(x, y) > minY Then
                minY = rTerrainMap.GetValue(x, y)
            End If

            'sliced
            terrainSlice(x).Y = rTerrainMap.GetValue(x, y)

            'set flat sections: UNSPAGHETTI, and actually randomize
            Dim length As Integer = Int((5 * Rnd()) + 1)

            If x > length + 1 And Int((7 * Rnd()) + 1) > 6 Then
                For b As Integer = 1 To length
                    terrainSlice(x - b).Y = rTerrainMap.GetValue(x, y)
                Next
            End If
        Next

        Console.WriteLine("maxY = " & maxY)

        'normalise to zero
        Dim normaliseAmount As Integer = -maxY
        'scale, probably make this a function
        Dim scaleFactor As Double = CDbl(Me.Height - 100) / (minY - maxY)

        For g As Integer = 0 To terrainSlice.GetLength(0) - 1
            terrainSlice(g).Y += normaliseAmount
            terrainSlice(g).Y *= scaleFactor
            'reshift down
            terrainSlice(g).Y += 50
        Next

        'can also flip terrain upside down if neccessary
        'If terrainSlice() Then

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Invalidate()
    End Sub

    'render loop
    Private Sub mainGameLoop(ByVal sender As Object, ByVal e As PaintEventArgs) Handles MyBase.Paint
        Dim whitePen As New Pen(Color.White, 3)
        Dim flamePen As New Pen(Color.FromArgb(168, 5, 5), 3)

        'draw terrain:
        Dim offset As Integer = Me.Width / terrainSlice.GetLength(0)

        For i As Integer = 0 To terrainSlice.Length() - 2 Step 1

            'need to store this info for collision detection later

            Dim offsetPoint As New Point(offset * (i), terrainSlice(i).Y)
            Dim newOffsetPoint As New Point(offset * (i + 1), terrainSlice(i + 1).Y)

            e.Graphics.DrawLine(whitePen, offsetPoint, newOffsetPoint)

        Next


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

        lStats.velocity += lStats.acceleration
        lStats.position += lStats.velocity

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

    Private Function terrainStarter(sideSize As Integer, random As System.Random)
        Dim initData(sideSize, sideSize) As Integer
        Dim terrainMap As New TerrainMap(initData)

        '                        random Value,   indicies
        terrainMap.SetValue(random.Next(0, 300), 0, 0)
        terrainMap.SetValue(random.Next(0, 300), sideSize, 0)
        terrainMap.SetValue(random.Next(0, 300), 0, sideSize)
        terrainMap.SetValue(random.Next(0, 300), sideSize, sideSize)

        Return terrainMap
    End Function

    'add to model
    'decrease randomness every recursion
    Private Sub recursiveTerrainAlgorithm(rTerrainMap As TerrainMap, random As System.Random, middleIndex As Point, size As Integer)
        'diamond step:
        Dim randomMagnitude As Integer = 50
        Dim diamondValues As Integer = rTerrainMap.GetValue(middleIndex.X - size, middleIndex.Y - size) + rTerrainMap.GetValue(middleIndex.X + size, middleIndex.Y - size) + rTerrainMap.GetValue(middleIndex.X - size, middleIndex.Y + size) + rTerrainMap.GetValue(middleIndex.X + size, middleIndex.Y + size)
        rTerrainMap.SetValue((diamondValues / 4) + random.Next(-size * randomMagnitude, size * randomMagnitude), middleIndex.X, middleIndex.Y)

        'square step -> clockwise maybe factor into subroutine?
        Dim squareValues As Integer = rTerrainMap.GetValue(middleIndex.X, middleIndex.Y - (2 * size)) +
            rTerrainMap.GetValue(middleIndex.X + size, middleIndex.Y - size) +
            rTerrainMap.GetValue(middleIndex.X, middleIndex.Y) +
            rTerrainMap.GetValue(middleIndex.X - size, middleIndex.Y - size)
        rTerrainMap.SetValue((squareValues / 4) + random.Next(-size * randomMagnitude, size * randomMagnitude), middleIndex.X, middleIndex.Y - size)

        squareValues = rTerrainMap.GetValue(middleIndex.X + size, middleIndex.Y - size) +
            rTerrainMap.GetValue(middleIndex.X + (2 * size), middleIndex.Y) +
            rTerrainMap.GetValue(middleIndex.X + size, middleIndex.Y + size) +
            rTerrainMap.GetValue(middleIndex.X, middleIndex.Y)
        rTerrainMap.SetValue((squareValues / 4) + random.Next(-size * randomMagnitude, size * randomMagnitude), middleIndex.X + size, middleIndex.Y)

        squareValues = rTerrainMap.GetValue(middleIndex.X, middleIndex.Y) +
            rTerrainMap.GetValue(middleIndex.X + size, middleIndex.Y + size) +
            rTerrainMap.GetValue(middleIndex.X, middleIndex.Y + (2 * size)) +
            rTerrainMap.GetValue(middleIndex.X - size, middleIndex.Y + size)
        rTerrainMap.SetValue((squareValues / 4) + random.Next(-size * randomMagnitude, size * randomMagnitude), middleIndex.X, middleIndex.Y + size)

        squareValues = rTerrainMap.GetValue(middleIndex.X - size, middleIndex.Y - size) +
            rTerrainMap.GetValue(middleIndex.X, middleIndex.Y) +
            rTerrainMap.GetValue(middleIndex.X - size, middleIndex.Y + size) +
            rTerrainMap.GetValue(middleIndex.X - (2 * size), middleIndex.Y)
        rTerrainMap.SetValue((squareValues / 4) + random.Next(-size * randomMagnitude, size * randomMagnitude), middleIndex.X - size, middleIndex.Y)


        Dim newSize As Integer = size / 2
        'Because newSize is an integer, this works:
        If newSize <> 0 Then
            'call recursively
            Dim newMiddleIndex As New Point(middleIndex.X - newSize, middleIndex.Y - newSize)
            recursiveTerrainAlgorithm(rTerrainMap, random, newMiddleIndex, newSize)
            newMiddleIndex.X = middleIndex.X + newSize
            newMiddleIndex.Y = middleIndex.Y - newSize
            recursiveTerrainAlgorithm(rTerrainMap, random, newMiddleIndex, newSize)
            newMiddleIndex.X = middleIndex.X + newSize
            newMiddleIndex.Y = middleIndex.Y + newSize
            recursiveTerrainAlgorithm(rTerrainMap, random, newMiddleIndex, newSize)
            newMiddleIndex.X = middleIndex.X - newSize
            newMiddleIndex.Y = middleIndex.Y + newSize
            recursiveTerrainAlgorithm(rTerrainMap, random, newMiddleIndex, newSize)
        End If

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
        If (lStats.position.Y + 40) >= Me.Height And lStats.velocity.Y * 30 < 5 And Math.Abs(lStats.velocity.X * 30) < 5 Then
            'successfull landing
            lStats.velocity.X = 0
            lStats.velocity.Y = 0
            'lStats.acceleration.X = 0
            lStats.acceleration.Y = 0
            lStats.gravity.Y = 0
            'lStats.thrust.Y = 0
            'lStats.thrust.X = 0
        ElseIf (lStats.position.Y + 40) >= Me.Height Then
            'failed landing
            Me.Close()
        End If
    End Sub

    'catch keyboard input
    ''' <summary>
    ''' Function sets boolean values of class keyInput in accordance with KeyDown events
    ''' </summary>
    ''' <param name="sender">The object that sent the KeyDown event.</param>
    ''' <param name="e">The object providing information about the KeyDown event.</param>
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

Public Class keyInput
    Public w As Boolean = False
    Public a As Boolean = False
    Public s As Boolean = False
    Public d As Boolean = False
    Public space As Boolean = False

End Class
