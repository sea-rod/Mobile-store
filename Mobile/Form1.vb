
Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If usr.Text.Equals("admin") And passwd.Text.Equals("admin") Then
            Me.Hide()
            Form3.Show()

        Else
            MessageBox.Show("Check your username and password and try agian", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub
End Class
