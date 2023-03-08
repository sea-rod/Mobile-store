Imports System.Diagnostics.Eventing
Imports MySql.Data.MySqlClient

Public Class Form3
    Dim flag As Boolean = True
    Dim connString As String = "server=localhost;user=root;password=$$$sea11$$$;database=mobile_store"
    Dim conn As New MySqlConnection(connString)

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn.Open()
    End Sub


    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        If e.ColumnIndex = 0 And e.RowIndex >= 0 Then
            Dim brc As String = DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            Dim cmd As New MySqlCommand("SELECT * FROM mobile where barcode=@brc", conn)
            cmd.Parameters.AddWithValue("@brc", brc)


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
                    MsgBox("Barcode does not exist", MsgBoxStyle.Critical, "Error")
                    reader.Close()

                End If


            Catch ex As Exception
                conn.Close()
                reader.Close()

            End Try
        End If

        Try

            If e.ColumnIndex = 4 And e.RowIndex >= 0 Then
                Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                If row.Cells("price").Value Then
                    row.Cells("amt").Value = row.Cells("price").Value * row.Cells("quantity").Value
                End If
            End If
        Catch ex As InvalidCastException

            MsgBox("The quantity value should be a integer", MsgBoxStyle.Critical, "Error")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")

        End Try


    End Sub

    Private Sub Form3_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        conn.Close()
        End
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If flag And DataGridView1.Rows.Count() > 1 Then
            Dim sum As Integer = 0
            For Each row As DataGridViewRow In DataGridView1.Rows
                sum += row.Cells("amt").Value
            Next
            Dim i As Integer = DataGridView1.Rows.Add()

            DataGridView1.Rows(i).Cells("quantity").Value = "total"
            DataGridView1.Rows(i).Cells("amt").Value = sum

            flag = False
            DataGridView1.ReadOnly = True
        Else
            MsgBox("Clear the table or enter some value in the table", MsgBoxStyle.Critical, "Error")
        End If
    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Hide()
        Form2.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DataGridView1.Rows.Clear()
        flag = True
        DataGridView1.ReadOnly = False
    End Sub
End Class