Public Class LeaderBoardFrm

    ''' <summary>
    '''     Closes the form, and returns the user to the title screen when they press the title screen button
    ''' </summary>
    ''' <param name="sender">The object that sent the event.</param>
    ''' <param name="e">The object providing information about the event.</param>
    Private Sub titleBtn_Click(sender As Object, e As EventArgs) Handles titleBtn.Click
        Me.Close()
    End Sub



    ''' <summary>
    '''     Shows the titleScreen form when the leaderboard form is closing.
    ''' </summary>
    ''' <param name="sender">The object that sent the event.</param>
    ''' <param name="e">The object providing information about the event.</param>
    Private Sub LeaderBoardFrm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        TitleScreenFrm.Show()
    End Sub



    ''' <summary>
    '''     The subroutine is triggered upon the loading of the leaderboard screen. Accordingly, it is responsible for orchestrating the other subroutines which get, sort and load the leaderboard into the list
    '''     for display.
    ''' </summary>
    ''' <param name="sender">The object that sent the event.</param>
    ''' <param name="e">The object providing information about the event.</param>
    Private Sub LeaderBoardFrm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TitleScreenFrm.Hide()

        Dim unsortedArr As String() = getScores()

        Dim unsortedScoresArr As Integer() = iterate(unsortedArr)

        Dim sortedScoresArr As Integer() = sortScores(unsortedScoresArr)

        Dim sortedArr As String() = rejoin(unsortedArr, sortedScoresArr)

        outputToList(sortedArr)
    End Sub



    ''' <summary>
    '''     A subroutine which takes in a sorted array and iterates over it outputting each name and score to the list.
    ''' </summary>
    ''' <param name="sortedArr"> The sorted array, with names follwed by scores.</param>
    Private Sub outputToList(sortedArr() As String)
        For i As Integer = 0 To sortedArr.Length - 1 Step 2
            HighScoresLstbx.Items.Add(sortedArr(i) & ": " & sortedArr(i + 1))
        Next
    End Sub



    ''' <summary>
    '''     rejoin() takes in an unsorted array of names and scores, and a sorted array, comprising only of scores. It then proceeds to use the unsorted list to find the names for the sorted scores
    '''     in the sorted list. Once all the scores can be attributed the correct name, by searching the array, the now sorted array of names and scores will be outputted in an array.
    ''' </summary>
    ''' <param name="unsortedArr"></param>
    ''' <param name="sortedScoresArr"></param>
    ''' <returns></returns>
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



    ''' <summary>
    '''     getScores is a function which accesses the highscores text file, and, using a streamreader dumps the contents of the file into a string. Following that, it proceeds to iterate over the
    '''     string, splitting it into name and score, and entering these values into an unsorted array.
    ''' </summary>
    ''' <returns>an unsorted array comprising usernames and scores.</returns>
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



    ''' <summary>
    '''     iterate extracts the unsorted scores from the unsorted array.
    ''' </summary>
    ''' <param name="unsortedArr"></param>
    ''' <returns>iterate returns an unsorted array of only scores.</returns>
    Private Function iterate(unsortedArr As String()) As Integer()
        Dim scores(unsortedArr.Length / 2 - 1) As Integer
        Dim inc As Integer = 0
        For i As Integer = 1 To unsortedArr.Length Step 2
            scores(inc) = CInt(unsortedArr(i))
            inc += 1
        Next i
        Return scores
    End Function

    ''' <summary>
    '''     sortScores uses a selection sort, in order to sort an unsorted array of scores into a sorted array of scores for output.
    ''' </summary>
    ''' <param name="unsortedScoresArr"></param>
    ''' <returns>A sorted array of scores.</returns>
    Public Function sortScores(unsortedScoresArr As Integer()) As Integer()
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

    ''' <summary>
    '''     swapVals() is a simple subroutine for swapping the value of two memory addresses, because of its byRef parameters.
    ''' </summary>
    ''' <param name="a">first parameter</param>
    ''' <param name="b">second parameter</param>
    Private Sub swapVals(ByRef a As Integer, ByRef b As Integer)
        Dim c = a
        a = b
        b = c
    End Sub

End Class