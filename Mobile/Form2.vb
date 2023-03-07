Imports MySql.Data.MySqlClient

Public Class Form2
    Dim connString As String = "server=localhost;user=root;password=$$$sea11$$$;database=mobile_store"
    Dim conn As New MySqlConnection(connString)
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn.Open()
        load_data()

    End Sub

    Private Sub load_data()
        Dim cmd As New MySqlCommand("SELECT * FROM mobile", conn)
        Dim dt As New DataTable()
        Dim da As New MySqlDataAdapter(cmd)
        da.Fill(dt)
        DataGridView1.DataSource = dt
    End Sub

    Private Sub cls(sender As Object, e As EventArgs) Handles MyBase.Closed
        conn.Close()
        End
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim q As String

        Try

            q = "insert into mobile(model,price,company) values ('" + model.Text + "'," + price.Text + ",'" + company.Text + "');"

            Dim cmd As New MySqlCommand(q, conn)
            Dim i As Integer = cmd.ExecuteNonQuery()

            If i <> 0 Then
                status.Text = "Added successfully"
                status.BackColor = Color.Green
            Else
                status.Text = "Error Ocurred"
                status.BackColor = Color.DarkRed
            End If
            load_data()

            model.Clear()
            price.Clear()
            company.Clear()
        Catch ex As Exception
            status.Text = ex.Message
            status.BackColor = Color.DarkRed
        End Try


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim q As String
        Try
            q = "delete from mobile where barcode = " + bcd.Text
            Dim cmd As New MySqlCommand(q, conn)
            Dim i As Integer = cmd.ExecuteNonQuery()

            If i <> 0 Then
                status.Text = "Deleted successfully"
                status.BackColor = Color.Green
            Else
                status.Text = "Error Ocurred"
                status.BackColor = Color.DarkRed
            End If


            load_data()
            bcd.Clear()

        Catch ex As Exception
            status.BackColor = Color.DarkRed
            status.Text = ex.Message

        End Try


    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Hide()
        Form3.Show()
    End Sub
End Class