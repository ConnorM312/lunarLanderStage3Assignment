Public Class TitleScreenFrm

    Private Sub PlayBtn_Click(sender As Object, e As EventArgs) Handles PlayBtn.Click
        GameFrm.Show()
    End Sub

    Private Sub LeaderBoardBtn_Click(sender As Object, e As EventArgs) Handles LeaderBoardBtn.Click
        LeaderBoardFrm.Show()
    End Sub
End Class