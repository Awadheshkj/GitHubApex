Imports clsDatabaseHelper
Imports clsMain
Imports System.Data
Partial Class password
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Getpassword()
        End If

    End Sub

    Private Sub Getpassword()
        Try
            Dim sql As String = ""
            Dim email As String = ""
            Dim pass As String = ""
            Dim epass As String = ""
            Dim epass1 As String = ""
            Dim ds As DataSet
            Dim memID As String = ""

            sql &= "Select username,password from APEX_Login order by username "
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    epass = ds.Tables(0).Rows(i)("password")
                    memID = ds.Tables(0).Rows(i)("username")
                    epass1 = epass.Remove(0, 1)
                    pass = Decrypt(epass1)
                    ds.Tables(0).Rows(i).Item("password") = pass
                Next
                GridView1.DataSource = ds
                GridView1.DataBind()
            Else

            End If
            'Else
            'lblPassword.Text = "Your username is incorrect!"
            'End If
        Catch ex As Exception

        End Try
    End Sub

End Class
