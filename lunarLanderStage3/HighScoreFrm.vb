Public Class HighScoreFrm
    Dim score As Integer = 0


    ''' <summary>
    '''     The load subroutine for the highScore form, it accesses the public finalScore of the player from GameFrm
    ''' </summary>
    ''' <param name="sender">The object that sent the event.</param>
    ''' <param name="e">The object providing information about the event.</param>
    Private Sub HighScore_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ScoreLabel.Text = GameFrm.finalScore
        score = GameFrm.finalScore
    End Sub


    ''' <summary>
    '''     Called when the form closes, the subroutine ensures that the titleScreen is shown upon closure.
    ''' </summary>
    ''' <param name="sender">The object that sent the event.</param>
    ''' <param name="e">The object providing information about the event.</param>
    Private Sub HighScore_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        TitleScreenFrm.Show()
    End Sub


    ''' <summary>
    '''     Called when the player presses the button to enter their name, the sub accesses the name and score of the player, and calls the computeUserInput sub on these values.
    ''' </summary>
    ''' <param name="sender">The object that sent the event.</param>
    ''' <param name="e">The object providing information about the event.</param>
    Private Sub EnterButton_Click(sender As Object, e As EventArgs) Handles EnterBtn.Click
        Dim name As String = inputTxtbx.Text
        Dim score As String = ScoreLabel.Text

        computeUserInput(name, score)
    End Sub


    ''' <summary>
    '''     Is responsible for managing the subroutines that storage and validation of the user name input. It also controls the display of a warning box when the input is invalid, and closing the form
    '''     upon successfull input entry, to prevent further entries of the same score.
    ''' </summary>
    ''' <param name="name"></param>
    ''' <param name="score"></param>
    Public Sub computeUserInput(name As String, score As String)
        If sanitizeInput(name) = True Then
            storeInput(name, score)

            'close input form, revealing title screen
            Me.Close()
        Else
            MessageBox.Show("Please enter a valid name. It must be between 1 and 10 characters, from A - z, with no spaces.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    ''' <summary>
    '''     sanitizeInput controls the subroutines that verify the aspects of input validation, returning true if the input is valid.
    ''' </summary>
    ''' <param name="name">the username inputed, to be sanitized</param>
    ''' <returns>boolean regarding state of validation</returns>
    Public Function sanitizeInput(name As String)
        If checkLength(name) = True AndAlso checkCharacters(name) = True Then
            Return True
        Else
            Return False
        End If
    End Function


    ''' <summary>
    '''     checkCharacters is responsible for ensuring that only the A - Z upper and lowercase characters can comprise an aspect of the user input. This is achieved by analysing the ascii codes of 
    '''     the input numbers.
    ''' </summary>
    ''' <param name="name">the username inputed to be sanitized.</param>
    ''' <returns>boolean regarding state of validation</returns>
    Public Function checkCharacters(name As String) As Boolean
        For i As Integer = 0 To name.Length - 1
            If Asc(name(i)) < 65 Or (Asc(name(i)) > 90 And Asc(name(i)) < 97) Or Asc(name(i)) > 122 Then
                Return False
            End If
        Next
        Return True
    End Function


    ''' <summary>
    ''' checkLength verifies that the length of the username is not too long, and that it is not an empty string.
    ''' </summary>
    ''' <param name="name">the username inputed to be sanitized.</param>
    ''' <returns>boolean regarding state of validation</returns>
    Public Function checkLength(name As String) As Boolean
        Select Case name
            Case ""
                Return False
            Case name.Length > 10
                Return False
            Case Else
                Return True
        End Select
    End Function

    ''' <summary>
    '''     storeInput uses a streamwriter to store the username in the highscores.txt file.
    ''' </summary>
    ''' <param name="sanitizedName">the username, after is has been verified to be safe.</param>
    ''' <param name="score">the score of the player to be entered also.</param>
    Public Sub storeInput(sanitizedName As String, score As String)
        'employs csv style, writing to file
        Dim detailsWriter As System.IO.StreamWriter
        detailsWriter = My.Computer.FileSystem.OpenTextFileWriter(Application.StartupPath & "\highScores.txt", True)
        detailsWriter.WriteLine(sanitizedName & "," & score)
        detailsWriter.Close()
    End Sub

End Class