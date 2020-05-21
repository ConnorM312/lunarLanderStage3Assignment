Public Class HighScoreFrm
    Dim score As Integer = 0
    Private Sub HighScore_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ScoreLabel.Text = GameFrm.finalScore
        score = GameFrm.finalScore
    End Sub

    Private Sub HighScore_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        TitleScreenFrm.Show()
    End Sub

    Private Sub EnterButton_Click(sender As Object, e As EventArgs) Handles EnterBtn.Click
        Dim name As String = inputTxtbx.Text
        Dim score As String = ScoreLabel.Text

        computeUserInput(name, score)
    End Sub

    Public Sub computeUserInput(name As String, score As String)
        If sanitizeInput(name) = True Then
            storeInput(name, score)

            'close input form, revealing title screen
            Me.Close()
        Else
            MessageBox.Show("Please enter a valid name. It must be between 1 and 10 characters, from A - z, with no spaces.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Public Function sanitizeInput(name As String)
        If checkLength(name) = True AndAlso checkCharacters(name) = True Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function checkCharacters(name As String) As Boolean
        For i As Integer = 0 To name.Length - 1
            If Asc(name(i)) < 65 Or (Asc(name(i)) > 90 And Asc(name(i)) < 97) Or Asc(name(i)) > 122 Then
                Return False
            End If
        Next
        Return True
    End Function

    Private Function checkLength(name As String) As Boolean
        Select Case name
            Case ""
                Return False
            Case name.Length > 10
                Return False
            Case Else
                Return True
        End Select
    End Function

    Public Sub storeInput(sanitizedName As String, score As String)
        'employs csv style, writing to file
        Dim detailsWriter As System.IO.StreamWriter
        detailsWriter = My.Computer.FileSystem.OpenTextFileWriter(Application.StartupPath & "\highScores.txt", True)
        detailsWriter.WriteLine(sanitizedName & "," & score)
        detailsWriter.Close()
    End Sub

End Class