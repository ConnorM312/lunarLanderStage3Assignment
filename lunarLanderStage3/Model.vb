Module Model
    Public Structure Vector
        Public X As Single
        Public Y As Single
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
    End Class

    Public Class TerrainMap
        Private data As Array
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
        Public Sub New(initData As Array)
            Me.data = initData
        End Sub

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

    End Class
End Module
