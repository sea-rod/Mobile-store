Imports MySql.Data.MySqlClient

Public Class Form3
    Dim connString As String = "server=localhost;user=root;password=$$$sea11$$$;database=mobile_store"
    Dim conn As New MySqlConnection(connString)

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn.Open()
    End Sub


    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        If e.ColumnIndex = 0 And e.RowIndex >= 0 Then
            Label1.Text = DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            Dim cmd As New MySqlCommand("SELECT * FROM mobile where barcode=" + Label1.Text, conn)

            Dim reader As MySqlDataReader = cmd.ExecuteReader
            Try
                If reader.HasRows Then
                    reader.Read()


                    Dim barcode As String = reader("barcode")
                    Dim model As String = reader("model")
                    Dim price As Double = reader("price")
                    Dim company As String = reader("company")

                    reader.Close()


                    Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    row.Cells("barcode").Value = barcode
                    row.Cells("model").Value = model
                    row.Cells("price").Value = price
                    row.Cells("company").Value = company
                Else
                    Label1.Text = "g"
                    reader.Close()

                End If


            Catch ex As Exception
                Label1.Text = "ffff"
                conn.Close()
                reader.Close()

            End Try
        End If


        If e.ColumnIndex = 4 And e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            If row.Cells("price").Value Then
                row.Cells("amt").Value = row.Cells("price").Value * row.Cells("quantity").Value
            End If
        End If


    End Sub

    Private Sub Form3_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        conn.Close()
        End
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim sum As Integer
        For Each row As DataGridViewRow In DataGridView1.Rows
            sum += row.Cells("amt").Value
        Next
        Dim i As Integer = DataGridView1.Rows.Add()

        DataGridView1.Rows(i).Cells("quantity").Value = "total"
        DataGridView1.Rows(i).Cells("amt").Value = sum
    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Hide()
        Form2.Show()
    End Sub
End Class