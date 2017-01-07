Imports clsDatabaseHelper
Imports clsMain
Imports System.Data
Imports clsApex

Partial Class AjaxCalls_AX_Leads
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim result As String = ""
        If Len(Request.QueryString("call")) > 0 Then
            If Request.QueryString("call").ToString() <> "" Then
                Dim callid As String = Request.QueryString("call").ToString()
                If callid = "1" Then
                    result = FillLead()
                End If

                If callid = "2" Then
                    Dim id As String = Request.QueryString("id").ToString()
                    result = ConvertLead(id)
                End If

                If callid = "3" Then
                    Dim cn As String = Request.QueryString("cn").ToString()
                    Dim pn As String = Request.QueryString("pn").ToString()
                    Dim kn As String = Request.QueryString("kn").ToString()
                    result = SearchLead(cn, pn, kn)
                End If
                If callid = "4" Then
                    result = Bindclient()
                End If
                If callid = "7" Then
                    result = BindActivity()
                End If
                If callid = "5" Then
                    If len(Request.QueryString("name")) > 0 And len(Request.QueryString("industry")) > 0 And len(Request.QueryString("turnover")) > 0 Then
                        Dim cid As Integer = 0

                        Dim name As String = Request.QueryString("name")
                        Dim industry As String = Request.QueryString("industry")
                        Dim turnover As String = Request.QueryString("turnover")
                        If CheckDuplicateClient(name) = False Then
                            result = AddClient(name, industry, turnover)
                        Else
                            result = "Client Already exist"
                        End If

                    Else
                        result = "Data Not Save"
                    End If
                End If
                If callid = "6" Then
                    If len(Request.QueryString("cid")) > 0 And len(Request.QueryString("name")) > 0 And len(Request.QueryString("email")) > 0 And len(Request.QueryString("mob")) > 0 Then
                        Dim cid As Integer = 0
                        cid = Request.QueryString("cid")
                        Dim name As String = Request.QueryString("name")
                        Dim email As String = Request.QueryString("email")
                        Dim mob As String = Request.QueryString("mob")
                        If CheckDuplicateContactPerson(email) = False Then
                            result = saveClientContact(cid, name, email, mob)
                        Else
                            result = "Contact Already exist with this email"
                        End If


                    Else
                        result = "Data Not Save"
                    End If

                End If
               
            End If

        End If

        Response.Write(result)
        Response.End()
    End Sub
    Private Function saveClientContact(ByVal cid As Integer, ByVal name As String, ByVal email As String, ByVal mob As String) As Boolean
        Dim flas As Boolean = False
        Dim sql As String = ""
        sql &= "INSERT INTO [APEX_ClientContacts]"
        sql &= "           ([RefClientID]"
        sql &= "           ,[ContactName]"
        sql &= "           ,[ContactOfficialEmailID]"
        sql &= "           ,[Mobile1]"
        sql &= "           ,[InsertedOn]"
        sql &= "           ,[InsertedBy]"
        sql &= "           )"
        sql &= "     VALUES"
        sql &= "           (" & Clean(cid) & ""
        sql &= "           ,'" & Clean(name) & "'"
        sql &= "           ,'" & Clean(email) & "'"
        sql &= "           ,'" & Clean(mob) & "'"
        sql &= "           ,getdate()"
        sql &= "           ," & getLoggedUserID() & ""
        sql &= "           )"
        If ExecuteNonQuery(sql) > 0 Then
            flas = True
        End If
        Return flas
    End Function
    Private Function AddClient(ByVal name As String, ByVal industry As String, ByVal turnover As String) As Boolean

        Dim flag As Boolean = False

        Dim sql As String = "Insert into APEX_Clients(Client,Industry,AnnualTurnover,InsertedBy) values ('" & Clean(name) & "',"

        sql &= "'" & Clean(industry) & "',"

        sql &= "'" & Clean(turnover) & "'"

        sql &= "," & getLoggedUserID() & ")"
        If ExecuteNonQuery(sql) > 0 Then
            flag = True

        End If

        Return flag
    End Function
    Private Function CheckDuplicateClient(ByVal name As String) As Boolean
        Dim result As Boolean = False
        
            Dim sql As String = "Select * from APEX_Clients where Client='" & Clean(name) & "'"
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                result = True
            End If

        Return result
    End Function
    Private Function CheckDuplicateContactPerson(ByVal email As String) As Boolean

        Dim result As Boolean = False
        Try
            Dim sql As String = "Select * from APEX_ClientContacts where ContactOfficialEmailID='" & email & "'"
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                result = True
            Else
                result = False
                
            End If
        Catch ex As Exception
            SendExMail(ex.Message, ex.ToString())
        End Try
        Return result
    End Function
    Private Function Bindclient() As String
        Dim jsonstring As String = ""
        Dim ds As New DataSet
        Dim sql As String = " Select Client,ClientID from APEX_clients where IsActive='Y' and IsDeleted='N' order by Client "

        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then

            jsonstring = GetJSONString(ds.Tables(0))
            Return jsonstring

        End If

        Return jsonstring

    End Function
    Private Function BindActivity() As String
        Dim jsonstring As String = ""
        Dim ds As New DataSet
        Dim sql As String = " Select ProjectType,ProjectTypeID from APEX_ActivityType where IsActive='Y' and IsDeleted='N'  "

        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then

            jsonstring = GetJSONString(ds.Tables(0))
            Return jsonstring

        End If

        Return jsonstring

    End Function
    Private Function FillLead() As String
        Dim capex As New clsApex
        Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())

        Dim jsonstring As String = ""
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim sql As String = " "
        sql &= "     select Distinct l.leadID,RefLeadID,Client,at.ProjectType as ActivityType,isnull(jc.JobCardNo,'N/A') as JobCardNo "
        sql &= "  ,ContactName as ContactPerson,l.Budget,convert(varchar(10),ActivityDate,105) as ActivityDate  "
        sql &= "  ,Briefed = case when jc.IsBriefed='Y' then 'Generated' when BriefID is not null then 'Pending' Else 'N/A' End  "
        sql &= " ,link_brief = case when BriefID is not null then 'BriefManager.aspx?mode=edit&bid=' + convert(varchar(10),b.BriefID) else '' end ,BriefID    ,  "
        sql &= "  link=case when jc.IsPrePnL='Y' then 'OpenPrePnlManager.aspx?mode=edit&bid=' + convert(varchar(10),b.BriefID) + "
        sql &= "  '&kid=y' when jc.IsPrePnL='N' then 'RequestForprePnl.aspx?bid=' + convert(varchar(10),b.BriefID) + '' end,  "
        sql &= "  linkname= case when jc.IsPrePnL='Y' then 'Generated' when jc.IsPrePnL='N' then 'Pending' else 'N/A' end  , "
        sql &= "  linkname2= case when isclientapproval='Y' then 'Generated' when isclientapproval is null then 'Pending' else 'N/A' end  , "
        sql &= " linkestimate= case when jobcardno is not null then 'openestimate.aspx?bid='+ convert(varchar(10),b.briefid) + '' when jobcardno is null then 'estimate.aspx?bid='+ convert(varchar(10),b.briefid)+''  else '#' end , "
        sql &= "  link2 = case when (jc.isprepnl='y' and jc.isestimated='n' and (jc.jobcompleted='n' or  jc.jobcompleted is null)) "
        sql &= "  then 'estimate.aspx?bid='+ convert(varchar(10),b.briefid)+''  "
        sql &= "  when (jc.isprepnl='y' and jc.isestimated='y' and (jc.jobcompleted='n' or  jc.jobcompleted is null) ) then  "
        sql &= "  'openestimate.aspx?bid='+ convert(varchar(10),b.briefid) + ''  when(jc.isprepnl='y' and jc.isestimated='y' "
        sql &= "  and (jc.jobcompleted='y' ) ) then ' estimate.aspx?bid= '+ convert(varchar(10),b.briefid) + ''  end"
        'sql &= "  link2 = 'OpenEstimate.aspx?bid='+ convert(varchar(10),b.BriefID)+''"
        sql &= "  ,et.EstimateID,  "
        'sql &= "  ,linkname2 = "
        'sql &= "  case when jc.IsEstimated='Y' then 'Generated' when  jc.IsPrePnL='Y' then 'Pending' else'N/A' End, "
        sql &= "  EstimateID ,isnull(FirstName,'') + ' '+ isnull(LastName,'') as KAMName ,LeadStatus=case  "
        sql &= "  when JobCardNo is null then 'Open' else 'Closed' End,LeadName,(case when l.insertedBy=" & getLoggedUserID() & " then 'Y' else 'N' end)ismyproject  From APEX_Leads as l   "
        sql &= "  left outer join APEX_Brief as b  on l.leadid=b.REfLeadID    "
        sql &= "  left outer join APEX_Clients as cl on l.RefClientID = cl.ClientID  "
        sql &= "  left outer join APEX_ClientContacts as cs on l.RefClientContactID=cs.ContactID     "
        sql &= "  left outer Join APEX_ActivityType as at on l.PrimaryActivityID = at.ProjectTypeID "
        sql &= "  left outer Join APEX_JobCard as jc on b.BriefID=jc.RefBriefID    "
        sql &= "  Left Outer Join APEX_Estimate as et on b.BriefID=et.RefBriefID  "
        sql &= "  left outer Join APEX_UsersDetails as ud on l.InsertedBy = ud.UserDetailsID "

        If role <> "A" And role <> "H" Then
            sql &= " where l.IsActive = 'Y' and l.IsDeleted = 'N' and l.insertedBy=" & getLoggedUserID() & " or l.insertedBy in(" & capex.getuserdetailsID(getLoggedUserID()) & ")   order by l.leadid Desc  "
        Else
            sql &= " where l.IsActive = 'Y' and l.IsDeleted = 'N'    order by l.leadid Desc  "
        End If

        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt)
            Return jsonstring
        End If

        Return jsonstring
    End Function

    Private Function ConvertLead(ByVal id As String) As String
        Dim result As Boolean = True
        Dim jsonstring As String = ""
        Dim dt As New DataTable
        Dim ds As New DataSet

        Dim leadSql As String = "Select * from APEX_Leads where LeadId=" & id

        Dim RefLeadID, PrimaryActivityID, ActivityTypeID, RefClientID, Budget, Lead, RefContactPersonID As String

        ds = ExecuteDataSet(leadSql)
        If ds.Tables(0).Rows.Count > 0 Then
            RefLeadID = id
            PrimaryActivityID = ds.Tables(0).Rows(0)("PrimaryActivityID").ToString()
            ActivityTypeID = ds.Tables(0).Rows(0)("RefActivityTypeID").ToString()
            RefClientID = ds.Tables(0).Rows(0)("RefClientID").ToString()
            Budget = ds.Tables(0).Rows(0)("Budget").ToString()
            Lead = ds.Tables(0).Rows(0)("LeadName").ToString()
            RefContactPersonID = ds.Tables(0).Rows(0)("RefClientContactID").ToString()

            Dim sql As String = "insert into APEX_Brief(RefLeadID,PrimaryActivityID,RefActivityTypeID,RefClientID,Budget,BriefName,RefContactPersonID,insertedBy) Values ("
            sql &= RefLeadID

            If PrimaryActivityID <> "" Then
                sql &= "," & PrimaryActivityID
            Else
                sql &= ",NULL"
            End If

            If ActivityTypeID <> "" Then
                sql &= ",'" & ActivityTypeID & "'"
            Else
                sql &= ",NULL"
            End If

            If RefClientID <> "" Then
                sql &= "," & RefClientID
            Else
                sql &= ",NULL"
            End If

            If Budget <> "" Then
                sql &= "," & Budget
            Else
                sql &= ",NULL"
            End If

            sql &= ",'" & Clean(Lead) & "'"

            If RefContactPersonID <> "" Then
                sql &= "," & RefContactPersonID
            Else
                sql &= ",NULL"
            End If
            sql &= "," & getLoggedUserID()
            sql &= ") Update APEX_Leads set IsBriefed='Y' where LeadID=" & RefLeadID
            If ExecuteNonQuery(sql) > 0 Then
                Dim sql1 As String = ""
                sql1 &= " select briefID from APEX_Brief where refLeadID=" & RefLeadID
                ds = ExecuteDataSet(sql1)
                If ds.Tables(0).Rows.Count > 0 Then
                    'Response.Redirect("BriefManager.aspx?mode=edit&bid=" & ds.Tables(0).Rows(0)("briefID"))
                    dt = ds.Tables(0)
                    jsonstring = GetJSONString(dt)
                    Return jsonstring
                End If

            End If
        End If
        Return jsonstring
    End Function

    'Private Function ConvertLead(ByVal id As String) As Boolean
    '    Dim result As Boolean = True

    '    Dim ds As New DataSet
    '    Dim leadSql As String = "Select * from APEX_Leads where LeadId=" & id

    '    Dim RefLeadID, PrimaryActivityID, ActivityTypeID, RefClientID, Budget, Lead, RefContactPersonID As String

    '    ds = ExecuteDataSet(leadSql)
    '    If ds.Tables(0).Rows.Count > 0 Then
    '        RefLeadID = id
    '        PrimaryActivityID = ds.Tables(0).Rows(0)("PrimaryActivityID").ToString()
    '        ActivityTypeID = ds.Tables(0).Rows(0)("RefActivityTypeID").ToString()
    '        RefClientID = ds.Tables(0).Rows(0)("RefClientID").ToString()
    '        Budget = ds.Tables(0).Rows(0)("Budget").ToString()
    '        Lead = ds.Tables(0).Rows(0)("LeadName").ToString()
    '        RefContactPersonID = ds.Tables(0).Rows(0)("RefClientContactID").ToString()

    '        Dim sql As String = "insert into APEX_Brief(RefLeadID,PrimaryActivityID,RefActivityTypeID,RefClientID,Budget,BriefName,RefContactPersonID,insertedBy) Values ("
    '        sql &= RefLeadID

    '        If PrimaryActivityID <> "" Then
    '            sql &= "," & PrimaryActivityID
    '        Else
    '            sql &= ",NULL"
    '        End If

    '        If ActivityTypeID <> "" Then
    '            sql &= ",'" & ActivityTypeID & "'"
    '        Else
    '            sql &= ",NULL"
    '        End If

    '        If RefClientID <> "" Then
    '            sql &= "," & RefClientID
    '        Else
    '            sql &= ",NULL"
    '        End If

    '        If Budget <> "" Then
    '            sql &= "," & Budget
    '        Else
    '            sql &= ",NULL"
    '        End If

    '        sql &= ",'" & Clean(Lead) & "'"

    '        If RefContactPersonID <> "" Then
    '            sql &= "," & RefContactPersonID
    '        Else
    '            sql &= ",NULL"
    '        End If
    '        sql &= "," & getLoggedUserID()
    '        sql &= ") Update APEX_Leads set IsBriefed='Y' where LeadID=" & RefLeadID
    '        If ExecuteNonQuery(sql) > 0 Then
    '            Dim sql1 As String = ""
    '            sql1 &= " select briefID from APEX_Brief where refLeadID=" & RefLeadID
    '            ds = ExecuteDataSet(sql1)
    '            If ds.Tables(0).Rows.Count > 0 Then
    '                Response.Redirect("BriefManager.aspx?mode=edit&bid=" & ds.Tables(0).Rows(0)("briefID"))
    '            End If
    '            result = True
    '        End If
    '    End If
    '    Return result
    'End Function

    Private Function SearchLead(ByVal cn As String, ByVal pn As String, ByVal kn As String) As String
        Dim capex As New clsApex
        Dim role As String = capex.GetRoleNameByUserID(getLoggedUserID())

        Dim jsonstring As String = ""
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim sql As String = " "
        'sql &= "     select Distinct l.leadID,RefLeadID,Client,at.ProjectType as ActivityType,isnull(jc.JobCardNo,'N/A') as JobCardNo    "
        'sql &= "  ,ContactName as ContactPerson,l.Budget,convert(varchar(10),ActivityDate,105) as ActivityDate  "
        'sql &= "  ,Briefed = case when jc.IsBriefed='Y' then 'Generated' when BriefID is not null then 'Pending' Else 'N/A' End  "
        'sql &= " ,link_brief = case when BriefID is not null then 'BriefManager.aspx?mode=edit&bid=' + convert(varchar(10),b.BriefID) else '' end ,BriefID    ,  "
        'sql &= "  link=case when jc.IsPrePnL='Y' then 'OpenPrePnlManager.aspx?mode=edit&bid=' + convert(varchar(10),b.BriefID) + "
        'sql &= "  '&kid=y' when jc.IsPrePnL='N' then 'RequestForprePnl.aspx?bid=' + convert(varchar(10),b.BriefID) + '' end,  "
        ''sql &= "  linkname= case when jc.IsPrePnL='Y' then 'Generated' when jc.IsPrePnL='N' then 'Pending' else 'N/A' end  , "
        'sql &= "  linkname= case when isclientapproval='Y' then 'Generated' when isclientapproval is null then 'Pending' else 'N/A' end , "
        ''sql &= "  link2 = 'OpenEstimate.aspx?bid='+ convert(varchar(10),b.BriefID)+''"
        'sql &= "  link2 = case when (jc.IsPrePnL='Y' and jc.IsEstimated='N' and (jc.jobCompleted='N' or  jc.jobCompleted is null)) "
        'sql &= "  then 'Estimate.aspx?bid='+ convert(varchar(10),b.BriefID)+''  "
        'sql &= "  when (jc.IsPrePnL='Y' and jc.IsEstimated='Y' and (jc.jobCompleted='N' or  jc.jobCompleted is null) ) then  "
        'sql &= "  'OpenEstimate.aspx?bid='+ convert(varchar(10),b.BriefID) + ''  when(jc.IsPrePnL='Y' and jc.IsEstimated='Y' "
        'sql &= "  and (jc.jobCompleted='Y' ) ) then ' Estimate.aspx?bid= '+ convert(varchar(10),b.BriefID) + ''  "
        'sql &= "  End,"
        'sql &= "  ,et.EstimateID  "
        'sql &= "  ,linkname2 = "
        'sql &= "  case when jc.IsEstimated='Y' then 'Generated' when  jc.IsPrePnL='Y' then 'Pending' else'N/A' "
        'sql &= "  End,EstimateID ,isnull(FirstName,'') + ' '+ isnull(LastName,'') as KAMName ,LeadStatus=case  "
        'sql &= "  when JobCardNo is null then 'Open' else 'Closed' End  From APEX_Leads as l   "
        'sql &= "  left outer join APEX_Brief as b  on l.leadid=b.REfLeadID    "
        'sql &= "  left outer join APEX_Clients as cl on l.RefClientID = cl.ClientID  "
        'sql &= "  left outer join APEX_ClientContacts as cs on l.RefClientContactID=cs.ContactID     "
        'sql &= "  left outer Join APEX_ActivityType as at on l.PrimaryActivityID = at.ProjectTypeID "
        'sql &= "  left outer Join APEX_JobCard as jc on b.BriefID=jc.RefBriefID    "
        'sql &= "  Left Outer Join APEX_Estimate as et on b.BriefID=et.RefBriefID  "
        'sql &= "  left outer Join APEX_UsersDetails as ud on l.InsertedBy = ud.UserDetailsID "

        sql &= "     select Distinct l.leadID,RefLeadID,Client,at.ProjectType as ActivityType,isnull(jc.JobCardNo,'N/A') as JobCardNo "
        sql &= "  ,ContactName as ContactPerson,l.Budget,convert(varchar(10),ActivityDate,105) as ActivityDate  "
        sql &= "  ,Briefed = case when jc.IsBriefed='Y' then 'Generated' when BriefID is not null then 'Pending' Else 'N/A' End  "
        sql &= " ,link_brief = case when BriefID is not null then 'BriefManager.aspx?mode=edit&bid=' + convert(varchar(10),b.BriefID) else '' end ,BriefID    ,  "
        sql &= "  link=case when jc.IsPrePnL='Y' then 'OpenPrePnlManager.aspx?mode=edit&bid=' + convert(varchar(10),b.BriefID) + "
        sql &= "  '&kid=y' when jc.IsPrePnL='N' then 'RequestForprePnl.aspx?bid=' + convert(varchar(10),b.BriefID) + '' end,  "
        sql &= "  linkname= case when jc.IsPrePnL='Y' then 'Generated' when jc.IsPrePnL='N' then 'Pending' else 'N/A' end  , "
        sql &= "  linkname2= case when isclientapproval='Y' then 'Generated' when isclientapproval is null then 'Pending' else 'N/A' end  , "
        sql &= " linkestimate= case when isclientapproval='Y' then 'openestimate.aspx?bid='+ convert(varchar(10),b.briefid) + '' when isclientapproval is null then 'estimate.aspx?bid='+ convert(varchar(10),b.briefid)+''  else '#' end , "
        sql &= "  link2 = case when (jc.isprepnl='y' and jc.isestimated='n' and (jc.jobcompleted='n' or  jc.jobcompleted is null)) "
        sql &= "  then 'estimate.aspx?bid='+ convert(varchar(10),b.briefid)+''  "
        sql &= "  when (jc.isprepnl='y' and jc.isestimated='y' and (jc.jobcompleted='n' or  jc.jobcompleted is null) ) then  "
        sql &= "  'openestimate.aspx?bid='+ convert(varchar(10),b.briefid) + ''  when(jc.isprepnl='y' and jc.isestimated='y' "
        sql &= "  and (jc.jobcompleted='y' ) ) then ' estimate.aspx?bid= '+ convert(varchar(10),b.briefid) + ''  end"
        'sql &= "  link2 = 'OpenEstimate.aspx?bid='+ convert(varchar(10),b.BriefID)+''"
        sql &= "  ,et.EstimateID,  "
        'sql &= "  ,linkname2 = "
        'sql &= "  case when jc.IsEstimated='Y' then 'Generated' when  jc.IsPrePnL='Y' then 'Pending' else'N/A' End, "
        sql &= "  EstimateID ,isnull(FirstName,'') + ' '+ isnull(LastName,'') as KAMName ,LeadStatus=case  "
        sql &= "  when JobCardNo is null then 'Open' else 'Closed' End  From APEX_Leads as l   "
        sql &= "  left outer join APEX_Brief as b  on l.leadid=b.REfLeadID    "
        sql &= "  left outer join APEX_Clients as cl on l.RefClientID = cl.ClientID  "
        sql &= "  left outer join APEX_ClientContacts as cs on l.RefClientContactID=cs.ContactID     "
        sql &= "  left outer Join APEX_ActivityType as at on l.PrimaryActivityID = at.ProjectTypeID "
        sql &= "  left outer Join APEX_JobCard as jc on b.BriefID=jc.RefBriefID    "
        sql &= "  Left Outer Join APEX_Estimate as et on b.BriefID=et.RefBriefID  "
        sql &= "  left outer Join APEX_UsersDetails as ud on l.InsertedBy = ud.UserDetailsID "

        If role <> "A" And role <> "H" Then
            sql &= " where l.IsActive = 'Y' and l.IsDeleted = 'N' and l.insertedBy=" & getLoggedUserID() & " and Client like '%" & cn & "%' and FirstName like '%" & kn & "%' and at.ProjectType like '%" & pn & "%' order by l.leadid Desc  "
        Else
            sql &= " where l.IsActive = 'Y' and l.IsDeleted = 'N' and Client like '%" & cn & "%' and FirstName like '%" & kn & "%' and at.ProjectType like '%" & pn & "%'   order by l.leadid Desc  "
        End If

        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            dt = ds.Tables(0)
            jsonstring = GetJSONString(dt)
            Return jsonstring

        End If

        Return jsonstring
    End Function

   
End Class
