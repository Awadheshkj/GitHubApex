Imports clsMain
Imports clsApex
Imports clsDatabaseHelper
Imports System.Data


Partial Class Projects
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Response.Write(FillJobDetails(Request.Form("PTYPE")))
        Response.End()
    End Sub

    Private Function FillJobDetails(ByVal ptype As String) As String
        Dim jsonstring As String = ""
        Dim capex As New clsApex

        Dim sql As String = " Select JobCardID as ProjectID"
        'sql &= " ,isnull(JobCardNo,'N/A') as JobCardNo"
        'sql &= " ,BriefID"
        sql &= " ,JobCardName as ProjectName"
        'sql &= " ,Client"
        sql &= " ,activity.ProjectType"
        sql &= " ,isnull((l.FirstName + ' ' + isnull(l.LastName,'')),'N/A') as ProjectManager   "
        sql &= " ,(Select isnull((FirstName + ' ' + isnull(LastName,'')),'N/A') from Apex_Login where RefUserDetailsID =b.InsertedBy) as KAM "
        sql &= " ,JobStatus = case when (JobCompleted='N' or JobCompleted is null) and JobCardNo is null then 'Pending' "
        sql &= "  when (JobCompleted='N' or JobCompleted is null) and JobCardNo is not null then 'Running'"
        sql &= "  else 'Completed' end"
        'sql &= " ,PostPnLStatus = case when ISPostPnl is null or IspostPnl='N' then 'Pending' when IsPostPnL = 'Y' then 'Generated' else 'N/A' end "
        'sql &= " ,isnull(j.isinvoiceing,'N')isinvoiceing "
        sql &= " from APEX_JobCard as j   "
        sql &= " Inner join APEX_Brief as b on b.BriefID=j.RefBriefID    "
        sql &= " Inner  join  APEX_Login as l on j.projectManagerID=l.refUserDetailsID"
        sql &= " Inner join APEX_ActivityType as activity on j.primaryActivityID=activity.ProjectTypeID"
        sql &= " Inner Join APEX_Clients as c on c.ClientID = j.RefClientID "
        sql &= " Left Outer Join APEX_PostPnL as t on j.JobCardID = t.RefJobCardID"

        sql &= "   where b.IsActive='Y' and b.IsDeleted='N' and j.IsEstimated='Y' and JobCardNo is not Null"

        If ptype = "Pending" Then
            sql &= " and (j.JobCompleted ='N' or j.JobCompleted is null) "
        Else
            sql &= " and  j.JobCompleted ='Y'"
        End If


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
