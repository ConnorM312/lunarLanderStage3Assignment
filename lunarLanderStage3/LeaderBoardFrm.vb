Public Class LeaderBoardFrm

    Private Sub EnterBtn_Click(sender As Object, e As EventArgs) Handles EnterBtn.Click
        Me.Close()
    End Sub

    Private Sub LeaderBoardFrm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        TitleScreenFrm.Show()
    End Sub

    Private Sub LeaderBoardFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TitleScreenFrm.Hide()

        Dim unsortedArr As String() = getScores()

        Dim unsortedScoresArr As Integer() = iterate(unsortedArr)

        Dim sortedScoresArr As Integer() = sortScores(unsortedScoresArr)

        Dim sortedArr As String() = rejoin(unsortedArr, sortedScoresArr)

        outputToList(sortedArr)
    End Sub

    Private Sub outputToList(sortedArr() As String)
        For i As Integer = 0 To sortedArr.Length - 1 Step 2
            HighScoresLstbx.Items.Add(sortedArr(i) & ": " & sortedArr(i + 1))
        Next
    End Sub

    Private Function rejoin(unsortedArr() As String, sortedScoresArr() As Integer) As String()
        Dim doubleInc As Integer = 1
        Dim sortedArr(unsortedArr.Length - 1) As String
        For i As Integer = 0 To sortedScoresArr.Length - 1 Step 1
            'loop through to find where the scores match, and therefore get the sorted name index.
            Dim u As Integer = 1
            Dim found = False
            While u <= unsortedArr.Length And found = False
                If sortedScoresArr(i) = unsortedArr(u) Then
                    'set name
                    sortedArr(doubleInc - 1) = unsortedArr(u - 1)
                    'set score
                    sortedArr(doubleInc) = unsortedArr(u)
                    found = True
                End If
                u += 2
            End While
            doubleInc += 2
        Next i
        Return sortedArr
    End Function

    Private Function getScores() As String()
        Dim unsortedArr(0) As String
        'dumps all the text file into dumpString
        Dim scoreReader As System.IO.StreamReader
        scoreReader = My.Computer.FileSystem.OpenTextFileReader(Application.StartupPath & "\highScores.txt")
        Dim dumpString As String = scoreReader.ReadToEnd()

        'split dump string on newlines
        Dim userSplit() = dumpString.Split({vbCrLf}, StringSplitOptions.RemoveEmptyEntries)
        'split dump string at commas, and insert it into unsortedArr
        For Each line In userSplit
            Dim values() = line.Split({","}, StringSplitOptions.RemoveEmptyEntries)

            If unsortedArr.Length > 1 Then
                Array.Resize(unsortedArr, unsortedArr.Length + 2)
            Else
                Array.Resize(unsortedArr, unsortedArr.Length + 1)
            End If

            unsortedArr(unsortedArr.Length - 2) = values(0)
            unsortedArr(unsortedArr.Length - 1) = values(1)
        Next
        Return unsortedArr
    End Function

    Private Function iterate(unsortedArr As String()) As Integer()
        Dim scores(unsortedArr.Length / 2 - 1) As Integer
        Dim inc As Integer = 0
        For i As Integer = 1 To unsortedArr.Length Step 2
            scores(inc) = CInt(unsortedArr(i))
            inc += 1
        Next i
        Return scores
    End Function

    Private Function sortScores(unsortedScoresArr As Integer()) As Integer()
        'selection sort, of scores -for lookup later
        Dim lengthUnsorted = unsortedScoresArr.Length - 1
        Dim passes = 0

        'loop while there is still an unsorted section
        While lengthUnsorted > 0
            Dim index As Integer = 0
            Dim max As Integer = unsortedScoresArr(index)
            Dim posMax As Integer = index
            'iterate over unsorted sections
            While index < lengthUnsorted
                index += 1
                'find the largest value
                If unsortedScoresArr(index) < max Then
                    max = unsortedScoresArr(index)
                    posMax = index
                End If
            End While
            passes += 1
            'swap the larger and smaller value
            swapVals(unsortedScoresArr(posMax), unsortedScoresArr(lengthUnsorted))
            lengthUnsorted -= 1
        End While

        Return unsortedScoresArr
    End Function

    Private Sub swapVals(ByRef a As Integer, ByRef b As Integer)
        Dim c = a
        a = b
        b = c
    End Sub

End Class