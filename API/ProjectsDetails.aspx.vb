Imports clsMain
Imports clsApex
Imports clsDatabaseHelper
Imports System.Data


Partial Class ProjectsDetails
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Response.Write(FillJobDetails(Request.Form("JID")))
        Response.End()
    End Sub

    Private Function FillJobDetails(ByVal JID As String) As String
        Dim jsonstring As String = ""
        Dim capex As New clsApex

        Dim sql As String = ""

        sql &= " select AccountID as TaskID,"
        sql &= " RefjobcardID,"
        sql &= " jc.JobcardName,"
        sql &= " TaskName,Particulars as SubTask,jc.ActivitystartDate "
        sql &= " ,Description, category"
        sql &= " ,StartDate,EndDate"
        sql &= " ,(isnull(ud.firstName,'')+' '+isnull(ud.LastName,'')) as 'PM Name'   "
        sql &= " ,Status,Remarks,TaskCompleted"
        sql &= " from APEX_TaskAccount ta"
        sql &= " left outer join APEX_UsersDetails as ud on  ud.UserDetailsID=ta.TL"
        sql &= " left outer join APEX_Jobcard as jc on jc.JobcardID = ta.RefjobcardID "
        sql &= " where RefjobcardID= " & JID
        sql &= " order by Category"
        sql &= ""


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
