Imports System.Data
Imports clsDatabaseHelper
Imports clsMain
Imports clsApex

Partial Class AjaxCalls_PMDetails
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim result As String = ""
        Dim capex As New clsApex
        If Len(Request.QueryString("call")) > 0 Then
            If Request.QueryString("call").ToString() <> "" Then
                Dim callid As String = Request.QueryString("call").ToString()
                Dim jid As String = "0"
                If Len(Request.QueryString("jid")) > 0 Then
                    If Request.QueryString("jid").ToString() <> "" Then
                        If Request.QueryString("jid").ToString() <> "null" Then
                            jid = Request.QueryString("jid").ToString()
                        Else
                            jid = 0
                        End If
                    Else
                        jid = 0
                    End If
                End If
                'Dim jid As String = Request.QueryString("jid").ToString()
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
                ElseIf callid = "14" Then
                    result = fillDetailsKAM1cnt()
                ElseIf callid = "15" Then
                    result = filltaskdetail(jid)
                ElseIf callid = "16" Then
                    result = fillprepnlsummary(jid)
                ElseIf callid = "17" Then
                    result = filltotalbudget(jid)
                ElseIf callid = "18" Then
                    result = FillDetailsPM1PREPNL(jid)
                ElseIf callid = "19" Then
                    result = TotalClaims()
                ElseIf callid = "20" Then
                    result = FillDetailsPM1Search()
                ElseIf callid = "21" Then
                    result = FillNewsPage()
                    'For Vendar 
                ElseIf callid = "22" Then
                    result = FillVendorDetails()
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
        sql &= "  where jc.IsActive='Y' and jc.IsDeleted='N' and (JobCompleted is null or JobCompleted = 'N') and IsEstimated='Y' and IsPrePnL='Y'  and JobcardNo is null and n.type = 11 order by Jobcardid desc"

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
        sql &= "  where jc.IsActive='Y' and jc.IsDeleted='N' and (JobCompleted is null or JobCompleted = 'N') and IsEstimated='Y' and IsPrePnL='Y'  and mp.StageLevel > 4 and JobcardNo is not null order by Jobcardid desc"

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
        Dim sql As String = " Select jc.JobCardID,JobCardNo as JobCardNo "
        sql &= " ,JobCardName"
        sql &= " ,isnull((l.FirstName+ isnull(l.LastName,'')),'N/A') as ProjectManager"
        sql &= " ,activity.ProjectType"
        sql &= " from APEX_JobCard as jc"
        sql &= " Inner join  APEX_UsersDetails as l on jc.projectManagerID=l.UserDetailsID "
        sql &= " Inner join APEX_ActivityType as activity on jc.primaryActivityID=activity.ProjectTypeID "
        sql &= " Inner join APEX_PostPnL as pl on jc.JobCardID=pl.RefJobCardID"
        sql &= " where jc.IsActive='Y' and jc.IsDeleted='N' and JobCompleted = 'Y' and ISBranchHeadApproved='Y' order by pl.Insertedon desc "
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

        'sql &= ""
        'sql &= " Select Distinct Client,link=case when ProjectManagerID=" & getLoggedUserID() & "  then (case when type =7 and rec.IsExecuted='N' and (jc.JobCompleted ='N' or jc.JobCompleted is null) "
        'sql &= "  then 'PrePnlManager.aspx?mode=edit&bid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)"
        'sql &= "   + '&kid=y'    when jc.isPrePnL='Y' and (jc.JobCompleted ='N' or jc.JobCompleted is null) then 'OpenPrePnlManager.aspx?mode=edit&bid=' +  "
        'sql &= "	   convert(varchar(10),BriefID) + '&kid=y' else 'PrePnlManager.aspx?mode=edit&bid=' + convert(varchar(10),BriefID) + '&kid=y' end) else '#' end,  "
        'sql &= "	     jc.jobCardID, isnull(jc.JobcardNo,'N/A')JobcardNo,Estimate.EstimatedTotal as Budget,    isnull((select sum(cl.Amount) from APEX_ClaimMaster as cm "
        'sql &= "		     JOIN APEX_ClaimTransaction AS cl on cm.ClaimMasterID=cl.RefClaimID    where jobcardNo=jc.jobCardID),0.00)as Claims, "
        'sql &= "			     Final_Cost=(b.Budget-(select sum(isnull(cl.Amount,0)) from APEX_ClaimMaster as cm 	 "
        'sql &= "				 JOIN APEX_ClaimTransaction AS cl on cm.ClaimMasterID=cl.RefClaimID where jobcardNo=jc.jobCardID)),	  "
        'sql &= "				 Savings=(select sum(isnull(cl.Amount,0)) from APEX_ClaimMaster as cm JOIN APEX_ClaimTransaction AS cl on cm.ClaimMasterID=cl.RefClaimID "
        'sql &= "				 	  where jobcardNo=jc.jobCardID), JobStatus = case when (JobCompleted='N' or JobCompleted is null) and JobCardNo is null then '-' "
        'sql &= "					    when (JobCompleted='N' or JobCompleted is null) and JobCardNo is not null then 'Open' "
        'sql &= "							   else 'Close' end,jc.INsertedOn,ProjectManagerID   from  APEX_JobCard as jc   	  "
        'sql &= "							   Inner join APEX_Notifications as nt on jc.RefBriefID=nt.AssociateID   	  "
        'sql &= "							   left outer join APEX_ActivityType  as At on Jc.PrimaryActivityID=At.ProjectTypeID   	  "
        'sql &= "							   join dbo.APEX_Recieptents as rec on nt.notificationID=rec.refnotificationID   	  "
        'sql &= "							   inner join APEX_Brief as b on jc.RefBriefID = b.BriefID   	  "
        'sql &= "							   inner join APEX_Clients as Client on jc.refclientID= client.ClientID	  "
        'sql &= "							   inner join Apex_Estimate as Estimate on b.BriefID=Estimate.RefBriefID	  "
        'sql &= "							   Left Join Apex_task T on JC.jobcardID=T.RefjobcardID	 "
        'sql &= "							   Left join Apex_taskAccount TA on T.taskID=TA.RefTaskID     "
        'sql &= "							   where ProjectManagerID=" & getLoggedUserID() & "  or  TA.TL=" & getLoggedUserID() & "    order by jc.INsertedOn desc"
        sql &= " "
        sql &= "  Select Distinct Briefname,jc.ModifiedOn,(select (FirstName + ' ' + Isnull(LastName,'')) from APex_usersdetails where userdetailsID=b.InsertedBy)Kamname,b.BriefID,Client, link=case when ProjectManagerID=" & getLoggedUserID() & "  then (case when type =7 and rec.IsExecuted='N' and (jc.JobCompleted ='N' or jc.JobCompleted is null)  "
        sql &= "   then 'PrePnlManager.aspx?mode=edit&bid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)   + '&kid=y'    "
        sql &= "	when jc.isPrePnL='Y' and (jc.JobCompleted ='N' or jc.JobCompleted is null) then 'OpenPrePnlManager.aspx?mode=edit&bid=' +  	   convert(varchar(10),BriefID) + "
        sql &= "	'&kid=y' else 'PrePnlManager.aspx?mode=edit&bid=' + convert(varchar(10),BriefID) + '&kid=y' end) else 'BriefManagerview.aspx?mode=edit&bid=' +  	   convert(varchar(10),BriefID) + 	'&kid=y' end,  	 jc.jobCardID,     " ',
        sql &= "	isnull(jc.JobcardNo,'N/A')JobcardNo,Estimate.EstimatedTotal as Budget,    isnull((select sum(cl.Amount) from APEX_ClaimMaster as cm 		      "
        sql &= "	JOIN APEX_ClaimTransaction AS cl on cm.ClaimMasterID=cl.RefClaimID    where jobcardNo=jc.jobCardID and isapproved='Y'),0.00)as Claims, 			    "
        sql &= "	 Final_Cost=isnull((select Sum(posteventcost) from [dbo].[APEX_tempPostPnLCost] where refbriefid=b.BriefID),0.00),	  				 "
        sql &= "	 Savings=isnull((Estimate.EstimatedTotal-(select Sum(posteventcost) from [dbo].[APEX_tempPostPnLCost] where refbriefid=b.BriefID)),0.00), "
        sql &= "	  JobStatus = case when (JobCompleted='N' or JobCompleted is null) and JobCardNo is null then '-' 					"
        sql &= "	      when (JobCompleted='N' or JobCompleted is null) and JobCardNo is not null then 'Open' 							"
        sql &= "		     else 'Close' end,jc.INsertedOn,Isnull(Convert(varchar(20),jc.ModifiedOn,106),'N/A')ModifiedOnDt,ProjectManagerID  	 "
        sql &= " ,(select (FirstName + ' ' + Isnull(LastName,'')) from APex_usersdetails where userdetailsID=ProjectManagerID)ProjectManager,isnull(refprepnlID,0)refprepnlID"
        sql &= "  from  APEX_JobCard as jc   	  						"
        sql &= "			   Inner join APEX_Notifications as nt on jc.RefBriefID=nt.AssociateID   	  							   "
        sql &= "			   left outer join APEX_ActivityType  as At on Jc.PrimaryActivityID=At.ProjectTypeID   	  							    "
        sql &= "			   join dbo.APEX_Recieptents as rec on nt.notificationID=rec.refnotificationID   	  							   "
        sql &= "			   inner join APEX_Brief as b on jc.RefBriefID = b.BriefID   	  							   "
        sql &= "			   inner join APEX_Clients as Client on jc.refclientID= client.ClientID	  							    "
        sql &= "			   inner join Apex_Estimate as Estimate on b.BriefID=Estimate.RefBriefID	  							   "
        sql &= "			   Left Join Apex_task T on JC.jobcardID=T.RefjobcardID	 							   "
        sql &= "			   Left join Apex_taskAccount TA on JC.jobcardID=TA.RefjobcardID      							  "
        sql &= "			    where (ProjectManagerID=" & getLoggedUserID() & "  or  TA.TL=" & getLoggedUserID() & ")  order by jc.ModifiedOn Desc "
        sql &= " "
        sql &= ""
        sql &= ""
        sql &= " "


        Dim ds As New DataSet
        Dim dt As New DataTable
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt)
        End If
        Return jsonstring
    End Function

    Private Function FillDetailsPM1PREPNL(ByVal jc As String) As String
        Dim jsonstring As String = ""
        Dim capex As New clsApex
        Dim sql As String = ""
        sql &= " "
        sql &= "  Select Distinct Client, link=case when ProjectManagerID=" & getLoggedUserID() & "  then (case when type =7 and rec.IsExecuted='N' and (jc.JobCompleted ='N' or jc.JobCompleted is null)  "
        sql &= "   then 'PrePnlManager.aspx?jc=" & jc & "&mode=edit&bid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)   + '&kid=y'    "
        sql &= "	when jc.isPrePnL='Y' and (jc.JobCompleted ='N' or jc.JobCompleted is null) then 'OpenPrePnlManager.aspx?jc=" & jc & "&mode=edit&bid=' +  	   convert(varchar(10),BriefID) + "
        sql &= "	'&kid=y' else 'PrePnlManager.aspx?jc=" & jc & "&mode=edit&bid=' + convert(varchar(10),BriefID) + '&kid=y' end) else 'BriefManagerview.aspx?mode=edit&bid=' +  	   convert(varchar(10),BriefID) + 	'&kid=y' end,  	 jc.jobCardID,     " ',
        sql &= "	isnull(jc.JobcardNo,'N/A')JobcardNo,Estimate.EstimatedTotal as Budget,    isnull((select sum(cl.Amount) from APEX_ClaimMaster as cm 		      "
        sql &= "	JOIN APEX_ClaimTransaction AS cl on cm.ClaimMasterID=cl.RefClaimID    where jobcardNo=jc.jobCardID),0.00)as Claims, 			    "
        sql &= "	 Final_Cost=isnull((select Sum(posteventcost) from [dbo].[APEX_tempPostPnLCost] where refbriefid=b.BriefID),0.00),	  				 "
        sql &= "	 Savings=isnull((Estimate.EstimatedTotal-(select Sum(posteventcost) from [dbo].[APEX_tempPostPnLCost] where refbriefid=b.BriefID)),0.00), "
        sql &= "	  JobStatus = case when (JobCompleted='N' or JobCompleted is null) and JobCardNo is null then '-' 					"
        sql &= "	      when (JobCompleted='N' or JobCompleted is null) and JobCardNo is not null then 'Open' 							"
        sql &= "		     else 'Close' end,jc.INsertedOn,ProjectManagerID  from  APEX_JobCard as jc   	  							 "
        sql &= "			   Inner join APEX_Notifications as nt on jc.RefBriefID=nt.AssociateID   	  							   "
        sql &= "			   left outer join APEX_ActivityType  as At on Jc.PrimaryActivityID=At.ProjectTypeID   	  							    "
        sql &= "			   join dbo.APEX_Recieptents as rec on nt.notificationID=rec.refnotificationID   	  							   "
        sql &= "			   inner join APEX_Brief as b on jc.RefBriefID = b.BriefID   	  							   "
        sql &= "			   inner join APEX_Clients as Client on jc.refclientID= client.ClientID	  							    "
        sql &= "			   inner join Apex_Estimate as Estimate on b.BriefID=Estimate.RefBriefID	  							   "
        'sql &= "			   Left Join Apex_task T on JC.jobcardID=T.RefjobcardID	 							   "
        sql &= "			   Left join Apex_taskAccount TA on JC.jobcardID=TA.RefjobcardID     							  "
        sql &= "			    where (ProjectManagerID=" & getLoggedUserID() & "  or  TA.TL=" & getLoggedUserID() & ")  and JC.jobcardID=" & jc & "   order by jc.INsertedOn desc"
        sql &= " "
        sql &= ""
        sql &= ""
        sql &= " "
        sql &= ""
        sql &= ""
        sql &= ""
        sql &= ""
        sql &= " "
        sql &= ""
        sql &= ""
        sql &= " "
        sql &= ""
        sql &= ""
        sql &= ""
        sql &= ""
        sql &= " "
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
        sql &= "  where jc.IsActive='Y' and jc.IsDeleted='N' and jc.IsEstimated='Y' and jc.IsPrePnL='Y'  and mp.StageLevel > 4 and jc.JobcardNo is not null and jc.projectManagerID=" & getLoggedUserID() & " and jobcardid='" & Request.QueryString("jid").ToString() & "' order by Jobcardid desc"
        sql &= " "
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
        'Dim sql As String = "Select distinct  " & getLoggedUserID() & " as TL,ta.AccountID, (select jobcardNo from apex_jobcard where jobcardID=RefJobCardID)jobcardNo"
        'sql &= " ,t.Title ,LTRIM(RTRIM(ta.Particulars))Particulars,ta.Quantity, ta.Amount, "
        'sql &= "  ta.Total, TaskCompleted =case when ta.TaskCompleted='Y' then 'Completed' else 'Pending' end , CategoryTask,RefJobCardID,(select jobcardNo from apex_jobcard where jobcardID=RefJobCardID)jobcardNo"
        'sql &= " ,(select isnull(IsPostPnL,'N')IsPostPnL  from apex_jobcard where jobcardID=RefJobCardID)IsPostPnL from APEX_Task  as t"
        'sql &= " join APEX_TaskAccount as ta on t.taskID=ta.ReftaskID"
        'sql &= " left outer join APEX_TaskTeam as tm on ta.AccountID=tm.REfTaskID"
        'sql &= " where ( ta.TL=" & getLoggedUserID() & " or tm.REfMemberID=" & getLoggedUserID() & " ) AND RefJobCardID='" & Request.QueryString("jid").ToString() & "' "


        Dim sql As String = ""
        sql &= "	select  (Case when ta.TL=" & getLoggedUserID() & " then 'Y' else 'N' end)ISclaim,AccountID ,RefjobcardID,(select isnull(jobcardno,'N') from apex_jobcard where jobcardID=RefjobcardID)jobcardNo,Particulars,Taskname,StartDate,EndDate,REPLACE(REPLACE(REPLACE(Cast([Description] as varchar(max)), CHAR(13), ''), CHAR(10), ''), CHAR(9), '') as Description,Quantity,Total,category,"
        sql &= "	(isnull(ud.firstName,'')+' '+isnull(ud.LastName,''))Name       "
        sql &= "	,Status,Remarks,TaskCompleted,Workstatus,ta.insertedby,(select isnull(IsPostPnL,'N')IsPostPnL  from apex_jobcard where jobcardID=RefjobcardID)IsPostPnL   "
        sql &= "  ,(select case when DATEDIFF(day,ActivityEndDate,getdate()) > 21 then 'Y' else 'N' end  from apex_jobcard where jobcardID=RefjobcardID)Isclaimedactive"
        sql &= ",  isnull((SELECT Sum(CT.amount) FROM APEX_ClaimMaster CM "
        sql &= "	Left join APEX_ClaimTransaction CT on CM.claimmasterID =CT.refClaimID"
        sql &= "	where CM.refTaskID=TA.accountID and isapproved='Y'),0)Claimed  from APEX_TaskAccount ta"
        sql &= "	left outer join APEX_UsersDetails as ud on  ud.UserDetailsID=ta.TL"
        sql &= "	 where RefjobcardID=" & Request.QueryString("jid").ToString() & " and (ta.InsertedBy=" & getLoggedUserID() & " or ta.TL=" & getLoggedUserID() & ") order by Category"
        sql &= ""


        'Dim sql As String = "Select  distinct top 1 " & getLoggedUserID() & " as TL,ta.InsertedOn,  ta.AccountID,replace(t.Title,'&','and')Title,convert(varchar(10),t.StartDate,105) as  startdate, "
        'sql &= " convert(varchar(10),t.enddate,105) as enddate,LTRIM(RTRIM(ta.Particulars))Particulars, ta.Quantity, ta.Amount, "
        'sql &= "  ta.Total, TaskCompleted =case when ta.TaskCompleted='Y' then 'Completed' else 'Pending' end , CategoryTask,RefJobCardID,(select jobcardNo from apex_jobcard where jobcardID=RefJobCardID)jobcardNo"
        'sql &= " ,(select isnull(IsPostPnL,'N')IsPostPnL  from apex_jobcard where jobcardID=RefJobCardID)IsPostPnL from APEX_Task  as t"
        'sql &= " join APEX_TaskAccount as ta on t.taskID=ta.ReftaskID"
        'sql &= " left outer join APEX_TaskTeam as tm on ta.AccountID=tm.REfTaskID"
        'sql &= " where ( ta.TL=" & getLoggedUserID() & " or tm.REfMemberID=" & getLoggedUserID() & " ) AND RefJobCardID='" & Request.QueryString("jid").ToString() & "' order by ta.InsertedOn desc"

        'Request.QueryString("jid").ToString()
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
        'sql &= " Select JobCardID,cm.ClaimMasterID,ClaimStatus  = case when IsApproved is null then 'Pending' "
        'sql &= "   when  IsApproved ='Y' then 'Approved' else  'Rejected' End ,"
        'sql &= " t.title,jc.JobCardNo, sum(cl.Amount) as Amount from APEX_ClaimMaster as cm"
        'sql &= " join APEX_TaskAccount as ta on cm.RefTaskID=Ta.AccountID"
        'sql &= " join APEX_Task as t on ta.refTaskID=t.taskid"
        'sql &= " join APEX_JobCard as jc on t.RefJobcardID=jc.JobCardID"
        'sql &= " JOIN APEX_ClaimTransaction AS cl on cm.ClaimMasterID=cl.RefClaimID"
        'sql &= " where SubmittedUserID=" & getLoggedUserID() & " and JobCardID='" & Request.QueryString("jid").ToString() & "' group by JobCardID,IsApproved,t.title,jc.JobCardNo,cl.Amount,cm.ClaimMasterID"

        'sql &= "	 Select TaskID,Title,convert(varchar(20),StartDate,106) as  startdate, convert(varchar(20),enddate,106) as enddate,"
        'sql &= "	 categoryTask,t.Description,  TaskCompleted =case when ta.TaskCompleted='Y' then 'Completed' else 'Pending' end  , isnull(ud.firstName,'')+' '+isnull(ud.LastName,'') as Name,"
        ''sql &= "	  (case when (isnull(ud.firstName,'')+' '+isnull(ud.LastName,'')) <>'' then  (isnull(ud.firstName,'')+' "
        ''sql &= "	  '+isnull(ud.LastName,'')) else '<a href=''TaskAccount.aspx?tid='+convert(varchar(10),TaskID) +'''> NA</a>' end)AsName  ,"
        ''sql &= "	  (select isnull(sum(PreEventTotal),0) as PreEventQuote from APEX_PrePnLcost where category=categoryTask and "
        ''sql &= "	  refbriefID=(select refbriefID from apex_jobcard where jobcardid=1))Budge  ,"
        'sql &= "	   (select (isnull(firstName,'')+' '+isnull(LastName,'')) from APEX_UsersDetails where UserDetailsID=t.insertedby)AssignFrom,"
        'sql &= "	   Total as Expence,particulars,AccountID,status,"
        'sql &= "	   ta.Remarks,t.insertedby,ClaimStatus = case when IsApproved is null then 'Pending' when  IsApproved ='Y' then 'Approved' "
        'sql &= "	   else  'Rejected' End,isnull(cl.Amount,'0.00') as Amount,ClaimMasterID  from APEX_Task as t   "
        'sql &= "	   left join APEX_TaskAccount as ta on t.taskid=ta.refTaskID "
        'sql &= "	   left outer join APEX_UsersDetails as ud on  ud.UserDetailsID=ta.TL"
        'sql &= "	   Left Join APEX_ClaimMaster as cm on t.taskID=cm.ReftaskID"
        'sql &= "	   Left JOIN APEX_ClaimTransaction AS cl on cm.ClaimMasterID=cl.RefClaimID "
        sql &= ""
        sql &= " Select (case when ta.TL='" & getLoggedUserID() & "' then 'Y' else 'N' end)TL,TaskID,Title,convert(varchar(20),ta.StartDate,106) as  startdate, convert(varchar(20),ta.enddate,106) as enddate,"
        sql &= " category categoryTask,t.Description,  TaskCompleted =case when ta.TaskCompleted='Y' then 'Completed' else 'Pending' end  ,"
        sql &= " isnull(ud.firstName,'')+' '+isnull(ud.LastName,'') as Name,	   (select (isnull(firstName,'')+' '+isnull(LastName,'')) "
        sql &= " from APEX_UsersDetails where UserDetailsID=ta.insertedby)AssignFrom,Total as Expence,particulars,AccountID,status,"
        sql &= " ta.Remarks,ta.insertedby,ClaimStatus = case when IsApproved is null then 'Pending' when  IsApproved ='Y' then 'Approved' 	"
        sql &= " when  IsApproved ='C' then 'Canceled' else  'Rejected' End,isnull(sum(cl.Amount),'0.00') as Amount,ClaimMasterID,sum(RequestedAmt)RequestedAmt"
        'isnull(sum(cl.Amount),'0.00') as Amount,ClaimMasterID,sum(RequestedAmt)RequestedAmt
        sql &= " from APEX_ClaimMaster as cm"
        sql &= " join APEX_TaskAccount as ta on cm.RefTaskID=Ta.AccountID"
        sql &= " Inner join APEX_UsersDetails as ud on  ud.UserDetailsID=ta.TL"
        'sql &= " Left join APEX_Task as t on ta.refTaskID=t.taskid "
        'sql &= " join APEX_JobCard as jc on ta.ReftaskID=jc.JobCardID"
        sql &= " Left join APEX_Task as t on ta.RefjobcardID=t.taskid "
        sql &= " join APEX_JobCard as jc on ta.RefjobcardID=jc.JobCardID "
        sql &= " JOIN APEX_ClaimTransaction AS cl on cm.ClaimMasterID=cl.RefClaimID"
        'sql &= " where JobCardID =4"
        sql &= ""
        sql &= ""
        'sql &= "	  where JobCardID='" & Request.QueryString("jid").ToString() & "' and ta.TL=" & getLoggedUserID() & "   "
        sql &= "	  where cm.JobCardNo='" & Request.QueryString("jid").ToString() & "' "

        Dim capex As New clsApex
        Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())
        If role = "K" Then

        Else
            sql &= "	  and (ta.insertedby=" & getLoggedUserID() & " or ta.TL=" & getLoggedUserID() & ")"
        End If

        '(t.insertedby=" & getLoggedUserID() & " or ta.TL=" & getLoggedUserID() & ")
        sql &= "   group by	 ta.TL,TaskID,Title,ta.StartDate,ta.enddate,categoryTask,t.Description,ta.TaskCompleted,ud.firstName,ud.LastName"
        sql &= "  ,Total,particulars,AccountID,status,ta.Remarks,ta.insertedby,IsApproved,ClaimMasterID,category"
        sql &= "  order by CategoryTask"
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
        sql &= " when type = 6 and IsExecuted='N' then 'RembursementClaimApproval.aspx?jc=' + convert(varchar(50),(select top 1 jobcardno from Apex_claimMaster where claimmasterID=AssociateID)) + '&type=SubTask&nid=' + convert(varchar(10),NotificationID)+ '&aid=' + convert(varchar(10),AssociateID)   "
        sql &= " when type = 5 and IsExecuted='N'  then 'RembursementClaimApproval.aspx?jc=' + convert(varchar(50),(select top 1 jobcardno from Apex_claimMaster where claimmasterID=AssociateID)) + '&type=Task&nid=' + convert(varchar(10),NotificationID) + '&aid=' + convert(varchar(10),AssociateID)   "
        sql &= " when type = 7 and IsExecuted='N'  then 'PrePnlManager.aspx?mode=edit&bid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)   "
        'sql &= " when type = 15 and IsExecuted='N'  then 'Estimate.aspx?mode=edit&bid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)+'&Approved=Y'   "
        sql &= " when type = 15 then 'Estimate.aspx?mode=edit&bid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)+'&Approved=Y'   "
        sql &= " when type = 8 and IsExecuted='N'  then 'JobCardManager.aspx?mode=edit&jid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)   "
        sql &= " when type = 9 and IsExecuted='N'  then 'Estimate.aspx?mode=edit&bid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)    "
        sql &= " when type = 10 and IsExecuted='N'  then 'rejectednotification.aspx?bid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)   "
        sql &= " when type = 11 and IsExecuted='N'  then 'JobCardManager.aspx?mode=edit&jid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)  "
        sql &= " when type = 13 and IsExecuted='N'  then 'JobCardManager.aspx?mode=edit&jid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)  "
        sql &= " when type = 14 then 'TaskAccount.aspx?jc=' + convert(varchar(10),AssociateID) + '&tid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID) "
        sql &= " when type = 12 and IsExecuted='N'  then 'Checklist.aspx?tid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)   "
        sql &= " when type = 16 and IsExecuted='N'  then 'RembursementClaimApproval.aspx?jc=' + convert(varchar(50),(select top 1 jobcardno from Apex_claimMaster where claimmasterID=AssociateID)) + '&type=Task&nid=' + convert(varchar(10),NotificationID) + '&aid=' + convert(varchar(10),AssociateID) + '&mode=app' "
        sql &= " when type = 17 and IsExecuted='N'  then 'RembursementClaimApproval.aspx?jc=' + convert(varchar(50),(select top 1 jobcardno from Apex_claimMaster where claimmasterID=AssociateID)) + '&type=Task&nid=' + convert(varchar(10),NotificationID) + '&aid=' + convert(varchar(10),AssociateID) + '&mode=rej'  "
        sql &= " when type = 18 and IsExecuted='N'  then 'ExcPrePnlManager.aspx?mode=edit&nid=' + convert(varchar(10),NotificationID) + '&aid=' + convert(varchar(10),AssociateID)  "
        sql &= " when type = 19 and IsExecuted='N'  then 'RembursementClaimApproval.aspx?jc=' + convert(varchar(50),(select top 1 jobcardno from Apex_claimMaster where claimmasterID=AssociateID)) + '&type=Task&nid=' + convert(varchar(10),NotificationID) + '&aid=' + convert(varchar(10),AssociateID)  "
        sql &= " when type = 20 and IsExecuted='N'  then 'viewJobInvoice.aspx?jid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)   "
        sql &= " when type = 22 and IsExecuted='N'  then 'PostPnLManager.aspx?jc=' + convert(varchar(10),AssociateID) + '&mode=view&jid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)  "
        sql &= " when type = 21 and IsExecuted='N'  then 'PostPnLManager.aspx?jc=' + convert(varchar(10),AssociateID) + '&jid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID) "
        sql &= " when type = 23 then 'PrePnlApproval.aspx?mode=edit&bid=' + convert(varchar(10),AssociateID) + ' &kid=y&nid=' + convert(varchar(10),NotificationID) "
        sql &= " when type = 24 then 'OpenPrePnLManager.aspx?mode=edit&bid=' + convert(varchar(10),AssociateID) + ' &kid=y&nid=' + convert(varchar(10),NotificationID)  "
        sql &= " when type = 25 then 'ListOfTask.aspx?jc=' + convert(varchar(10),AssociateID) + '&jid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)  "
        sql &= " when type = 26 then 'Estimate_VS_Actuals.aspx?jid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)  "
        sql &= " when type = 27 then 'JobCardManager.aspx?mode=edit&jid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID) "
        sql &= " when type = 28 then 'JobCardManager.aspx?mode=edit&jid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID) "
        sql &= " when type = 29 then 'JobCardManager.aspx?mode=edit&jid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID) "
        sql &= " when type = 31 then 'JobCardManager.aspx?mode=edit&jid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID) "
        sql &= " when type = 30 then 'PMTasklist.aspx?jc=' + convert(varchar(10),AssociateID) + '&jid=' + convert(varchar(10),AssociateID) + '&mode=Claim_Detail&nid=' + convert(varchar(10),NotificationID) "

        sql &= " else '' end"
        sql &= " from APEX_Recieptents as rc"
        sql &= " inner join APEX_Notifications as nf on rc.RefNotificationID = nf.NotificationID"
        sql &= " where UserID=" & getLoggedUserID()
        'sql &= " Order By StartDate desc"
        sql &= " Order By RecieptentID desc"

        Dim ds As New DataSet
        Dim dt As New DataTable
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt)
        End If
        Return jsonstring
    End Function

    Private Function fillDetailsKAM1cnt() As String
        Dim jsonstring As String = ""

        Dim sql As String = ""
        sql &= "select count(IsExecuted)cnt  from APEX_Recieptents as rc "
        sql &= "inner join APEX_Notifications as nf on rc.RefNotificationID = nf.NotificationID where UserID=" & getLoggedUserID() & " and IsExecuted<>'Y'"
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
    'filltaskdetail

    Private Function filltaskdetail(ByVal jc As Integer) As String
        Dim jsonstring As String = ""
        Dim sql As String = ""
        sql &= ""
        sql &= ""
        sql &= " select (select count(AccountID) from APEX_TaskAccount where RefJobcardID=" & jc & " and (insertedby=" & getLoggedUserID() & " or TL=" & getLoggedUserID() & ")) as total"
        sql &= " ,(select count(AccountID) from APEX_TaskAccount where RefJobcardID=" & jc & " and (insertedby=" & getLoggedUserID() & " or TL=" & getLoggedUserID() & ")  and [status] <> 'Completed' )Running,"
        sql &= "  (select count(AccountID) from APEX_TaskAccount where RefJobcardID=" & jc & " and (insertedby=" & getLoggedUserID() & " or TL=" & getLoggedUserID() & ")  and [status] = 'Completed' ) Complete"
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

    Private Function fillprepnlsummary(ByVal jc As Integer) As String
        Dim jsonstring As String = ""
        Dim capex As New clsApex
        Dim briefid As String = capex.GetBriefIDByJobCardID(jc)
        Dim sql As String = ""

        sql &= ""
        sql &= "select Sum(preeventcost)Budget,category from [dbo].[APEX_PrePnLCost]"
        sql &= " where refbriefID=" & briefid & " group by category "
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

    Private Function filltotalbudget(ByVal jc As Integer) As String
        Dim jsonstring As String = ""
        Dim sql As String = ""
        Dim capex As New clsApex
        Dim briefid As String = capex.GetBriefIDByJobCardID(jc)

        sql &= "	select isnull((select Sum(preeventcost)Budget from [dbo].[APEX_PrePnLCost]"
        sql &= "	where refbriefID=" & briefid & "),'0.00')Total, "
        sql &= "  isnull((select sum(total)Assign from APEX_TaskAccount where RefjobcardID =" & jc & "),'0.00')Assign,"
        'sql &= "    isnull((select sum(total)Assign from APEX_Task as t   "
        'sql &= "    left join APEX_TaskAccount as ta on t.taskid=ta.refTaskID "
        'sql &= "    where RefJobcardID=" & jc & "),'0.00')Assign,"
        sql &= "	isnull((select Sum(preeventcost)Budget from [dbo].[APEX_PrePnLCost]"
        sql &= "	where refbriefID=" & briefid & "),'0.00')-"
        sql &= "  isnull((select sum(total)Assign from APEX_TaskAccount where RefjobcardID =" & jc & "),'0.00')Remaining"
        'sql &= "    isnull((select sum(total)Assign from APEX_Task as t   "
        'sql &= "    left join APEX_TaskAccount as ta on t.taskid=ta.refTaskID "
        'sql &= "    where RefJobcardID=" & jc & "),'0.00')Remaining"
        sql &= ""
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
    'filltotalbudget

    Private Function checkpostpnlstatus(ByVal jid As Integer) As Boolean
        Dim status As Boolean = False
        Dim Sql As String = ""
        Sql &= "select isnull(IsPostPnL,'N')IsPostPnL  from apex_jobcard where JobCardID=" & jid
        Sql &= ""
        Sql &= ""
        Sql &= ""
        Dim ds As New DataSet
        ds = ExecuteDataSet(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0)(0)("IsPostPnL") = "Y" Then
                status = True
            Else
                status = False
            End If
        End If
        Return status
    End Function

    Private Function TotalClaims() As String
        Dim jsonstring As String = ""

        Dim sql As String = " "

        sql &= ""
        'sql &= " Select TaskID,Title,convert(varchar(20),ta.StartDate,106) as  startdate, convert(varchar(20),ta.enddate,106) as enddate,"
        'sql &= " categoryTask,t.Description,  TaskCompleted =case when ta.TaskCompleted='Y' then 'Completed' else 'Pending' end  ,"
        'sql &= " isnull(ud.firstName,'')+' '+isnull(ud.LastName,'') as Name,	   (select (isnull(firstName,'')+' '+isnull(LastName,'')) "
        'sql &= " from APEX_UsersDetails where UserDetailsID=t.insertedby)AssignFrom,Total as Expence,particulars,AccountID,status,"
        'sql &= " ta.Remarks,t.insertedby,ClaimStatus = case when IsApproved is null then 'Pending' when  IsApproved ='Y' then 'Approved' 	"
        'sql &= " else  'Rejected' End,isnull(cl.Amount,'0.00') as Amount,ClaimMasterID,RequestedAmt"
        'sql &= " from APEX_ClaimMaster as cm"
        'sql &= " join APEX_TaskAccount as ta on cm.RefTaskID=Ta.AccountID"
        'sql &= " Inner join APEX_UsersDetails as ud on  ud.UserDetailsID=ta.TL"
        'sql &= " join APEX_Task as t on ta.refTaskID=t.taskid"
        'sql &= " join APEX_JobCard as jc on t.RefJobcardID=jc.JobCardID"
        'sql &= " JOIN APEX_ClaimTransaction AS cl on cm.ClaimMasterID=cl.RefClaimID"
        'sql &= " where JobCardID =4"

        sql &= ""
        sql &= "  Select AccountID TaskID, TaskName  Title,convert(varchar(20),ta.StartDate,106) as  startdate, convert(varchar(20),"
        sql &= "  ta.enddate,106) as enddate, "
        sql &= "  category categoryTask,REPLACE(REPLACE(Cast(ta.Description as varchar(max)), CHAR(13), ''), CHAR(10), '') as Description,  TaskCompleted =case when ta.TaskCompleted='Y' then 'Completed' else 'Pending' end  , "
        sql &= "  isnull(ud.firstName,'')+' '+isnull(ud.LastName,'') as Name,	   (select (isnull(firstName,'')+' '+isnull(LastName,''))  "
        sql &= "  from APEX_UsersDetails where UserDetailsID=ta.insertedby)AssignFrom,Total as Expence,particulars,AccountID,status, "
        sql &= "  ta.Remarks,ta.insertedby,ClaimStatus = case when IsApproved is null then 'Pending' when  IsApproved ='Y' then 'Approved' "
        sql &= "  	 else  'Rejected' End,isnull(cl.Amount,'0.00') as Amount,ClaimMasterID,RequestedAmt from APEX_ClaimMaster as cm "
        sql &= "	 join APEX_TaskAccount as ta on cm.RefTaskID=Ta.AccountID"
        sql &= "	  Inner join APEX_UsersDetails as ud on  ud.UserDetailsID=ta.TL "
        ' sql &= "	  --join APEX_Task as t on ta.refTaskID=t.taskid "
        sql &= "	  join APEX_JobCard as jc on ta.RefJobcardID=jc.JobCardID "
        sql &= "	  JOIN APEX_ClaimTransaction AS cl on cm.ClaimMasterID=cl.RefClaimID	  where JobCardID='" & Request.QueryString("jid").ToString() & "' "
        sql &= "   order by Category "
        sql &= ""
        sql &= ""
        sql &= ""
        sql &= ""
        sql &= ""
        sql &= ""
        sql &= ""
        sql &= ""
        sql &= ""
        sql &= ""
        'sql &= "	  where JobCardID='" & Request.QueryString("jid").ToString() & "'  order by CategoryTask "
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

    Private Function FillDetailsPM1Search() As String
        Dim jsonstring As String = ""
        Dim capex As New clsApex
        Dim sql As String = ""

        sql &= " "
        'sql &= "  Select Distinct Client, link=case when ProjectManagerID=" & getLoggedUserID() & "  then (case when type =7 and rec.IsExecuted='N' and (jc.JobCompleted ='N' or jc.JobCompleted is null)  "
        'sql &= "   then 'PrePnlManager.aspx?mode=edit&bid=' + convert(varchar(10),AssociateID) + '&nid=' + convert(varchar(10),NotificationID)   + '&kid=y'    "
        'sql &= "	when jc.isPrePnL='Y' and (jc.JobCompleted ='N' or jc.JobCompleted is null) then 'OpenPrePnlManager.aspx?mode=edit&bid=' +  	   convert(varchar(10),BriefID) + "
        'sql &= "	'&kid=y' else 'PrePnlManager.aspx?mode=edit&bid=' + convert(varchar(10),BriefID) + '&kid=y' end) else 'BriefManagerview.aspx?mode=edit&bid=' +  	   convert(varchar(10),BriefID) + 	'&kid=y' end,  	 jc.jobCardID,     " ',
        'sql &= "	isnull(jc.JobcardNo,'N/A')JobcardNo,(case when JobCompleted = 'N' then (isnull(EstimatedTotal,0)) else (isnull((select top 1 subtotal  from APEX_TempEstimate where refestimateID=Estimate.estimateID),0)) end) as Budget,    isnull((select sum(cl.Amount) from APEX_ClaimMaster as cm 		      "
        'sql &= "	JOIN APEX_ClaimTransaction AS cl on cm.ClaimMasterID=cl.RefClaimID    where jobcardNo=jc.jobCardID),0.00)as Claims, 			    "
        'sql &= "	 Final_Cost=isnull((select Sum(posteventcost) from [dbo].[APEX_tempPostPnLCost] where refbriefid=b.BriefID),0.00),	  				 "
        'sql &= "	 Savings=isnull(((case when JobCompleted = 'N' then (isnull(EstimatedTotal,0)) else (isnull((select top 1 subtotal  from APEX_TempEstimate where refestimateID=Estimate.estimateID),0)) end)-(select Sum(posteventcost) from [dbo].[APEX_tempPostPnLCost] where refbriefid=b.BriefID)),0.00), "
        'sql &= "	  JobStatus = case when (IsEstimateVsActuals='N' or IsEstimateVsActuals is null) and JobCardNo is null then '-' 					"
        'sql &= "	      when (JobCompleted='N' or JobCompleted is null) and JobCardNo is not null then 'Open' 							"
        'sql &= "		     else 'Close' end,jc.INsertedOn,ProjectManagerID  from  APEX_JobCard as jc   	  							 "
        'sql &= "			   Inner join APEX_Notifications as nt on jc.RefBriefID=nt.AssociateID   	  							   "
        'sql &= "			   left outer join APEX_ActivityType  as At on Jc.PrimaryActivityID=At.ProjectTypeID   	  							    "
        'sql &= "			   join dbo.APEX_Recieptents as rec on nt.notificationID=rec.refnotificationID   	  							   "
        'sql &= "			   inner join APEX_Brief as b on jc.RefBriefID = b.BriefID   	  							   "
        'sql &= "			   inner join APEX_Clients as Client on jc.refclientID= client.ClientID	  							    "
        'sql &= "			   inner join Apex_Estimate as Estimate on b.BriefID=Estimate.RefBriefID	  							   "
        'sql &= "			   Left Join Apex_task T on JC.jobcardID=T.RefjobcardID	 							   "
        'sql &= "			   Left join Apex_taskAccount TA on T.taskID=TA.RefTaskID     							  "
        sql &= "Select Distinct Briefname,b.BriefID,Client, link=case when ProjectManagerID=" & getLoggedUserID() & "  then (case when type =7 and rec.IsExecuted='N' and (jc.JobCompleted ='N' or"
        sql &= "    jc.JobCompleted is null)     then 'PrePnlManager.aspx?mode=edit&bid=' + convert(varchar(10),AssociateID) + '&nid=' "
        sql &= "	+ convert(varchar(10),NotificationID)   + '&kid=y'    	when jc.isPrePnL='Y' and (jc.JobCompleted ='N' or jc.JobCompleted is null) "
        sql &= "	then 'OpenPrePnlManager.aspx?mode=edit&bid=' +  	   convert(varchar(10),BriefID) + 	'&kid=y' else "
        sql &= "	'PrePnlManager.aspx?mode=edit&bid=' + convert(varchar(10),BriefID) + '&kid=y' end) else 'BriefManagerview.aspx?mode=edit&bid=' +"
        sql &= "	convert(varchar(10),BriefID) + 	'&kid=y' end,  	 jc.jobCardID,     	isnull(jc.JobcardNo,'N/A')JobcardNo,"
        sql &= "	(case when isnull(IsEstimateVsActuals,'N') = 'N' then (isnull(EstimatedTotal,0)) else (isnull((select top 1 total  "
        sql &= "	 from APEX_TempEstimate where refestimateID=Estimate.estimateID),0)) end) as Budget,isnull((select sum(cl.Amount) from APEX_ClaimMaster as cm 	"
        sql &= "	JOIN APEX_ClaimTransaction AS cl on cm.ClaimMasterID=cl.RefClaimID 	 where jobcardNo=jc.jobCardID and IsApproved='Y'),0.00)as Claims,			    	 "
        sql &= "	 Final_Cost="
        sql &= "	 (case when isnull(IsEstimateVsActuals,'N') = 'N' then (select sum(preeventcost) from [dbo].[APEX_PrePnLCost] pre "
        sql &= "	 where pre.refbriefID=b.BriefID) else"
        sql &= "	 isnull((select Sum(posteventcost) from [dbo].[APEX_tempPostPnLCost] where refbriefid=b.BriefID),0.00) end),	"
        sql &= "	 Savings=isnull(((case when isnull(IsEstimateVsActuals,'N') = 'N' then (isnull(EstimatedTotal,0)) else "
        sql &= "	(isnull((select top 1 total  from APEX_TempEstimate where refestimateID=Estimate.estimateID),0)) "
        sql &= "      end)-(case when isnull(IsEstimateVsActuals,'N') = 'N' then (select sum(preeventcost) from [dbo].[APEX_PrePnLCost] pre where "
        sql &= "	  pre.refbriefID=b.BriefID) else"
        sql &= "	 isnull((select Sum(posteventcost) from [dbo].[APEX_tempPostPnLCost] where refbriefid=b.BriefID),0.00) end)),0.00),"
        sql &= "	JobStatus = case when (isnull(IsEstimateVsActuals,'N')='N') and JobCardNo"
        sql &= "	is null then '-' when (isnull(IsEstimateVsActuals,'N')='N') and JobCardNo is not null then 'Open' "
        sql &= "	else 'Close' end,jc.INsertedOn,ProjectManagerID  from  APEX_JobCard as jc   	  							 			   "
        sql &= "	Inner join APEX_Notifications as nt on jc.RefBriefID=nt.AssociateID   	  							   			   "
        sql &= "	left outer join APEX_ActivityType  as At on Jc.PrimaryActivityID=At.ProjectTypeID "
        sql &= "	join dbo.APEX_Recieptents as rec on nt.notificationID=rec.refnotificationID   	  							   			   "
        sql &= "	inner join APEX_Brief as b on jc.RefBriefID = b.BriefID   	  							   			   "
        sql &= "	inner join APEX_Clients as Client on jc.refclientID= client.ClientID	  							    			   "
        sql &= "	inner join Apex_Estimate as Estimate on b.BriefID=Estimate.RefBriefID	  							   			   "
        'sql &= "	Left Join Apex_task T on JC.jobcardID=T.RefjobcardID	 							   			   "
        sql &= "	Left join Apex_taskAccount TA on JC.jobcardID=TA.RefjobcardID     							  			    "


        sql &= "			    where (ProjectManagerID=" & getLoggedUserID() & "  or  TA.TL=" & getLoggedUserID() & ")    "
        sql &= " "
        Dim Searchtype As String = ""
        Dim txtsearch As String = Request.QueryString("searchtxt").ToString()
        If Len(Request.QueryString("Searchtype")) > 0 Then
            If Request.QueryString("Searchtype").ToString() <> "" Then
                If Request.QueryString("Searchtype").ToString() <> "null" Then
                    Searchtype = Request.QueryString("Searchtype").ToString()
                Else
                    Searchtype = 0
                End If
            Else
                Searchtype = 0
            End If
        End If
        If Searchtype.ToString = "0" Then

            If txtsearch.ToString() <> "" Then
                sql &= " and jc.JobcardNo='" & txtsearch & "'"
            End If
        ElseIf Searchtype.ToString() = "1" Then
            If txtsearch <> "" Then
                sql &= " and Client= '" & txtsearch & "'"
            End If

        End If

        sql &= ""
        sql &= ""
        sql &= " order by jc.INsertedOn desc"


        Dim ds As New DataSet
        Dim dt As New DataTable
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt)
        End If
        Return jsonstring
    End Function

    Private Function FillNewsPage() As String
        Dim Sql As String = ""
        Dim jsonstring As String = ""
        Dim ds As DataSet = Nothing

        Dim uid As String = getLoggedUserID()

        Dim capex As New clsApex
        Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())


        Sql &= "SELECT [NEWSID],[Title],[ShortDesc],[Description],[NewsDate],[ImageURL],[Type],[InsertedOn],[ISactive],[ISDeleted] FROM [Apex_News] order by [NewsDate] desc"

        ds = ExecuteDataSet(Sql)

        If ds.Tables(0).Rows.Count > 0 Then
            jsonstring &= "<thead><tr>"
            jsonstring &= "<th>ID</th>"
            jsonstring &= "<th>Title</th>"
            jsonstring &= "<th>Short Desc</th>"
            jsonstring &= "<th>Type</th>"
            jsonstring &= "<th>News Date</th>"
            jsonstring &= "</tr></thead>"

            jsonstring &= "<tbody id='tblbody'>"
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                jsonstring &= "<tr>"
                jsonstring &= "<td>" & ds.Tables(0).Rows(i)("NewsID").ToString() & "</td>"
                jsonstring &= "<td>" & ds.Tables(0).Rows(i)("Title").ToString() & "</td>"
                jsonstring &= "<td>" & ds.Tables(0).Rows(i)("ShortDesc").ToString() & "</td>"
                jsonstring &= "<td>" & ds.Tables(0).Rows(i)("Type").ToString() & "</td>"
                jsonstring &= "<td>" & ds.Tables(0).Rows(i)("NewsDate").ToString() & "</td>"
                jsonstring &= "</tr>"

            Next

            jsonstring &= "</tbody>"
            'jsonstring = GetJSONString(ds.Tables(0))
        End If
        Return jsonstring
    End Function

    Public Function FillVendorDetails() As String
        Dim jsonstring As String = ""


        Dim sql As String = ""

        sql &= "	select ROW_NUMBER() over(order by VID desc) as row ,VID,"
        sql &= "	VendorNumber, Category, Name, Name2, Address, Address2, Phoneno, City, "
        sql &= "	State, Country, PIN, Contact, contact2, Email, website, CreditTeam, PANNo, ServiceTax, TIN, "
        sql &= "	CIN, KeyConcern, Remarks, InsertedOn, Isactive, IsDeleted, InsertedBy"
        sql &= "	from Apex_VendorFM"
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

End Class
