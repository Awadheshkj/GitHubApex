Imports clsApex
Imports clsMain
Imports clsDatabaseHelper
Imports System.Data
Imports System
Imports ApexSupport

Partial Class RembursementClaim
    Inherits System.Web.UI.Page
    Public claimamt As Decimal
    Public currclaimamount As Decimal


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Page.Header.DataBind()

        If Not Page.IsPostBack Then
            divError.Visible = False

            If Len(Request.QueryString("taid")) > 0 Then
                If Request.QueryString("taid").ToString() <> Nothing Then
                    hdnTaskID.Value = Request.QueryString("taid").ToString()
                    If Len(Request.QueryString("sid")) > 0 Then
                        If Request.QueryString("sid").ToString() <> Nothing Then
                            hdnSubTaskID.Value = Request.QueryString("sid").ToString()

                        End If
                    End If

                    fillDetails()
                    fillClaims()
                    'BindColletral()
                Else
                    CallDivError()
                End If
            Else
                CallDivError()
            End If
        Else
            CallDivError()
        End If

    End Sub

    Private Sub fillDetails()

        'Dim sql As String = "Select t.title,ta.particulars,ta.Amount from APEX_TAskAccount as ta "
        'sql &= " join APEX_Task as t on ta.refTaskID=t.taskID where ta.AccountID=" & hdnTaskID.Value
        Dim sql As String = ""
        'sql &= " select ta.AccountID,EMPCODe,t.title,ta.particulars,ta.Total as amount,isnull((UD.FirstNAme + ' ' + LastName),'N/A')Name,Empcode,emailID,isnull(Mobile1,convert(varchar(20),'N/A'))Mobile1,(select isnull((FirstNAme + ' ' + LastName),'N/A') from apex_usersdetails where userdetailsID=ta.InsertedBy)PM ,UD.EmailID from APEX_TAskAccount as ta"
        'sql &= " Left join APEX_Task as t on ta.refTaskID=t.taskID "
        'sql &= " inner join apex_usersdetails UD on ta.TL=UD.userdetailsID"
        sql &= "select ta.AccountID,EMPCODe,taskname as title,ta.particulars,ta.Total"
        sql &= " as amount,isnull((UD.FirstNAme + ' ' + LastName),'N/A')Name,Empcode,emailID,"
        sql &= " isnull(Mobile1,convert(varchar(20),'N/A'))Mobile1,(select isnull((FirstNAme + ' ' + LastName),'N/A') "
        sql &= " from apex_usersdetails where userdetailsID=ta.InsertedBy)PM ,UD.EmailID from APEX_TAskAccount as ta "
        sql &= " inner join apex_usersdetails UD on ta.TL=UD.userdetailsID "
        sql &= ""

        sql &= " where ta.AccountID=" & hdnTaskID.Value
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0)("title")) Then
                lblTaskTitle.Text = ds.Tables(0).Rows(0)("title")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("particulars")) Then
                lblparticulars.Text = ds.Tables(0).Rows(0)("particulars")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("Amount")) Then
                lblAmount.Text = ds.Tables(0).Rows(0)("Amount")
            End If
            hdnTaskAccountID.Value = ds.Tables(0).Rows(0)("AccountID")
            lblEmployeeCode.Text = ds.Tables(0).Rows(0)("Empcode")
            lblEmployeeName.Text = ds.Tables(0).Rows(0)("NAME")
            'lblManager.Text = ""
            lblManager.Text = ds.Tables(0).Rows(0)("PM")
            lblContactNumber.Text = ds.Tables(0).Rows(0)("Mobile1")
            lblKAM.Text = getKAMName()
            txtEMailID.Text = ds.Tables(0).Rows(0)("EmailID")

            ' lblEmployeeCode.Text = ds.Tables(0).Rows(0)("EMPCODe")

        End If

        Dim jid As String = ""
        Dim capex As New clsApex
        Dim tid As String = getTaskIDbyAccountID(hdnTaskID.Value)
        If tid <> "" Then
            'jid = capex.GetJobCardByjcID(tid)
            jid = tid
            hdnJobCardID.Value = jid
        End If

        If hdnJobCardID.Value <> "" Then

            Dim sql2 As String = "Select JobCardNo,jobcardname  from APEX_JobCard where JobCardID = " & hdnJobCardID.Value
            Dim ds2 As New DataSet
            ds2 = ExecuteDataSet(sql2)
            If ds2.Tables(0).Rows.Count > 0 Then
                lblJobCode.Text = ds2.Tables(0).Rows(0)(0).ToString()
                lblprojectName.Text = ds2.Tables(0).Rows(0)("jobcardname")
                ' lblEmployeeName.Text = getLoggedUserName()

            End If
        End If
    End Sub

    Private Sub fillClaims()
        Dim arr As New ArrayList
        For i As Integer = 0 To 19
            arr.Add(i)
        Next
        gvClaims.DataSource = arr
        gvClaims.DataBind()
    End Sub

    Private Sub CallDivError()
        divContent.Visible = False
        divError.Visible = True
        Dim capex As New clsApex
        lblError.Text = capex.SetErrorMessage()
    End Sub

    Private Function checkValidAmount(ByVal amount As Double) As Boolean
        Dim result As Boolean = False
        Dim total As Double = 0
        If hdnSubTaskID.Value.Length > 0 Then

            Dim sql As String = "select Balance "
            sql &= " From APEX_SubTaskAccount as sta"
            sql &= " Inner Join APEX_SubTask as st on st.SubTaskID = sta.RefSubTaskID "
            sql &= " where RefSubTaskID =" & hdnSubTaskID.Value
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                Dim grow As GridViewRow
                For Each grow In gvClaims.Rows
                    Dim txtAmount As New TextBox
                    txtAmount = grow.FindControl("txtAmount")
                    If txtAmount.Text.Length > 0 Then
                        If IsNumeric(txtAmount.Text) Then
                            total += Convert.ToDouble(txtAmount.Text)
                        End If
                    End If
                Next

                total += amount
                If total <= Convert.ToDouble(ds.Tables(0).Rows(0)(0).ToString()) Then
                    result = True
                Else
                    result = False
                End If
            End If
        Else
            Dim sql1 As String = " Select Balance "
            sql1 &= " From APEX_TaskAccount as ta"
            sql1 &= " Inner Join APEX_Task as tt on tt.TaskID = ta.RefTaskID "
            sql1 &= " where RefTaskID = " & hdnTaskID.Value

            Dim ds1 As New DataSet
            ds1 = ExecuteDataSet(sql1)
            If ds1.Tables(0).Rows.Count > 0 Then
                Dim grow1 As GridViewRow
                For Each grow1 In gvClaims.Rows
                    Dim txtAmount1 As New TextBox

                    txtAmount1 = grow1.FindControl("txtAmount")
                    If txtAmount1.Text.Length > 0 Then
                        If IsNumeric(txtAmount1.Text) Then
                            total += Convert.ToDouble(txtAmount1.Text)
                        End If
                    End If
                Next

                total += amount
                If ds1.Tables(0).Rows(0)(0).ToString() <> "" Then
                    If total <= Convert.ToDouble(ds1.Tables(0).Rows(0)(0).ToString()) Then
                        result = True
                    Else
                        result = False
                    End If
                End If
            End If
        End If
        Return result
    End Function

    Public Sub SendForApprovalClaim()
        If hdnNotificationID.Value = "" Then
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
            sql &= "('Claim Approval'"
            sql &= ",'<b>From: </b>" & getLoggedUserName() & "<br /><b>Project Name: </b>" & Clean(getProjectName()) & "<br /><b>KAM: </b>" & Clean(getKAMName()) & "<br /><hr  /><b>Message: </b>Approval request for claim from <b>" & Clean(lblEmployeeName.Text) & "</b> against <b>" & Clean(lblTaskTitle.Text) & "</b> of&nbsp; Rs.&nbsp;<b>" & claimamt & "</b> for  JC - <b>#" & lblJobCode.Text & "</b>'"

            sql &= ",'H'"
            'If hdnSubTaskID.Value.Length > 0 Then
            '    sql &= "," & NotificationType.CLSA
            '    sql &= "," & hdnClaimMasterID.Value
            'Else
            sql &= "," & NotificationType.CLTA
            sql &= "," & hdnClaimMasterID.Value
            'End If
            sql &= ",getdate()"
            sql &= ",NULL"
            sql &= ",'N'"
            sql &= ",'N')"

            If ExecuteNonQuery(sql) > 0 Then
                'lblError.Text = "Data Submitted Succesfully"
                'divError.Visible = True
                InsertRecieptentDetails()
            End If
        Else
            InsertRecieptentDetails()
        End If
    End Sub

    Private Sub InsertRecieptentDetails()
        If hdnNotificationID.Value = "" Then
            Dim sql As String = ""
            Dim title As String = ""
            Dim message As String = ""
            'If hdnSubTaskID.Value.Length > 0 Then
            '    sql = "Select top(1) NotificationID from APEX_Notifications where Type=" & NotificationType.CLSA & " and AssociateID=" & hdnClaimMasterID.Value & " order by NotificationID desc"
            'Else
            sql = "Select  top(1) NotificationID,NotificationTitle,NotificationMessage from APEX_Notifications where Type=" & NotificationType.CLTA & " and AssociateID=" & hdnClaimMasterID.Value & "  order by NotificationID desc"
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

            If hdnJobCardID.Value <> "" Then

                sql3 = "Select ProjectManagerID from  APEX_JobCard  where jobcardID = " & hdnJobCardID.Value
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
                    If bheadid <> "" Then
                        Dim uid As Integer = Convert.ToInt32(bheadid)
                        emailid = GetEmailIDByUserID(uid)
                    End If

                End If

                If emailid <> "" Then
                    sendMail(title, message, "", emailid, "")
                End If

            End If
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If checkEmptyGrid() = False Then
            If checkpostpnl(hdnJobCardID.Value) = "Y" Then
                Dim grow As GridViewRow
                Dim totalamt As Double = "0.00"
                For Each grow In gvClaims.Rows
                    Dim hdnamount As HiddenField
                    hdnamount = grow.FindControl("hdnamount")
                    currclaimamount = currclaimamount + hdnamount.Value
                Next
                If checkpostpnlwithclaim(hdnJobCardID.Value, currclaimamount, getpostpnltotal()) Then

                Else

                    divContent.Visible = False
                    lblError.Text = "You cannot claim more than the Post P&L amount. If you want to claim please revised the post P&L amount."
                    divError.Visible = True
                    Exit Sub
                End If
            End If

            If hdnClaimMasterID.Value.Length = 0 Then
                Dim sql As String = "INSERT INTO APEX_ClaimMaster"
                sql &= "(JobCardNo"
                sql &= ",RefTaskID"
                sql &= ",RefSubTaskID"
                sql &= ",SubmittedUserID"
                sql &= ",SubmittedRemarks"
                sql &= ",ClaimFile"
                sql &= ",InsertedBy)"
                sql &= "VALUES"
                sql &= "(" & hdnJobCardID.Value
                sql &= "," & hdnTaskID.Value

                sql &= ",NULL"

                sql &= "," & getLoggedUserID()
                'sql &= "," & getPMIND()
                If txtRemarks.Text.Length > 0 Then
                    sql &= ",'" & Clean(txtRemarks.Text.Trim()) & "'"
                Else
                    sql &= ",NULL"
                End If
                Dim path As String = ""
                If FUpld_Documents.HasFile = True Then
                    Dim filename As String = FUpld_Documents.FileName
                    Dim fname As String = filename.Substring(0, filename.LastIndexOf("."))
                    Dim fext As String = filename.Substring(filename.LastIndexOf(".") + 1, filename.Length - (filename.LastIndexOf(".") + 1))
                    Dim encname As String = ""
                    'txtUploads.Text = fname

                    encname = fname & DateTime.Now.Year & DateTime.Now.Month & DateTime.Now.Date.Day & DateTime.Now.Date.Hour & DateTime.Now.Date.Minute & DateTime.Now.Date.Second
                    FUpld_Documents.SaveAs(Server.MapPath("Collateral/Claims/" & encname & "." & fext))
                    path = "Collateral/Claims/" & encname & "." & fext

                End If
                If path <> "" Then
                    sql &= ",'" & path & "'"
                Else
                    sql &= ",NULL"
                End If

                sql &= "," & getLoggedUserID() & ")"

                If ExecuteNonQuery(sql) > 0 Then
                    GetClaimID()
                    InsertClaimTransaction()
                End If
            Else
                InsertClaimTransaction()
            End If
        Else
            divError.Visible = True
            lblError.Text = "Fields with * are mandatory fields"
            lblError.Focus()
            divContent.Visible = True

        End If
    End Sub
    Public Function checkpostpnl(ByVal jobcardID As Integer) As String
        Dim sql As String = "Select Isnull(Ispostpnl,'N')Ispostpnl from Apex_jobcard where jobcardId=" & jobcardID
        Return ExecuteSingleResult(sql, _DataType.AlphaNumeric)
    End Function


    Private Sub GetClaimID()
        Dim sql As String = "Select ClaimMasterID from APEX_ClaimMaster where "
        sql &= " JobCardNo=" & hdnJobCardID.Value
        sql &= " and RefTaskID=" & hdnTaskID.Value
        'If hdnSubTaskID.Value <> "" Then
        '    sql &= " and RefSubTaskID=" & hdnSubTaskID.Value
        'End If
        sql &= " and SubmittedUserID=" & getLoggedUserID()
        'getPMIND()
        If txtRemarks.Text <> "" Then
            sql &= " and SubmittedRemarks='" & Clean(txtRemarks.Text.Trim()) & "'"
        End If
        sql &= " and InsertedBy=" & getLoggedUserID()
        sql &= " Order By InsertedOn desc"
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            hdnClaimMasterID.Value = ds.Tables(0).Rows(0)(0).ToString()
        End If
    End Sub

    Private Sub InsertClaimTransaction()
        Dim grow As GridViewRow
        Dim totalamt As Double = "0.00"
        For Each grow In gvClaims.Rows
            Dim txtParticular As New TextBox
            Dim txtAmount, txthotel, txtnumberofdays, txtqty, txtrate, txtfrom, txtto, txtvendername, txtremarks, txtbasic, txttaxes As New TextBox
            Dim hdnamount As New HiddenField
            'txtdate,
            Dim ddlcategory, ddlsubmissionType As New DropDownList

            ddlsubmissionType = grow.FindControl("ddlsubmissionType")
            ddlcategory = grow.FindControl("ddlexp_category")
            'txtdate = grow.FindControl("txtdate")
            'txthotel = grow.FindControl("txthotel")
            'txtnumberofdays = grow.FindControl("txtnumberofdays")
            'txtqty = grow.FindControl("txtqty")
            'txtrate = grow.FindControl("txtrate")
            'txtfrom = grow.FindControl("txtfrom")
            'txtto = grow.FindControl("txtto")
            txtvendername = grow.FindControl("txtvendername")
            txtremarks = grow.FindControl("txtremarks")
            txtParticular = grow.FindControl("txtParticular")
            txtAmount = grow.FindControl("txtAmount")

            txtbasic = grow.FindControl("txtbasic")
            txttaxes = grow.FindControl("txttaxes")

            'txtExpensePeriodfrom.Text
            'txtExpensePeriodto.Text
            'txtExpensePlace.Text
            'If txtnumberofdays.Text = "" Then
            '    txtnumberofdays.Text = 0
            'End If
            'If txtqty.Text = "" Then
            '    txtqty.Text = 0
            'End If
            'If txtrate.Text = "" Then
            '    txtrate.Text = 0
            'End If
            hdnamount = grow.FindControl("hdnamount")
            txtAmount.Text = hdnamount.Value
            If IsNumeric(txtAmount.Text) Then
                totalamt += txtAmount.Text
            End If

            If txtParticular.Text <> "" And txtAmount.Text <> "" Then
                If IsNumeric(txtAmount.Text) Then
                    If txtAmount.Text > 0 Then
                        ' If checkValidAmount(Convert.ToInt32(txtAmount.Text)) = True Then
                        Dim sql As String = "INSERT INTO APEX_ClaimTransaction"
                        sql &= "(RefClaimID"
                        sql &= ",Description"
                        sql &= ",RequestedAmt,Amount, RefExpe_Category, Date, Hotel, Days, Qty, Rate, From_City, ToCity, VenderName, ExpensePeriodFrom, ExpensePeriodTo, ExpensePlace,Remarks,submissionType,basic,taxes)"
                        sql &= "VALUES"
                        sql &= "(" & hdnClaimMasterID.Value
                        sql &= ",'" & Clean(txtParticular.Text) & "'"
                        sql &= "," & Clean(txtAmount.Text) & ""
                        sql &= "," & Clean(txtAmount.Text) & ""
                        sql &= "," & Clean(ddlcategory.SelectedValue) & ""
                        sql &= ",convert(datetime,getdate(),105)"
                        sql &= ",''"
                        sql &= ",'0'"
                        sql &= ",'0'"
                        sql &= ",'0'"
                        sql &= ",''"
                        sql &= ",''"
                        sql &= ",'" & Clean(txtvendername.Text) & "'"
                        sql &= ",convert(datetime,'" & Clean(txtExpensePeriodfrom.Text) & "',105)"
                        sql &= ",convert(datetime,'" & Clean(txtExpensePeriodto.Text) & "',105)"
                        'sql &= "," & Clean(txtExpensePeriodto.Text) & "'"
                        sql &= ",'" & Clean(txtExpensePlace.Text) & "'"
                        sql &= ",'" & Clean(txtremarks.Text) & "','" & ddlsubmissionType.SelectedValue & "','" & txtbasic.Text & "','" & txttaxes.Text & "')"

                        If ExecuteNonQuery(sql) > 0 Then
                            claimamt += CDbl(txtAmount.Text)
                            Dim capex As New clsApex
                            'If hdnSubTaskID.Value <> "" Then
                            '    capex.DeductSubTaskBalance(hdnSubTaskID.Value, txtAmount.Text)

                            'Else
                            capex.DeductTaskBalanceNew(hdnTaskID.Value, txtAmount.Text)

                            'End If
                        End If
                        'Else
                        '    lblError.Text = "The amount cannot exceed"
                        '    divError.Visible = True
                        '    Exit For
                        'End If
                    Else
                        lblError.Text = "The Amount cannot be less than 0"
                        divError.Visible = True
                        'Exit For
                        Exit Sub
                    End If
                Else
                    lblError.Text = "The amount should be numeric"
                    divError.Visible = True
                    'Exit For
                    Exit Sub
                End If
            End If
        Next
        'divContent.Visible = False
        'MessageDiv.Visible = True
        'Label1.Text = "Send Request to PM for the Claim Approval."
        'CheckAccountBalance()

        'SendForApprovalClaim()
        If lblEmployeeName.Text = lblManager.Text Then
            Dim DivRemaining As HiddenField
            DivRemaining = CType(Master.FindControl("HiddenField1"), HiddenField)
            Dim claimedamt As String = getclaimedtotalfORpm(hdnJobCardID.Value)
            DivRemaining.Value += lblAmount.Text - claimedamt
            'lblContactNumber.Text = DivRemaining.Value
            'DivRemaining.Value = (DivRemaining.Value - claimedamt)

            If IsclaimValid(hdnJobCardID.Value, totalamt, hdnTaskAccountID.Value) Then

                'If DivRemaining.Value >= totalamt Then
                Dim sql As String = "update APEX_ClaimMaster set "
                sql &= " IsApproved = 'P'"
                sql &= ",ApprovedOn = getdate()"
                sql &= ",ApprovedBy = " & getLoggedUserID()
                sql &= ",ApprovalRemarks = '" & Clean(txtRemarks.Text) & "'"
                sql &= " where ClaimMasterID = " & hdnClaimMasterID.Value
                If ExecuteNonQuery(sql) Then
                    MessageDiv.Visible = True
                    SendForApprovalClaimTOKAM()
                    'Label1.Text = "Claim Process success. </br> <a href='RembursementClaimApprovalView.aspx?jc=" & hdnJobCardID.Value & "&type=Task&nid=0&aid=" & hdnClaimMasterID.Value & "'>Click here</a> to printout your claim."
                    Label1.Text = "Your claim request send to KAM please wait for KAM approval."
                    'Response.Redirect("RembursementClaimApprovalView.aspx?type=Task&nid=0&aid=" & hdnClaimMasterID.Value)
                End If
            Else
                Dim sql As String = "select refbriefID from apex_jobcard where jobcardid=" & hdnJobCardID.Value & ""
                Dim ds As New DataSet
                ds = ExecuteDataSet(sql.ToString())
                If ds.Tables(0).Rows.Count > 0 Then
                    MessageDiv.Visible = True
                    Label1.Text = "Your remaining prepnl amount for this category is less than claimed amount. </br> <a href='OpenPrePnlManager.aspx?mode=edit&bid=" & ds.Tables(0).Rows(0)("refbriefID") & "&kid=y'>Click here</a> to increase your prepnl Amount."
                    'Response.Redirect("OpenPrePnlManager.aspx?mode=edit&bid=" & ds.Tables(0).Rows(0)("refbriefID") & "&kid=y")
                End If

            End If
        Else
            SendForApprovalClaim()
            MessageDiv.Visible = True
            Label1.Text = "Send Request to PM for the Claim Approval."
        End If
        'Response.Redirect("Home.aspx?mode=task")
        'If hdnSubTaskID.Value <> "" Then
        '    Response.Redirect("SubTaskAccount.aspx?sid=" & hdnSubTaskID.Value)
        'Else
        '    Response.Redirect("TaskAccount.aspx?tid=" & hdnTaskID.Value)
        'End If

    End Sub

    Public Sub SendForApprovalClaimTOKAM()
        Dim amt As Double = 0
        For Each grow In gvClaims.Rows
            Dim hdnamount As New HiddenField
            hdnamount = grow.FindControl("hdnamount")
            amt += hdnamount.Value
        Next
        'If hdnNotificationID.Value = "" Then
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
        sql &= "('Claim Approval'"
        sql &= ",'<b>From: </b>" & getLoggedUserName() & "<br /><b>Project Name: </b>" & Clean(getProjectName()) & "<br /><b>KAM: </b>" & Clean(getKAMName()) & "<br /><hr  /><b>Message: </b>Approval request for claim from <b>" & Clean(lblEmployeeName.Text) & "</b> against <b>" & Clean(lblTaskTitle.Text) & "</b> of&nbsp; Rs.&nbsp;<b>" & amt & "</b> for  JC - <b>#" & lblJobCode.Text & "</b>'"

        sql &= ",'H'"
        'If hdnSubTaskID.Value.Length > 0 Then
        '    sql &= "," & NotificationType.CLSA
        '    sql &= "," & hdnClaimMasterID.Value
        'Else
        sql &= "," & NotificationType.ClaimRequestfromPMtoKam
        sql &= "," & hdnClaimMasterID.Value
        'End If
        sql &= ",getdate()"
        sql &= ",NULL"
        sql &= ",'N'"
        sql &= ",'N')"

        If ExecuteNonQuery(sql) > 0 Then
            'lblError.Text = "Data Submitted Succesfully"
            'divError.Visible = True
            InsertRecieptentDetailstokam()
        End If
        'Else
        '    InsertRecieptentDetails()
        'End If
    End Sub

    Private Sub InsertRecieptentDetailstokam()

        Dim sql As String = ""
        Dim title As String = ""
        Dim message As String = ""
        sql = "Select  top(1) NotificationID,NotificationTitle,NotificationMessage from APEX_Notifications where Type=" & NotificationType.ClaimRequestfromPMtoKam & " and AssociateID=" & hdnClaimMasterID.Value & "  order by NotificationID desc"

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
        Dim capex As New clsApex

        If Request.QueryString("jc") <> "" Then

            sql3 = "select InsertedBy from Apex_brief where briefID= " & capex.GetBriefIDByJobCardID(Request.QueryString("jc"))
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
        sql1 &= ", UserID"
        sql1 &= ", IsSeen"
        sql1 &= ", IsHidden"
        sql1 &= ", IsExecuted"
        sql1 &= ", SMSSentOn"
        sql1 &= ", EmailSentOn"
        sql1 &= ", InsertedBy) "
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
                If bheadid <> "" Then
                    Dim uid As Integer = Convert.ToInt32(bheadid)
                    emailid = GetEmailIDByUserID(uid)
                End If

            End If

            If emailid <> "" Then
                sendMail(title, message, "", emailid, "")
            End If

        End If

    End Sub

    Public Function getclaimedtotalfORpm(ByVal jc As Integer) As Integer
        Dim result As Integer = 0
        If jc > 0 Then
            Dim sql As String = ""
            sql += " Select  isnull(sum(cl.Amount),'0.00') as ClaimedAmt from APEX_ClaimMaster as cm "
            sql += " join APEX_TaskAccount as ta on cm.RefTaskID=Ta.AccountID "
            'sql += " Left join APEX_Task as t on ta.refTaskID=t.taskid "
            sql += " join APEX_JobCard as jc on ta.RefJobcardID=jc.JobCardID "
            sql += " JOIN APEX_ClaimTransaction AS cl on cm.ClaimMasterID=cl.RefClaimID "
            sql += " where SubmittedUserID='" & getLoggedUserID() & "' and JobCardID='" & jc & "' and ta.AccountID='" & hdnTaskAccountID.Value & "'  and (IsApproved ='Y' or IsApproved ='P')"
            sql += ""
            sql += ""

            Dim ds As DataSet = Nothing
            ds = ExecuteDataSet(sql)
            If ds.Tables(0).Rows.Count > 0 Then
                If Not IsDBNull(ds.Tables(0).Rows(0)("ClaimedAmt")) Then
                    result = ds.Tables(0).Rows(0)("ClaimedAmt")
                End If
            End If
        End If
        Return result
    End Function

    Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        txtRemarks.Text = ""
        Dim grow As GridViewRow
        For Each grow In gvClaims.Rows
            Dim txtParticular As New TextBox
            Dim txtAmount As New TextBox

            txtParticular = grow.FindControl("txtParticular")
            txtAmount = grow.FindControl("txtAmount")

            txtParticular.Text = ""
            txtAmount.Text = ""
        Next
        lblError.Text = ""
        divError.Visible = False
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        'If hdnSubTaskID.Value <> "" Then
        '    Response.Redirect("SubTaskAccount.aspx?sid=" & hdnSubTaskID.Value)
        'Else
        '    Response.Redirect("TaskAccount.aspx?tid=" & hdnTaskID.Value)
        'End If
        Response.Redirect("Dashboard.aspx")
    End Sub

    Private Function checkEmptyGrid() As Boolean
        Dim result As Boolean = True
        Dim grow As GridViewRow
        Dim i As Integer = 0

        For Each grow In gvClaims.Rows
            Dim txtParticular As New TextBox
            Dim txtAmount As New TextBox
            Dim txtvandername As New TextBox
            Dim hdnamount As New HiddenField

            txtParticular = grow.FindControl("txtParticular")
            txtAmount = grow.FindControl("txtAmount")
            txtvandername = grow.FindControl("txtvendername")
            hdnamount = grow.FindControl("hdnamount")

            If txtParticular.Text.Trim() <> "" And hdnamount.Value.Trim() <> "" And txtvandername.Text.Trim() <> "" Then
                i += 1
            End If
        Next

        If i > 0 Then
            result = False
            lblError.Text = ""
            divError.Visible = False
        Else
            result = True
        End If
        Return result
    End Function

    Private Function getBriefID() As String
        Dim bid As String = ""
        Dim jid As String = ""
        Dim tid As String = ""
        Dim capex As New clsApex

        If hdnTaskID.Value <> "" Then
            tid = getTaskIDbyAccountID(hdnTaskID.Value)
            'jid = capex.GetJobCardByTaskID(tid)
            jid = tid
            bid = capex.GetBriefIDByJobCardID(jid)
        End If

        Return bid
    End Function

    Private Function getProjectName() As String
        Dim bid As String = getBriefID()
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
        Dim bid As String = getBriefID()
        Dim result As String = ""
        Dim sql As String = "select isnull(FirstName,'') + ' ' + isnull(LastName,'') as KAMName "
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

    Protected Sub gvClaims_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvClaims.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ddlexp_category As DropDownList = TryCast(e.Row.FindControl("ddlexp_category"), DropDownList)
            ' Display the company name in italics.
            'e.Row.Cells(1).Text = "<i>" & e.Row.Cells(1).Text & "</i>"
            getexpen_catigory(ddlexp_category)

            Dim ddlsubmissiontype As DropDownList = TryCast(e.Row.FindControl("ddlsubmissionType"), DropDownList)
            If Len(Request.QueryString("Type")) > 0 Then
                If Request.QueryString("Type").ToString() <> Nothing Then
                    If Request.QueryString("Type").ToString() = "E" Then
                        ddlsubmissiontype.SelectedValue = "Employee advance"
                        lblheader.Text = "Imprest-Employee"
                    ElseIf Request.QueryString("Type").ToString() = "V" Then
                        ddlsubmissiontype.SelectedValue = "Vendor advance"

                        lblheader.Text = "Imprest-Vendor"
                    ElseIf Request.QueryString("Type").ToString() = "C" Then
                        ddlsubmissiontype.SelectedValue = "Claim"
                        lblheader.Text = "Reimbursment Claim"

                    End If

                End If
            End If
            'ddlsubmissiontype.Enabled = False

        End If
    End Sub

    Private Sub getexpen_catigory(ddlexp_category As DropDownList)
        Try
            Dim sql As String = "select ID,CategoryName from [dbo].[Apex_ClaimsCategory] where Isactive='Y'"
            Dim ds As New DataSet
            ds = ExecuteDataSet(sql)
            ddlexp_category.DataValueField = "ID"
            ddlexp_category.DataTextField = "CategoryName"
            ddlexp_category.DataSource = ds
            ddlexp_category.DataBind()
            ddlexp_category.Items.Insert(0, New ListItem("Select", "0"))

        Catch ex As Exception

        End Try
    End Sub

    Private Function getPMIND() As String
        Dim sql3 As String = ""
        Dim result As String = ""
        If hdnJobCardID.Value <> "" Then
            sql3 = "Select ProjectManagerID from  APEX_JobCard  where jobcardID = " & hdnJobCardID.Value
        End If

        Dim ds As New DataSet
        ds = ExecuteDataSet(sql3)
        If ds.Tables(0).Rows.Count > 0 Then
            result = ds.Tables(0).Rows(0)("ProjectManagerID").ToString()
        End If
        Return result
    End Function

    'claim approval data
    Private Sub CheckAccountBalance()
        Dim sql As String = ""
        sql += " select isnull(Balance,0) as Balance,Total as Allow,"
        sql += "  (select isnull(sum(PreEventTotal),0) as PreEventQuote from APEX_PrePnLcost where category=Apex_taskAccount.category and refbriefID="
        sql += "  (select refbriefID from apex_jobcard where jobcardid=" & hdnJobCardID.Value & ")Total"
        sql += "  from  Apex_taskAccount where accountID=" & hdnTaskID.Value
        sql += ""
        Dim ds As New DataSet
        ds = ExecuteDataSet(sql)
        If ds.Tables(0).Rows.Count > 0 Then
            Dim bal As Double = Convert.ToDouble(ds.Tables(0).Rows(0)("Balance"))
            Dim Allow As Double = Convert.ToDouble(ds.Tables(0).Rows(0)("Allow"))
            Dim Total As Double = Convert.ToDouble(ds.Tables(0).Rows(0)("Total"))
            'hdntotalpmallow.Value = Convert.ToDouble(ds.Tables(0).Rows(0)("Allow"))
            'hdntotal.Value = Convert.ToDouble(ds.Tables(0).Rows(0)("Total"))
            'txtapprovalamt_Text()
        End If
    End Sub

    'Private Sub txtapprovalamt_Text()
    '    Dim grow As GridViewRow
    '    Dim approval As Decimal = "0.00"
    '    For Each grow In gdvClaims.Rows
    '        Dim txtapprovalamt As New TextBox
    '        txtapprovalamt = grow.FindControl("txtapprovalamt")
    '        approval += txtapprovalamt.Text
    '    Next
    '    If lblTotalApproval.Text = "" Then
    '        lblTotalApproval.Text = 0
    '    End If

    '    approval = approval + lblTotalApproval.Text
    '    If approval > hdntotalpmallow.Value Then
    '        btnApproval.Visible = True
    '        btnApproval.Text = "Request to Exceed Amount From PM"
    '    Else
    '        btnApproval.Visible = True
    '        btnApproval.Text = "Approve"
    '    End If
    '    If approval >= lbltB.Text Then
    '        btnExceed.Visible = True
    '        btnApproval.Visible = False

    '    End If
    'End Sub

    'end claim approval data 


    'Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    Dim DivRemaining As HiddenField
    '    DivRemaining = CType(Master.FindControl("HiddenField1"), HiddenField)
    '    'Leftmenu.Attributes.CssStyle.Add("Display", "none")
    '    MessageDiv.Visible = True
    '    'Label1.Text = Request.Form(DivRemaining)
    '    Label1.Text = DivRemaining.Value
    'End Sub

    Private Function getpostpnltotal() As Double
        Dim perc As Double
        Dim sql As String = "select sum(posteventcost)posteventtotal from Apex_tempPostPnLCost where refjobcardID=" & hdnJobCardID.Value
        perc = ExecuteSingleResult(sql, _DataType.AlphaNumeric)
        Return perc
    End Function

    Public Function checkpostpnlwithclaim(ByVal jobcardID As Integer, ByVal currentfillclaimamount As Decimal, ByVal postpnlamount As Double) As Boolean
        Dim retval As Boolean = False
        Dim totalclaim As Decimal = Nothing
        Dim sql As String = " select isnull(Sum(amount),0) from [dbo].[APEX_ClaimTransaction] CT"
        sql &= " inner join APEX_ClaimMaster CM on CM.claimMasterID=CT.RefClaimID"
        sql &= " where jobcardNo=" & jobcardID & " and isapproved='Y'"
        totalclaim = ExecuteSingleResult(sql, _DataType.AlphaNumeric)

        totalclaim = totalclaim + currentfillclaimamount

        If CDbl(postpnlamount) < totalclaim Then
            retval = False
        Else
            retval = True
        End If

        Return retval
    End Function
End Class