Imports clsApex
Imports clsDatabaseHelper
Imports clsMain
Imports System.Data
Partial Class Checklisthistory
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If len(Request.QueryString("lid")) > 0 Then
            Dim listid As String = Request.QueryString("lid")
            If listid <> "" Then
                Response.Write(returnhistory(listid))
                Response.End()
            End If

        End If
    End Sub
    Private Function returnhistory(ByVal id As String) As String
        Dim tempstr As String = ""
        Dim sql As String = ""
        Dim innertext As String = ""
        sql &= "Select convert(varchar(20),chklist.Statuson,105) as statuson ,statu="
        sql &= "case when chklist.Status=2 then 'Running' else 'Completed' end ,"
        sql &= "(isnull(ud.firstname,'')+' '+isnull(ud.lastname,'')) as insertedby, chklist.Remark"
        sql &= " from [APEX_ChecklistStatus] as chklist"
        sql &= " join   APEX_UsersDetails as ud on chklist.InsertedBY=ud.userdetailsID"
        sql &= " where chklist.isActive='Y' and chklist.IsDeleted='N' and chklist.RefChecklistID is not NULL  and chklist.REfchecklistID=" & id & " "
        Dim ds As DataSet = Nothing
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            innertext &= "<table border='1' style='width:500px;border-collapse:collapse;'>"
            innertext &= "<tr style='color:White;background-color:#333333;'><th>Status Date</th><th>Status</th><th>Status Set by</th><th>Remark</th></tr>"
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                innertext &= "<tr>"
                innertext &= "<td>" & ds.Tables(0).Rows(i)(0) & " </td>"
                innertext &= "<td>" & ds.Tables(0).Rows(i)(1) & " </td>"
                innertext &= "<td>" & ds.Tables(0).Rows(i)(2) & " </td>"
                innertext &= "<td>" & ds.Tables(0).Rows(i)(3) & " </td>"
                innertext &= "</tr>"
            Next
            innertext &= "</table>"
        End If
        tempstr = innertext
        Return tempstr
    End Function
End Class
