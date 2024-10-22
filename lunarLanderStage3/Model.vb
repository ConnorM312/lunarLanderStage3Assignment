﻿Module Model

    ''' <summary>
    ''' A custom vector structure, similar to Points, faciltiating the addition and subtraction of x and y components simultaneously.
    ''' This saved a great deal of complexity in the code.
    ''' </summary>
    Public Structure Vector
        Public X As Single
        Public Y As Single
        'sum the x and y components, and return the resultant vector.
        Public Shared Operator +(ByVal v1 As Vector, ByVal v2 As Vector)
            Dim vsum As Vector
            vsum.X = v1.X + v2.X
            vsum.Y = v1.Y + v2.Y
            Return vsum
        End Operator
        'subtract the x and y components, and return the resultant vector.
        Public Shared Operator -(ByVal v1 As Vector, ByVal v2 As Vector)
            Dim vsum As Vector
            vsum.X = v1.X - v2.X
            vsum.Y = v1.Y - v2.Y
            Return vsum
        End Operator
    End Structure


    Public Class landerStatistics
        Public position As Vector
        Public realPosition As Vector
        Public angle As Double
        Public velocity As Vector
        Public acceleration As Vector
        Public gravity As Vector
        Public thrust As Vector
        Public thrustConst As Double
        Public fuel As Double
        Public landed As Boolean
        Public Score As Integer
        Public gameOver As Boolean
    End Class

    ''' <summary>
    ''' custom terrainMap class facilitated wrap-around lookups outside of the bounds of the array
    ''' It also facilitated returning 0 when there was no value, preventing crashes.
    ''' </summary>
    Public Class TerrainMap
        Private data As Array
        ''' <summary>
        ''' look up of value at a give x and y coordinate in the TerrainMap.
        ''' </summary>
        ''' <param name="x">The x index.</param>
        ''' <param name="y">The y index.</param>
        ''' <returns></returns>
        Public Function GetValue(x As Integer, y As Integer)
            If x < 0 Then
                x = Math.Abs(x)
                x = x Mod data.GetLength(0)
                x = data.GetLength(0) - x
            End If
            If y < 0 Then
                y = Math.Abs(y)
                y = y Mod data.GetLength(0)
                y = data.GetLength(0) - y
            End If
            'works if in or outside constraints - wraps value around
            x = x Mod data.GetLength(0)
            y = y Mod data.GetLength(0)

            Return data(x, y)
        End Function
        ''' <summary>
        ''' sets intialisation data to be local data.
        ''' </summary>
        ''' <param name="initData">the initialisation data, for all TerrainMaps</param>
        Public Sub New(initData As Array)
            Me.data = initData
        End Sub
        ''' <summary>
        ''' Takes in a value and points, and facilitates single line setting of a terrainMap index, providing a significantly more readable implementation of algorithms such as the recursiveTerrainAlgorithm
        ''' </summary>
        ''' <param name="value"></param>
        ''' <param name="x"></param>
        ''' <param name="y"></param>
        Public Sub SetValue(value As Integer, x As Integer, y As Integer)
            If (x >= 0) And (x < Me.data.GetLength(0)) And (y >= 0) And (y < Me.data.GetLength(0)) Then
                Me.data(x, y) = value
            End If
        End Sub
    End Class
    Public Class keyInput
        Public w As Boolean = False
        Public a As Boolean = False
        Public s As Boolean = False
        Public d As Boolean = False
        Public space As Boolean = False
        Public Enter As Boolean = False

    End Class
End Module
