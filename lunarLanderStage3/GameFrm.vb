'External references:
'landerWallpaper.jpg , Photograph: NASA
'astronaut.jpg , Photograph: NASA
Imports System.Drawing.Drawing2D

Public Class GameFrm

    Dim frameRate As Double = 0
    Dim frameCounter As Integer

    Dim gradient As Double = 0.0

    Dim terrainSlice(0) As Point
    Dim terrainCollider(0) As Point

    Dim terrainFlatIndex(0) As Integer
    Dim terrainFlatValues(0) As Integer
    Dim terrainLandingPoints(0) As Point

    Dim labelMaxInc As Integer = 0
    Dim level As Integer = 1

    Dim lStats As New landerStatistics
    Dim kPut As New keyInput

    Public finalScore As Integer = 0

    Dim stopWatch As New Stopwatch()



    ''' <summary>
    ''' The subroutine that is ran upon loading the form. It:
    ''' Sets default values and calls the generate terrain subroutine.
    ''' Overall it is responsible for preparing the game level for running, and generating all neccessary data.
    ''' </summary>
    ''' <param name="sender">The object that sent the load event.</param>
    ''' <param name="e">The object providing information about the event.</param>
    Private Sub Game_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'set default values
        setDefaults()

        'generates terrain, ensures that there is always at least 1 landing spot
        Do
            generateTerrain()
        Loop While terrainFlatIndex.Length < 2


        spamLabels()

        Me.WindowState = FormWindowState.Maximized
        TitleScreenFrm.Hide()
    End Sub


    ''' <summary>
    ''' The subroutine that is run when the form is closed. (exit x is clicked)
    ''' It also sets a public global to the score, providing accessibility for other forms.
    ''' </summary>
    ''' <param name="sender">The object that sent the event.</param>
    ''' <param name="e">The object providing information about the event.</param>
    Private Sub Game_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        finalScore = lStats.Score
        If lStats.gameOver Then
            HighScoreFrm.Show()
        Else
            TitleScreenFrm.Show()
        End If

    End Sub



    ''' <summary>
    ''' Main Game Loop:
    ''' This sub is ran in a looping fashion, dictated by the framerate of the game invalidating the graphics, and thus the Main Game Loop is ran many times per second.
    ''' It is responsible for calling all the required rendering and logical subroutines.
    ''' This includes calling subroutines to: render the terrain, apply the effect of movement to the lander, rotate the graphics matrix, drawing the lander and checking if a collision has occured.
    ''' </summary>
    ''' <param name="sender">The object that sent the event.</param>
    ''' <param name="e">The object providing information about the event. -In this case it is PaintEventArgs, because the graphics need to be re-rendered.</param>
    Private Sub mainGameLoop(ByVal sender As Object, ByVal e As PaintEventArgs) Handles MyBase.Paint

        CheckFrameRate()

        Dim whitePen As New Pen(Color.White, 3)
        Dim flamePen As New Pen(Color.FromArgb(168, 5, 5), 3)
        Dim thickPen As New Pen(Color.White, 7)
        Dim debugPen As New Pen(Color.FromArgb(255, 0, 255), 3)

        'set centre position from the top left of the lander.
        lStats.realPosition.X = lStats.position.X + 10
        lStats.realPosition.Y = lStats.position.Y + 17

        'increment frameCounter to facilitate measurement of performance
        frameCounter += 1

        drawTerrain(whitePen, thickPen, e)

        updateLabels()

        applyRotationalEffects()

        'apply acceleration of thruster or gravity depending on user input
        applyAccelerationGravityFromUserInput()

        'debugging lines on screen, also functions as a HUD to make landings easier
        'debugHUD(debugPen, e)

        'rotates the graphics rendered after this point -lander
        rotateMatrix(e)

        drawLander(whitePen, flamePen, e)

        'check if collision has occured.
        evaluateGameState(sender, e)

    End Sub



    ''' <summary>
    ''' The subroutine that invalidates the grapics already rendered, forcing mainGameLoop to run.
    ''' The timer tick rate influences the speed that the entire game runs at.
    ''' </summary>
    ''' <param name="sender">The object that sent the event.</param>
    ''' <param name="e">The object providing information about the event.</param>
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Invalidate()
    End Sub



    ''' <summary>
    ''' The subroutine that generates the terrain, calling all the neccessary algorithm subs responsible for this process.
    ''' This sub is very extensive due to the complexity of the operations. -see journal.
    ''' First it purges any global variables that may result in distorted re-production of the terrain.
    ''' Next, it establishes the variables required for the recursiveTerrainAlgorithm.
    ''' In the recursiveTerrainAlgorithm, (explained more elsewhere) the side lengths of the 3d heightmap array must be of the form 2^n + 1, where n is relative to the level the player is playing on.
    ''' The higher the n value, the more variation (2^n) appears in the terrain, making landing more difficult.
    ''' After establishing various parameters, recursiveTerrainMap is called, populating the rTerrainMap array.
    ''' Next, a slicing process ising a for loop occurs, where a horizontal slither across the center of the 3d array is taken out, and made into a 2d heighmap. -akin to a cross section.
    ''' Then, inorder to provide landing points, flat sections are added randomly, with a restricted length. These are then indexed in separate data structures for the ability to verify landings.
    ''' The terrainSlice is then resized by multiplying all points by a predetermined value, to ensured that the random heights of the terrain does not go off the screen.
    ''' Additionally, to prevent extra difficulty by spawning the lander close to the ground, the terrain is checked if it is too close to the lander, and if it is, then all the y values are flipped, essentially inverting the terrain
    ''' away from the lander, and innovative technique.
    ''' NOTE: a significant portion of terrain generation is broken down into various subs that generateTerrain() is responsible for calling, each with less broad logical focus.
    ''' </summary>
    Private Sub generateTerrain()

        purgeRemnants()

        'the length of the side of the square, maintaining 2^n + 1 form, however arrays are from 0 to value, not 1, hence the + 1 is unnecessary
        Dim n As Integer = 6 + level / 3
        Dim sideSize As Integer = 2 ^ n
        Dim random As System.Random = New System.Random()
        'initialise array as a terrainMap, with correct characteristics -custom type to facilitate unique operations.
        Dim rTerrainMap As TerrainMap = terrainStarter(sideSize, random)


        sideSize /= 2
        Dim middleIndex As New Point(sideSize, sideSize)

        'runs the diamond-square algorithm
        recursiveTerrainAlgorithm(rTerrainMap, random, middleIndex, sideSize)

        Dim maxY As Integer = 50
        Dim minY As Integer = Me.Height - 50
        Array.Resize(terrainSlice, sideSize)

        Dim prevFlatSection As Boolean = False
        Dim oldLength As Integer = 1

        'converts the 3d heightmap to 2d:
        'also adds the flat sections
        For x As Integer = 0 To terrainSlice.GetLength(0) - 1
            Dim y As Integer = terrainSlice.GetLength(0) / 2
            'Regulate y magnitude, to keep it on the screen


            'finds highest and lowest points, -usefull later
            If rTerrainMap.GetValue(x, y) < maxY Then
                maxY = rTerrainMap.GetValue(x, y)
            ElseIf rTerrainMap.GetValue(x, y) > minY Then
                minY = rTerrainMap.GetValue(x, y)
            End If

            'sliced
            terrainSlice(x).Y = rTerrainMap.GetValue(x, y)

            'set flat sections:
            Dim length As Integer = Int((5 * Rnd()) + 1)

            If x > (oldLength + length) And Int((7 * Rnd()) + 1) > 6 And Not prevFlatSection Then
                oldLength += x

                For b As Integer = length To 1 Step -1
                    'go backwards into the already sliced terrain, overwriting the terrain to flat
                    terrainSlice(x - b).Y = rTerrainMap.GetValue(x, y)
                Next

                'record the flat sections array index
                If terrainFlatIndex.Length = 1 Then
                    Array.Resize(terrainFlatIndex, terrainFlatIndex.Length + 1)
                    terrainFlatIndex(0) = x - length
                    terrainFlatIndex(terrainFlatIndex.Length - 1) = x
                Else
                    Array.Resize(terrainFlatIndex, terrainFlatIndex.Length + 2)
                    terrainFlatIndex(terrainFlatIndex.Length - 2) = x - length
                    terrainFlatIndex(terrainFlatIndex.Length - 1) = x
                End If

                prevFlatSection = True
            Else
                prevFlatSection = False
            End If
        Next

        'resize both the landing values array, and the valid landing points array
        Array.Resize(terrainFlatValues, terrainFlatIndex.Length / 2)
        Array.Resize(terrainLandingPoints, terrainFlatIndex.Length)


        'This code ensures that the terrain conforms to the desired heights and depths
        'It ensures that load times are always the same, with no INFINITE load times. - allows very high heightmap resolution.
        'THis is because it scales the already generated terrain, rather than completely re-generate it.
        'normalise to zero:
        Dim normaliseAmount As Integer = -maxY
        'scale the array to fit on the screen vertically
        Dim scaleFactor As Double = CDbl(Me.Height - 100) / (minY - maxY)

        For x As Integer = 0 To terrainSlice.GetLength(0) - 1
            terrainSlice(x).Y += normaliseAmount
            terrainSlice(x).Y *= scaleFactor
            'reshift down
            terrainSlice(x).Y += 50
        Next



        'can also flip terrain upside down if neccessary - provides clearance for start
        Dim CoordOfStart As Integer = lStats.position.X / (Me.Width / terrainSlice.GetLength(0))
        If terrainSlice(CoordOfStart).Y < lStats.position.X + 200 Then
            'flip terrain:
            For x As Integer = 0 To terrainSlice.GetLength(0) - 1
                terrainSlice(x).Y = Me.Height - terrainSlice(x).Y
            Next
        End If

    End Sub



    ''' <summary>
    '''     terrainStarter is responsible for generating a new instance of my custom TerrainMap class, with custom operations.
    '''     It sets the correct side size, and then generates the random 4 corner points at the edges of the terrainMap.
    ''' </summary>
    ''' <param name="sideSize">The side length of the 3d terrain map, generated by 2^n + 1</param>
    ''' <param name="random">The pseudo random sequence being passed.</param>
    ''' <returns>terrainMap, the large multidimensional array of points, as of yet unpopulated.</returns>
    Private Function terrainStarter(sideSize As Integer, random As System.Random) As Model.TerrainMap
        Dim initData(sideSize, sideSize) As Integer
        Dim terrainMap As New TerrainMap(initData)

        '                        random Value,   indicies
        terrainMap.SetValue(random.Next(0, 300), 0, 0)
        terrainMap.SetValue(random.Next(0, 300), sideSize, 0)
        terrainMap.SetValue(random.Next(0, 300), 0, sideSize)
        terrainMap.SetValue(random.Next(0, 300), sideSize, sideSize)

        Return terrainMap
    End Function



    ''' <summary>
    '''     recursiveTerrainAlgorithm is responsible for the implementation of the diamond-square algorithm.
    '''     Also known as the midpoint displacement algorithm, it comprises of averaging the corners of sucssively smaller squares in the center, and adding a random value.
    '''     An explaination can be found here:
    '''     https://en.wikipedia.org/wiki/Diamond-square_algorithm#Midpoint_displacement_algorithm
    '''
    '''     Max recursion depth = 11, assuming that 1920 is the total side value of heightmap since (2^11) > 1920
    '''     I found that:
    '''     The relationship between recursion depth and side length of the 3d heightmap array is logarithmic, given by this equation:
    '''     Hence it is viable to implement this algorithm without significant risk of blowing the stack.
    '''     https://www.desmos.com/calculator/eie2mmuwzz
    '''     For more analysis of this, see the journal.
    '''     NOTE: Since visual basic does not support tail-recursion, infinite recursion is impossible, because the stack will be blown -therfore no infinite load times and undesireable behaviour.
    ''' </summary>
    ''' <param name="rTerrainMap"></param>
    ''' <param name="random"></param>
    ''' <param name="middleIndex"></param>
    ''' <param name="size"></param>
    Private Sub recursiveTerrainAlgorithm(rTerrainMap As TerrainMap, random As System.Random, middleIndex As Point, size As Integer)
        'diamond step: (sum the corners at the center.)
        Dim randomMagnitude As Integer = 50
        Dim diamondValues As Integer = rTerrainMap.GetValue(middleIndex.X - size, middleIndex.Y - size) + rTerrainMap.GetValue(middleIndex.X + size, middleIndex.Y - size) + rTerrainMap.GetValue(middleIndex.X - size, middleIndex.Y + size) + rTerrainMap.GetValue(middleIndex.X + size, middleIndex.Y + size)
        rTerrainMap.SetValue((diamondValues / 4) + random.Next(-size * randomMagnitude, size * randomMagnitude), middleIndex.X, middleIndex.Y)

        'square step -> in a clockwise direction.
        'this is where the diamond and the corners are summed to fill in the ever-smaller squares.
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

        'adjusts the parameters specifying the size and location of the squares on which the diamond squar algorithm is run.
        'this process is recursively making the squares generated from smaller
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



    ''' <summary>
    '''     drawTerrain is responsible for fitting the x axis, and drawing the terrainSlice on the screen. It also draws the thicker flat sections, and generates the multiplier value for them,
    '''     on the first execution.
    ''' </summary>
    ''' <param name="whitePen">The white pen for drawing.</param>
    ''' <param name="thickPen">THe larger white pen for drawing the flat sections of the terrain.</param>
    ''' <param name="e">The object providing information about the drawing event.</param>
    Private Sub drawTerrain(whitePen As Pen, thickPen As Pen, e As PaintEventArgs)

        'calculate the offset required to fit the (comparitively few) points of the terrainSlice onto the screen.
        Dim offset As Integer = Me.Width / (terrainSlice.GetLength(0) - 1)

        Array.Resize(terrainCollider, (terrainSlice.Length))

        Dim lPointArInc As Integer = 0
        Dim lValArInc As Integer = 0

        For i As Integer = 0 To terrainSlice.Length() - 2 Step 1

            'store these points for collision detection later
            Dim offsetPoint As New Point(offset * (i), terrainSlice(i).Y)
            Dim newOffsetPoint As New Point(offset * (i + 1), terrainSlice(i + 1).Y)
            terrainCollider(i) = offsetPoint
            terrainCollider(i + 1) = newOffsetPoint

            'draw the terrain.
            e.Graphics.DrawLine(whitePen, offsetPoint, newOffsetPoint)


            'also apply the fitting offset to the flat sections x coordinates
            For g As Integer = 0 To terrainFlatIndex.Length - 2 Step 1
                If terrainFlatIndex(g) = i And g Mod 2 = 0 Then
                    Dim endFlatOffsetPoint As New Point(offset * terrainFlatIndex(g + 1), terrainSlice(terrainFlatIndex(g + 1)).Y)

                    'render the landing spots thicker
                    e.Graphics.DrawLine(thickPen, offsetPoint, endFlatOffsetPoint)

                    'add values to the valid landing spots
                    terrainLandingPoints(lPointArInc) = offsetPoint
                    terrainLandingPoints(lPointArInc + 1) = endFlatOffsetPoint
                    lPointArInc += 2

                    'ascribe a value modifier to the landing zone
                    terrainFlatValues(lValArInc) = 6 - (terrainFlatIndex(g + 1) - terrainFlatIndex(g))

                    'draw the value modifier to the screen.
                    If labelMaxInc <= terrainFlatValues.Length Then
                        Dim ValueLabel As New Label()
                        ValueLabel.Text = terrainFlatValues(lValArInc) & "x"
                        ValueLabel.Location = New Point(CInt((offsetPoint.X + endFlatOffsetPoint.X) / 2), offsetPoint.Y - 20)
                        ValueLabel.Size = New Size(ValueLabel.PreferredWidth, ValueLabel.PreferredHeight)
                        ValueLabel.BackColor = Color.Transparent
                        ValueLabel.ForeColor = Color.White
                        ValueLabel.AutoSize = True
                        Me.Controls.Add(ValueLabel)

                        labelMaxInc += 1
                    End If

                    lValArInc += 1
                End If
            Next
        Next
    End Sub



    ''' <summary>
    '''     updateLabels refreshes the labels displaying information to the user, such as the label diplaying the speed.
    '''     This is performed at the same framerate as the game, and is called from withing the mainGameLoop.
    ''' </summary>
    Private Sub updateLabels()
        'alter the velocity to seem more speedy and display it
        VerticalLbl.Text = "VERTICAL VELOCITY: " & CInt(lStats.velocity.Y * 30)
        HorizontalLbl.Text = "HORIZONTAL VELOCITY: " & CInt(lStats.velocity.X * 30)
        AltitudeLbl.Text = "ALTITUDE: " & CInt(checkTerrainHeight(2).Y - lStats.realPosition.Y)
        ScoreLbl.Text = "SCORE: " & lStats.Score
        TimeLbl.Text = "TIME:  " & CInt(stopWatch.Elapsed.TotalSeconds)
        FuelLbl.Text = "FUEL:  " & CInt(lStats.fuel)
        LevelLbl.Text = "LEVEL: " & level
        'display [Press Enter] untill the player does so, adivising on how to commence gameplay.
        If lStats.landed = True Then
            LevelLbl.Text = "LEVEL: " & level & " [Press Enter]"
        End If
    End Sub



    ''' <summary>
    ''' applyRotationEffects rotates the lander around a central point, based on the user pressing the "a" and "d" keys. 
    ''' It also calculates the thrust x and y components when the lander is on an angle, not firing purely vertical.
    ''' </summary>
    Private Sub applyRotationalEffects()
        'rotate lander based on user input
        If kPut.a = True And lStats.angle - 0.29 >= 180 And Not lStats.landed Then
            lStats.angle -= 0.29
        ElseIf kPut.d = True And lStats.angle + 0.29 <= 360 And Not lStats.landed Then
            lStats.angle += 0.29
        End If

        'adjust raw thrust values on x and y axis based on current angular orientation
        '   -sideways angle = X thrust becomes bigger, Y thrust smaller
        lStats.thrust.X = Math.Cos(Math.PI / 180 * (lStats.angle)) * lStats.thrustConst
        lStats.thrust.Y = Math.Sin(Math.PI / 180 * (lStats.angle)) * lStats.thrustConst
    End Sub



    ''' <summary>
    ''' sums the vectors of acceleration, velocity and gravity, providing accurate and realistic movement, compliant with Newtonian Physics.
    ''' This fundamental part of the gameplay was important, and great focus was placed on the feel of the gameplay.
    ''' This sub modifies the lander position dependant on the user input activating the thrusters, or without input, the influence of gravity.
    ''' The Newtonian feel is achieved by summing vector components, which are my own custom type, facilitating the summing of both x and y at the same time.
    ''' </summary>
    Private Sub applyAccelerationGravityFromUserInput()
        'apply acceleration of thruster or gravity depending on user input
        If kPut.space And lStats.fuel > 0 And Not lStats.landed Then
            lStats.angle = 270
            'double thrust for emergency escape
            lStats.acceleration = lStats.gravity + lStats.thrust + lStats.thrust
            If lStats.fuel - 4 > 0 Then
                lStats.fuel -= 3
            Else
                lStats.fuel = 0
            End If

        ElseIf kPut.w And lStats.fuel > 0 And Not lStats.landed Then
            'total acceleration is the acceleration from gravity + the acceleration from thrusters
            lStats.acceleration = lStats.gravity + lStats.thrust
            'lStats.fuel = max(0, lStats.fuel - 0.25)
            If lStats.fuel - 0.25 > 0 Then
                lStats.fuel -= 0.25
            Else
                lStats.fuel = 0
            End If
        ElseIf kPut.enter = True Then
            lStats.landed = False
        ElseIf Not lStats.landed Then
            lStats.acceleration = lStats.gravity
        End If

        'perform the summing of vectors, hence updating position
        lStats.velocity += lStats.acceleration
        lStats.position += lStats.velocity
    End Sub




    ''' <summary>
    '''     A usefull hud, which when enabled, provides movement and impact data for the user. -Note: this is for the advanced user or developer, not a casual user without access to source code.
    ''' </summary>
    ''' <param name="debugPen">The magenta pen used for debug rendering.</param>
    ''' <param name="e">The object providing information about the event.</param>
    Private Sub debugHUD(debugPen As Pen, e As PaintEventArgs)
        'debug stuff
        Dim retArr As Point() = checkTerrainHeight()
        Dim debugLoc As New Point(retArr(0).X, 20)
        e.Graphics.DrawLine(debugPen, retArr(0), debugLoc)
        debugLoc = New Point(retArr(1).X, 20)
        e.Graphics.DrawLine(debugPen, retArr(1), debugLoc)
        debugLoc = New Point(Me.Width, retArr(2).Y)
        e.Graphics.DrawLine(debugPen, retArr(2), debugLoc)
        e.Graphics.DrawLine(debugPen, lStats.realPosition.X, lStats.realPosition.Y, Me.Width, lStats.realPosition.Y)

        'gradient drawing for debugging
        Dim endPoint As New Point(retArr(0).X + Math.Cos((Math.PI / 180.0) * gradient) * 150, retArr(0).Y + Math.Sin((Math.PI / 180.0) * gradient) * 150)
        e.Graphics.DrawLine(debugPen, retArr(0), endPoint)
        'Console.WriteLine("endPoint: (" & endPoint.X & "," & endPoint.Y)
    End Sub




    ''' <summary>
    ''' evaluateGameState is responsible for managing the subroutines relating to the end of the game or next level, and verifies that the state of the landing subroutine
    ''' </summary>
    ''' <param name="sender">The object that sent the event.</param>
    ''' <param name="e">The object providing information about the event.</param>
    Private Sub evaluateGameState(sender As Object, e As System.EventArgs)
        'verify if a collision has occured. Note: the checking of the x coordinate is to prevent a gradient = 0, false crash from occuring.
        If lStats.realPosition.Y + 25 >= checkTerrainHeight(2).Y And lStats.realPosition.X >= 0 Then
            'impact has occured, verify if it is legitimate or a crash
            If landing() Then
                'legitimate landing
                gameWon(sender, e)
            Else
                'crash has occured
                gameOver()
            End If
        ElseIf lStats.realPosition.Y > Me.Height Then
            'offscreen crash has occured, by going below the bottom of the screen
            'this is to prevent an infinite flight where fuel = 0 and the lander is falling offscreen
            gameOver()
        End If
    End Sub



    ''' <summary>
    '''     drawLander draws the lander body from a series of lines, around a central point.
    '''     Each line is a specific distance and angle from the centeral point, which drawLander calls the inbuilt DrawLine function on.
    ''' </summary>
    ''' <param name="whitePen"></param>
    ''' <param name="flamePen"></param>
    ''' <param name="e">The object providing information about the event.</param>
    Private Sub drawLander(whitePen As Pen, flamePen As Pen, e As PaintEventArgs)
        Dim landerLinesPosition As New Point(lStats.position.X, lStats.position.Y)

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



    ''' <summary>
    '''     CheckFrameRate is more usefull for the developer than the end client. This sub measures the framerate of the logic, though not neccesarily the graphics rendering.
    '''     Additionally, the subroutine increments the score once per frame, over time.
    ''' </summary>
    Private Sub CheckFrameRate()
        frameRate = frameCounter / stopWatch.Elapsed.TotalSeconds
        'displays framerate live, facilitating adjustment of timing parameters if the user wishes to troubleshoot
        Console.WriteLine("The framerate is: " & frameRate & " Total frames are: " & frameCounter)

        'prevent infinite score whilst stationary
        If lStats.landed = False Then
            lStats.Score += 1
        End If

    End Sub



    ''' <summary>
    '''     landing is called when a collision has occured, and determines if the collsion was at a slow speed and strait angle, on a flat section.
    '''     If these criteria are fufilled, then the function returns true.
    ''' </summary>
    ''' <returns>Boolean, stating whether the landing was successful</returns>
    Private Function landing() As Boolean
        'hitboxes are the lowest possible points, -at all time these are the lander legs' feet

        'What does that mean?
        'The distance from the foot to the center of the lander on the y-axis and x-axis are roughly equivalent
        'Because there is 2 feet, and restricted rotation, there will always be a foot to hit first - before lander chassis
        'Hence, -an abstraction of 25 pixels from the center of the lander will estimate collision detection.
        '-this is because of a similarity in the y axis distance irregardless of rotation.

        'speed (in user scale) is less than 9 on x and y axis.
        Dim validSpeed As Boolean = Math.Abs(lStats.velocity.X) * 30 < 8 And Math.Abs(lStats.velocity.Y) * 30 < 8
        'angle +- 12 degrees to the vertical (270)
        Dim validAngle As Boolean = lStats.angle >= 258 And lStats.angle <= 282

        If validSpeed And validAngle Then
            Dim valueIndex As Integer
            'check landing location is flat and valid
            For i As Integer = 1 To terrainLandingPoints.Length - 2 Step 2
                If lStats.realPosition.X - 10 >= terrainLandingPoints(i).X And lStats.realPosition.X + 10 <= terrainLandingPoints(i + 1).X Then
                    'add fuel according to the landing pad amount
                    lStats.fuel += terrainFlatValues(i - 1) * 100
                    lStats.Score += terrainFlatValues(i - 1) * 150
                    'return true because a valid landing has occured
                    Return True
                End If
                valueIndex += 1
            Next i
            'landing on the jagged section
            Return False
        Else
            'crash has occured
            Return False
        End If
    End Function

    ''' <summary>
    ''' rotateMatrix sets all of the graphics drawing on an angle. THis means that any subsequent drawing (e.g. the lander) is rotated by the specified amount.
    ''' This is how the lander is rotated graphically.
    ''' </summary>
    ''' <param name="e"></param>
    Private Sub rotateMatrix(e As PaintEventArgs)
        'rotates the entire graphics rendering around an axis, after the e.Graphics.Transform = myMatrix
        Dim centerOfRotation As New Point(lStats.position.X + 10, lStats.position.Y + 17)
        'do the rotation matrix
        Dim myMatrix As New Matrix
        myMatrix.RotateAt(lStats.angle + 90, centerOfRotation)
        e.Graphics.Transform = myMatrix
    End Sub

    ''' <summary>
    '''     checkTerrainHeight functions to determine the closest edges of the flat terrain segment on the left and right of the lander.
    '''     From this, it can then deduce the impact point using linear equations. (see desmos)
    '''     This is then returned in an array of points.
    ''' </summary>
    ''' <returns>Array of points</returns>
    Private Function checkTerrainHeight() As Point()
        'find closest points in terrain on left and right of lander
        Dim closestLeft As New Point(0, 0)
        Dim closestRight As New Point(0, 0)
        For x As Integer = 0 To terrainCollider.Length - 2
            If lStats.realPosition.X >= closestRight.X Then
                closestLeft = terrainCollider(x)
                closestRight = terrainCollider(x + 1)
            End If
        Next

        'Terrain Collision mk.2-3 (mathematical graphing: https://www.desmos.com/calculator/cizpv1b4bb)
        'STEP 1: find gradient between the two points -> irregardless of which is higher
        'prevent NaN gradient (divide by 0) and subsequent overflow:
        If lStats.realPosition.X <= 0 Then
            gradient = 0
        Else
            gradient = (closestRight.Y - closestLeft.Y) / (closestRight.X - closestLeft.X)
        End If

        'STEP 2: Find the y-value of the line given by equation: y = gradient( lstats.Position.X - closestLeft.X) + closestLeft.Y
        Dim collisionHeight As Integer = gradient * (CDbl(lStats.realPosition.X) - CDbl(closestLeft.X)) + CDbl(closestLeft.Y)

        'return the collision height as a point
        Dim imPoint As New Point(lStats.realPosition.X, collisionHeight)
        Dim retArr As Point() = {closestLeft, closestRight, imPoint}
        Return retArr
    End Function



    ''' <summary>
    '''     gameWon is called when a successful landing takes place, and map needs to be re-generated.
    '''     gameWon then increments level, removes all the controls, including the terrain and UI labels, and then triggers a re-load of the form, with a fresh map.
    ''' </summary>
    ''' <param name="sender">The object that sent the event.</param>
    ''' <param name="e">The object providing information about the event.</param>
    Private Sub gameWon(sender As Object, e As System.EventArgs)
        level += 1
        'restart game, different map
        Me.Controls.Clear() 'removes all the controls on the form
        InitializeComponent() 'load all the controls again
        Game_Load(sender, e) 'load everything in the form, triggering load event again.

    End Sub



    ''' <summary>
    ''' gameOver is ran when the player crashes.
    ''' The remaining fuel is added to the score, and the form is closed
    ''' A boolean is set which makes the next form loaded be the highscore entering form.
    ''' </summary>
    Private Sub gameOver()
        'convert remaining fuel to points
        lStats.Score += lStats.fuel
        'index score, reveal highScore, name enter screen
        lStats.gameOver = True
        Me.Close()
    End Sub



    ''' <summary>
    ''' Purges all the global terrain arrays which are not immediately re-written at launch.
    ''' </summary>
    Private Sub purgeRemnants()
        ReDim terrainFlatIndex(0)
        ReDim terrainFlatValues(0)
        ReDim terrainLandingPoints(0)

        labelMaxInc = 0

    End Sub



    ''' <summary>
    ''' This subroutine adds invisible labels to the screen, to ensure that there is always a consistent number on the screen, irregardless of the terrain value labels.
    ''' This is to control the strange phenomena, where more labels results in more frame-rate.
    ''' </summary>
    Private Sub spamLabels()
        For i As Integer = 0 To (10 - labelMaxInc) Step 1
            Dim SpamLabel As New Label()
            SpamLabel.Text = "Performance Spam Label"
            'Note: must be onscreen
            SpamLabel.Location = New Point(Me.Width / 2 + i * 10, 0)
            SpamLabel.Size = New Size(SpamLabel.PreferredWidth, SpamLabel.PreferredHeight)
            SpamLabel.BackColor = Color.Transparent
            SpamLabel.ForeColor = Color.Black
            SpamLabel.AutoSize = True
            Me.Controls.Add(SpamLabel)
        Next i
    End Sub



    ''' <summary>
    ''' Function sets boolean values of class keyInput in accordance with KeyDown events
    ''' </summary>
    ''' <param name="sender">The object that sent the KeyDown event.</param>
    ''' <param name="e">The object providing information about the KeyDown event.</param>
    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyData
            Case Keys.W
                kPut.w = True
            Case Keys.A
                kPut.a = True
            Case Keys.S
                kPut.s = True
            Case Keys.D
                kPut.d = True
            Case Keys.Space
                kPut.space = True
            Case Keys.Enter
                kPut.enter = True
        End Select
    End Sub


    ''' <summary>
    ''' Function sets boolean values of class keyInput in accordance with KeyUp events
    ''' </summary>
    ''' <param name="sender">The object that sent the KeyUp event.</param>
    ''' <param name="e">The object providing information about the KeyUp event.</param>
    Private Sub Form1_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        Select Case e.KeyData
            Case Keys.W
                kPut.w = False
            Case Keys.A
                kPut.a = False
            Case Keys.S
                kPut.s = False
            Case Keys.D
                kPut.d = False
            Case Keys.Space
                kPut.space = False
            Case Keys.Enter
                kPut.Enter = False
        End Select
    End Sub



    ''' <summary>
    ''' setDefaults sets a series of parameters and globals to their correct states
    ''' This is called on launch.
    ''' </summary>
    Private Sub setDefaults()
        Timer1.Enabled = True
        stopWatch.Start()

        'define gravity etc...
        lStats.gravity.Y = 0.00075
        lStats.gravity.X = 0

        lStats.thrust.X = 0
        lStats.thrust.Y = 0.0012

        lStats.thrustConst = 0.0012

        lStats.position.X = 100
        lStats.position.Y = 100


        lStats.velocity.X = 0
        lStats.velocity.Y = 0

        lStats.acceleration.X = 0
        lStats.acceleration.Y = 0

        lStats.angle = 270
        lStats.fuel = 2000

        lStats.landed = True
        lStats.gameOver = False

        Randomize()
    End Sub

End Class