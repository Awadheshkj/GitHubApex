Imports System
Imports System.Data
Imports clsApex
Imports clsDatabaseHelper
Imports clsMain

Partial Class AjaxCalls_AX_Jobs
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim result As String = ""
        If Len(Request.QueryString("call")) > 0 Then
            If Request.QueryString("call").ToString() <> "" Then
                Dim callid As String = Request.QueryString("call").ToString()
                If callid = "1" Then
                    result = FillJobDetails()
                ElseIf callid = "2" Then
                    Dim jid As String = Request.QueryString("jid").ToString()
                    result = UpdateJobDetails(jid)
                ElseIf callid = "7" Then
                    result = Fillinvoicedata()

                End If

            End If
        End If
        Response.Write(result)
        Response.End()
    End Sub

    Private Function FillJobDetails() As String
        Dim jsonstring As String = ""
        Dim capex As New clsApex

        Dim sql As String = " Select  ROW_NUMBER() OVER(ORDER BY BriefID DESC) AS Row,JobCardID"
        sql &= ",isnull(JobCardNo,'N/A') as JobCardNo"
        sql &= ",BriefID,JobCardName,Client,activity.ProjectType"
        sql &= ",isnull((l.FirstName + ' ' + isnull(l.LastName,'')),'N/A') as ProjectManager   "
        sql &= ",JobStatus = case when (JobCompleted='N' or JobCompleted is null) and JobCardNo is null then 'Pending' "
        sql &= "  when (JobCompleted='N' or JobCompleted is null) and JobCardNo is not null then 'Running'"
        sql &= "  else 'Completed' end"
        sql &= " ,PostPnLStatus = case when ISPostPnl is null or IspostPnl='N' then 'Pending' when IsPostPnL = 'Y' then 'Generated' else 'N/A' end "
        sql &= " ,isnull(j.isinvoiceing,'N')isinvoiceing,(case when b.insertedBy=" & getLoggedUserID() & " then 'Y' else 'N' end)ismyproject "
        sql &= " from APEX_JobCard as j   "
        sql &= " Inner join APEX_Brief as b on b.BriefID=j.RefBriefID    "
        sql &= " Inner  join  APEX_Login as l on j.projectManagerID=l.refUserDetailsID"
        sql &= " Inner join APEX_ActivityType as activity on j.primaryActivityID=activity.ProjectTypeID"
        sql &= " Inner Join APEX_Clients as c on c.ClientID = j.RefClientID "
        sql &= " Left Outer Join APEX_PostPnL as t on j.JobCardID = t.RefJobCardID"

        Dim Searchtype As String = ""
        Dim ddlorder As String = ""
        Dim txtsearch As String = ""
        Dim ddlstatus As String = ""
        If Len(Request.QueryString("Searchtype")) > 0 Then
            If Request.QueryString("Searchtype").ToString() <> "" Then
                Searchtype = Request.QueryString("Searchtype").ToString().Replace("undefined", 0)
            End If
        End If
        If Len(Request.QueryString("ddlorder")) > 0 Then
            If Request.QueryString("ddlorder").ToString() <> "" Then
                ddlorder = Request.QueryString("ddlorder").ToString().Replace("undefined", "")
            End If
        End If
        If Len(Request.QueryString("txtsearch")) > 0 Then
            If Request.QueryString("txtsearch").ToString() <> "" Then
                txtsearch = Request.QueryString("txtsearch").ToString().Replace("undefined", "")
            End If
        End If
        If Len(Request.QueryString("ddlstatus")) > 0 Then
            If Request.QueryString("ddlstatus").ToString() <> "" Then
                ddlstatus = Request.QueryString("ddlstatus").ToString().Replace("undefined", "N")
            End If
        End If

        Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())
        If role <> "A" And role <> "H" Then
            Dim jid As String = capex.GetJobCardsIDnew(getLoggedUserID)
            If jid <> "" Then
                sql &= "   where b.IsActive='Y' and b.IsDeleted='N' and j.IsEstimated='Y' and JobCardID in(" & jid & ")"
            Else
                sql &= "   where b.IsActive='Y' and b.IsDeleted='N' and j.IsEstimated='Y' and JobCardID in(0)"
            End If
        Else
            sql &= "   where b.IsActive='Y' and b.IsDeleted='N' and j.IsEstimated='Y'"

        End If
        'If Searchtype = "JobCompleted" Then
        '    sql &= " and isnull(JobCompleted,'N')= '" & ddlstatus & "'"

        'Else
        '    If txtsearch <> "" Then
        '        sql &= "and " & Searchtype & " like '%" & txtsearch & "%' "
        '    End If
        '    If Searchtype <> "0" Then
        '        sql &= " Order by " & Searchtype & " " & ddlorder
        '    End If
        'End If
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

    Private Function UpdateJobDetails(ByVal jid As String) As String
        Dim result As String = ""
        Dim sql As String = ""
        sql = "Update APEX_jobcard set JobCompleted=NULL where JobCardID=" & jid
        If ExecuteNonQuery(sql) > 0 Then
            result = "done"
        End If
        Return result
    End Function

    Private Function Fillinvoicedata() As String
        Dim capex As New clsApex
        ' Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())

        Dim jsonstring As String = ""
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim sql As String = " "
        sql &= " select JOBINVOICEID,RefMinTempEstimateID,RefMaxTempEstimateID,RefJobCardID,GrandTotal  from [dbo].[APEX_JobInvoice] where RefJobCardID='" & Request.QueryString("jc") & "' "
        sql &= " "
        sql &= " "


        'If role <> "A" And role <> "H" Then
        '    sql &= " where l.IsActive = 'Y' and l.IsDeleted = 'N' and l.insertedBy=" & getLoggedUserID() & "   order by l.leadid Desc  "
        'Else
        '    sql &= " where l.IsActive = 'Y' and l.IsDeleted = 'N'    order by l.leadid Desc  "
        'End If

        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt)
            Return jsonstring

        End If

        Return jsonstring
    End Function

End Class
