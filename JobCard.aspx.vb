Imports clsMain
Imports System.Data
Imports clsDatabaseHelper
Imports clsApex

Partial Class JobCard
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'FillJobCard()
        End If
    End Sub

    'Private Sub FillJobCard()
    '    Dim capex As New clsApex

    '    Dim sql As String = " Select  ROW_NUMBER() OVER(ORDER BY BriefID DESC) AS Row,JobCardID"
    '    sql &= ",isnull(JobCardNo,'N/A') as JobCardNo"
    '    sql &= ",BriefID,JobCardName,Client,activity.ProjectType"
    '    sql &= ",isnull((l.FirstName+ isnull(l.LastName,'')),'N/A') as ProjectManager   "
    '    sql &= ",JobStatus = case when (JobCompleted='N' or JobCompleted is null) and JobCardNo is null then 'Pending' "
    '    sql &= "  when (JobCompleted='N' or JobCompleted is null) and JobCardNo is not null then 'Running'"
    '    sql &= "  else 'Completed' end"
    '    sql &= " ,PostPnLStatus = case when ISPostPnl is null or IspostPnl='N' then 'Pending' when IsPostPnL = 'Y' then 'Generated' else 'N/A' end "
    '    sql &= " from APEX_JobCard as j   "
    '    sql &= " Inner join APEX_Brief as b on b.BriefID=j.RefBriefID    "
    '    sql &= " Inner  join  APEX_Login as l on j.projectManagerID=l.refUserDetailsID"
    '    sql &= " Inner join APEX_ActivityType as activity on j.primaryActivityID=activity.ProjectTypeID"
    '    sql &= " Inner Join APEX_Clients as c on c.ClientID = j.RefClientID "
    '    sql &= " Left Outer Join APEX_PostPnL as t on j.JobCardID = t.RefJobCardID"

    '    Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())
    '    If role <> "A" And role <> "H" Then
    '        Dim jid As String = capex.GetJobCardsIDnew(getLoggedUserID)
    '        If jid <> "" Then
    '            sql &= "   where b.IsActive='Y' and b.IsDeleted='N' and j.IsEstimated='Y' and JobCardID in(" & jid & ")"
    '        Else
    '            sql &= "   where b.IsActive='Y' and b.IsDeleted='N' and j.IsEstimated='Y' and JobCardID in(0)"
    '        End If
    '    Else
    '        sql &= "   where b.IsActive='Y' and b.IsDeleted='N' and j.IsEstimated='Y'"
    '    End If
    '    Dim ds As New DataSet
    '    ds = ExecuteDataSet(sql)
    '    If ds.Tables(0).Rows.Count > 0 Then
    '        gvJobCard.DataSource = ds
    '        gvJobCard.DataBind()
    '        lbl_PendingNotfound.Visible = False

    '    Else
    '        lbl_PendingNotfound.Visible = True


    '    End If
    'End Sub

    'Protected Sub gvJobCard_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvJobCard.PageIndexChanging
    '    gvJobCard.PageIndex = e.NewPageIndex
    '    If ddlJobStatus.SelectedIndex > 0 Then
    '        Dim capex As New clsApex

    '        Dim sql As String = " Select  ROW_NUMBER() OVER(ORDER BY BriefID DESC) AS Row,JobCardID"
    '        sql &= ",isnull(JobCardNo,'N/A') as JobCardNo"
    '        sql &= ",BriefID,JobCardName,Client,activity.ProjectType"
    '        sql &= ",isnull((l.FirstName+ isnull(l.LastName,'')),'N/A') as ProjectManager   "
    '        sql &= ",JobStatus = case when (JobCompleted='N' or JobCompleted is null) and JobCardNo is null then 'Pending' "
    '        sql &= "  when (JobCompleted='N' or JobCompleted is null) and JobCardNo is not null then 'Running'"
    '        sql &= "  else 'Completed' end"
    '        sql &= " ,PostPnLStatus = case when ISPostPnl is null or IspostPnl='N' then 'Pending' when IsPostPnL = 'Y' then 'Generated' else 'N/A' end "
    '        sql &= " from APEX_JobCard as j   "
    '        sql &= " Inner join APEX_Brief as b on b.BriefID=j.RefBriefID    "
    '        sql &= " Inner  join  dbo.APEX_Login as l on j.projectManagerID=l.refUserDetailsID"
    '        sql &= " Inner join APEX_ActivityType as activity on j.primaryActivityID=activity.ProjectTypeID"
    '        sql &= " Inner Join APEX_Clients as c on c.ClientID = j.RefClientID "

    '        Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())

    '        If role <> "A" And role <> "H" Then
    '            Dim jid As String = capex.GetJobCardsIDnew(getLoggedUserID)
    '            If jid <> "" Then
    '                If ddlJobStatus.SelectedItem.Text = "Pending" Then
    '                    sql &= "   where b.IsActive='Y' and b.IsDeleted='N' and j.IsEstimated='Y' and (JobCompleted='N' or JobCompleted is null) and JobCardNo is null and JobCardID in(" & jid & ")"
    '                ElseIf ddlJobStatus.SelectedItem.Text = "Running" Then
    '                    sql &= "   where b.IsActive='Y' and b.IsDeleted='N' and j.IsEstimated='Y' and (JobCompleted='N' or JobCompleted is null) and JobCardNo is not null and JobCardID in(" & jid & ")"
    '                Else
    '                    sql &= "   where b.IsActive='Y' and b.IsDeleted='N' and j.IsEstimated='Y' and JobCompleted='Y' and JobCardID in(" & jid & ")"
    '                End If
    '            Else
    '                If ddlJobStatus.SelectedItem.Text = "Pending" Then
    '                    sql &= "   where b.IsActive='Y' and b.IsDeleted='N' and j.IsEstimated='Y' and (JobCompleted='N' or JobCompleted is null) and JobCardNo is null and JobCardID in(0)"
    '                ElseIf ddlJobStatus.SelectedItem.Text = "Running" Then
    '                    sql &= "   where b.IsActive='Y' and b.IsDeleted='N' and j.IsEstimated='Y' and (JobCompleted='N' or JobCompleted is null) and JobCardNo is not null and JobCardID in(0)"
    '                Else
    '                    sql &= "   where b.IsActive='Y' and b.IsDeleted='N' and j.IsEstimated='Y' and JobCompleted='Y' and JobCardID in(0)"
    '                End If
    '            End If
    '        Else
    '            If ddlJobStatus.SelectedItem.Text = "Pending" Then
    '                sql &= "   where b.IsActive='Y' and b.IsDeleted='N' and j.IsEstimated='Y' and (JobCompleted='N' or JobCompleted is null) and JobCardNo is null"
    '            ElseIf ddlJobStatus.SelectedItem.Text = "Running" Then
    '                sql &= "   where b.IsActive='Y' and b.IsDeleted='N' and j.IsEstimated='Y' and (JobCompleted='N' or JobCompleted is null) and JobCardNo is not null"
    '            Else
    '                sql &= "   where b.IsActive='Y' and b.IsDeleted='N' and j.IsEstimated='Y' and JobCompleted='Y'"
    '            End If
    '        End If
    '        Dim ds As New DataSet
    '        ds = ExecuteDataSet(sql)
    '        If ds.Tables(0).Rows.Count > 0 Then
    '            gvJobCard.DataSource = ds
    '            gvJobCard.DataBind()
    '            lbl_PendingNotfound.Visible = False

    '        Else
    '            lbl_PendingNotfound.Visible = True


    '        End If
    '    Else
    '        FillJobCard()
    '    End If
    'End Sub

    'Protected Sub ddlJobStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJobStatus.SelectedIndexChanged


    '    If ddlJobStatus.SelectedIndex > 0 Then
    '        Dim capex As New clsApex

    '        Dim sql As String = " Select  ROW_NUMBER() OVER(ORDER BY BriefID DESC) AS Row,JobCardID"
    '        sql &= ",isnull(JobCardNo,'N/A') as JobCardNo"
    '        sql &= ",BriefID,JobCardName,Client,activity.ProjectType"
    '        sql &= ",isnull((l.FirstName+ isnull(l.LastName,'')),'N/A') as ProjectManager   "
    '        sql &= ",JobStatus = case when (JobCompleted='N' or JobCompleted is null) and JobCardNo is null then 'Pending' "
    '        sql &= "  when (JobCompleted='N' or JobCompleted is null) and JobCardNo is not null then 'Running'"
    '        sql &= "  else 'Completed' end"
    '        sql &= " ,PostPnLStatus = case when ISPostPnl is null or IspostPnl='N' then 'Pending' when IsPostPnL = 'Y' then 'Generated' else 'N/A' end "
    '        sql &= " from APEX_JobCard as j   "
    '        sql &= " Inner join APEX_Brief as b on b.BriefID=j.RefBriefID    "
    '        sql &= " Inner  join  dbo.APEX_Login as l on j.projectManagerID=l.refUserDetailsID"
    '        sql &= " Inner join APEX_ActivityType as activity on j.primaryActivityID=activity.ProjectTypeID"
    '        sql &= " Inner Join APEX_Clients as c on c.ClientID = j.RefClientID "

    '        Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())

    '        If role <> "A" And role <> "H" Then
    '            Dim jid As String = capex.GetJobCardsIDnew(getLoggedUserID)
    '            If jid <> "" Then
    '                If ddlJobStatus.SelectedItem.Text = "Pending" Then
    '                    sql &= "   where b.IsActive='Y' and b.IsDeleted='N' and j.IsEstimated='Y' and (JobCompleted='N' or JobCompleted is null) and JobCardNo is null and JobCardID in(" & jid & ")"
    '                ElseIf ddlJobStatus.SelectedItem.Text = "Running" Then
    '                    sql &= "   where b.IsActive='Y' and b.IsDeleted='N' and j.IsEstimated='Y' and (JobCompleted='N' or JobCompleted is null) and JobCardNo is not null and JobCardID in(" & jid & ")"
    '                Else
    '                    sql &= "   where b.IsActive='Y' and b.IsDeleted='N' and j.IsEstimated='Y' and JobCompleted='Y' and JobCardID in(" & jid & ")"
    '                End If
    '            Else
    '                If ddlJobStatus.SelectedItem.Text = "Pending" Then
    '                    sql &= "   where b.IsActive='Y' and b.IsDeleted='N' and j.IsEstimated='Y' and (JobCompleted='N' or JobCompleted is null) and JobCardNo is null and JobCardID in(0)"
    '                ElseIf ddlJobStatus.SelectedItem.Text = "Running" Then
    '                    sql &= "   where b.IsActive='Y' and b.IsDeleted='N' and j.IsEstimated='Y' and (JobCompleted='N' or JobCompleted is null) and JobCardNo is not null and JobCardID in(0)"
    '                Else
    '                    sql &= "   where b.IsActive='Y' and b.IsDeleted='N' and j.IsEstimated='Y' and JobCompleted='Y' and JobCardID in(0)"
    '                End If
    '            End If
    '        Else
    '            If ddlJobStatus.SelectedItem.Text = "Pending" Then
    '                sql &= "   where b.IsActive='Y' and b.IsDeleted='N' and j.IsEstimated='Y' and (JobCompleted='N' or JobCompleted is null) and JobCardNo is null"
    '            ElseIf ddlJobStatus.SelectedItem.Text = "Running" Then
    '                sql &= "   where b.IsActive='Y' and b.IsDeleted='N' and j.IsEstimated='Y' and (JobCompleted='N' or JobCompleted is null) and JobCardNo is not null"
    '            Else
    '                sql &= "   where b.IsActive='Y' and b.IsDeleted='N' and j.IsEstimated='Y' and JobCompleted='Y'"
    '            End If
    '        End If
    '        Dim ds As New DataSet
    '        ds = ExecuteDataSet(sql)
    '        If ds.Tables(0).Rows.Count > 0 Then
    '            gvJobCard.DataSource = ds
    '            gvJobCard.DataBind()
    '            lbl_PendingNotfound.Visible = False

    '        Else
    '            lbl_PendingNotfound.Visible = True


    '        End If
    '    Else
    '        FillJobCard()
    '    End If
    'End Sub

    'Private Function OpenNotification() As DataSet
    '    Dim ds As New DataSet

    '    Dim sql As String = "select NotificationTitle,NotificationMessage,convert(varchar(10),StartDate,105) as NotificationDate,Status = case when IsExecuted='Y' then 'Closed' else 'Open' End"
    '    sql &= " from APEX_Recieptents as rc"
    '    sql &= " inner join APEX_Notifications as nf on rc.RefNotificationID = nf.NotificationID"
    '    sql &= " where UserID=" & getLoggedUserID() & " and (IsExecuted='N' or IsExecuted is null)"
    '    sql &= " Order By StartDate desc"
    '    ds = ExecuteDataSet(sql)
    '    Return ds
    'End Function

    'Private Function CloseNotification() As DataSet
    '    Dim ds As New DataSet

    '    Dim sql As String = "select NotificationTitle,NotificationMessage,convert(varchar(10),StartDate,105) as NotificationDate,Status = case when IsExecuted='Y' then 'Closed' else 'Open' End"
    '    sql &= " from APEX_Recieptents as rc"
    '    sql &= " inner join APEX_Notifications as nf on rc.RefNotificationID = nf.NotificationID"
    '    sql &= " where UserID=" & getLoggedUserID() & " and IsExecuted='Y'"
    '    sql &= " Order By StartDate desc"
    '    ds = ExecuteDataSet(sql)
    '    Return ds
    'End Function
End Class