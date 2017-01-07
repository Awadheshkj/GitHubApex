Imports System.Data
Imports clsDatabaseHelper
Imports clsMain
Imports clsApex

Partial Class AjaxCalls_Dashboard
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim result As String = ""
        Dim capex As New clsApex
        If Len(Request.QueryString("call")) > 0 Then
            If Request.QueryString("call").ToString() <> "" Then
                Dim callid As String = Request.QueryString("call").ToString()
                'Dim cid As String = Request.QueryString("id").ToString()


                If callid = "1" Then
                    result = FillDetailsFM1()
                ElseIf callid = "2" Then
                    result = FillDetailsFM2()
                ElseIf callid = "3" Then
                    result = FillDetailsFM3()
                ElseIf callid = "4" Then
                    result = capex.GetRoleNameByUserID(getLoggedUserID).ToString()
                ElseIf callid = "5" Then
                    result = FillDetailsPM1()
                ElseIf callid = "6" Then
                    result = FillDetailPM2()
                ElseIf callid = "7" Then
                    result = FillDetailsPM3()
                ElseIf callid = "8" Then
                    result = FillDetailsPM4()
                ElseIf callid = "9" Then
                    result = FillDetailsPM5()
                ElseIf callid = "10" Then
                    result = fillDetailsKAM2()
                ElseIf callid = "11" Then
                    result = fillDetailsKAM3()
                ElseIf callid = "12" Then
                    result = fillDetailsKAM4()
                ElseIf callid = "13" Then
                    result = fillDetailsKAM1()
                    'Reports
                ElseIf callid = "14" Then
                    result = fillDetailsREport()
                ElseIf callid = "15" Then
                    result = fillDetailsClientWise()
                ElseIf callid = "16" Then
                    result = fillDetailsKAMWise()
                ElseIf callid = "17" Then
                    result = fillDetailsCatigoryWise()
                ElseIf callid = "18" Then
                    result = fillRM()
                ElseIf callid = "19" Then
                    result = fillRMReport()
                    'For Task Report
                ElseIf callid = "20" Then
                    result = fillTaskReport()
                ElseIf callid = "21" Then
                    result = FillDetailsFM3Invoicing()

                ElseIf callid = "22" Then
                    result = FillJobCounts()

                ElseIf callid = "23" Then
                    result = FillPieChart()
                ElseIf callid = "24" Then
                    result = FillBubbleChart()
                ElseIf callid = "25" Then
                    result = FillNews()
                ElseIf callid = "26" Then
                    result = FillJobCountsForPM()
                ElseIf callid = "27" Then
                    If Len(Request.QueryString("jc")) > 0 Then
                        result = fillassignbudgetsummary(Request.QueryString("jc"))
                    End If
                ElseIf callid = "28" Then
                    If Len(Request.QueryString("jc")) > 0 Then
                        result = fillassignbudgetsummaryOnClaimSummary(Request.QueryString("jc"))
                    End If
                End If

                Response.Write(result)
                Response.End()
            End If

        End If
    End Sub

    Private Function FillDetailsFM1() As String
        Dim jsonstring As String = ""
        Dim capex As New clsApex
        Dim sql As String = " Select distinct jc.JobCardID,ISNULL(JobCardNo,'N/A') as JobCardNo"
        sql &= " ,JobCardName,IsTask  = case when JobCardNo is null then 'N/A' else 'Manage' End "
        sql &= "  ,isnull((l.FirstName+ isnull(l.LastName,'')),'N/A') as ProjectManager  ,activity.ProjectType "
        sql &= " , link = case when n.type = 11 then  'JobCardManager.aspx?mode=edit&jid='+ convert(varchar(10),jc.JobCardID) "
        sql &= "   + '&nid=' + convert(varchar(10),NotificationID) else 'N/A' end  from APEX_JobCard as jc"
        sql &= " left outer  join  dbo.APEX_Login as l on jc.projectManagerID=l.refUserDetailsID"
        sql &= "  left outer join APEX_ActivityType as activity on jc.primaryActivityID=activity.ProjectTypeID"
        sql &= " left outer join  APEX_Notifications as n on jc.JobCardID=n.AssociateID "
        sql &= "  where jc.IsActive='Y' and jc.IsDeleted='N' and IsEstimated='Y' and IsPrePnL='Y' and isNull(ISclientapprovalverified,'N')='N'  and n.type = 11 and jobcardno Like '%KIMS%' order by Jobcardid desc"
        'and (JobCompleted = 'N')
        Dim ds As New DataSet
        Dim dt As New DataTable
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt)
        End If
        Return jsonstring
    End Function

    Private Function FillDetailsFM2() As String
        Dim jsonstring As String = ""
        Dim capex As New clsApex
        Dim sql As String = " Select distinct jc.RefClientID,jc.JobCardID,ISNULL(JobCardNo,'N/A') as JobCardNo"
        sql &= " ,JobCardName,IsTask  = case when JobCardNo is null then 'N/A' else 'Manage' End "
        sql &= "  ,isnull((l.FirstName+ isnull(l.LastName,'')),'N/A') as ProjectManager  ,activity.ProjectType "
        sql &= " from APEX_JobCard as jc"
        sql &= "  inner join APEX_MappingProjectStage as mp on jc.JobCardID  = mp.RefJobCardID "
        sql &= " left outer  join  dbo.APEX_Login as l on jc.projectManagerID=l.refUserDetailsID"
        sql &= "  left outer join APEX_ActivityType as activity on jc.primaryActivityID=activity.ProjectTypeID"

        Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())
        'and (JobCompleted is null or JobCompleted = 'N') 
        sql &= "  where jc.IsActive='Y' and jc.IsDeleted='N' and IsEstimated='Y' and IsPrePnL='Y'  and mp.StageLevel > 4 and JobcardNo is not null and jobcardno Not Like '%KIMS%' order by Jobcardid desc"

        Dim ds As New DataSet
        Dim dt As New DataTable
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt)
        End If
        Return jsonstring
    End Function

    Private Function FillDetailsFM3() As String
        Dim jsonstring As String = ""
        Dim capex As New clsApex
        'Dim sql As String = " Select jc.JobCardID,JobCardNo as JobCardNo "
        'sql &= " ,JobCardName"
        'sql &= " ,isnull((l.FirstName+ isnull(l.LastName,'')),'N/A') as ProjectManager"
        'sql &= " ,activity.ProjectType"
        'sql &= " from APEX_JobCard as jc"
        'sql &= " Inner join  APEX_UsersDetails as l on jc.projectManagerID=l.UserDetailsID "
        'sql &= " Inner join APEX_ActivityType as activity on jc.primaryActivityID=activity.ProjectTypeID "
        'sql &= " Inner join APEX_PostPnL as pl on jc.JobCardID=pl.RefJobCardID"
        'sql &= " where jc.IsActive='Y' and jc.IsDeleted='N' and JobCompleted = 'Y' and ISBranchHeadApproved='Y' and IsInvoiceing='P'  order by pl.Insertedon desc "
        Dim sql As String = " "
        sql &= "  Select RefMinTempEstimateID,RefMaxTempEstimateID,jc.JobCardID,JobCardNo as JobCardNo  ,JobCardName ,"
        sql &= "  isnull((l.FirstName+ isnull(l.LastName,'')),'N/A') as ProjectManager ,"
        sql &= "  activity.ProjectType,IsInvoiceing from APEX_JobCard as jc"
        sql &= "  Inner join  APEX_UsersDetails as l  on jc.projectManagerID=l.UserDetailsID  "
        sql &= "  Inner join APEX_ActivityType as activity on   jc.primaryActivityID=activity.ProjectTypeID "
        sql &= "  Inner join APEX_PostPnL as pl   on jc.JobCardID=pl.RefJobCardID "
        sql &= "  inner join APEX_JobInvoice jinv on jc.JobCardID=jinv.RefjobcardID"
        sql &= "  where jc.IsActive='Y' and jc.IsDeleted='N' "
        ' sql &= "  and ISBranchHeadApproved='Y'  and IsInvoiceing='P'   order by pl.Insertedon desc "
        sql &= "  and ISBranchHeadApproved='Y' and jinv.isinvoice='N'  order by pl.Insertedon desc "
        sql &= ""
        sql &= ""

        Dim ds As New DataSet
        Dim dt As New DataTable
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt)
        End If
        Return jsonstring
    End Function

    Private Function FillDetailsFM3Invoicing() As String
        Dim jsonstring As String = ""
        Dim capex As New clsApex
        Dim sql As String = " "
        sql &= "  Select RefMinTempEstimateID,RefMaxTempEstimateID,jc.JobCardID,JobCardNo as JobCardNo  ,JobCardName ,"
        sql &= "  isnull((l.FirstName+ isnull(l.LastName,'')),'N/A') as ProjectManager ,"
        sql &= "  activity.ProjectType,IsInvoiceing from APEX_JobCard as jc"
        sql &= "  Inner join  APEX_UsersDetails as l  on jc.projectManagerID=l.UserDetailsID  "
        sql &= "  Inner join APEX_ActivityType as activity on   jc.primaryActivityID=activity.ProjectTypeID "
        sql &= "  Inner join APEX_PostPnL as pl   on jc.JobCardID=pl.RefJobCardID "
        sql &= "  inner join APEX_JobInvoice jinv on jc.JobCardID=jinv.RefjobcardID"
        sql &= "  where jc.IsActive='Y' and jc.IsDeleted='N' "
        'sql &= "  and ISBranchHeadApproved='Y' and IsInvoiceing='Y'  order by pl.Insertedon desc "
        sql &= "  and ISBranchHeadApproved='Y' and jinv.isinvoice='Y'   order by pl.Insertedon desc "
        sql &= ""
        sql &= ""

        Dim ds As New DataSet
        Dim dt As New DataTable
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt)
        End If
        Return jsonstring
    End Function
    Private Function FillDetailsPM1() As String
        Dim jsonstring As String = ""
        Dim capex As New clsApex
        Dim sql As String = ""
        sql = " Select Distinct  b.BriefName,jc.JobcardID,isnull(convert(varchar(20), b.Activitydate,103),'N/A') as Activitydate ,"
        sql &= "  link=case when type =7 and rec.IsExecuted='N' and (jc.JobCompleted ='N' or jc.JobCompleted is null) then 'PrePnlManager.aspx?mode=edit&bid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID) + '&kid=y' when jc.isPrePnL='Y' and (jc.JobCompleted ='N' or jc.JobCompleted is null) then 'OpenPrePnlManager.aspx?mode=edit&bid=' + convert(varchar(10),BriefID) + '&kid=y' else 'PrePnlManager.aspx?mode=edit&bid=' + convert(varchar(10),BriefID) + '&kid=y' end, linkname= case when jc.IsPrePnL='Y' then 'Generated' else 'Pending' end "
        sql &= " ,ProjectType,jc.INsertedOn,"
        sql &= " isnull(jc.JobcardNo,'N/A')JobcardNo,"
        sql &= "(select isnull(FirstName,'') + ' ' + isnull(LastName,'')   from APEX_UsersDetails as ud  Inner join APEX_Brief as ab on ud.UserDetailsID = ab.InsertedBy where ab.BriefID = b.BriefID) as KAMName"
        sql &= " from  APEX_JobCard as jc"
        sql &= " Inner join APEX_Notifications as nt on jc.RefBriefID=nt.AssociateID"
        sql &= " left outer join APEX_ActivityType  as At on Jc.PrimaryActivityID=At.ProjectTypeID"
        sql &= " join dbo.APEX_Recieptents as rec on nt.notificationID=rec.refnotificationID"
        sql &= " inner join APEX_Brief as b on jc.RefBriefID = b.BriefID"
        sql &= "  where ProjectManagerID=" & getLoggedUserID() & " and rec.isseen='N' order by jc.INsertedOn desc"
        Dim ds As New DataSet
        Dim dt As New DataTable
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt)
        End If
        Return jsonstring
    End Function

    Private Function FillDetailPM2() As String
        Dim jsonstring As String = ""
        Dim sql As String = " Select distinct jc.RefClientID,jc.JobCardID,ISNULL(JobCardNo,'N/A') as JobCardNo"
        sql &= " ,JobCardName,IsTask  = case when JobCardNo is null then 'N/A' else 'Manage' End "
        sql &= "  ,isnull((l.FirstName+' '+isnull(l.LastName,'')),'N/A') as ProjectManager  ,activity.ProjectType,isnull(JobCompleted,'N')JobCompleted "
        sql &= " from APEX_JobCard as jc"
        sql &= " join APEX_Brief as br on jc.refBriefID=br.BriefID "
        sql &= "  inner join APEX_MappingProjectStage as mp on jc.JobCardID  = mp.RefJobCardID "
        sql &= " left outer  join  dbo.APEX_Login as l on jc.projectManagerID=l.refUserDetailsID"
        sql &= "  left outer join APEX_ActivityType as activity on jc.primaryActivityID=activity.ProjectTypeID"
        sql &= "  where jc.IsActive='Y' and jc.IsDeleted='N' and jc.IsEstimated='Y' and jc.IsPrePnL='Y'  and mp.StageLevel > 4 and jc.JobcardNo is not null and jc.projectManagerID=" & getLoggedUserID() & " order by Jobcardid desc"
        'sql &= "  where jc.IsActive='Y' and jc.IsDeleted='N' and (JobCompleted is null or JobCompleted = 'N') and jc.IsEstimated='Y' and jc.IsPrePnL='Y'  and mp.StageLevel > 4 and jc.JobcardNo is not null and jc.projectManagerID=" & getLoggedUserID() & " order by Jobcardid desc"
        Dim ds As New DataSet
        Dim dt As New DataTable
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt)
        End If
        Return jsonstring
    End Function

    Public Function FillDetailsPM3() As String
        Dim jsonstring As String = ""
        Dim sql As String = "Select  distinct " & getLoggedUserID() & " as TL,ta.InsertedOn,  ta.AccountID,t.Title,convert(varchar(10),t.StartDate,105) as  startdate, "
        sql &= " convert(varchar(10),t.enddate,105) as enddate,ta.Particulars, ta.Quantity, ta.Amount, "
        sql &= "  ta.Total, TaskCompleted =case when ta.TaskCompleted='Y' then 'Completed' else 'Pending' end , CategoryTask,RefJobCardID,(select jobcardNo from apex_jobcard where jobcardID=RefJobCardID)jobcardNo"
        sql &= " from APEX_Task  as t"
        sql &= " join APEX_TaskAccount as ta on t.taskID=ta.ReftaskID"
        sql &= " left outer join APEX_TaskTeam as tm on ta.AccountID=tm.REfTaskID"
        sql &= " where ( ta.TL=" & getLoggedUserID() & " or tm.REfMemberID=" & getLoggedUserID() & " ) order by ta.InsertedOn desc"
        Dim ds As New DataSet
        Dim dt As New DataTable
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt)
        End If
        Return jsonstring
    End Function

    Private Function FillDetailsPM4() As String
        Dim jsonstring As String = ""

        Dim sql As String = " "
        sql &= " Select JobCardID,cm.ClaimMasterID,ClaimStatus  = case when IsApproved is null then 'Pending' "
        sql &= "   when  IsApproved ='Y' then 'Approved' else  'Rejected' End ,"
        sql &= " t.title,jc.JobCardNo, sum(cl.Amount) as Amount from APEX_ClaimMaster as cm"
        sql &= " join APEX_TaskAccount as ta on cm.RefTaskID=Ta.AccountID"
        sql &= " join APEX_Task as t on ta.refTaskID=t.taskid"
        sql &= " join APEX_JobCard as jc on t.RefJobcardID=jc.JobCardID"
        sql &= " JOIN APEX_ClaimTransaction AS cl on cm.ClaimMasterID=cl.RefClaimID"
        sql &= " where SubmittedUserID=" & getLoggedUserID() & " group by JobCardID,IsApproved,t.title,jc.JobCardNo,cl.Amount,cm.ClaimMasterID"

        Dim ds As New DataSet
        Dim dt As New DataTable
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt)
        End If
        Return jsonstring
    End Function

    Private Function FillDetailsPM5() As String
        Dim jsonstring As String = ""

        Dim sql As String = "Select JobCardID,jc.JobCardName,isnull(convert(varchar(20), jc.ActivityEndDate,103),'N/A') as Activitydate"
        sql &= " ,IsPostPnL as PostPnl"
        '= Case when IsPostPnL = 'Y' then 'Generated' else 'Prepare' end"
        sql &= ",ProjectType "
        sql &= " from  APEX_JobCard as jc "
        sql &= " Inner join APEX_ActivityType  as At on Jc.PrimaryActivityID=At.ProjectTypeID "
        sql &= " Left Outer Join APEX_PostPnL as pl on jc.JobCardID = pl.RefJobCardID"
        sql &= " where JobCompleted='Y' and  ProjectManagerID=" & getLoggedUserID()

        Dim ds As New DataSet
        Dim dt As New DataTable
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt)
        End If
        Return jsonstring
    End Function


    Private Function fillDetailsKAM2() As String
        Dim jsonstring As String = ""

        Dim sql As String = "select j.jobcardno as JobCardNo,CategoryTask as Category,Title as Task,Description,Specifications,c.Quantity,(Select isnull(FirstName,'') + isnull(LastName,'') from APEX_UsersDetails where UserDetailsID=Incharge) as Incharge,(Select isnull(FirstName,'') + isnull(LastName,'') from APEX_UsersDetails where UserDetailsID= m.RefMemberID) as Member,convert(varchar(10),ETD,105) as ETD"
        sql &= ",Status = case when Completed = 3 then 'Completed' else 'Running' end"
        sql &= ",VendorName as Vendor,ContactName,c.Remarks "
        sql &= "from APEX_Task as t"
        sql &= " inner join APEX_TaskAccount as a on t.TaskID = a.RefTaskID"
        sql &= " inner join APEX_Checklist as c on a.AccountID = c.RefTaskID"
        sql &= " Left Outer join APEX_TaskTeam as m on t.TaskID = m.RefTaskID"
        sql &= " Left Outer join APEX_Vendor as v on t.RefVendor = v.VendorID"
        sql &= " Left Outer join APEX_VendorContacts as vc on vc.VendorContactID = t.RefVendorContactPerson"
        sql &= " Inner Join APEX_JobCard as j on j.JobCardID = t.RefJobCardID "
        sql &= " Inner Join APEX_Brief as b on b.BriefID = j.RefBriefID"

        Dim capex As New clsApex
        Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())
        If role = "K" Then
            sql &= " where b.InsertedBy=" & getLoggedUserID()
        End If

        Dim ds As New DataSet
        Dim dt As New DataTable
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt)
        End If
        Return jsonstring
    End Function


    Private Function fillDetailsKAM4() As String
        Dim jsonstring As String = ""

        Dim sql As String = ""
        sql &= " Select jobcardID,JobCardNo,Client,isnull(ud.FirstName,'') + ' '+ isnull(ud.LastName,'') as KAM"
        sql &= ",isnull(pud.FirstName,'') + ' '+ isnull(pud.LastName,'') as ProjectManager ,ProjectType,JobCompleted=case when JobCompleted='Y' then 'Closed' else 'running' end ,JobCardName"
        sql &= " ,isnull(sum(temppost.PostEventCost),0) as postpnltotal ,est.EstimatedSubTotal "
        sql &= " ,(isnull(est.EstimatedSubTotal,0) -isnull(sum(temppost.PostEventCost),0)) as remaining "
        sql &= " ,(isnull(est.EstimatedSubTotal,0) -isnull(sum(temppost.PostEventCost),0))/isnull(sum(temppost.PostEventCost),0)*100 as 'porofit'"
        sql &= "  from  APEX_jobcard  as jc"
        sql &= "  Inner Join APEX_Clients as cl on jc.RefClientID = cl.ClientID "
        sql &= " Inner Join APEX_Brief  as br on jc.RefBriefID  = br.BriefID  "
        sql &= " Inner Join APEX_UsersDetails as ud on br.InsertedBy   = ud.UserDetailsID"
        sql &= " Inner Join APEX_UsersDetails as pud on jc.projectManagerID = pud.UserDetailsID"
        sql &= "  Inner Join APEX_ActivityType as at on jc.PrimaryActivityID = at.ProjectTypeID   "
        sql &= "  inner join  APEX_Estimate as est on est.RefBriefID =jc.RefbriefId       "
        sql &= "  join   APEX_PostPnL  as post on jc.JobCardID =post .RefJobCardID "
        sql &= "  join APEX_temppostPnlCost as temppost on post.PostPnLID =temppost.RefPostPnLID "
        sql &= "  where jc.IsActive='Y' and jc.IsDeleted='N'  and jc.IsEstimated='Y' and jc.IsPrePnL='Y' "

        Dim capex As New clsApex
        Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())
        If role = "K" Then
            sql &= " and br.InsertedBy=" & getLoggedUserID()
        End If

        sql &= "  group by jobcardID,jobcardNo,Client, ProjectType,JobCompleted ,JobCardName,br.InsertedBy "
        sql &= " ,temppost.PostEventCost ,est.EstimatedSubTotal,ud.FirstName,ud.LastName,pud.FirstName ,pud.LastName order by Jobcardid desc"

        Dim ds As New DataSet
        Dim dt As New DataTable
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt)
        End If
        Return jsonstring
    End Function

    Private Function fillDetailsKAM3() As String
        Dim jsonstring As String = ""

        Dim sql As String = ""
        sql &= " select getdate() as Date,JobCardNo,Client,isnull(FirstName,'') + ' ' + isnull(LastName,'') as KAM,JobCardName"
        sql &= " ,ProjectType,convert(varchar(10),ActivityStartDate,105) as StartDate,"
        sql &= " convert(varchar(10),ActivityEndDate,105) as EndDate,"
        sql &= " JobStatus = case when JobCompleted = 'Y' then 'Closed' Else 'Open' End,"
        sql &= " (isnull(EstimatedSubTotal,0)-( isnull(sum(postcots.PostEventCost),0) "
        sql &= " )) as NetRevenue,( isnull(sum(postcots.PostEventCost),0)) as TotalCost, "
        sql &= " ( isnull(EstimatedSubTotal,0)-( isnull(sum(postcots.PostEventCost),0) ))"
        sql &= " as Profit,  (isnull(EstimatedSubTotal,0)-(isnull(sum(postcots.PostEventCost),0)))/(isnull(EstimatedSubTotal,0)) * 100 as ProfitPercent"
        sql &= "  , isnull(EstimatedSubTotal,0) as InternalBudget, p.PreProfitPercent as PrePnLPer,t.PostProfitPercent as PostPnLPer "
        sql &= "   from APEX_JobCard as j inner join APEX_Clients as c on j.RefClientID =c.ClientID "
        sql &= "   inner join APEX_Brief as b on j.RefBriefID = b.BriefID "
        sql &= "   inner join APEX_UsersDetails as u on b.InsertedBy = u.UserDetailsID "
        sql &= "   inner join APEX_Estimate as e on e.RefBriefID = b.BriefID "
        sql &= "   inner join APEX_PrePnL as p on p.RefBriefID = b.BriefID "
        sql &= "   Inner Join APEX_PostPnL as t on t.RefJobCardID = j.JobCardID "
        sql &= "   inner join APEX_tempPostPnLCost  as postcots on postcots .RefPostPnLID =t.PostPnLID "
        sql &= "   Inner Join APEX_ActivityType as a on a.ProjectTypeID = j.PrimaryActivityID "

        Dim capex As New clsApex
        Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())
        If role = "K" Then
            sql &= " where b.InsertedBy=" & getLoggedUserID()
        End If
        sql &= "   group by JobCardNo,Client ,FirstName ,LastName ,JobCardName ,ProjectType ,ActivityEndDate ,ActivityStartDate ,JobCompleted "
        sql &= "   ,BriefID ,EstimatedSubTotal,PreProfitPercent ,PostProfitPercent,PostEventCost  "

        Dim ds As New DataSet
        Dim dt As New DataTable
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt)
        End If
        Return jsonstring
    End Function

    Private Function fillDetailsKAM1() As String
        Dim jsonstring As String = ""

        Dim sql As String = "select NotificationTitle,NotificationMessage,convert(varchar(10),StartDate,105) as NotificationDate,Status = case when IsExecuted='Y' then 'Closed' else 'Open' End"
        sql &= " ,Link = case when type = 4 and IsExecuted='N' then 'Estimate.aspx?mode=app&bid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID) +'&Appro=Y'   "
        sql &= " when type = 6 and IsExecuted='N' then 'RembursementClaimApproval.aspx?type=SubTask&nid=' + convert(varchar(10),NotificationID)+ '&aid=' + convert(varchar(10),AssociateID)   "
        sql &= " when type = 5 and IsExecuted='N'  then 'RembursementClaimApproval.aspx?type=Task&nid=' + convert(varchar(10),NotificationID) + '&aid=' + convert(varchar(10),AssociateID)   "
        sql &= " when type = 7 and IsExecuted='N'  then 'PrePnlManager.aspx?mode=edit&bid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)   "
        sql &= " when type = 15 and IsExecuted='N'  then 'Estimate.aspx?mode=edit&bid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)+'&Approved=Y'   "
        sql &= " when type = 8 and IsExecuted='N'  then 'JobCardManager.aspx?mode=edit&jid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)   "
        sql &= " when type = 9 and IsExecuted='N'  then 'Estimate.aspx?mode=edit&bid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)    "
        sql &= " when type = 10 and IsExecuted='N'  then 'rejectednotification.aspx?bid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)   "
        sql &= " when type = 11 and IsExecuted='N'  then 'JobCardManager.aspx?mode=edit&jid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)  "
        sql &= " when type = 13 and IsExecuted='N'  then 'JobCardManager.aspx?mode=edit&jid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)  "
        sql &= " when type = 14 and IsExecuted='N'  then 'JobCardManager.aspx?mode=edit&jid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)  "
        sql &= " when type = 12 and IsExecuted='N'  then 'Checklist.aspx?tid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)   "
        sql &= " when type = 16 and IsExecuted='N'  then 'RembursementClaimApproval.aspx?type=Task&nid=' + convert(varchar(10),NotificationID) + '&aid=' + convert(varchar(10),AssociateID) + '&mode=app' "
        sql &= " when type = 17 and IsExecuted='N'  then 'RembursementClaimApproval.aspx?type=Task&nid=' + convert(varchar(10),NotificationID) + '&aid=' + convert(varchar(10),AssociateID) + '&mode=rej'  "
        sql &= " when type = 18 and IsExecuted='N'  then 'ExcPrePnlManager.aspx?mode=edit&nid=' + convert(varchar(10),NotificationID) + '&aid=' + convert(varchar(10),AssociateID)  "
        sql &= " when type = 19 and IsExecuted='N'  then 'RembursementClaimApproval.aspx?type=Task&nid=' + convert(varchar(10),NotificationID) + '&aid=' + convert(varchar(10),AssociateID)  "
        sql &= " when type = 20 and IsExecuted='N'  then 'viewJobInvoice.aspx?jid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)   "
        sql &= " when type = 22 and IsExecuted='N'  then 'PostPnLManager.aspx?mode=view&jid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)  "
        sql &= " when type = 21 and IsExecuted='N'  then 'PostPnLManager.aspx?jid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID) "
        sql &= " else '' end"
        sql &= " from APEX_Recieptents as rc"
        sql &= " inner join APEX_Notifications as nf on rc.RefNotificationID = nf.NotificationID"
        sql &= " where UserID=" & getLoggedUserID()
        sql &= " Order By StartDate desc"

        Dim ds As New DataSet
        Dim dt As New DataTable
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt)
        End If
        Return jsonstring
    End Function


    Private Function fillDetailsREport() As String
        Dim jsonstring As String = ""

        Dim sql As String = ""

        sql &= "  select getdate() as Date,JobCardNo,Client,isnull(FirstName,'') + ' ' + isnull(LastName,'') as KAM,JobCardName   ,ProjectType,convert(varchar(10),ActivityStartDate,105) as "
        sql &= "   StartDate, 	convert(varchar(10),ActivityEndDate,105) as EndDate,     JobStatus = case when isnull(IsEstimateVsActuals,'N') = 'Y' then 'Closed' Else 'Open' End,   "
        sql &= "    (case when isnull(IsEstimateVsActuals,'N') = 'N' then (isnull(EstimatedSubTotal,0)) else (isnull((select top 1 subtotal  from APEX_TempEstimate where "
        sql &= "	refestimateID=e.estimateID),0)) end)Revenue1,   (case when isnull(IsEstimateVsActuals,'N') = 'N' then (isnull(Estimatedmanagentfees,0)) else "
        sql &= "	 (isnull((select top 1 managementfee  from APEX_TempEstimate where refestimateID=e.estimateID),0)) end)Revenue2 	,(case when isnull(IsEstimateVsActuals,'N') = 'N'  "
        sql &= "	 then (isnull(EstimatedTotal,0)) else (isnull((select top 1 total  from APEX_TempEstimate where refestimateID=e.estimateID),0)) end) NetRevenue, 	 "
        sql &= "	 isnull((select sum(cl.Amount) from APEX_ClaimMaster as cm  	JOIN APEX_ClaimTransaction AS cl on cm.ClaimMasterID=cl.RefClaimID  where jobcardNo=j.jobCardID and"
        sql &= "	  cm.ISapproved='Y'),0.00)as Claims, 	Profit=(case when isnull(IsEstimateVsActuals,'N') = 'N' then (isnull(EstimatedTotal,0)) else isnull((select top 1 total "
        sql &= "	   from APEX_TempEstimate where refestimateID=e.estimateID),0) end)-(isnull((select sum(cl.Amount) from APEX_ClaimMaster as cm  	JOIN APEX_ClaimTransaction AS"
        sql &= "	    cl on cm.ClaimMasterID=cl.RefClaimID  where jobcardNo=j.jobCardID and cm.ISapproved='Y'),0.00)), 	"
        sql &= ""
        sql &= "		Profitperc=cast(ROUND((((((case when isnull(IsEstimateVsActuals,'N')= 'N' then (isnull(EstimatedTotal,0)) else "
        sql &= "		isnull((select top 1 total  from APEX_TempEstimate where refestimateID=e.estimateID),0) end))-(isnull((select sum(cl.Amount) from APEX_ClaimMaster as cm  	"
        sql &= "		JOIN APEX_ClaimTransaction AS cl on cm.ClaimMasterID=cl.RefClaimID  where jobcardNo=j.jobCardID and cm.ISapproved='Y'),0.00)))/ 	"
        sql &= "	 (case when isnull(IsEstimateVsActuals,'N') = 'N' then (isnull(EstimatedTotal,0)) when 	 isnull((select top 1 total  from APEX_TempEstimate where refestimateID=e.estimateID)"
        sql &= "	 ,0)=0 then '1' 	 ELSE isnull((select top 1 total  from APEX_TempEstimate where refestimateID=e.estimateID),0) 	  end) 	)*"
        sql &= "	 (case when (case when isnull(IsEstimateVsActuals,'N') = 'N' then (isnull(EstimatedTotal,0)) else 	 isnull((select top 1 total  from APEX_TempEstimate where"
        sql &= "	  refestimateID=e.estimateID),0) end)= 0 then '1'  else '100' end)"
        sql &= "	 ),2) as float)"
        sql &= "	 "
        sql &= "	  	from APEX_JobCard as j  	 "
        sql &= "		   inner join APEX_Clients as c on j.RefClientID =c.ClientID   	 "
        sql &= "		   inner join APEX_Brief as b on j.RefBriefID =  b.BriefID       "
        sql &= "		   inner join APEX_UsersDetails as u on b.InsertedBy = u.UserDetailsID   	 "
        sql &= "		   Inner Join APEX_ActivityType as a on a.ProjectTypeID = j.PrimaryActivityID  "
        sql &= "		   inner join APEX_Estimate as e on e.RefBriefID = b.BriefID  "
        sql &= ""
        sql &= "	"
        sql &= " "
        sql &= " "
        sql &= " "
        sql &= " "
        sql &= " "
        sql &= " "

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

        Dim capex As New clsApex
        Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())
        If role = "K" Then
            'and e.EstimatedGrandTotal is not NUll
            sql &= " where b.InsertedBy=" & getLoggedUserID() & " and e.EstimatedGrandTotal is not NUll and j.JobCardNo is not NUll "
            If Searchtype <> "0" Then
                If Searchtype = "Status" Then
                    sql &= " and isnull(JobCompleted,'N')= '" & ddlstatus & "'"

                Else
                    'If txtsearch <> "" Then
                    '    sql &= " and " & Searchtype & " like '%" & txtsearch & "%' "
                    'End If
                    If txtsearch <> "" Then

                        If Searchtype = "profitpercentage" Then
                            sql &= " and   (cast(ROUND((((((case when JobCompleted = 'N' then  "
                            sql &= "(isnull(EstimatedTotal,0)) else isnull((select top 1 total  from APEX_TempEstimate where refestimateID=e.estimateID),0) "
                            sql &= "end))-(isnull((select sum(cl.Amount) from APEX_ClaimMaster as cm  	JOIN APEX_ClaimTransaction AS cl on "
                            sql &= "cm.ClaimMasterID=cl.RefClaimID  where jobcardNo=j.jobCardID and cm.ISapproved='Y'),0.00)))/ 	 "
                            sql &= "(case when JobCompleted = 'N' then (isnull(EstimatedTotal,0)) when 	 isnull((select top 1 total   "
                            sql &= "from APEX_TempEstimate where refestimateID=e.estimateID),0)=0 then '1' 	 ELSE isnull((select top 1 total   "
                            sql &= "from APEX_TempEstimate where refestimateID=e.estimateID),0) 	  end) 	)*(case when JobCompleted = 'N' "
                            sql &= "then (isnull(EstimatedTotal,0)) when 	 isnull((select top 1 total  from APEX_TempEstimate where  "
                            sql &= "refestimateID=e.estimateID),0)=0 then '1' 	 else '100' end 	 )),2) as float)) "
                            sql &= " =" & txtsearch & ""
                        Else
                            sql &= "and " & Searchtype & " like '%" & txtsearch & "%' "
                        End If

                    End If
                    If Searchtype = "profitpercentage" Then
                        sql &= " Order by  "
                        sql &= "(cast(ROUND((((((case when JobCompleted = 'N' then  "
                        sql &= "(isnull(EstimatedTotal,0)) else isnull((select top 1 total  from APEX_TempEstimate where refestimateID=e.estimateID),0) "
                        sql &= "end))-(isnull((select sum(cl.Amount) from APEX_ClaimMaster as cm  	JOIN APEX_ClaimTransaction AS cl on "
                        sql &= "cm.ClaimMasterID=cl.RefClaimID  where jobcardNo=j.jobCardID and cm.ISapproved='Y'),0.00)))/ 	 "
                        sql &= "(case when JobCompleted = 'N' then (isnull(EstimatedTotal,0)) when 	 isnull((select top 1 total   "
                        sql &= "from APEX_TempEstimate where refestimateID=e.estimateID),0)=0 then '1' 	 ELSE isnull((select top 1 total   "
                        sql &= "from APEX_TempEstimate where refestimateID=e.estimateID),0) 	  end) 	)*(case when JobCompleted = 'N' "
                        sql &= "then (isnull(EstimatedTotal,0)) when 	 isnull((select top 1 total  from APEX_TempEstimate where  "
                        sql &= "refestimateID=e.estimateID),0)=0 then '1' 	 else '100' end 	 )),2) as float)) "
                        sql &= "" & ddlorder
                    Else
                        sql &= " Order by " & Searchtype & " " & ddlorder
                    End If


                End If
            End If
        Else

            'Searchtype ddlorder txtsearch ddlstatus
            sql &= " where  e.EstimatedGrandTotal is not NUll and j.JobCardNo is not NUll "
            If Searchtype <> "0" Then
                If Searchtype = "Status" Then

                    sql &= "and isnull(JobCompleted,'N')= '" & ddlstatus & "' and e.EstimatedGrandTotal is not NUll"
                Else

                    If txtsearch <> "" Then

                        If Searchtype = "profitpercentage" Then
                            sql &= " and   (cast(ROUND((((((case when JobCompleted = 'N' then  "
                            sql &= "(isnull(EstimatedTotal,0)) else isnull((select top 1 total  from APEX_TempEstimate where refestimateID=e.estimateID),0) "
                            sql &= "end))-(isnull((select sum(cl.Amount) from APEX_ClaimMaster as cm  	JOIN APEX_ClaimTransaction AS cl on "
                            sql &= "cm.ClaimMasterID=cl.RefClaimID  where jobcardNo=j.jobCardID and cm.ISapproved='Y'),0.00)))/ 	 "
                            sql &= "(case when JobCompleted = 'N' then (isnull(EstimatedTotal,0)) when 	 isnull((select top 1 total   "
                            sql &= "from APEX_TempEstimate where refestimateID=e.estimateID),0)=0 then '1' 	 ELSE isnull((select top 1 total   "
                            sql &= "from APEX_TempEstimate where refestimateID=e.estimateID),0) 	  end) 	)*(case when JobCompleted = 'N' "
                            sql &= "then (isnull(EstimatedTotal,0)) when 	 isnull((select top 1 total  from APEX_TempEstimate where  "
                            sql &= "refestimateID=e.estimateID),0)=0 then '1' 	 else '100' end 	 )),2) as float)) "
                            sql &= " =" & txtsearch & ""
                        Else
                            sql &= "and " & Searchtype & " like '%" & txtsearch & "%' "
                        End If

                    End If

                    If Searchtype = "profitpercentage" Then
                        sql &= " Order by  "
                        sql &= "(cast(ROUND((((((case when JobCompleted = 'N' then  "
                        sql &= "(isnull(EstimatedTotal,0)) else isnull((select top 1 total  from APEX_TempEstimate where refestimateID=e.estimateID),0) "
                        sql &= "end))-(isnull((select sum(cl.Amount) from APEX_ClaimMaster as cm  	JOIN APEX_ClaimTransaction AS cl on "
                        sql &= "cm.ClaimMasterID=cl.RefClaimID  where jobcardNo=j.jobCardID and cm.ISapproved='Y'),0.00)))/ 	 "
                        sql &= "(case when JobCompleted = 'N' then (isnull(EstimatedTotal,0)) when 	 isnull((select top 1 total   "
                        sql &= "from APEX_TempEstimate where refestimateID=e.estimateID),0)=0 then '1' 	 ELSE isnull((select top 1 total   "
                        sql &= "from APEX_TempEstimate where refestimateID=e.estimateID),0) 	  end) 	)*(case when JobCompleted = 'N' "
                        sql &= "then (isnull(EstimatedTotal,0)) when 	 isnull((select top 1 total  from APEX_TempEstimate where  "
                        sql &= "refestimateID=e.estimateID),0)=0 then '1' 	 else '100' end 	 )),2) as float)) "
                        sql &= "" & ddlorder
                    Else
                        sql &= " Order by " & Searchtype & " " & ddlorder
                    End If
                    'ElseIf Searchtype = "Client" Then
                    '    If txtsearch <> "" Then
                    '        sql &= "Where Client= " & txtsearch
                    '    End If

                    '    sql &= " Order by Client " & ddlorder
                    'ElseIf Searchtype = "KAM" Then
                    '    If txtsearch <> "" Then
                    '        sql &= "Where Firstname like '%" & txtsearch & "%' "
                    '    End If

                    '    sql &= " Order by Firstname " & ddlorder
                    'ElseIf Searchtype = "KAM" Then
                    '    If txtsearch <> "" Then
                    '        sql &= "Where Firstname like '%" & txtsearch & "%' "
                    '    End If

                    '    sql &= " Order by Firstname " & ddlorder
                End If
            End If

            sql &= " "
            sql &= " "
            sql &= " "
            sql &= " "
            sql &= " "
            sql &= " "
            sql &= " "
            sql &= " "
            sql &= " "
            sql &= " "
            sql &= " "
            sql &= " "
            sql &= " "
            sql &= " "
            sql &= " "
        End If


        Dim ds As New DataSet
        Dim dt As New DataTable
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt)
        End If
        Return jsonstring
    End Function

    Private Function fillDetailsClientWise() As String
        Dim jsonstring As String = ""
        Dim capex As New clsApex
        Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())

        Dim sql As String = ""

        sql &= "select top 10 B.client,sum(b.Revinew)Revinew1,sum(b.Review1) as Rev2,(sum(b.Review1)-sum(b.Revinew)) as Differ,   "
        sql &= "(case when sum(b.Revinew)=0 then '100' else     "
        sql &= "cast(ROUND(((sum(b.Review1)-sum(b.Revinew))/sum(b.Revinew)),2) as float) end) as change     "
        sql &= " from(select a.Client,sum(a.Revinew)Revinew,0 as Review1 from     "
        sql &= "(select Client,(case when isnull(IsEstimateVsActuals,'N') = 'N'  then (isnull(EstimatedTotal,0)) else    "
        sql &= "(select top 1 total from  APEX_TempEstimate where refbriefID=b.briefID ) end)Revinew     "
        sql &= "from APEX_JobCard as j  	 "
        sql &= "		   inner join APEX_Clients as c on j.RefClientID =c.ClientID   	 "
        sql &= "		   inner join APEX_Brief as b on j.RefBriefID =  b.BriefID       "
        sql &= "		   inner join APEX_Estimate as e on e.RefBriefID = b.BriefID  "
        sql &= "		where ClientApprovedDatetime <=   (DATEADD(MM,datediff(month,0,getdate()),0)-1)      "

        'sql &= "		--Kam Name-- and b.InsertedBy=1"
        If role = "K" Then
            sql &= " and b.InsertedBy=" & getLoggedUserID()
        End If

        sql &= "		   ) a"
        sql &= "		group by a.Client   "
        sql &= "		union all    "
        sql &= "select a.Client,0,sum(a.Revinew)Revinew2 from  "
        sql &= ""
        sql &= "(select Client,(case when isnull(IsEstimateVsActuals,'N') = 'N'  then (isnull(EstimatedTotal,0)) else     "
        sql &= "(select top 1 total from  APEX_TempEstimate where refbriefID=b.briefID ) end)Revinew     "
        sql &= "from APEX_JobCard as j  	 "
        sql &= "		   inner join APEX_Clients as c on j.RefClientID =c.ClientID   	 "
        sql &= "		   inner join APEX_Brief as b on j.RefBriefID =  b.BriefID       "
        sql &= "		   inner join APEX_Estimate as e on e.RefBriefID = b.BriefID  "
        sql &= "		where ClientApprovedDatetime<=   Getdate()    "
        'sql &= "		--Kam Name-- and b.InsertedBy=1"
        If role = "K" Then
            sql &= " and b.InsertedBy=" & getLoggedUserID()
        End If
        sql &= "		   ) a "
        sql &= "		group by a.Client   )B "
        sql &= "		group by B.client"
        sql &= "		order by sum(b.Review1) Desc"
        sql &= ""





        Dim ds As New DataSet
        Dim dt As New DataTable
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt)
        End If
        Return jsonstring
    End Function
    Private Function fillDetailsKAMWise() As String
        Dim jsonstring As String = ""
        Dim capex As New clsApex
        Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())

        Dim sql As String = ""

        sql &= "select B.KAM,sum(b.Revinew)Revinew1,sum(b.Review1) as Rev2,(sum(b.Review1)-sum(b.Revinew)) as Differ, "
        sql &= "(case when sum(b.Revinew)=0 then '100' else "
        sql &= "cast(ROUND(((sum(b.Review1)-sum(b.Revinew))/sum(b.Revinew)),2) as float) end) as change "
        sql &= " from "
        sql &= "  (select a.KAM,sum(a.Revinew)Revinew,0 as Review1 from ( "
        sql &= " select isnull(FirstName,'') + ' ' + isnull(LastName,'') as KAM,(case when isnull(IsEstimateVsActuals,'N') = 'N'  then (isnull(EstimatedTotal,0)) else  "
        sql &= " (select top 1 total from  APEX_TempEstimate where refbriefID=b.briefID ) end)Revinew "
        sql &= " from APEX_JobCard as j  "
        sql &= "		   inner join APEX_Brief as b on j.RefBriefID =  b.BriefID	 "
        sql &= "		   inner join APEX_UsersDetails as u on b.InsertedBy = u.UserDetailsID  "
        sql &= "		       "
        sql &= "		   inner join APEX_Estimate as e on e.RefBriefID = b.BriefID  "
        sql &= "		where ClientApprovedDatetime <=   (DATEADD(MM,datediff(month,0,getdate()),0)-1) "
        If role = "K" Then
            sql &= " and b.InsertedBy=" & getLoggedUserID()
        End If

        sql &= "		   ) a"
        sql &= "		group by a.KAM   "
        sql &= ""
        sql &= "		union all"
        sql &= ""
        sql &= " select a.KAM,0,sum(a.Revinew)Revinew2 from  "
        sql &= " (select isnull(FirstName,'') + ' ' + isnull(LastName,'') as KAM,(case when isnull(IsEstimateVsActuals,'N') = 'N'  then (isnull(EstimatedTotal,0)) else "
        sql &= " (select top 1 total from  APEX_TempEstimate where refbriefID=b.briefID ) end)Revinew "
        sql &= " from APEX_JobCard as j  	 "
        sql &= "			inner join APEX_Brief as b on j.RefBriefID =  b.BriefID		"
        sql &= "		   inner join APEX_UsersDetails as u on b.InsertedBy = u.UserDetailsID  	 		"
        sql &= "		   		"
        sql &= "		   inner join APEX_Estimate as e on e.RefBriefID = b.BriefID  		"
        sql &= "		where ClientApprovedDatetime<=   Getdate()"
        If role = "K" Then
            sql &= " and b.InsertedBy=" & getLoggedUserID()
        End If
        sql &= "		   ) a"
        sql &= "		group by a.KAM   )B"
        sql &= "		group by B.KAM"
        sql &= "				"
        sql &= "				"
        sql &= "		"
        sql &= "		"
        sql &= ""




        Dim ds As New DataSet
        Dim dt As New DataTable
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt)
        End If
        Return jsonstring
    End Function

    Private Function fillDetailsCatigoryWise() As String
        Dim jsonstring As String = ""
        Dim capex As New clsApex
        Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())

        Dim sql As String = ""

        'sql &= "		select B.Category,sum(b.Revinew)Revinew1,sum(b.Review1) as Rev2,(sum(b.Review1)-sum(b.Revinew)) as Differ,"
        'sql &= "		(case when sum(b.Revinew)=0 then '100' else"
        'sql &= "		cast(ROUND(((sum(b.Review1)-sum(b.Revinew))/sum(b.Revinew)),2) as float) end) as change"
        'sql &= "		 from"
        'sql &= "		  (select a.Category,sum(a.Revinew)Revinew,0 as Review1 from ("
        'sql &= "		 select APEX_TempEstimate.category,(case when isnull(IsEstimateVsActuals,'N') = 'N'  then (isnull(EstimatedTotal,0)) else "
        'sql &= "		 (select top 1 Sum(Actual+(Actual*Agencyfee/100)) from  APEX_TempEstimate where refbriefID=b.briefID ) end)Revinew"
        'sql &= "		 from APEX_JobCard as j  "
        'sql &= "				   inner join APEX_Brief as b on j.RefBriefID =  b.BriefID	 "
        'sql &= "				   inner join APEX_UsersDetails as u on b.InsertedBy = u.UserDetailsID  "
        'sql &= "				   Inner Join APEX_ActivityType as a on a.ProjectTypeID = j.PrimaryActivityID 	 "
        'sql &= "				   inner join APEX_Estimate as e on e.RefBriefID = b.BriefID  "
        'sql &= "				   inner join APEX_TempEstimate on e.EstimateID =APEX_TempEstimate.RefEstimateID"
        'sql &= "				where ClientApprovedDatetime <=   (DATEADD(MM,datediff(month,0,getdate()),0)-1)"
        'If role = "K" Then
        '    sql &= " and b.InsertedBy=" & getLoggedUserID()
        'End If
        'sql &= "				   ) a"
        'sql &= "				group by a.Category   "
        'sql &= "		"
        'sql &= "				union all"
        'sql &= "		"
        'sql &= "		 select a.Category,0,sum(a.Revinew)Revinew2 from  "
        'sql &= "		 (select APEX_TempEstimate.category,(case when isnull(IsEstimateVsActuals,'N') = 'N'  then (isnull(EstimatedTotal,0)) else "
        'sql &= "		 (select top 1 (Actual+Agencyfee) from  APEX_TempEstimate where refbriefID=b.briefID ) end)Revinew"
        'sql &= "		 from APEX_JobCard as j  	 "
        'sql &= "					inner join APEX_Brief as b on j.RefBriefID =  b.BriefID"
        'sql &= "				   inner join APEX_UsersDetails as u on b.InsertedBy = u.UserDetailsID  	 "
        'sql &= "				   Inner Join APEX_ActivityType as a on a.ProjectTypeID = j.PrimaryActivityID "
        'sql &= "				   inner join APEX_Estimate as e on e.RefBriefID = b.BriefID  "
        'sql &= "				   inner join APEX_TempEstimate on e.EstimateID =APEX_TempEstimate.RefEstimateID"
        'sql &= "				where ClientApprovedDatetime<=   Getdate()"
        'If role = "K" Then
        '    sql &= " and b.InsertedBy=" & getLoggedUserID()
        'End If
        'sql &= "				   ) a"
        'sql &= "				group by a.Category   )B"
        'sql &= "				group by B.Category"
        'sql &= "				"
        sql &= "   select B.Category,cast(sum(b.Revinew) as decimal(18,2))Revinew1,cast(sum(b.Review1)as decimal(18,2)) as Rev2,"
        sql &= "	cast((sum(b.Review1)-sum(b.Revinew))as decimal(18,2)) as Differ,		"
        sql &= "		(case when sum(b.Revinew)=0 then '100' else		cast(ROUND(((sum(b.Review1)-sum(b.Revinew))/sum(b.Revinew)),2) as float) end)"
        sql &= "		 as change from		 		  "
        sql &= "		 (select a.Category,Revinew,0 as Review1 from ("
        sql &= "				  select  category,0 as Revinew1, (case when isnull(IsEstimateVsActuals,'N') = 'N'  then"
        sql &= "				 (isnull((Estimate+(Estimate*Agencyfee/100)),0)) else (Actual+(Actual*Agencyfee/100))end) Revinew from  APEX_TempEstimate TE"
        sql &= "				  inner join APEX_Brief as b on TE.RefBriefID =  b.BriefID"
        sql &= "				  inner join APEX_JobCard as J on TE.RefBriefID =  j.refBriefID	"
        sql &= "				  inner join APEX_Estimate as e on e.RefBriefID = b.BriefID"
        sql &= "				  where ClientApprovedDatetime <=   (DATEADD(MM,datediff(month,0,getdate()),0)-1) 				"
        If role = "K" Then
            sql &= " and b.InsertedBy=" & getLoggedUserID()
        End If
        sql &= "				   and LEFT(JobCardNo, 1)<>'K'  ) a"
        sql &= "			"
        sql &= "				union all	"
        sql &= "				select a.Category,0,Revinew2 from ("
        sql &= "				select  category,(case when isnull(IsEstimateVsActuals,'N') = 'N'  then"
        sql &= "				 (isnull((Estimate+(Estimate*Agencyfee/100)),0)) else (Actual+(Actual*Agencyfee/100))end)Revinew2 from  APEX_TempEstimate TE"
        sql &= "				  inner join APEX_Brief as b on TE.RefBriefID =  b.BriefID	"
        sql &= "				  inner join APEX_JobCard as J on TE.RefBriefID =  j.refBriefID	"
        sql &= "				  inner join APEX_Estimate as e on e.RefBriefID = b.BriefID"
        sql &= "				  where ClientApprovedDatetime<=   Getdate() 			"
        If role = "K" Then
            sql &= " and b.InsertedBy=" & getLoggedUserID()
        End If
        sql &= "				  and LEFT(JobCardNo, 1)<>'K'   ) a"
        sql &= "				  	   )B				group by B.Category		order by  Revinew1	desc"
        sql &= ""




        Dim ds As New DataSet
        Dim dt As New DataTable
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt)
        End If
        Return jsonstring
    End Function

    Private Function fillRM() As String
        Dim jsonstring As String = ""
        Dim capex As New clsApex
        Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())

        Dim sql As String = ""

        sql &= "  Select  JobcardNo,JobCardName,Client,isnull((l.FirstName+ ' ' + isnull(l.LastName,'')),'N/A') as ProjectManager, "
        sql &= "  convert(varchar(20),ActivityDate,5)ActivityDate,"
        sql &= "  isnull((ll.FirstName+ ' ' + isnull(ll.LastName,'')),'N/A') as KAM, "
        sql &= "  EventManager = case when (isnull(ud.firstName,'')+' '+isnull(ud.LastName,'')) <>'' then"
        sql &= "  (isnull(ud.firstName,'')+' '+isnull(ud.LastName,'')) end,CP.ContactName,ta.TL,j.projectManagerID as pmid,b.insertedby as kamid,ta.category   from APEX_Task as t   "
        sql &= "  "
        sql &= "  left join APEX_TaskAccount as ta on t.taskid=ta.refTaskID "
        sql &= "  left outer join APEX_UsersDetails as ud on  ud.UserDetailsID=ta.TL "
        sql &= "  inner join APEX_JobCard as j on j.jobcardID=RefJobcardID"
        sql &= "  Inner join APEX_Brief as b on b.BriefID=j.RefBriefID   "
        sql &= "  Inner Join APEX_Clients as c on c.ClientID = j.RefClientID "
        sql &= "  Inner  join APEX_Login as l on j.projectManagerID=l.refUserDetailsID"
        sql &= "  inner join APEX_Login as ll on b.insertedby=ll.refUserDetailsID"
        sql &= "  inner join APEX_ClientContacts CP on j.ClientContactPerson=CP.ContactID"
        sql &= "   "
        'sql &= "   --RefJobcardID=10  and"
        sql &= "  where isnull((isnull(ud.firstName,'')+' '+isnull(ud.LastName,'')),'') <>''"
        If Len(Request.QueryString("jc")) > 0 Then
            If Request.QueryString("jc").ToString() <> "" Then
                sql &= " and JobcardNo='" & Request.QueryString("jc").ToString() & "'"
            End If
        End If
        sql &= ""
        sql &= "  group by JobcardNo,JobCardName,Client,(case when (isnull(ud.firstName,'')+' '+isnull(ud.LastName,'')) <>'' then"
        sql &= "  (isnull(ud.firstName,'')+' '+isnull(ud.LastName,'')) end),isnull((l.FirstName+ ' ' + isnull(l.LastName,'')),'N/A'),ActivityDate"
        sql &= "  ,isnull((ll.FirstName+ ' ' + isnull(ll.LastName,'')),'N/A'),CP.ContactName,ta.TL,j.projectManagerID,b.insertedby,ta.category "
        sql &= ""
        Dim ds As New DataSet
        Dim dt As New DataTable
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt)
        End If
        Return jsonstring
    End Function


    Private Function fillRMReport() As String
        Dim jsonstring As String = ""
        Dim capex As New clsApex
        Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())

        Dim sql As String = ""

        sql &= "	select * "
        sql &= "	from "
        sql &= "	("
        sql &= "	  	select refjcID,Type,Apex_FeedBackHeadings.name ,Apex_FeedBack.Rate"
        sql &= "		,isnull((ud.FirstName+ ' ' + isnull(ud.LastName,'')),'N/A') as Employee"
        sql &= "		,JobCardName,Client, CP.ContactName,convert(varchar(20),ActivityDate,5)ActivityDate	 from Apex_FeedBackHeadings"
        sql &= "		Inner join [Apex_FeedBack] on Apex_FeedBackHeadings.ID=[Apex_FeedBack].RefheadingID	"
        sql &= "		left outer join APEX_UsersDetails as ud on  ud.UserDetailsID=Apex_FeedBack.employeeID"
        sql &= "		inner join APEX_JobCard as j on j.jobcardID=RefJcID  "
        sql &= "		Inner join APEX_Brief as b on b.BriefID=j.RefBriefID  "
        sql &= "		Inner Join APEX_Clients as c on c.ClientID = j.RefClientID "
        sql &= "		inner join APEX_ClientContacts CP on j.ClientContactPerson=CP.ContactID "
        sql &= "		where Apex_FeedBackHeadings.Isactive='Y' and Apex_FeedBackHeadings.Isdeleted='N'		"
        sql &= "	) a"
        sql &= "	pivot"
        sql &= "	("
        sql &= "	  max(Rate)"
        sql &= "	   for [name] in ("
        sql &= "	  [a],"
        sql &= "	[b],"
        sql &= "	[c],"
        sql &= "	[d],"
        sql &= "	[e],"
        sql &= "	[f],"
        sql &= "	[g],"
        sql &= "	[h],"
        sql &= "	[i],"
        sql &= "	[j],"
        sql &= "	[k]"
        sql &= "	  )"
        sql &= "	) piv"
        sql &= "	"

        Dim ds As New DataSet
        Dim dt As New DataTable
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt)
        End If
        Return jsonstring
    End Function

    Private Function fillTaskReport() As String
        Dim jsonstring As String = ""
        Dim capex As New clsApex
        Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())

        Dim sql As String = ""

        sql &= "  Select  Quantity,taskname Title,convert(varchar(10),ta.StartDate,4) as  startdate,RefJobcardID,"
        sql &= "  convert(varchar(10),ta.enddate,4) as enddate,category categoryTask,REPLACE(REPLACE(REPLACE(Cast([Description] as varchar(max)), CHAR(13), ''), CHAR(10), ''), CHAR(9), '') as Description, "
        sql &= "        TaskCompleted =case when ta.TaskCompleted='Y' then 'Completed' else 'Pending' end "
        sql &= "        ,  Name = case when (isnull(ud.firstName,'')+' '+isnull(ud.LastName,'')) <>'' then"
        sql &= "        (isnull(ud.firstName,'')+' '+isnull(ud.LastName,'')) "
        sql &= " 		else '<a href=''TaskAccount.aspx?tid='+convert(varchar(10),ta.refjobcardID) +'''> N/A</a>' end"
        sql &= "        ,Total as Expence,(select (isnull(firstName,'')+' '+isnull(LastName,'')) from APEX_UsersDetails where UserDetailsID=ta.insertedby)"
        sql &= " 		AssignFrom,particulars,AccountID,status,ta.Remarks,ta.insertedby ,( case when workstatus='' then '0' else workstatus end)workstatus,"
        sql &= " 		BRIEF.InsertedBy as KAMID,(isnull(KAMud.firstName,'') + '  ' + isnull(KAMud.LastName,''))KAM ,jc.JobCardNo  "
        sql &= "   ,isnull((select sum(cl.Amount) from APEX_ClaimMaster as cm 		      "
        sql &= " JOIN APEX_ClaimTransaction AS cl on cm.ClaimMasterID=cl.RefClaimID    where reftaskID=ta.AccountID),0.00)as Claims"
        sql &= "        from "
        sql &= "        APEX_TaskAccount as ta "
        sql &= "        left outer join APEX_UsersDetails as ud on  ud.UserDetailsID=ta.TL"
        sql &= " 		inner join apex_jobcard jc on jc.JobcardID= ta.RefJobcardID"
        sql &= " 		inner join Apex_brief BRIEF on jc.RefBriefID=BRIEF.BriefID"
        sql &= " 		left outer join APEX_UsersDetails as KAMud on  KAMud.UserDetailsID=BRIEF.InsertedBy"
        sql &= ""

        If role = "K" Then
            sql &= " Where KAMud.UserDetailsID=" & getLoggedUserID()
            If Len(Request.QueryString("jid")) > 0 Then
                If Request.QueryString("jid").ToString() <> "" Then
                    Dim jid As String = Request.QueryString("jid").ToString()
                    sql &= " and RefJobcardID='" & jid & "'"
                End If
            End If
        Else
            If Len(Request.QueryString("jid")) > 0 Then
                If Request.QueryString("jid").ToString() <> "" Then
                    Dim jid As String = Request.QueryString("jid").ToString()
                    sql &= " where RefJobcardID='" & jid & "'"
                End If
            End If
        End If


        sql &= " order by jc.JobCardNo"
        sql &= ""
        sql &= ""
        sql &= ""



        Dim ds As New DataSet
        Dim dt As New DataTable
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt)
        End If
        Return jsonstring
    End Function

    Private Function FillJobCounts() As String
        Dim Sql As String = ""
        Dim jsonstring As String = ""
        Dim ds As DataSet = Nothing
        Dim dt As DataTable = Nothing
        dt = New DataTable
        dt.TableName = "Table"
        dt.Columns.Add("Jobs")

        Dim rw As DataRow = Nothing
        Dim uid As String = getLoggedUserID()

        Dim capex As New clsApex
        Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())

        Sql = "select count(1)TotalJobs from Apex_Leads "
        If Not (role = "H" Or role = "F") Then
            Sql &= " where insertedby='" & uid & "'"
        End If

        Sql &= " select count(1)RunningJobs from Apex_jobcard jc"
        Sql &= " inner join Apex_brief B on jc.RefbriefID=B.briefID"
        Sql &= " where jobcardno  is not null "
        If Not (role = "H" Or role = "F") Then
            Sql &= " and B.insertedby='" & uid & "'"
        End If
        Sql &= " select count(1)CompletedJobs from Apex_jobcard jc"
        Sql &= " inner join Apex_brief B on jc.RefbriefID=B.briefID"
        Sql &= " where JobCompleted='Y' "

        If Not (role = "H" Or role = "F") Then
            Sql &= " and B.insertedby='" & uid & "'"
        End If

        Sql &= " select count(1)PipelineJobs from Apex_Leads where ISbriefed='N' "
        If Not (role = "H" Or role = "F") Then
            Sql &= " and insertedby='" & uid & "'"
        End If
        ds = ExecuteDataSet(Sql)

        If ds.Tables.Count > 0 Then
            For i As Integer = 0 To 3
                rw = dt.NewRow
                rw(0) = ds.Tables(i).Rows(0)(0).ToString()
                dt.Rows.Add(rw)
            Next
            jsonstring = GetJSONString(dt)
        End If
        Return jsonstring

    End Function

    Private Function FillJobCountsForPM() As String
        Dim Sql As String = ""
        Dim jsonstring As String = ""
        Dim ds As DataSet = Nothing
        Dim dt As DataTable = Nothing
        dt = New DataTable
        dt.TableName = "Table"
        dt.Columns.Add("Jobs")

        Dim rw As DataRow = Nothing
        Dim uid As String = getLoggedUserID()

        Dim capex As New clsApex
        Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())

        Sql = "select count(1)TotalJobs from Apex_Leads "
        If Not (role = "H" Or role = "F") Then
            Sql &= " where insertedby='" & uid & "'"
        End If

        Sql &= " select count(1)RunningJobs from Apex_jobcard jc"
        Sql &= " inner join Apex_brief B on jc.RefbriefID=B.briefID"
        Sql &= " where jobcardno  is not null "
        If Not (role = "H" Or role = "F") Then
            Sql &= " and B.insertedby='" & uid & "'"
        End If
        Sql &= " select count(1)CompletedJobs from Apex_jobcard jc"
        Sql &= " inner join Apex_brief B on jc.RefbriefID=B.briefID"
        Sql &= " where JobCompleted='Y' "

        If Not (role = "H" Or role = "F") Then
            Sql &= " and B.insertedby='" & uid & "'"
        End If

        Sql &= " select count(1)PipelineJobs from Apex_Leads where ISbriefed='N' "
        If Not (role = "H" Or role = "F") Then
            Sql &= " and insertedby='" & uid & "'"
        End If
        ds = ExecuteDataSet(Sql)

        If ds.Tables.Count > 0 Then
            For i As Integer = 0 To 3
                rw = dt.NewRow
                rw(0) = ds.Tables(i).Rows(0)(0).ToString()
                dt.Rows.Add(rw)
            Next
            jsonstring = GetJSONString(dt)
        End If
        Return jsonstring

    End Function

    Private Function FillPieChart() As String
        Dim Sql As String = ""
        Dim jsonstring As String = ""
        Dim ds As DataSet = Nothing
        Dim dt As DataTable = Nothing
        dt = New DataTable
        dt.TableName = "Table"
        dt.Columns.Add("Category")
        dt.Columns.Add("Percent")

        Dim rw As DataRow = Nothing
        Dim uid As String = getLoggedUserID()

        Dim capex As New clsApex
        Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())

        Sql &= "select a.Category,Sum(Revinew2)Revinew from ( "
        Sql &= "select  category,(case when isnull(IsEstimateVsActuals,'N') = 'N'  then "
        Sql &= "(isnull((Estimate+(Estimate*Agencyfee/100)),0)) else (Actual+(Actual*Agencyfee/100))end)Revinew2 "
        Sql &= "from  APEX_TempEstimate TE "
        Sql &= "inner join APEX_Brief as b on TE.RefBriefID =  b.BriefID      "
        Sql &= "inner join APEX_JobCard as J on TE.RefBriefID =  j.refBriefID "
        Sql &= "inner join APEX_Estimate as e on e.RefBriefID = b.BriefID "
        If role = "K" Then
            Sql &= "              and b.InsertedBy=" & getLoggedUserID()
        End If
        Sql &= ") a group by a.Category"
        Sql &= " "
        Sql &= ""

        ds = ExecuteDataSet(Sql)

        If ds.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                rw = dt.NewRow
                rw(0) = ds.Tables(0).Rows(i)(0).ToString()
                rw(1) = ds.Tables(0).Rows(i)(1).ToString()
                dt.Rows.Add(rw)
            Next
            jsonstring = GetJSONString(dt)
        End If
        Return jsonstring


    End Function

    Private Function FillBubbleChart() As String
        Dim Sql As String = ""
        Dim jsonstring As String = ""
        Dim ds As DataSet = Nothing
       
        Dim uid As String = getLoggedUserID()

        Dim capex As New clsApex
        Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())

        Sql &= " select a.Client,sum(a.Revinew)Revinew,con as projects from   "
        Sql &= " (select Client,(case when isnull(IsEstimateVsActuals,'N') = 'N'  then (isnull(EstimatedTotal,0)) else"
        Sql &= " (select top 1 total from  APEX_TempEstimate where refbriefID=b.briefID ) end)Revinew ,"
        Sql &= " (select count(1) from APEX_JobCard where  RefClientID =c.ClientID)con    "
        Sql &= " from APEX_JobCard as j       "
        Sql &= " inner join APEX_Clients as c on j.RefClientID =c.ClientID       "
        Sql &= " inner join APEX_Brief as b on j.RefBriefID =  b.BriefID        "
        Sql &= " inner join APEX_Estimate as e on e.RefBriefID = b.BriefID"
        If role = "K" Then
            Sql &= " and b.InsertedBy=" & getLoggedUserID()
        End If
        Sql &= " )  a"
        Sql &= " group by a.Client,con"
        Sql &= ""

        ds = ExecuteDataSet(Sql)

        If ds.Tables(0).Rows.Count > 0 Then
            jsonstring = GetJSONString(ds.Tables(0))
        End If
        Return jsonstring
    End Function

    Private Function FillNews() As String
        Dim Sql As String = ""
        Dim jsonstring As String = ""
        Dim ds As DataSet = Nothing

        Dim uid As String = getLoggedUserID()

        Dim capex As New clsApex
        Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())


        Sql &= "SELECT top 5 [NEWSID],[Title],[ShortDesc],[NewsDate],[ImageURL],[Type],[InsertedOn],[ISactive],[ISDeleted] FROM [Apex_News] order by [NewsDate] desc"

        ds = ExecuteDataSet(Sql)

        If ds.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                jsonstring &= "<li class='item'>"
                jsonstring &= "<div class='product-img'>"

                If ds.Tables(0).Rows(i)("Type") = "Internal" Then
                    jsonstring &= "<img src='dist/img/newslogo.png' alt='Product Image' /></div>"
                Else
                    jsonstring &= "<img src='dist/img/ExtNews.jpg' alt='Product Image' /></div>"
                End If

                jsonstring &= "<div class='product-info'>"
                jsonstring &= "<a href='javascript::;' class='product-title'>" & ds.Tables(0).Rows(i)("Title").ToString() & "</a>"
                jsonstring &= "<span class='product-description'>" & ds.Tables(0).Rows(i)("ShortDesc").ToString() & "</span>"
                jsonstring &= "</div></li>"

            Next

            'jsonstring = GetJSONString(ds.Tables(0))
        End If
        Return jsonstring
    End Function

    Private Function fillassignbudgetsummary(ByVal jid As Integer) As String
        Dim Sql As String = ""
        Dim jsonstring As String = ""
        Dim ds As DataSet = Nothing

        Dim uid As String = getLoggedUserID()

        Dim capex As New clsApex
        Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())


        'Sql &= "	select AccountID ,RefjobcardID,Particulars ,StartDate,EndDate,Description,Quantity,Total,category,TaskName,"
        'Sql &= "	(isnull(ud.firstName,'')+' '+isnull(ud.LastName,''))Name       "
        'Sql &= "	,Status,Remarks,TaskCompleted,Workstatus,ta.insertedby,ta.TL,(case when Status<>'Completed' and Enddate < Getdate() then 'expired' else '' end)sts,ta.TL   from APEX_TaskAccount ta"
        'Sql &= "	left outer join APEX_UsersDetails as ud on  ud.UserDetailsID=ta.TL"

        Sql &= "		select RefjobcardID,category,"
        Sql &= "      	(isnull(ud.firstName,'')+' '+isnull(ud.LastName,''))Name       "
        Sql &= "      	,count(RefjobcardID)cnt,sum(total)Budget from APEX_TaskAccount ta"
        Sql &= "      	left outer join APEX_UsersDetails as ud on  ud.UserDetailsID=ta.TL"
        Sql &= ""

        Sql &= " "

        Sql &= "	 where RefjobcardID=" & jid & "  "
        'and (ta.insertedby =" & getLoggedUserID() & " or ta.insertedby in(" & capex.getuserdetailsID(getLoggedUserID()) & ") or ta.TL=" & getLoggedUserID() & " or  ta.TL in(" & capex.getuserdetailsID(getLoggedUserID()) & "))
        ' sql &= " and (ta.InsertedBy=" & getLoggedUserID() & " or ta.TL=" & getLoggedUserID() & ") "
        Sql &= "		Group by RefjobcardID,category,"
        Sql &= "      	(isnull(ud.firstName,'')+' '+isnull(ud.LastName,''))"

        Sql &= " order by Category"


        ds = ExecuteDataSet(Sql)
        jsonstring = GetJSONString(ds.Tables(0))

        Return jsonstring
    End Function

    Private Function fillassignbudgetsummaryOnClaimSummary(ByVal jid As Integer) As String
        Dim Sql As String = ""
        Dim jsonstring As String = ""
        Dim ds As DataSet = Nothing

        Dim uid As String = getLoggedUserID()

        Dim capex As New clsApex
        Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())



        'Sql &= "		select RefjobcardID,category,"
        'Sql &= "      	(isnull(ud.firstName,'')+' '+isnull(ud.LastName,''))Name       "
        'Sql &= "      	,count(RefjobcardID)cnt,sum(total)Budget from APEX_TaskAccount ta"
        'Sql &= "      	left outer join APEX_UsersDetails as ud on  ud.UserDetailsID=ta.TL"
        'Sql &= ""

        'Sql &= " "

        'Sql &= "	 where RefjobcardID=" & jid & "  "
        ''and (ta.insertedby =" & getLoggedUserID() & " or ta.insertedby in(" & capex.getuserdetailsID(getLoggedUserID()) & ") or ta.TL=" & getLoggedUserID() & " or  ta.TL in(" & capex.getuserdetailsID(getLoggedUserID()) & "))
        '' sql &= " and (ta.InsertedBy=" & getLoggedUserID() & " or ta.TL=" & getLoggedUserID() & ") "
        'Sql &= "		Group by RefjobcardID,category,"
        'Sql &= "      	(isnull(ud.firstName,'')+' '+isnull(ud.LastName,''))"

        'Sql &= " order by Category"

        Sql &= "  select * from	(select RefjobcardID,category,"
        Sql &= "        	(isnull(ud.firstName,'')+' '+isnull(ud.LastName,''))Name       "
        Sql &= "        	,count(RefjobcardID)cnt,sum(total)Budget,'EM' as Role from APEX_TaskAccount ta"
        Sql &= "        	left outer join APEX_UsersDetails as ud on  ud.UserDetailsID=ta.TL"
        Sql &= ""
        Sql &= "			where refjobcardid='" & jid & "'"
        Sql &= ""
        Sql &= "			Group by RefjobcardID,category,"
        Sql &= "        	(isnull(ud.firstName,'')+' '+isnull(ud.LastName,'')),ud.role"
        Sql &= "			union all"
        Sql &= "			select 0,''category, (isnull(firstName,'')+' '+isnull(LastName,''))name,0,0,'PM' from APEX_UsersDetails where UserDetailsID=(select projectManagerID from Apex_jobcard where jobcardID='" & jid & "')"
        Sql &= "			union all"
        Sql &= "			select 0,''category, (isnull(firstName,'')+' '+isnull(LastName,''))name,0,0,'KAM' from APEX_UsersDetails where UserDetailsID=(select Insertedby from Apex_brief where briefID=(select refbriefID from Apex_jobcard where jobcardID='" & jid & "'))"
        Sql &= ""
        Sql &= "			) A"
        Sql &= ""
        Sql &= "			order by Name"


        ds = ExecuteDataSet(Sql)
        jsonstring = GetJSONString(ds.Tables(0))

        Return jsonstring
    End Function

End Class