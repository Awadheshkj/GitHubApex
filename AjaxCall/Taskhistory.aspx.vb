Imports clsApex
Imports clsDatabaseHelper
Imports clsMain
Imports System.Data
Partial Class Checklisthistory
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Len(Request.QueryString("lid")) > 0 Then
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
        'sql &= "Select convert(varchar(20),chklist.Statuson,105) as statuson ,statu="
        'sql &= "case when chklist.Status=2 then 'Running' else 'Completed' end ,"
        'sql &= "(isnull(ud.firstname,'')+' '+isnull(ud.lastname,'')) as insertedby, chklist.Remark"
        'sql &= " from [APEX_ChecklistStatus] as chklist"
        'sql &= " join   APEX_UsersDetails as ud on chklist.InsertedBY=ud.userdetailsID"
        'sql &= " where chklist.isActive='Y' and chklist.IsDeleted='N' and chklist.RefChecklistID is not NULL  and chklist.REfchecklistID=" & id & " "
        'sql &= "select convert(date,Insertedon,103)statuson,status as statu,Incharge as insertedby,Remarks as Remark from [dbo].[Apex_TaskHistory]  where reftaskID=" & id & ""
        sql &= "select Insertedon as statuson,status as statu,Incharge as insertedby,Remarks as Remark,workstatus,CheckListID,"
        sql &= " (case when (select isnull(count(ID),0) from Apex_TaskCollaterals where isdeleted='N' and reftaskhistoryID=CheckListID)=0 then 'N/A' else "
        sql &= " convert(varchar(10),CheckListID)   end)cnt from [dbo].[Apex_TaskHistory]  where reftaskID=" & id
        Dim ds As DataSet = Nothing
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            innertext &= "<div style='height:300px; overflow:auto; '>"
            innertext &= "<table border='1' style='width:580px; border-collapse:collapse; '>"
            innertext &= "<tr style='color:White;background-color:#333333;'><th  width='100px'>Status Date</th><th  width='80px'>Status</th><th  width='100px'>Status by</th><th width='220px'>Remark</th><th  width='50px'>Doc</th></tr>"
            '<th  width='50px'>Workstatus(%)</th>
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                innertext &= "<tr>"
                innertext &= "<td>" & ds.Tables(0).Rows(i)(0) & " </td>"
                innertext &= "<td>" & ds.Tables(0).Rows(i)(1) & "</br> " & ds.Tables(0).Rows(i)(4) & " %  </td>"
                innertext &= "<td>" & ds.Tables(0).Rows(i)(2) & " </td>"
                innertext &= "<td>" & ds.Tables(0).Rows(i)(3) & " </td>"
                If ds.Tables(0).Rows(i)("cnt") = "N/A" Then
                    innertext &= "<td>" & ds.Tables(0).Rows(i)("cnt") & " </td>"
                Else
                    innertext &= "<td><a href='Taskcollatralsview.aspx?THid=" & ds.Tables(0).Rows(i)("CheckListID") & "' target='_blank'>View</a></td>"
                End If
                innertext &= "</tr>"
            Next
            innertext &= "</table>"
            innertext &= "</div>"
        Else
            innertext &= "Data Not Available..."
        End If
        tempstr = innertext
        Return tempstr
    End Function
End Class
