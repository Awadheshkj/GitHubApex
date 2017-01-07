Imports clsMain
Imports clsApex
Imports clsDatabaseHelper
Imports System.Data

Partial Class NewsDetails
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Response.Write(FillJobDetails(Request.Form("NID"), Request.Form("TYPE")))
        Response.End()
    End Sub

    Private Function FillJobDetails(ByVal NID As String, ByVal Type As String) As String
        Dim jsonstring As String = ""
        Dim capex As New clsApex

        Dim sql As String = ""

        sql &= " Select NEWSID, Title, Description, NewsDate, ImageURL "
        sql &= " , InsertedOn from [dbo].[Apex_News]"
        sql &= " where NEWSID=" & NID & " and type = '" & Type & "'"


        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        Dim dt As New DataTable
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt).Replace(Chr(13), "").Replace(Chr(10), "")

            Return jsonstring

        End If

        Return jsonstring
    End Function


End Class
