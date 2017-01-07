Imports QueryStringModule

Partial Class AjaxCalls_DecriptQuery
    Inherits System.Web.UI.Page

    Private Sub form1_Init(sender As Object, e As EventArgs) Handles form1.Init

    End Sub

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Dim qstr As String = ""
        If Len(Request.QueryString("enc")) > 0 Then
            qstr = QueryStringModule.Decrypt(Request.QueryString("enc"))
        End If
        Response.Write(qstr)
        Response.End()
    End Sub
End Class
