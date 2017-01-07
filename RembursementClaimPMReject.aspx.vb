Imports clsApex
Imports clsMain
Imports clsDatabaseHelper
Imports System.Data
Imports System

Partial Class RembursementClaimApproval
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            divError.Visible = False
            btnExceed.Visible = False
            divError.Attributes.CssStyle.Add("display", "none")
            ' btnExceed.Attributes.CssStyle.Add("display", "none")
            If Len(Request.QueryString("nid")) > 0 Then
                If Request.QueryString("nid").ToString <> Nothing Then
                    hdnNotificationID.Value = Request.QueryString("nid").ToString()
                    If Len(Request.QueryString("type")) > 0 Then
                        If Request.QueryString("type").ToString() <> Nothing Then
                            hdnType.Value = Request.QueryString("type").ToString()
                            If Len(Request.QueryString("aid")) > 0 Then
                                If Request.QueryString("aid").ToString() <> Nothing Then
                                    hdnID.Value = Request.QueryString("aid").ToString()
                                    fillDetails()
                                    fillClaims()
                                    fillalldetails()
                                    'BindColletral()
                                    Dim capex As New clsApex
                                    capex.UpdateRecieptentSeen(hdnNotificationID.Value, getLoggedUserID())
                                    CheckAccountBalance()

                                    If Len(Request.QueryString("mode")) > 0 Then
                                        If Request.QueryString("mode") <> Nothing Then
                                            If Request.QueryString("mode") = "app" Or Request.QueryString("mode") = "rej" Or Request.QueryString("mode") = "exc" Then
                                                capex.UpdateRecieptentSeen(hdnNotificationID.Value, getLoggedUserID)
                                                capex.UpdateRecieptentExecuted(hdnNotificationID.Value, getLoggedUserID)
                                            End If
                                            btnExceed.Visible = False
                                            btnApproval.Visible = False
                                            btnRejected.Visible = False


                                        End If
                                    End If
                                Else
                                    CallDivError()
                                End If
                            Else
                                CallDivError()
                            End If
                        Else
                            CallDivError()
                        End If
                    Else
                        CallDivError()
                    End If
                Else
                    CallDivError()
                End If
            Else
                CallDivError()
            End If
        End If
    End Sub

    Private Sub fillDetails()

        Dim sql As String = "Select ta.*,clm.ClaimFile,clm.SubmittedRemarks from APEX_TAskAccount "
        sql &= " as ta "
        sql &= "  join APEX_ClaimMaster  as clm on ta.AccountID=clm.RefTaskID"
        sql &= "  where ClaimMasterID = " & hdnID.Value
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            lblTaskTitle.Text = ds.Tables(0).Rows(0)("Taskname").ToString()
            hdnTaskID.Value = ds.Tables(0).Rows(0)("AccountID").ToString()
            If ds.Tables(0).Rows(0)("ClaimFile").ToString() <> "" Then
                lblfulpl_Mail.NavigateUrl = ds.Tables(0).Rows(0)("ClaimFile").ToString()
            Else
                lblfulpl_Mail.Text = "N/A"
            End If
            lblSRemarks.Text = ds.Tables(0).Rows(0)("SubmittedRemarks").ToString()
        End If

        Dim jobcardid As String
        Dim capex As New clsApex
        Dim tid As String = ""
        tid = getTaskIDbyAccountID(hdnTaskID.Value)
        'jobcardid = capex.GetJobCardByTaskID(tid)
        jobcardid = tid


        Dim sql2 As String = "Select JobCardNo  from APEX_JobCard where JobCardID = " & jobcardid
        Dim ds2 As New DataSet
        ds2 = ExecuteDataSet(sql2)
        If ds2.Tables(0).Rows.Count > 0 Then
            lblJobCode.Text = ds2.Tables(0).Rows(0)(0).ToString()
        End If
    End Sub

    Private Sub fillClaims()
        Dim sql As String = ""

        'sql = "Select ROW_NUMBER() over(order by ClaimMasterID desc) as ID,* from APEX_ClaimMaster as cm right outer join APEX_ClaimTransaction as ct on cm.ClaimMasterID=ct.RefClaimID where cm.RefTaskID =" & hdnTaskID.Value & " and ClaimMasterID=" & hdnID.Value
        ''sql += " select isnull((select categoryName from Apex_ClaimsCategory where ID=B.RefExpe_category),'')Exp_Type,ClaimTransactionID,B.*,A.Budge as [Totalbudget],A.Expence as [Allowbudget],A.CategoryTask,A.Name As [Working Person] from ("
        ''sql += "  Select  AccountID,TaskID,Title,convert(varchar(10),ta.StartDate,105) as  startdate, convert(varchar(10),ta.enddate,105)"
        ''sql += "  as enddate,categoryTask,ta.Description,  TaskCompleted =case when ta.TaskCompleted='Y' then 'Completed' else 'Pending' end  , "
        ''sql += "   Name = case when (isnull(ud.firstName,'')+' '+isnull(ud.LastName,'')) <>'' then  (isnull(ud.firstName,'')+' '+isnull(ud.LastName,'')) else 'N/A' end ,"
        ''sql += "  (select isnull(sum(PreEventTotal),0) as PreEventQuote from APEX_PrePnLcost where category=categoryTask and refbriefID="
        ''sql += "  (select refbriefID from apex_jobcard where jobcardid=(select jobcardNO from APEX_ClaimMaster where claimmasterID=" & hdnID.Value & ")))Budge"
        ''sql += "  ,Total as Expence    from APEX_Task as t "
        ''sql += "    join APEX_TaskAccount as ta on t.taskid=ta.refTaskID "
        ''sql += "  left outer join APEX_UsersDetails as ud on  ud.UserDetailsID=ta.TL where RefJobcardID=(select jobcardNO from APEX_ClaimMaster where claimmasterID=" & hdnID.Value & ")"
        ''sql += "  )A"
        ''sql += " inner join ( Select ROW_NUMBER() over(order by ClaimMasterID desc) as ID,*,(case when ISapproved='Y' then 0 else Amount end)RequestAmt,(case when ISapproved Is Null then 0 else Amount end)ApprovedAmt from APEX_ClaimMaster as cm "
        ''sql += " right outer join APEX_ClaimTransaction as ct on cm.ClaimMasterID=ct.RefClaimID where cm.RefTaskID =" & hdnTaskID.Value & " and jobcardNo=(select jobcardNO from APEX_ClaimMaster where claimmasterID=" & hdnID.Value & ") and claimmasterID=" & hdnID.Value & ")B on A.AccountID=B.RefTaskID"
        'sql += " right outer join APEX_ClaimTransaction as ct on cm.ClaimMasterID=ct.RefClaimID where cm.RefTaskID =" & hdnTaskID.Value & " and ClaimMasterID=" & hdnID.Value & ")B on A.AccountID=B.RefTaskID"
        sql += ""
        sql += " select isnull((select categoryName from Apex_ClaimsCategory where ID=B.RefExpe_category),'')Exp_Type,"
        sql += " ClaimTransactionID,B.*,A.Budge as [Totalbudget],A.Expence as [Allowbudget],A.Category,A.Name As [Working Person] "
        sql += " from (  Select  AccountID,TaskName as Title,convert(varchar(10),ta.StartDate,105) as  startdate, "
        sql += " convert(varchar(10),ta.enddate,105)  as enddate,category,ta.Description,  "
        sql += " TaskCompleted =case when ta.TaskCompleted='Y' then 'Completed' else 'Pending' end  ,    "
        sql += " Name = case when (isnull(ud.firstName,'')+' '+isnull(ud.LastName,'')) <>'' then  "
        sql += " (isnull(ud.firstName,'')+' '+isnull(ud.LastName,'')) else 'N/A' end ,  (select isnull(sum(PreEventTotal),0) as PreEventQuote "
        sql += " from APEX_PrePnLcost where category=category and refbriefID=  (select refbriefID from apex_jobcard "
        sql += " where jobcardid=(select jobcardNO from APEX_ClaimMaster where claimmasterID=" & hdnID.Value & ")))Budge,Total as Expence    "
        sql += " from APEX_TaskAccount as ta     "
        sql += "  left outer join APEX_UsersDetails as ud on  ud.UserDetailsID=ta.TL "
        sql += "  where Ta.RefJobcardID=(select jobcardNO from APEX_ClaimMaster where claimmasterID=" & hdnID.Value & ")  )A "
        sql += "  inner join ( Select ROW_NUMBER() over(order by ClaimMasterID desc) as ID,*,"
        sql += "  (case when ISapproved='Y' then 0 else Amount end)RequestAmt,(case when ISapproved Is Null then 0 else Amount end)ApprovedAmt"
        sql += "   from APEX_ClaimMaster as cm  right outer join APEX_ClaimTransaction as ct on cm.ClaimMasterID=ct.RefClaimID "
        sql += "   where cm.RefTaskID =" & hdnTaskID.Value & " and jobcardNo=(select jobcardNO from APEX_ClaimMaster where claimmasterID=" & hdnID.Value & ") and"
        sql += "    claimmasterID=" & hdnID.Value & ")B on A.AccountID=B.RefTaskID"
        sql += ""
        sql += ""
        sql += ""
        sql += ""
        sql += ""
        sql += ""


        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)

        gdvClaims.DataSource = ds
        gdvClaims.DataBind()
    End Sub

    Protected Sub gdvClaims_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gdvClaims.PageIndexChanging
        gdvClaims.PageIndex = e.NewPageIndex
        fillClaims()
    End Sub
    Private Sub CallDivError()
        divContent.Visible = False
        divError.Visible = True
        Dim capex As New clsApex
        lblError.Text = capex.SetErrorMessage()
    End Sub
    Protected Sub btnApproval_Click(sender As Object, e As EventArgs) Handles btnApproval.Click
        Dim capex As New clsApex
        capex.UpdateRecieptentExecuted(hdnNotificationID.Value, getLoggedUserID())
        Dim sql As String = "update APEX_ClaimMaster set "
        sql &= " IsApproved = 'Y'"
        sql &= ",ApprovedOn = getdate()"
        sql &= ",ApprovedBy = " & getLoggedUserID()
        sql &= ",ApprovalRemarks = '" & Clean(txtRemarks.Text) & "'"
        sql &= " where ClaimMasterID = " & hdnID.Value
        'ClaimMasterID = " & hdnID.Value"
        'jobcardNo=(select jobcardNO from APEX_ClaimMaster where claimmasterID=" & hdnID.Value & 

        If ExecuteNonQuery(sql) > 0 Then
            'capex.UpdateRecieptentExecuted(hdnNotificationID.Value, getLoggedUserID())
            SendForApprovalClaim(hdnID.Value, NotificationType.ClaimApproved)
            Dim grow As GridViewRow
            Dim sql1 As String = ""
            For Each grow In gdvClaims.Rows
                Dim txtapprovalamt As New TextBox
                Dim hdnid As New HiddenField
                hdnid = grow.FindControl("hdnid")
                txtapprovalamt = grow.FindControl("txtapprovalamt")
                sql1 &= "update APEX_ClaimTransaction set Amount ='" & txtapprovalamt.Text & "' where ClaimTransactionID =" & hdnid.Value
                sql1 &= "   "
            Next
            ExecuteNonQuery(sql1)
            ' Response.Redirect("Home.aspx?mode=task")
            MessageDiv.Visible = True
            Label2.Text = "Approved successfully."
            divContent.Visible = False
        End If
    End Sub

    Protected Sub btnRejected_Click(sender As Object, e As EventArgs) Handles btnRejected.Click
        Dim capex As New clsApex
        capex.UpdateRecieptentExecuted(hdnNotificationID.Value, getLoggedUserID())
        Dim sql As String = "update APEX_ClaimMaster set "
        sql &= " IsApproved = 'C'"
        sql &= ",ApprovedOn = getdate()"
        sql &= ",ApprovedBy = " & getLoggedUserID()
        sql &= ",ApprovalRemarks = '" & txtRemarks.Text & "'"
        sql &= " where ClaimMasterID=" & hdnID.Value

        If ExecuteNonQuery(sql) > 0 Then

            ' Dim id As String = GetIDClaim()
            Dim amt As Double = GetClaimTotalAmount(hdnID.Value)
            'If hdnType.Value = "SubTask" Then
            '    capex.AddSubTaskBalance(hdnSubTaskID.Value, amt)
            'Else
            capex.UpdateRecieptentExecuted(hdnNotificationID.Value, getLoggedUserID())
            capex.AddTaskBalanceNew(hdnTaskID.Value, amt)
            'End If
            'SendForApprovalClaim(hdnID.Value, NotificationType.ClaimRejected)
            'Response.Redirect("Home.aspx?mode=task")
            'Response.Redirect(Request.Url.AbsolutePath)
            MessageDiv.Visible = True
            Label2.Text = "Cancel successfully."
            divContent.Visible = False
        End If
    End Sub

    Private Function GetClaimTotalAmount(ByVal ID As String) As Double
        Dim result As Double = 0
        Dim sql As String = "select SUM(Amount) as Amount from APEX_ClaimTransaction where RefClaimID =" & hdnID.Value
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = ds.Tables(0).Rows(0)(0).ToString()
        End If
        Return result
    End Function

    Private Function GetIDClaim() As String
        Dim result As String = ""
        Dim sql As String = "select RefTaskID from APEX_ClaimMaster where ClaimMasterID =" & hdnID.Value
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then

            result = ds.Tables(0).Rows(0)("RefTaskID").ToString()

        End If
        Return result
    End Function

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("Home.aspx")
    End Sub
    'Protected Sub btnsend_Click(sender As Object, e As EventArgs) Handles btnsend.Click
    '    CheckAccountBalance()
    'End Sub

    Private Sub CheckAccountBalance()
        'Dim sql As String = "select isnull(Balance,0) as Balance,isnull(Amount,0)Amount from APEX_TaskAccount where AccountID=" & hdnTaskID.Value
        Dim sql As String = ""

        sql += " select isnull(Balance,0) as Balance,Total as Allow,"
        sql += "  (select isnull(sum(PreEventTotal),0) as PreEventQuote from APEX_PrePnLcost where category=Apex_taskAccount.category and refbriefID="
        sql += "  (select refbriefID from apex_jobcard where jobcardid=(select jobcardNO from APEX_ClaimMaster where claimmasterID=" & hdnID.Value & ")))Total"
        sql += "  from  Apex_taskAccount where accountID=" & hdnTaskID.Value
        sql += ""
        sql += ""
        sql += ""
        sql += ""
        sql += ""
        sql += ""


        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then

            Dim bal As Double = Convert.ToDouble(ds.Tables(0).Rows(0)("Balance"))
            Dim Allow As Double = Convert.ToDouble(ds.Tables(0).Rows(0)("Allow"))
            Dim Total As Double = Convert.ToDouble(ds.Tables(0).Rows(0)("Total"))

            hdntotalpmallow.Value = Convert.ToDouble(ds.Tables(0).Rows(0)("Allow"))
            hdntotal.Value = Convert.ToDouble(ds.Tables(0).Rows(0)("Total"))
            'If bal < 0 Then
            '    'If lbltB.Text >= lblTotalApproval.Text Then
            '    If lbltB.Text >= lblTotalRequestedamt.Text Then
            '        btnExceed.Visible = False
            '        btnApproval.Visible = True 
            '        btnApproval.Text = "Request to Exceed Amount From PM"
            '    Else
            '        btnExceed.Visible = True
            '        'btnExceed.Enabled = False
            '        btnApproval.Visible = False
            '    End If

            'Else

            '    btnApproval.Visible = True
            '    btnExceed.Visible = False

            'End If

            txtapprovalamt_Text()
            'If bal < 0 Then
            '    If lbltB.Text >= lblTotalRequestedamt.Text Then
            '        btnExceed.Visible = False
            '        btnApproval.Visible = True
            '        btnApproval.Text = "Request to Exceed Amount From PM"
            '    Else
            '        btnExceed.Visible = True
            '        btnApproval.Visible = False
            '    End If

            'Else

            '    btnApproval.Visible = True
            '    btnExceed.Visible = False

            'End If
        End If
    End Sub

    Protected Sub btnExceed_Click(sender As Object, e As EventArgs) Handles btnExceed.Click

        Dim sql As String = "select refbriefID from apex_jobcard where jobcardid=(select jobcardNO from APEX_ClaimMaster where claimmasterID=" & hdnID.Value & ")"
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql.ToString())
        If ds.Tables(0).Rows.Count > 0 Then
            Response.Redirect("OpenPrePnlManager.aspx?mode=edit&bid=" & ds.Tables(0).Rows(0)("refbriefID") & "&kid=y")
        End If



        'Dim sqlBal As String = "select ABS(isnull(Balance,0)) as Balance from APEX_TaskAccount where AccountID=" & hdnTaskID.Value
        'Dim dsBal As New DataSet
        'dsBal = ExecuteDataSet(sqlBal)
        'If dsBal.Tables(0).Rows.Count > 0 Then
        '    Dim approvalKamamt As Double = "0.00"
        '    Dim grow As GridViewRow
        '    For Each grow In gdvClaims.Rows
        '        Dim txtapprovalamtKam As New TextBox
        '        txtapprovalamtKam = grow.FindControl("txtapprovalamtKam")
        '        approvalKamamt += txtapprovalamtKam.Text
        '    Next


        '    Dim sql As String = "INSERT INTO APEX_TaskExceedAmount"
        '    sql &= " (RefAccountID"
        '    sql &= " ,ExceedAmount"
        '    sql &= " ,RefClaimMasterID"
        '    sql &= " ,IsExceeded)"
        '    sql &= " VALUES"
        '    sql &= " (" & hdnTaskID.Value
        '    'sql &= " ," & dsBal.Tables(0).Rows(0)("Balance").ToString()
        '    'sql &= " ," & (lblTotalRequestedamt.Text - lbltB.Text)
        '    sql &= " ," & (approvalKamamt)
        '    sql &= " ," & hdnID.Value
        '    sql &= " ,'N')"
        '    If ExecuteNonQuery(sql) > 0 Then
        '        Dim clsapex As New clsApex
        '        clsapex.UpdateRecieptentExecuted(hdnNotificationID.Value, getLoggedUserID())
        '        SendForApprovalClaim(hdnTaskID.Value, NotificationType.ClaimExceedReq)


        '        'Response.Redirect("Home.aspx?mode=task")
        '        MessageDiv.Visible = True
        '        Label2.Text = "Request Sent to Kam For Exceed Amount."
        '        divContent.Visible = False
        '    End If
        'End If
    End Sub

    Public Sub SendForApprovalClaim(ByVal aid As String, ByVal type As String)
        If type = NotificationType.ClaimExceedReq Then
            Dim sql As String = "INSERT INTO APEX_Notifications"
            sql &= "(NotificationTitle"
            sql &= ",NotificationMessage"
            sql &= ",Severity"
            sql &= ",Type"
            sql &= ",AssociateID"
            sql &= ",StartDate"
            sql &= ",EndDate"
            sql &= ",SendSMS"
            sql &= ",SendEmail)"
            sql &= "VALUES"
            sql &= "('Request to exceed amount'"
            sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>KAM: </b>" & getKAMName() & "<br /><hr  /><b>Message: </b>You have been requested to exceed amount'"
            sql &= ",'H'"
            'If hdnSubTaskID.Value.Length > 0 Then
            '    sql &= "," & NotificationType.CLSA
            '    sql &= "," & hdnClaimMasterID.Value
            'Else
            sql &= "," & type
            sql &= "," & aid
            'End If
            sql &= ",getdate()"
            sql &= ",NULL"
            sql &= ",'N'"
            sql &= ",'N')"

            If ExecuteNonQuery(sql) > 0 Then
                InsertRecieptentDetails(type, aid)
            End If
        ElseIf type = NotificationType.ClaimApproved Then
            Dim sql As String = "INSERT INTO APEX_Notifications"
            sql &= "(NotificationTitle"
            sql &= ",NotificationMessage"
            sql &= ",Severity"
            sql &= ",Type"
            sql &= ",AssociateID"
            sql &= ",StartDate"
            sql &= ",EndDate"
            sql &= ",SendSMS"
            sql &= ",SendEmail)"
            sql &= "VALUES"
            sql &= "('Claim Approved'"
            sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>KAM: </b>" & getKAMName() & "<br /><hr  /><b>Message: </b>Approval for the Claim'"
            sql &= ",'H'"
            'If hdnSubTaskID.Value.Length > 0 Then
            '    sql &= "," & NotificationType.CLSA
            '    sql &= "," & hdnClaimMasterID.Value
            'Else
            sql &= "," & type
            sql &= "," & aid
            'End If
            sql &= ",getdate()"
            sql &= ",NULL"
            sql &= ",'N'"
            sql &= ",'N')"

            If ExecuteNonQuery(sql) > 0 Then
                InsertRecieptentDetails(type, aid)
            End If
        Else
            Dim sql As String = "INSERT INTO APEX_Notifications"
            sql &= "(NotificationTitle"
            sql &= ",NotificationMessage"
            sql &= ",Severity"
            sql &= ",Type"
            sql &= ",AssociateID"
            sql &= ",StartDate"
            sql &= ",EndDate"
            sql &= ",SendSMS"
            sql &= ",SendEmail)"
            sql &= "VALUES"
            sql &= "('Claim Rejected'"
            sql &= ",'<b>Project Name: </b>" & getProjectName() & "<br /><b>KAM: </b>" & getKAMName() & "<br /><hr  /><b>Message: </b>Rejection of the Claim'"
            sql &= ",'H'"
            'If hdnSubTaskID.Value.Length > 0 Then
            '    sql &= "," & NotificationType.CLSA
            '    sql &= "," & hdnClaimMasterID.Value
            'Else
            sql &= "," & type
            sql &= "," & aid
            'End If
            sql &= ",getdate()"
            sql &= ",NULL"
            sql &= ",'N'"
            sql &= ",'N')"

            If ExecuteNonQuery(sql) > 0 Then
                InsertRecieptentDetails(type, aid)
            End If
        End If
    End Sub

    Private Sub InsertRecieptentDetails(ByVal type As String, ByVal aid As String)

        If hdnNotificationID.Value = "" Or NotificationType.ClaimExceedReq Then
            Dim sql As String
            'If hdnSubTaskID.Value.Length > 0 Then
            '    sql = "Select top(1) NotificationID from APEX_Notifications where Type=" & NotificationType.CLSA & " and AssociateID=" & hdnClaimMasterID.Value & " order by NotificationID desc"
            'Else
            Dim title As String = ""
            Dim message As String = ""
            sql = "Select  top(1) NotificationID,NotificationTitle,NotificationMessage from APEX_Notifications where Type=" & type & " and AssociateID=" & aid & "  order by NotificationID desc"
            'End If
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)

            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0)("NotificationID")) Then
                    hdnNotificationID.Value = ds.Tables(0).Rows(0)("NotificationID").ToString()
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0)("NotificationTitle")) Then
                    title = ds.Tables(0).Rows(0)("NotificationTitle").ToString()
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0)("NotificationMessage")) Then
                    message = ds.Tables(0).Rows(0)("NotificationMessage").ToString()
                End If
            End If

            Dim bheadid As String = ""
            Dim sql3 As String = ""

            If type <> NotificationType.ClaimExceedReq Then
                If hdnID.Value <> "" Then
                    sql3 = "Select SubmittedUserID from  APEX_CLaimMaster  where ClaimMasterID = " & aid
                Else
                    bheadid = 0
                End If

                Dim ds3 As New DataSet
                ds3 = ExecuteDataSet(sql3)
                If ds3.Tables(0).Rows.Count > 0 Then
                    bheadid = ds3.Tables(0).Rows(0)(0).ToString()
                End If

                Dim sql1 As String = "INSERT INTO APEX_Recieptents"
                sql1 &= " (RefNotificationID"
                sql1 &= ",UserID"
                sql1 &= ",IsSeen"
                sql1 &= ",IsHidden"
                sql1 &= ",IsExecuted"
                sql1 &= ",SMSSentOn"
                sql1 &= ",EmailSentOn"
                sql1 &= ",InsertedBy)"
                sql1 &= "VALUES"
                sql1 &= "(" & hdnNotificationID.Value
                sql1 &= "," & bheadid
                sql1 &= ",'N'"
                sql1 &= ",'N'"
                sql1 &= ",'N'"
                sql1 &= ",NULL"
                sql1 &= ",NULL"
                sql1 &= "," & getLoggedUserID() & ")"

                If ExecuteNonQuery(sql1) > 0 Then
                    Dim emailid As String = ""
                    If bheadid <> "" Then
                        Dim uid As Integer = Convert.ToInt32(bheadid)
                        emailid = GetEmailIDByUserID(uid)
                    End If

                    If emailid <> "" Then
                        sendMail(title, message, "", emailid, "")
                    End If

                End If
            Else
                Dim sql4 As String = ""
                If hdnID.Value <> "" Then

                    sql4 = "select bf.InsertedBy from APEX_Brief as bf"
                    sql4 &= " Inner Join APEX_JobCard as jc on bf.BriefID= jc.RefBriefID "
                    sql4 &= " inner join APEX_ClaimMaster as cm on jc.JobCardID = cm.JobCardNo where ClaimMasterID = " & hdnID.Value
                Else
                    bheadid = 0
                End If

                Dim ds4 As New DataSet
                ds4 = ExecuteDataSet(sql4)
                If ds4.Tables(0).Rows.Count > 0 Then
                    bheadid = ds4.Tables(0).Rows(0)(0).ToString()
                End If

                Dim sql1 As String = "INSERT INTO APEX_Recieptents"
                sql1 &= " (RefNotificationID"
                sql1 &= ",UserID"
                sql1 &= ",IsSeen"
                sql1 &= ",IsHidden"
                sql1 &= ",IsExecuted"
                sql1 &= ",SMSSentOn"
                sql1 &= ",EmailSentOn"
                sql1 &= ",InsertedBy)"
                sql1 &= "VALUES"
                sql1 &= "(" & hdnNotificationID.Value
                sql1 &= "," & bheadid
                sql1 &= ",'N'"
                sql1 &= ",'N'"
                sql1 &= ",'N'"
                sql1 &= ",NULL"
                sql1 &= ",NULL"
                sql1 &= "," & getLoggedUserID() & ")"

                If ExecuteNonQuery(sql1) > 0 Then
                    Dim emailid As String = ""
                    If bheadid <> "" Then
                        Dim uid As Integer = Convert.ToInt32(bheadid)
                        emailid = GetEmailIDByUserID(uid)
                    End If

                    If emailid <> "" Then
                        sendMail(title, message, "", emailid, "")
                    End If

                End If
            End If
        Else
            If type = NotificationType.ClaimExceedReq Then

            End If
        End If
    End Sub

    Private Function getProjectName() As String
        Dim tid As String = getTaskIDbyAccountID(hdnTaskID.Value)
        Dim capex As New clsApex
        Dim jid As String = capex.GetJobCardByTaskID(tid)
        Dim bid As String = capex.GetBriefIDByJobCardID(jid)
        Dim result As String = ""
        Dim sql As String = "Select BriefName from APEX_Brief where BriefID=" & bid
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = ds.Tables(0).Rows(0)("BriefName").ToString()
        End If
        Return result
    End Function

    Private Function getKAMName() As String
        Dim tid As String = getTaskIDbyAccountID(hdnTaskID.Value)
        Dim capex As New clsApex
        Dim jid As String = capex.GetJobCardByTaskID(tid)
        Dim bid As String = capex.GetBriefIDByJobCardID(jid)
        Dim result As String = ""
        Dim sql As String = "select isnull(FirstName,'') + isnull(LastName,'') as KAMName "
        sql &= " from APEX_UsersDetails as ud "
        sql &= " Inner join APEX_Brief as ab on ud.UserDetailsID = ab.InsertedBy "
        sql &= " where ab.BriefID = " & bid
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            result = ds.Tables(0).Rows(0)("KAMName").ToString()
        End If
        Return result
    End Function

    Private Sub fillalldetails()
        Try
            Dim sql As New StringBuilder
            Dim ID As String = getLoggedUserID()
            'sql.Append("select")
            'sql.Append("( select ")
            'sql.Append("(select isnull(sum(PreEventcost),0) as PreEventQuote from APEX_PrePnLcost where category=Apex_taskAccount.category and refbriefID=")
            'sql.Append("(select refbriefID from apex_jobcard where jobcardid=(select jobcardNO from APEX_ClaimMaster where claimmasterID=" & hdnID.Value & ")))Total")
            'sql.Append(" from  Apex_taskAccount where accountID=" & hdnTaskID.Value & ")TotalB,")
            'sql.Append("")
            'sql.Append("")
            'sql.Append(" Isnull((select sum(amount) from APEX_ClaimMaster CM ")
            'sql.Append(" inner join APEX_ClaimTransaction on RefclaimID=CM.claimmasterID")
            'sql.Append(" where claimmasterID=" & hdnID.Value & " ),0) RequestedT,")
            ''Approvedby=" & ID.ToString() & " and jobcardno=(select jobcardNO from APEX_ClaimMaster where claimmasterID=" & hdnID.Value & ")
            'sql.Append(" (select sum(amount) from APEX_ClaimMaster CM ")
            'sql.Append(" inner join APEX_ClaimTransaction on RefclaimID=CM.claimmasterID")
            'sql.Append(" where claimmasterID=" & hdnID.Value & " and Isapproved='Y') ApprovedB,")
            '' Approvedby=" & ID.ToString() & " and jobcardno=(select jobcardNO from APEX_ClaimMaster where claimmasterID=" & hdnID.Value & ")
            'sql.Append("   (select sum(amount) from APEX_ClaimMaster CM ")
            'sql.Append(" inner join APEX_ClaimTransaction on RefclaimID=CM.claimmasterID")
            'sql.Append(" where claimmasterID=" & hdnID.Value & " and IsApproved Is Null)PendingB")
            ' Approvedby=" & ID.ToString() & " and jobcardno=(select jobcardNO from APEX_ClaimMaster where claimmasterID=" & hdnID.Value & ")

            sql.Append(" select( select (select isnull(sum(PreEventcost),0) as PreEventQuote from APEX_PrePnLcost where category=Apex_taskAccount.category ")
            sql.Append(" and refbriefID=(select refbriefID from apex_jobcard where jobcardid=(select jobcardNO from APEX_ClaimMaster where claimmasterID=" & hdnID.Value & ")))Total from ")
            sql.Append("  Apex_taskAccount where accountID=" & hdnTaskID.Value & ")TotalB, ")
            sql.Append("   (select( select ((select sum(amount) from APEX_ClaimMaster CM  inner join APEX_ClaimTransaction on  RefclaimID=CM.claimmasterID where reftaskID=" & hdnTaskID.Value & " and ")
            sql.Append("   submitteduserID='" & GetIDGetClaim() & "'))Total from   Apex_taskAccount where accountID=" & hdnTaskID.Value & "))RequestedT,")
            sql.Append("  (select( select ((select sum(amount) from APEX_ClaimMaster CM  inner join APEX_ClaimTransaction on  RefclaimID=CM.claimmasterID where reftaskID=" & hdnTaskID.Value & " and ")
            sql.Append("  submitteduserID='" & GetIDGetClaim() & "' and Isapproved='Y'))Total from   Apex_taskAccount where accountID=" & hdnTaskID.Value & "))ApprovedB,")
            sql.Append("  (select( select ((select sum(amount) from APEX_ClaimMaster CM  inner join APEX_ClaimTransaction on  RefclaimID=CM.claimmasterID where reftaskID=" & hdnTaskID.Value & " and ")
            sql.Append("  submitteduserID='" & GetIDGetClaim() & "' and Isapproved Is Null))Total from   Apex_taskAccount where accountID=" & hdnTaskID.Value & "))PendingB")
            sql.Append("")
            sql.Append("")
            

            sql.Append("")
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql.ToString())
            If ds.Tables(0).Rows.Count > 0 Then
                GridView1.DataSource = ds
                GridView1.DataBind()
                lblTotalRequestedamt.Text = ds.Tables(0).Rows(0)("RequestedT")
                lbltB.Text = ds.Tables(0).Rows(0)("TotalB")
                lblTotalApproval.Text = ds.Tables(0).Rows(0)("ApprovedB")
                lblTotalPendingApprovals.Text = ds.Tables(0).Rows(0)("PendingB")

                hdnapprovalAmt.Value = ds.Tables(0).Rows(0)("ApprovedB")
                If hdnapprovalAmt.Value = "" Then
                    hdnapprovalAmt.Value = 0
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub lnkshowhide_Click(sender As Object, e As EventArgs) Handles lnkshowhide.Click
        If lnkshowhide.Text = "+ View Details" Then
            divshow.Visible = True
            lnkshowhide.Text = "- Hide Details"
        Else
            divshow.Visible = False
            lnkshowhide.Text = "+ View Details"
        End If
    End Sub

    Protected Sub txtapprovalamt_TextChanged(sender As Object, e As EventArgs)
        Dim grow As GridViewRow
        For Each grow In gdvClaims.Rows
            Dim txtapprovalamt As New TextBox
            Dim txtapprovalamtKam As New TextBox
            Dim lblamt As New Label

            lblamt = grow.FindControl("lblamt")
            txtapprovalamt = grow.FindControl("txtapprovalamt")
            txtapprovalamtKam = grow.FindControl("txtapprovalamtKam")
            If lblTotalRequestedamt.Text <= lbltB.Text Then
                Response.Write("<script>alert('You can also approve this amount from allowted budget.');</script>")
                txtapprovalamtKam.Text = "0.00"
                txtapprovalamt.Text = lblamt.Text
                Exit Sub
            Else
                txtapprovalamt.Text = lblamt.Text - txtapprovalamtKam.Text
                btnExceed.Enabled = True
            End If
        Next
    End Sub

    Protected Sub txtapprovalamt_TextChanged1(sender As Object, e As EventArgs)

        txtapprovalamt_Text()
    End Sub

    Private Sub txtapprovalamt_Text()
        Dim grow As GridViewRow
        Dim approval As Decimal = "0.00"
        For Each grow In gdvClaims.Rows
            Dim txtapprovalamt As New TextBox
            txtapprovalamt = grow.FindControl("txtapprovalamt")
            approval += txtapprovalamt.Text
        Next
        If lblTotalApproval.Text = "" Then
            lblTotalApproval.Text = 0
        End If

        approval = approval + lblTotalApproval.Text
        If approval > hdntotalpmallow.Value Then
            btnApproval.Visible = True
            btnApproval.Text = "Request to Exceed Amount From PM"
        Else
            btnApproval.Visible = True
            btnApproval.Text = "Approve"
        End If
        If approval > lbltB.Text Then
            btnExceed.Visible = True
            btnApproval.Visible = False
        Else
            btnExceed.Visible = False
            btnApproval.Visible = True
        End If

        'If lbltB.Text >= approval Then
        '    'If lbltB.Text >= lblTotalRequestedamt.Text Then
        '    'If (approval - lbltB.Text) <= 0 Then
        '    '    btnExceed.Visible = False
        '    '    btnApproval.Visible = True
        '    '    btnApproval.Enabled = False
        '    '    btnApproval.Text = "Request to Exceed Amount From PM"
        '    '    Response.Write("<script>alert('Your approval amount should greater then zero.');</script>")
        '    'Else
        '    btnExceed.Visible = False
        '    btnApproval.Visible = True
        '    btnApproval.Text = "Request to Exceed Amount From PM"
        '    'End If

        'Else
        '    btnExceed.Visible = True
        '    'btnExceed.Enabled = False
        '    btnApproval.Visible = False
        'End If
    End Sub
    Private Function GetIDGetClaim() As String
        Dim result As String = ""
        Dim sql As String = "select Insertedby from APEX_Recieptents where refnotificationID='" & hdnNotificationID.Value & "'"
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then

            result = ds.Tables(0).Rows(0)("Insertedby").ToString()

        End If
        Return result
    End Function

End Class