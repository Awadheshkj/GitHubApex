Imports clsMain
Imports clsApex
Imports clsDatabaseHelper
Imports System.Data

Partial Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Len(Request.Form("U")) > 0 And Len(Request.Form("P")) > 0 Then
            Authenticate(Request.Form("U"), Request.Form("P"))
        End If
    End Sub

    Private Sub Authenticate(ByVal _USR As String, ByVal _PASS As String)
        Dim capex As New clsApex
        Dim RetCode As String = requestAccess(_USR, _PASS)

        Select Case RetCode
            Case _ReturnCodes.AccessDenied
                Response.Write(gendata("false", -1))
            Case _ReturnCodes.AccountInActive
                Response.Write(gendata("false", -1))
            Case _ReturnCodes.LoginNotAllowed
                Response.Write(gendata("false", -1))
            Case _ReturnCodes.InValidPassword
                Response.Write(gendata("false", -1))
            Case _ReturnCodes.InValidUsername
                Response.Write(gendata("false", -1))
            Case _ReturnCodes.AccessGranted
                'Response.Write("Access Granted")
                Response.Write(gendata("true", getLoggedUserID()))
        End Select
        Response.End()
    End Sub

    Private Function gendata(ByVal login As String, ByVal id As Integer) As String
        Dim table As New DataTable
        table.TableName = "Table1"


        ' Create four typed columns in the DataTable.
        table.Columns.Add("login", GetType(String))
        table.Columns.Add("id", GetType(Integer))

        ' Add five rows with those columns filled in the DataTable.
        table.Rows.Add(login, id)
        Dim jsonstring As String = ""

        jsonstring = GetJSONString(table).Replace(Chr(13), "").Replace(Chr(10), "")

        Return jsonstring

    End Function
End Class
