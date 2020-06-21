Public Class TestingFrm

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles testBtn.Click
        testOutputLstbx.Items.Add("Testing of: checkLength subroutine")
        testOutputLstbx.Items.Add("Input: 'string' Result: '" & HighScoreFrm.checkLength("string") & "'")
        testOutputLstbx.Items.Add("Testing of: checkCharacters subroutine")
        testOutputLstbx.Items.Add("Input: 'string' Result: '" & HighScoreFrm.checkCharacters("string") & "'")
        testOutputLstbx.Items.Add("Testing of: sortScores subroutine")
        Dim testArr() As Integer = {12, 12, 23, 43, 64, 98, 1, 143, 65}
        testOutputLstbx.Items.Add("Input: '" & ArrayToString(testArr) & "' Result: '" & ArrayToString(LeaderBoardFrm.sortScores(testArr)) & "'")

    End Sub
    Private Function ArrayToString(inputArr As Integer()) As String
        Dim returnString As String = ""
        For i As Integer = 0 To inputArr.Length() - 1 Step 1
            If i <> inputArr.Length() - 1 Then
                returnString += CStr(inputArr(i)) & ", "
            Else
                returnString += CStr(inputArr(i))
            End If
        Next i
        Return returnString
    End Function
End Class