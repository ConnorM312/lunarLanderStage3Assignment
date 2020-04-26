Module Model
    Public Structure Vector
        Public X As Single
        Public Y As Single
        'make it so that I can add and subtract vectors, not multiply though
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
        Public angle As Double
        Public velocity As Vector
        Public acceleration As Vector
        Public gravity As Vector
        Public thrust As Vector
        Public thrustConst As Double
        Public fuel As Double
    End Class

    'does edges, makes generic
    Public Class TerrainMap
        Private data As Array
        Public Function GetValue(x As Integer, y As Integer)
            If x <= data.GetLength(0) And y <= data.GetLength(0) Then
                Return data(x, y)
            Else
                Return 0
            End If
        End Function
        Public Sub New(initData As Array)
            Me.data = initData
        End Sub

        Public Sub SetValue(value As Integer, x As Integer, y As Integer)
            Me.data(x, y) = value
        End Sub
    End Class

End Module
