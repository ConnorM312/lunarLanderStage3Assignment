Public Class TitleScreenFrm
    ''' <summary>
    '''     Activates the game when the player presses the play button.
    ''' </summary>
    '''<param name="sender">The object that sent the event.</param>
    ''' <param name="e">The object providing information about the event.</param>
    Private Sub PlayBtn_Click(sender As Object, e As EventArgs) Handles PlayBtn.Click
        GameFrm.Show()
    End Sub



    ''' <summary>
    '''     Summons the leaderboard upon the user pressing the button.
    ''' </summary>
    ''' <param name="sender">The object that sent the event.</param>
    ''' <param name="e">The object providing information about the event.</param>
    Private Sub LeaderBoardBtn_Click(sender As Object, e As EventArgs) Handles LeaderBoardBtn.Click
        LeaderBoardFrm.Show()
    End Sub
End Class