Public Class TestingFrm

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles testBtn.Click
        'CheckLength
        testOutputLstbx.Items.Add("Testing of checkLength subroutine:")
        testOutputLstbx.Items.Add("Input: 'string' Result: '" & HighScoreFrm.checkLength("string") & "'") 'should be valid
        testOutputLstbx.Items.Add("Input: '' Result: '" & HighScoreFrm.checkLength("") & "'") 'should be invalid, empty string
        testOutputLstbx.Items.Add("Input: 'tooLongString' Result: '" & HighScoreFrm.checkLength("tooLongString") & "'") 'should be invalid, more than 10 characters - ERROR detected. 'error found in testing -by the nature of a casewhere, this is never ran. 'potential fix: Select case length, but even then it is not intended for an infinite number of cases
        testOutputLstbx.Items.Add("Input: ' spaces ' Result: '" & HighScoreFrm.checkLength(" spaces ") & "'") 'should be valid, spaces are not invalid in this subroutine - testing of unexpected input
        testOutputLstbx.Items.Add("Input: ' -1!@𝓷几 ' Result: '" & HighScoreFrm.checkLength("' -1!@𝓷几 ") & "'") 'testing unexpected input, unicode input, should STILL be valid.

        'checkCharacters
        testOutputLstbx.Items.Add("Testing of: checkCharacters subroutine")
        testOutputLstbx.Items.Add("Input: 'string' Result: '" & HighScoreFrm.checkCharacters("string") & "'") 'should be valid
        testOutputLstbx.Items.Add("Input: 'STRING' Result: '" & HighScoreFrm.checkCharacters("STRING") & "'") 'should be valid
        testOutputLstbx.Items.Add("Input: 'string with spaces' Result: '" & HighScoreFrm.checkCharacters("string with spaces") & "'") 'should be invalid, space is not within the described ascii bounds
        testOutputLstbx.Items.Add("Input: 'stringWithNumbers1234' Result: '" & HighScoreFrm.checkCharacters("stringWithNumbers1234") & "'") 'should be invalid, numerical characters not within the bounds
        testOutputLstbx.Items.Add("Input: 'stringWithEmoticons😃' Result: '" & HighScoreFrm.checkCharacters("stringWithEmoticons😃") & "'") 'should be invalid, unicode outside ascii bounds
        testOutputLstbx.Items.Add("Input: 'vbCrLf' Result: '" & HighScoreFrm.checkCharacters("vbCrLf") & "'") 'should be valid, interpret the newline as a string
        testOutputLstbx.Items.Add("Input: 'reallyVeryLongButOtherwiseValidString' Result: '" & HighScoreFrm.checkCharacters("reallyVeryLongButOtherwiseValidString") & "'") 'should be valid, despite failure in checkLength function.
        testOutputLstbx.Items.Add("Input: '' Result: '" & HighScoreFrm.checkCharacters("") & "'") 'should be ???, this input is unexpected, and assumes a failure in the previous checkLength subroutine -due to shortcircuiting in call

        'sortscores
        testOutputLstbx.Items.Add("Testing of: sortScores subroutine")
        Dim testArr() As Integer = {12, 12, 23, 43, 64, 98, 1, 143, 65}
        testOutputLstbx.Items.Add("Input: '" & ArrayToString(testArr) & "' Result: '" & ArrayToString(LeaderBoardFrm.sortScores(testArr)) & "'")
        testArr = {5, 6, 7}
        testOutputLstbx.Items.Add("Input: '" & ArrayToString(testArr) & "' Result: '" & ArrayToString(LeaderBoardFrm.sortScores(testArr)) & "'")
        testArr = {5, 7, 6}
        testOutputLstbx.Items.Add("Input: '" & ArrayToString(testArr) & "' Result: '" & ArrayToString(LeaderBoardFrm.sortScores(testArr)) & "'")
        testArr = {6, 5, 7}
        testOutputLstbx.Items.Add("Input: '" & ArrayToString(testArr) & "' Result: '" & ArrayToString(LeaderBoardFrm.sortScores(testArr)) & "'")
        testArr = {6, 7, 5}
        testOutputLstbx.Items.Add("Input: '" & ArrayToString(testArr) & "' Result: '" & ArrayToString(LeaderBoardFrm.sortScores(testArr)) & "'")
        testArr = {7, 5, 6}
        testOutputLstbx.Items.Add("Input: '" & ArrayToString(testArr) & "' Result: '" & ArrayToString(LeaderBoardFrm.sortScores(testArr)) & "'")
        testArr = {7, 6, 5}
        testOutputLstbx.Items.Add("Input: '" & ArrayToString(testArr) & "' Result: '" & ArrayToString(LeaderBoardFrm.sortScores(testArr)) & "'")
        testArr = {1, 1, 1}
        testOutputLstbx.Items.Add("Input: '" & ArrayToString(testArr) & "' Result: '" & ArrayToString(LeaderBoardFrm.sortScores(testArr)) & "'") 'unexpected input


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